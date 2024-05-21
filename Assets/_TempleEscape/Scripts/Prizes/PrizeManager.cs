using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDriveGame;
using static Models;
using Bitel.PlanetaParacaidas.Map;

namespace Bitel.PlanetaEscape.Game
{
    public class PrizeManager : MonoBehaviour
    {


        [SerializeField] private ZDGGameController gameController;
        
        [Tooltip("The sprite order in the array must be the same used in the method GetPrice")]
        public Sprite[] prizeSprites;
        public Texture[] prizesTextures;
        public GameObject prizePrefab;
        public PrizePopup prizePopup;

        private List<PrizeClass> totalLevelPrizes = new List<PrizeClass>();
        private List<PrizeClass> spawnedPrizes = new List<PrizeClass>();
        private static List<PrizeClass> possiblePrizes = new List<PrizeClass>();
        private static List<PrizeClass> grabbedPrizes = new List<PrizeClass>();
        private bool PrizeServiceReady = false;

        [SerializeField]
        private bool useServices;
        #region Event Subscription
        private void OnEnable()
        {
            PrizeInstance.pickupEvent += PrizePickup;
            //GameManager.SpawnPrizeEvent += GetPrizeToSpawn;
        }

        private void OnDisable()
        {
            PrizeInstance.pickupEvent -= PrizePickup;
            //GameManager.SpawnPrizeEvent -= GetPrizeToSpawn;
        }
        #endregion

        private void Awake()
        {

            if (useServices)
            {
                InitLevelPrizes();

            }
            else
            {
                InitLevelPrizesTesting();
            }
        }

        private void InitLevelPrizes()
        {
            InitServiceInfo(MapController.currentLevel);
            SenderInfoCustomer info = Constants.getSenderInfoCustomer();
            SenderInfoCustomerGamePrizeRoute sender = new SenderInfoCustomerGamePrizeRoute();
            sender.token = info.token;
            sender.userId = info.userId;
            sender.msisdn = info.msisdn;
            sender.appInfo = info.appInfo;
            sender.type = "ROUTE";
            sender.codeGame = Constants.CODE_GAME_COLOR_JUMP;
            sender.level = MapController.currentLevel;
            StartCoroutine(ApiService.getGamePrizeRoute(sender, (ResponseGamePrizeRoute response) =>
            {
                if (response == null)
                {
                    Constants.log(tag, "error loadPrizes");
                    return;
                }
                if (response.gameConfigPrizes == null)
                    return;
                List<PrizeClass> toAdd = new List<PrizeClass>();
                foreach (ConfigPrizeRoute configPrize in response.gameConfigPrizes)
                {
                    if (configPrize.gamePrize.id != 0)
                    {
                        Constants.log(tag, "ConfigPRIZE id: " + configPrize.id +
                                ", ConfigPRIZE prizeCode: " + configPrize.prizeCode +
                                ", prize id: " + configPrize.gamePrize.id +
                                " , codePrize: " + configPrize.gamePrize.codePrize +
                                ", name: " + configPrize.gamePrize.name +
                                ", type: " + configPrize.gamePrize.type);
                        PrizeClass prize = GetPrize(configPrize);
                        Constants.addPrizeCodeReference(configPrize.id, configPrize.gamePrize.codePrize);
                        toAdd.Add(prize);
                    }
                }
                SetLevelPrizes(toAdd);
                PrizeServiceReady = true;
            }));
        }

        private void InitLevelPrizesTesting()
        {
            int currentLevel = MapController.currentLevel;
            List<Dictionary<string, object>> data;
            
            List<PrizeClass> toAdd = new List<PrizeClass>();

            data = CSVReader.Read("premios_bitel_testing");
            Constants.clearPrizeCodeReference();
            for (var i = 0; i < data.Count; i++)
            {
                if ((int)data[i]["level"] == currentLevel)
                {
                    ConfigPrizeRoute cfgRoute = new ConfigPrizeRoute();
                    cfgRoute.gamePrize = new GamePrizeRoute();
                    cfgRoute.gamePrize.codePrize = data[i]["codePrize"].ToString();
                    cfgRoute.gamePrize.id = (int)data[i]["id"];
                    PrizeClass prize = GetPrize(cfgRoute);
                    Constants.addPrizeCodeReference(cfgRoute.gamePrize.id, cfgRoute.gamePrize.codePrize);
                    toAdd.Add(prize);

                }
               

            }

            SetLevelPrizes(toAdd);
            PrizeServiceReady = true;
        }
        private PrizeClass GetPrize(ConfigPrizeRoute configPrize)
        {
            int serviceId = configPrize.id;
            switch (configPrize.gamePrize.codePrize)
            {
                case Constants.CODE_10_MIN_ROUTE:
                    return new PrizeClass(PrizeClass.PrizeType.Min, 10, configPrize.id, 0);
                case Constants.CODE_15_MIN_ROUTE:
                    return new PrizeClass(PrizeClass.PrizeType.Min, 15, configPrize.id, 1);
                case Constants.CODE_20_MIN_ROUTE:
                    return new PrizeClass(PrizeClass.PrizeType.Min, 20, configPrize.id, 2);
                case Constants.CODE_25_MB_ROUTE:
                    return new PrizeClass(PrizeClass.PrizeType.MB, 25, configPrize.id, 3);
                case Constants.CODE_35_MB_ROUTE:
                    return new PrizeClass(PrizeClass.PrizeType.MB, 35, configPrize.id, 4);
                case Constants.CODE_50_MB_ROUTE:
                    return new PrizeClass(PrizeClass.PrizeType.MB, 50, configPrize.id, 5);
                case Constants.CODE_75_MB_ROUTE:
                    return new PrizeClass(PrizeClass.PrizeType.MB, 75, configPrize.id, 6);
                case Constants.CODE_100_MB_ROUTE:
                    return new PrizeClass(PrizeClass.PrizeType.MB, 100, configPrize.id, 7);
                case Constants.CODE_150_MB_ROUTE:
                    return new PrizeClass(PrizeClass.PrizeType.MB, 150, configPrize.id, 8);
                case Constants.CODE_1_LIFE_ROUTE:
                    return new PrizeClass(PrizeClass.PrizeType.Life, 1, configPrize.id, 9);
                default:
                    return null;
            }
        }

       

        public void SetLevelPrizes(List<PrizeClass> levelPrizes)
        {
            foreach (PrizeClass pc in levelPrizes)
            {
                totalLevelPrizes.Add(pc);
            }
            possiblePrizes = totalLevelPrizes;
            grabbedPrizes.Clear();
        }
        
        
        public void GetPrizeToSpawn(Vector3 spawnPosition)
        {
            StartCoroutine(GetPrizeToSpawnCorroutine(spawnPosition));

        }

      

        public IEnumerator GetPrizeToSpawnCorroutine(Vector3 spawnPosition)
        {
            while (!PrizeServiceReady)
                yield return null;
            if (WantToSpawnPrize())
            {
                GameObject spawnedPrize = Instantiate(prizePrefab); 
                spawnedPrize.transform.position = spawnPosition;

                int index = possiblePrizes.Count - 1;
                PrizeClass current = possiblePrizes[index];
                if (current != null)
                {
                    Debug.Log(index);
                    Debug.Log(WantToSpawnPrize());
                    spawnedPrizes.Add(current);
                    possiblePrizes.RemoveAt(index);
                }
                PrizeInstance prefabInstance = spawnedPrize.GetComponentInChildren<PrizeInstance>();
                prefabInstance.SetPrizeClass(current);
                //prefabInstance.SetSprite(GetPrizeSprite(current));
                prefabInstance.SetTexture(GetPrizeTexture(current));
                spawnedPrize.SetActive(true);
                yield return null;
            }
        }

        private void PrizePickup(PrizeClass pc)
        {
            Debug.Log(pc.prizeType + " : " + pc.value);
            for (int i = 0; i < spawnedPrizes.Count; i++)
            {
                PrizeClass prize = spawnedPrizes[i];
                if (prize != null && prize.Equals(pc))
                {
                    prizePopup.SetImage(GetPrizeSprite(prize));
                    prizePopup.gameObject.SetActive(true);
                    spawnedPrizes.Remove(prize);
                    grabbedPrizes.Add(pc);                 
                    gameController.collectedPrizes.Add(pc.serviceId);
                    return;
                }
            }
        }

        private Sprite GetPrizeSprite(PrizeClass pc)
        {
            return prizeSprites[pc.spriteIndex];
        }

        private Texture GetPrizeTexture(PrizeClass pc)
        {
            return prizesTextures[pc.spriteIndex];
        }
        

        private bool WantToSpawnPrize()
        {
            bool toReturn = false;
            if (possiblePrizes.Count == 0)
            {
                return toReturn;

            }
            else if (possiblePrizes.Count > 0)
            {
                toReturn = true;

            }
            return toReturn;
        }
        private void InitServiceInfo(int level)
        {
            SenderInfoCustomer info = Constants.getSenderInfoCustomer();
            SenderInfoCustomerBenefitGameDaily levelProgressInfo = new SenderInfoCustomerBenefitGameDaily();
            levelProgressInfo.token = info.token;
            levelProgressInfo.userId = info.userId;
            levelProgressInfo.msisdn = info.msisdn;
            levelProgressInfo.appInfo = Constants.getAppInfo();
            levelProgressInfo.codeGame = Constants.CODE_GAME_COLOR_JUMP;
            levelProgressInfo.gameLevel = level;
            Constants.setLevelProgressInfo(levelProgressInfo);
            gameController.collectedPrizes = new List<int>();
            Constants.clearPrizeCodeReference();
        }
    }

}
