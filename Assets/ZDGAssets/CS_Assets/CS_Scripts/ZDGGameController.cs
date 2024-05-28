﻿using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ZombieDriveGame.Types;
using System.Collections.Generic;

namespace ZombieDriveGame
{
	public class ZDGGameController : MonoBehaviour 
	{
        public static ZDGGameController instance;

        public delegate void SpawnPrizeEvent(Vector3 position);
        public event SpawnPrizeEvent OnSpawnPrizeEvent;

        public List<int> collectedPrizes;

        // The camera object and the camera holder that contains it and follows the player
        internal Camera cameraObject;
        internal Transform cameraHolder;

        [Tooltip("The player object assigned from the scene")]
        public ZDGPlayer playerObject;
        public static ZDGPlayer currentPlayerObject = null;

        // The turning direction of the player
        internal float turnDirection = 0;

        [Tooltip("The ground object that repeats under the player while he is moving")]
        public Transform groundObject;

        [Tooltip("How far should the player move before the ground object repeats")]
        public float groundRepeatDistance = 20;

        [Tooltip("The edge of the street where there is a railing that bounces the player back")]
        public float streetEdge = 2;

        [Tooltip("How much damage touching the railing causes to the player")]
        public float streetEdgeDamage = 1;

        [Tooltip("The effect that appears when hitting a rail, assigned from the player object")]
        public Transform streetEdgeEffect;

        [Tooltip("The sound that plays when hitting the rail")]
        public AudioClip streetEdgeSound;

        public GameObject tokenPrefab;

        [Tooltip("A list of all the obstacles that can be spawned, some good and some bad")]
        public Spawn[] spawnObstacles;
        internal Spawn[] spawnObstaclesList;

        [Tooltip("A list of all items you can pick up, which appear only at a certain rate")]
        public Transform[] spawnPickups;

        [Tooltip("The rate at which a pickup appear. For example if set to 50, we will create 50 obstacles before showing an item pickup")]
        public int spawnPickupRate = 50;
        internal int spawnPickupRateCount;
        internal int spawnPickupIndex = 0;
        
        [Tooltip("How gap between each two obstacles")]
        public Vector2 spawnGap = new Vector2(1,2);
        internal float spawnGapCount = 0;
        
        internal bool isSpawning = true;

        [Tooltip("How long to wait before starting the game. Ready?GO! time")]
        public float startDelay = 1;

        [Tooltip("The effect displayed before starting the game")]
        public Transform readyGoEffect;

        [Tooltip("The turn button, click it or tap it to turn the player in the opposite direction")]
        public string turnButton = "Fire1";

        [Tooltip("A delay to prevent the player from too much health at once. If you lose health, you will not lose more health for some time")]
        public float loseHealthDelay = 1;
        internal float loseHealthDelayCount;

        [Tooltip("The score of the player")]
        public int score = 0;

        [Tooltip("How many score the player needs to collect before leveling up")]
        public int levelUpEveryScore = 100;
        internal int increaseCount = 0;

        [Tooltip("How much faster the game becomes when we level up")]
        public float levelUpSpeedIncrease = .5f;

        [Tooltip("The score text object which displays the current score of the player")]
        public Transform scoreText;
		internal int highScore = 0;
		internal int scoreMultiplier = 1;
        
        // Various canvases for the UI
        public Transform gameCanvas;
        public Transform healthCanvas;
        public Transform fuelCanvas;
        public Transform pauseCanvas;
		public Transform gameOverCanvas;

		// Is the game over?
		internal bool  isGameOver = false;
		
		// The level of the main menu that can be loaded after the game ends
		public string mainMenuLevelName = "StartMenu";
		
		// Various sounds and their source
		public AudioClip soundGameOver;
		public string soundSourceTag = "GameController";
		internal GameObject soundSource;
		
		// The button that will restart the game after game over
		public string confirmButton = "Submit";
		
		// The button that pauses the game. Clicking on the pause button in the UI also pauses the game
		public string pauseButton = "Cancel";
		internal bool  isPaused = false;

		// A general use index
		internal int index = 0;

        //public Transform slowMotionEffect;

        public Animator vanAnimator;
        public Animator cuyAnimator;
        bool leftButton = true;
        public bool turn;
        int tokensCollected = 0;
        int totalTokensInLevel;

        int flipSound;
        public bool tutorial = true;
        public List<GameObject> lifesSprites = new List<GameObject>();
        public Material[] materials;
        bool spawnLife;
        public Transform lifePrefab;
        bool finish;

        Touch lastTouch;

        public bool mobile;

        void Awake()
        {
            instance = this;
            Time.timeScale = 1;

            // Activate the pause canvas early on, so it can detect info about sound volume state
            if (pauseCanvas) pauseCanvas.gameObject.SetActive(true);

            if (currentPlayerObject == null)
            {
                // Give the player control after a delay
                playerObject.Invoke("GetControl", startDelay);
            }
            else
            {
                AssignCurrentPlayer(currentPlayerObject);
            }
        }

        /// <summary>
        /// Assigns the current player object, ususally from a character selector script
        /// </summary>
        /// <param name="player"></param>
        public void AssignCurrentPlayer(ZDGPlayer player)
        {
            // If we have a character selected, assign it as the current player
            currentPlayerObject = player;

            // Remove the previous player
            Destroy(playerObject.gameObject);

            // Create the new player object and place it in the scene
            playerObject = Instantiate(currentPlayerObject, playerObject.transform.position, playerObject.transform.rotation);

            // If we have a player defined, set its attributes in the gamecontroller
            if (playerObject)
            {
                // Set the turning speed of the player
                turnDirection = playerObject.turnRange;

                // Set the health and fuel of the player
                playerObject.healthMax = playerObject.health;
                playerObject.fuelMax = playerObject.fuel;
            }

            // Update the number of lives we have
            ChangeHealth(0);

            loseHealthDelayCount = 0;

        }


        /// <summary>
        /// Start is only called once in the lifetime of the behaviour.
        /// The difference between Awake and Start is that Start is only called if the script instance is enabled.
        /// This allows you to delay any initialization code, until it is really needed.
        /// Awake is always called before any Start functions.
        /// This allows you to order initialization of scripts
        /// </summary>
        void Start()
		{
            //if (LevelManager.instance.levelNumber != 1) tutorial = false;
            StartCoroutine(IncreseSpeed());
            //totalTokensInLevel = LevelManager.instance.levelNumber + 9;
            //totalTokensInLevel = (LevelManager.instance.levelNumber - 1) * (20 - 5) / (100 - 1) + 5;
            totalTokensInLevel = 1000000;

            spawnPickupRate =  5;
            
            spawnGap.x = 3.5F;
            spawnGap.y = spawnGap.x;

            playerObject.speed =  7;

            scoreText.GetComponent<Text>().text = tokensCollected.ToString();

            SoundController.instance.stopMusicSound();
            SoundController.instance.stopFxSound(SoundController.instance.fxAudioSource);
            SoundController.instance.playMusic(SoundController.instance.GameMusic);
            SoundController.instance.playSound(SoundController.instance.CarSound, true, SoundController.instance.fxAudioSource);

            // If the camera is not assigned yet, assign it and set the camera holder too
            if (cameraObject == null)
            {
                cameraObject = Camera.main;

                // Set the camera holder from the camera object
                if (cameraObject.transform.root) cameraHolder = cameraObject.transform.root;
            }

            // If we have a player defined, set its attributes in the gamecontroller
            if (playerObject)
            {
                // Set the turning speed of the player
                turnDirection = playerObject.turnRange;
                
                // Set the health and fuel of the player
                playerObject.healthMax = playerObject.health;
                playerObject.fuelMax = playerObject.fuel;
            }

            // Disable multitouch so that we don't tap two answers at the same time ( prevents multi-answer cheating, thanks to Miguel Paolino for catching this bug )
            Input.multiTouchEnabled = true;
            

            ChangeHealth(0);

            loseHealthDelayCount = 0;
            
            //Hide the cavases
            if ( gameOverCanvas )    gameOverCanvas.gameObject.SetActive(false);
			if ( pauseCanvas )    pauseCanvas.gameObject.SetActive(false);

			//Get the highscore for the player
			highScore = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "HighScore", 0);

//CALCULATING SPAWN CHANCES
            // Calculate the chances for the objects to spawn
            int totalSpawns = 0;
            int totalSpawnsIndex = 0;

            // Calculate the total number of spawns with their chances
            for (index = 0; index < spawnObstacles.Length; index++)
            {
                totalSpawns += spawnObstacles[index].spawnChance;
            }

            // Create a new list of the objects that can be dropped
            spawnObstaclesList = new Spawn[totalSpawns];

            // Go through the list again and fill out each type of drop based on its drop chance
            for (index = 0; index < spawnObstacles.Length; index++)
            {
                int laneChanceCount = 0;

                while (laneChanceCount < spawnObstacles[index].spawnChance)
                {
                    spawnObstaclesList[totalSpawnsIndex] = spawnObstacles[index];

                    laneChanceCount++;

                    totalSpawnsIndex++;
                }
            }

            spawnPickupRateCount = spawnPickupRate;

            //Assign the sound source for easier access
            if ( GameObject.FindGameObjectWithTag(soundSourceTag) )    soundSource = GameObject.FindGameObjectWithTag(soundSourceTag);

			// Reset the spawn delay
			spawnGapCount = Random.Range( spawnGap.x, spawnGap.y);
			
            
			if ( readyGoEffect && !tutorial)
            {
                ReadyGoEffecf();
            }
		}

        public void ReadyGoEffecf()
        {
            var readyGo = Instantiate(readyGoEffect);
            readyGo.parent = cameraObject.transform;
        }


        public void TurnRight()
        {
            turn = true;
            if (!leftButton)
            {
                turnDirection *= -1;
                leftButton = true;
            }
            if (flipSound != 3)
            {
                SoundController.instance.playSound(SoundController.instance.FlipSound, false, SoundController.instance.carAudioSource);
                flipSound = 3;
            }
        }

        public void TurnLeft()
        {
            turn = true;
            if (leftButton)
            {
                turnDirection *= -1;
                leftButton = false;
            }
            if (flipSound != 1)
            {
                SoundController.instance.playSound(SoundController.instance.FlipSound, false, SoundController.instance.carAudioSource);
                flipSound = 1;
            }
        }


        public void CenterCar()
        {
            if (!playerObject) return;
            
            playerObject.transform.Translate(Vector3.forward * Time.deltaTime * playerObject.speed, Space.Self);
            Vector3.Lerp(playerObject.transform.position, new Vector3(0, 0, playerObject.transform.position.z), Time.deltaTime * playerObject.speed);

            if (turn)
                playerObject.transform.eulerAngles = Vector3.up * Mathf.LerpAngle(playerObject.transform.eulerAngles.y, turnDirection, Time.deltaTime * 1);//  Vector3.RotateTowards(playerObject.transform.eulerAngles, Vector3.up * turnDirection, Time.deltaTime * playerObject.turnSpeed, 0.0F);
            else
                playerObject.transform.eulerAngles = Vector3.up * Mathf.LerpAngle(playerObject.transform.eulerAngles.y, 0, Time.deltaTime * 100);

            if (playerObject.transform.position.x < -.1f || playerObject.transform.position.x > .1f)
            {
                if (playerObject.transform.position.x < -.1f) TurnRight();
                else if (playerObject.transform.position.x > .1f) TurnLeft();
            }
            else
            {
                turn = false;
                if (!finish)
                {
                    finish = true;
                }
            } 
        }

        void Update()
		{
			// Delay the start of the game
			if ( startDelay > 0 )
			{
				startDelay -= Time.deltaTime;

                if (playerObject)
                {
                    // Move the player forward even before we start playing
                    playerObject.transform.Translate(Vector3.forward * Time.deltaTime * playerObject.speed, Space.Self);
                }
            }
			else
			{

				
                    // If there is a player object, move it forward and turn it in the correct direction
                    if (playerObject)
                    {

                        playerObject.transform.Translate(Vector3.forward * Time.deltaTime * playerObject.speed, Space.Self);

                        if(turn)
                            /*if ( playerObject.transform.eulerAngles.y != turnDirection ) */playerObject.transform.eulerAngles = Vector3.up * Mathf.LerpAngle(playerObject.transform.eulerAngles.y, turnDirection, Time.deltaTime * playerObject.turnSpeed);//  Vector3.RotateTowards(playerObject.transform.eulerAngles, Vector3.up * turnDirection, Time.deltaTime * playerObject.turnSpeed, 0.0F);
                        else
                        {
                            playerObject.transform.eulerAngles = Vector3.up * Mathf.LerpAngle(playerObject.transform.eulerAngles.y, 0, Time.deltaTime * playerObject.turnSpeed);
                        }

                        if (playerObject.transform.position.x > streetEdge || playerObject.transform.position.x < -streetEdge) BounceOffRail();
                    }

                    if (loseHealthDelayCount > 0) loseHealthDelayCount -= Time.deltaTime;


                    if ( isSpawning == true && playerObject != null)
                    {
                        if (spawnGapCount > 0) spawnGapCount -= playerObject.speed * Time.deltaTime;
                        else
                        {
                            // Reset the spawn delay count
                            spawnGapCount = Random.Range(spawnGap.x, spawnGap.y);

                            /*if((tokensCollected == 3 || tokensCollected == 8 || tokensCollected == 13 || tokensCollected == 18) && spawnLife)
                            {
                                Transform newSpawn = Instantiate(lifePrefab) as Transform;
                                newSpawn.position = new Vector3(Random.Range(-streetEdge, streetEdge), 0, playerObject.transform.position.z + 20);
                                spawnLife = false;
                                spawnPickupRateCount = spawnPickupRate;
                            }*/
                            if (spawnPickupRateCount > 0)
                            {
                                // Choose a random spawn from the list of spawns
                                //int randomSpawn = Mathf.FloorToInt(Random.Range(0, spawnObstaclesList.Length));
                                int randomSpawn = Mathf.FloorToInt(Random.Range(0, 4));

                            // Create a new random target from the target list
                            //Transform newSpawn = Instantiate(spawnObstaclesList[randomSpawn].spawnObject) as Transform;
                                ZDGTouchable newSpawn = RockFactory.Instance.GetBullet();
                                newSpawn.SetTouchable(randomSpawn);
                           
                                // Place the target at a random position along the height
                                newSpawn.transform.position = new Vector3(Random.Range(-streetEdge, streetEdge), 0, playerObject.transform.position.z + 20);

                                spawnPickupRateCount--;

                                // Add to the spawn gap based on the spawn object
                                spawnGapCount += spawnObstaclesList[randomSpawn].spawnGap;
                            }
                            else
                            {
                            // Create a new random target from the target list
                            //Transform newSpawn = Instantiate(tokenPrefab.transform) as Transform;
                                ZDGTouchable newSpawn = TokenFactory.Instance.GetBullet();
                

                                // Place the target at a random position along the height
                                newSpawn.transform.position = new Vector3(Random.Range(-streetEdge, streetEdge), 0, playerObject.transform.position.z + 20);
                                spawnPickupRateCount = spawnPickupRate;  
                            }
                        }
                    }

					//Toggle pause/unpause in the game
					if ( Input.GetButtonDown(pauseButton) )
					{
						if ( isPaused == true )    Unpause();
						else    Pause(true);
					}
				
			}

            if (!tutorial)
            {
                if (mobile)
                {
                    if (Input.touchCount > 0)
                    {
                        lastTouch = Input.GetTouch(Input.touchCount - 1);
                        if (lastTouch.position.x < (float)Screen.width * .5f)
                        {
                            if (lastTouch.phase == TouchPhase.Began || lastTouch.phase == TouchPhase.Stationary)
                                TurnLeft();
                        }
                        else if (lastTouch.position.x > (float)Screen.width * .5f)
                            if (lastTouch.phase == TouchPhase.Began || lastTouch.phase == TouchPhase.Stationary)
                                TurnRight();
                    }
                    else
                    {
                        turn = false;
                        if (flipSound != 2 && flipSound != 0)
                        {
                            SoundController.instance.stopFxSound(SoundController.instance.carAudioSource);
                            flipSound = 2;
                        }
                    }
                }
                else
                {
                    if (Input.GetAxisRaw("Horizontal") != 0)
                    {
                        if (Input.GetAxisRaw("Horizontal") < 0) TurnLeft();
                        else if (Input.GetAxisRaw("Horizontal") > 0) TurnRight();
                    }
                    else
                    {
                        turn = false;
                        if (flipSound != 2 && flipSound != 0)
                        {
                            SoundController.instance.stopFxSound(SoundController.instance.carAudioSource);
                            flipSound = 2;
                        }
                    }
                }
            }
        }

        void LateUpdate()
        {
            if (playerObject)
            {
                // Make the camera holder follow the position of the player along the street
                //cameraHolder.position = new Vector3(cameraHolder.position.x, cameraHolder.position.y, playerObject.transform.position.z);
                if ( cameraHolder )    cameraHolder.position = new Vector3(cameraHolder.position.x, cameraHolder.position.y, playerObject.transform.position.z);

                // Repeat the ground object if the player goes forward enough
                if (groundObject && playerObject.transform.position.z > groundObject.position.z + groundRepeatDistance) groundObject.position += Vector3.forward * groundRepeatDistance;

            }
        }


        /// <summary>
        /// Changes the lives of the player. If lives reach 0, it's game over
        /// </summary>
        /// <param name="changeValue"></param>
        public void ChangeFuel(float changeValue)
        {
            // Change the value of the fuel
            playerObject.fuel += changeValue;

            // Limit the value of the fuel to the maximum allowed value
            if (playerObject.fuel > playerObject.fuelMax) playerObject.fuel = playerObject.fuelMax;

            // Play the animation of the fuel icon
            if (fuelCanvas && fuelCanvas.GetComponent<Animation>()) fuelCanvas.GetComponent<Animation>().Play();
        }

        /// <summary>
        /// Bounces the player off the rail, damaging it and returning it to the center of the street
        /// </summary>
        public void BounceOffRail()
        {
            if ( playerObject )
            {
                // Damage the player
                ChangeHealth(-1);

                // If the player went too far to the right, bounce it back to the left
                if ( playerObject.transform.position.x > streetEdge )
                {
                    // Switch the turning direction of the player
                    //turnDirection = -playerObject.turnRange;

                    // Move the player a little to the center so it doesn't get stuck in the railing
                    playerObject.transform.position = new Vector3( streetEdge - .2f, 0, playerObject.transform.position.z);

                    // Create the effect of hitting the rail
                    if (streetEdgeEffect)
                    {
                        // Set the scale to normal
                        streetEdgeEffect.localScale = new Vector3( 1, streetEdgeEffect.localScale.y, streetEdgeEffect.localScale.z);
                        
                        // Play the particle effect of hitting the rail
                        streetEdgeEffect.Find("Particle").GetComponent<ParticleSystem>().Emit(10);
                    }

                    //leftButton = false;
                }
                else if (playerObject.transform.position.x < -streetEdge ) // Otherwise, if the player went too far to the left, bounce it back to the right
                {
                    // Switch the turning direction of the player
                    //turnDirection = playerObject.turnRange;

                    // Move the player a little to the center so it doesn't get stuck in the railing
                    playerObject.transform.position = new Vector3( -streetEdge + 0.2f, 0, playerObject.transform.position.z);
                    
                    // Create the effect of hitting the rail
                    if (streetEdgeEffect)
                    {
                        // Flip the scale to the other side, so that the effect appers on the left side of the player
                        streetEdgeEffect.localScale = new Vector3( -1, streetEdgeEffect.localScale.y, streetEdgeEffect.localScale.z);

                        // Play the particle effect of hitting the rail
                        streetEdgeEffect.Find("Particle").GetComponent<ParticleSystem>().Emit(10);
                    }

                   // leftButton = true;
                }

                // If there is a sound and a source, play it
                if (soundSource && streetEdgeSound) soundSource.GetComponent<AudioSource>().PlayOneShot(streetEdgeSound);

                // Reset the rotation of the player
                playerObject.transform.eulerAngles = Vector3.up;
            }
        }

        /// <summary>
        /// Changes the lives of the player. If lives reach 0, it's game over
        /// </summary>
        /// <param name="changeValue"></param>
        public void ChangeHealth(float changeValue)
        {
            // Change the health value
            playerObject.health += changeValue;

            var aux = 3 - playerObject.health;

            foreach (var sprite in lifesSprites)
            {
                sprite.SetActive(true);
            }
            for (int i = 2; i >= playerObject.health; i--)
            {
                lifesSprites[i].SetActive(false);
            }
            if (lifesSprites[0].GetComponentInParent<Animation>()) lifesSprites[0].GetComponentInParent<Animation>().Play();

            // Limit the value of the health to the maximum allowed value
            if (playerObject.health > playerObject.healthMax) playerObject.health = playerObject.healthMax;

            // If we are recieving damage, check if we should die
            if ( loseHealthDelayCount <= 0 && changeValue < 0 )
            {
                if (playerObject.health <= 0)
                {
                    playerObject.Die();

                    // Health reached 0, so the target should die
                    StartCoroutine(GameOver(1));
                }

                loseHealthDelayCount = loseHealthDelay;
            }

            // Update the health bar 
            if (healthCanvas)
            {
                // Update the health bar based on the health we have
                healthCanvas.GetComponent<Image>().fillAmount = playerObject.health / playerObject.healthMax;

                // Play the animation of the health icon
                if (healthCanvas.GetComponent<Animation>()) healthCanvas.GetComponent<Animation>().Play();
            }
        }

        /// <summary>
        /// Change the score and update it
        /// </summary>
        /// <param name="changeValue">Change value</param>
        public void  ChangeScore( int changeValue )
		{
            // Change the score value
			score += changeValue;
            tokensCollected++;
            spawnLife = true;
            SoundController.instance.playSound(SoundController.instance.Token2, false, SoundController.instance.fxAudioSource);
            //Update the score text
            if (scoreText)
            {
                scoreText.GetComponent<Text>().text = tokensCollected.ToString();

                // Play the score object animation
                if (scoreText.GetComponent<Animation>()) scoreText.GetComponent<Animation>().Play();
            }

            //Increase the counter to the next level
            increaseCount += changeValue;

            //If we reached the required score, level up!
            if (increaseCount >= levelUpEveryScore)
            {
                increaseCount -= levelUpEveryScore;

                LevelUp();
            }

            if (tokensCollected >= totalTokensInLevel)
            {
                isGameOver = true;
            }
        }

        /// <summary>
		/// Levels up, and increases the difficulty of the game
		/// </summary>
		void LevelUp()
        {
            //Increase game speed
            playerObject.speed += levelUpSpeedIncrease;
        }

        /// <summary>
        /// Set the score multiplier ( Get double score for hitting and destroying targets )
        /// </summary>
        void SetScoreMultiplier( int setValue )
		{
			// Set the score multiplier
			scoreMultiplier = setValue;
		}

        /// <summary>
        /// Pause the game, and shows the pause menu
        /// </summary>
        /// <param name="showMenu">If set to <c>true</c> show menu.</param>
        public void Pause(bool showMenu)
        {
            isPaused = true;

            //Set timescale to 0, preventing anything from moving
            Time.timeScale = 0;

            //Show the pause screen and hide the game screen
            if (showMenu == true)
            {
                if (pauseCanvas) pauseCanvas.gameObject.SetActive(true);
                if (gameCanvas) gameCanvas.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Resume the game
        /// </summary>
        public void Unpause()
        {
            isPaused = false;

            //Set timescale back to the current game speed
            Time.timeScale = 1;

            //Hide the pause screen and show the game screen
            if (pauseCanvas) pauseCanvas.gameObject.SetActive(false);
            if (gameCanvas) gameCanvas.gameObject.SetActive(true);
        }

        /// <summary>
        /// Runs the game over event and shows the game over screen
        /// </summary>
        IEnumerator GameOver(float delay)
		{
			isGameOver = true;

			yield return new WaitForSeconds(delay);

            //loseCanvas.SetActive(true);

            yield return new WaitForSeconds(2);

            PlayerPrefs.SetInt("Tokens", PlayerPrefs.GetInt("Tokens") + tokensCollected);

            SceneManager.LoadScene(1);
		}
		
		
		void  MainMenu()
		{
			SceneManager.LoadScene(mainMenuLevelName);
		}


        IEnumerator IncreseSpeed()
        {
            yield return new WaitForSeconds(10);
            if(!tutorial)
                playerObject.speed += 0.2f;
            StartCoroutine(IncreseSpeed());
        }

    }
}