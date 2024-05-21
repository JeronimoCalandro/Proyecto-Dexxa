using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Models
{
    [System.Serializable]
    public class ResponseLevel
    {
        public Platform[] platforms;
        public Elements[] elements;
    }

    [System.Serializable]
    public class Platform
    {
        public int type_id;
        public Position position;
        public Movement movement;
        public Elements[] elements;
    }

    [System.Serializable]
    public class Position
    {
        public int x;
        public int y;
        public int width;
    }

    [System.Serializable]
    public class Movement
    {
        public int start_x;
        public int end_x;
        public int start_y;
        public int end_y;
        public float velocity;
    }

    [System.Serializable]
    public class Elements
    {
        public int type_id;
        public Position position;
        public Movement movement;
    }

    /////
    /////

    [System.Serializable]
    public class SenderLogin
    {
        public string msisdn;
        public string password;
    }

    [System.Serializable]
    public class ResponseLogin
    {
        public int responseCode;
        public string responseMessage;
        public string[] responseError;
        public ResponseResult responseResult;
    }

    [System.Serializable]
    public class ResponseResult
    {
        public string token;
        public int firstLogin;
        public string clienteId;
        public string msisdn;
        public int acceptPersonalTerms;
    }

    [System.Serializable]
    public class SenderInfoCustomer
    {
        public string token;
        public string userId;
        public string msisdn;
        public AppInfo appInfo;
    }

    [System.Serializable]
    public class AppInfo
    {
        public int os;
        public string versionApp;
        public string versionOs;
    }

    [System.Serializable]
    public class SenderInfoCustomerLevel : SenderInfoCustomer
    {
        public int level;
        // falta id de touch o voz
    }

    [System.Serializable]
    public class ResponseInfoCustomer
    {
        public int responseCode;
        public string responseMessage;
        public string[] responseError;
        public JumpCuyCustomer jumpCuyCustomer;
        public int maxLevel;
        public int coinPerTicket;
        public bool vip;
    }

    [System.Serializable]
    public class JumpCuyCustomer
    {
        public int id;
        public int clientId;
        public int life;
        public int coin;
        public string lastShareTime;
        public string lastPlayTime;
        public int isWinMainPrice;
        public int isInternalCus;
        public int currentLevel;
        public string createDatetime;
        public string updateDatetime;
        public string listKey;
        public int totalKey;
        public long lastWinRappiTime;
        public int isWinGiftCard;
        public int levelBubbleGame;
        public int levelEarthGame;
        public int levelRobotDefense;
        public int levelJellyCrush;
        public int levelHoverShift;
        public int levelJetPack;
        public int levelColorJump;
    }

    [System.Serializable]
    public class ResponseTickersInfo
    {
        public int responseCode;
        public string responseMessage;
        public string[] responseError;
        public JumpCuyTicket[] jumpCuyTickets;
    }

    [System.Serializable]
    public class JumpCuyTicket
    {
        public int id;
        public int clientId;
        public int status;
        public int priceCoin;
        public string createDatetime;
        public string expireDateTime;
        public string updateDatetime;
    }

    [System.Serializable]
    public class ResponseGameStructure
    {
        public int responseCode;
        public string responseMessage;
        public string[] responseError;
        public JumpCuyGameStructure jumpCuyGameStructure;
        public JumpCuyGamePlatforms[] jumpCuyGamePlatforms;

    }

    [System.Serializable]
    public class JumpCuyGameStructure
    {
        public int id;
        public int levelGame;
        public int timePlay;
        public int region;
        public string backgroundImg;
    }

    [System.Serializable]
    public class JumpCuyGamePlatforms
    {
        public int id;
        public int idStructure;
        public int position;
        public int typeId; // 1 enemy, 2 benefit, 3 time, 4 nothing
        public int positionX;
        public int positionY;
        public int width;
        public int movementStartX;
        public int movementEndX;
        public int movementVelocity;
        public string elementCode;
        public int elementPosition;
        public int elementStartX;
        public int elementStartY;
        public float elementVelocity;
        public string elementCollider;
        public string platformImageUrl;
        public GameElement gameElement;

    }

    [System.Serializable]
    public class GameElement
    {
        public int id;
        public int typeId; // 1 enemy, 2 benefit, 3 time, 4 nothing
        public string elementCode;
        public string name;
        public string description;
        public int bonusTime;
        public string elementImg;
    }

    [System.Serializable]
    public class Response
    {
        public int responseCode;
        public string responseMessage;
        public string[] responseError;
    }

    [System.Serializable]
    public class ResponseRandomDoor : Response
    {
        public string prizeCode;
        public string title;
        public string description;
        public string imgUrl;
    }

    [System.Serializable]
    public class ResponsePrizeBigDraw : Response
    {
        public PrizeBigDraw[] gamePrizeList;
        public string dateBigDraw;
        public string descriptionBigDraw;
        public string urlBigDraw;
    }

    [System.Serializable]
    public class ResponseGiveBenefit : Response
    {
        public int numberCoin;
        public int key;
        public int bonusPrize;
    }

    [System.Serializable]
    public class PrizeBigDraw
    {
        public int id;
        public string codePrize;
        public string name;
        public int type;
        public string value;
        public string description;
        public int priority;
        public string imgUrl;
        public string createDateTime;
    }

    [System.Serializable]
    public class SenderInfoCustomerBenefit : SenderInfoCustomer
    {
        public int idStructure;
        public int winLevel;
        public int gameMode;
        public int[] listPlatformId;
        public string newValue;
    }

    [System.Serializable]
    public class SenderInfoCustomerBenefitGameDaily : SenderInfoCustomer
    {
        public int winLevel;
        public string codeGame;
        public int gameLevel;
        public int[] gameConfigPrizeId;
        public string newValue;
    }

    [System.Serializable]
    public class SenderInfoCustomerCoinToTicket : SenderInfoCustomer
    {
        public int quantityOfTicket;
    }

    [System.Serializable]
    public class ResponseCoinToTicket : Response
    {
        public PrizeBigDraw[] gamePrizeList;
        public string dateBigDraw;
        public string descriptionBigDraw;
        public string urlBigDraw;
    }

    [System.Serializable]
    public class BenefitWin
    {
        public List<JumpCuyGamePlatforms> list;
    }

    [System.Serializable]
    public class ResponseListMission : Response
    {
        public JumpCuyMission[] jumpCuyMissions;
    }

    [System.Serializable]
    public class JumpCuyMission
    {
        public int id;
        public string codePrize;
        public string name;
        public string title;
        public string content;
        public int lifeAdd;
        public int priority;
        public string buttonValue;
        public string section;
        public long createDatetime;
        public long updateDatetime;
        public int status;
    }

    [System.Serializable]
    public class ResponseListWinner : Response
    {
        public JumpCuyWinner[] winnerList;
    }

    [System.Serializable]
    public class JumpCuyWinner
    {
        public string msisdn;
        public string name;
        public string createDate;
        public string dateStr;
    }

    [System.Serializable]
    public class ResponseListHistory : Response
    {
        public JumpCuyHistory[] lifeHistory;
    }

    [System.Serializable]
    public class JumpCuyHistory
    {
        public int id;
        public string reason;
        public int clientId;
        public string referenceId;
        public int total;
        public int used;
        public int expireNumber;
        public int remain;
        public string createDatetime;
        public string expireDate;
        public string updateDatetime;
        public int isExpire;
        public string description;
    }

    [System.Serializable]
    public class ResponseListHistoryAwardsWon : Response
    {
        public HistoryAwardWon[] listPrizeJumpGame;
    }

    [System.Serializable]
    public class HistoryAwardWon
    {
        public int id;
        public string prizeCode;
        public string prizeType;
        public string resultCode;
        public string resultDesc;
        public string respCodePro;
        public string respMesPro;
        public string codeGame;
        public string createDatetime;
        public string updateDatetime;
        public string prizeName;
        public string createDatetimeStr;
    }

    [System.Serializable]
    public class ResponseLevels : Response
    {
        public Level[] jumpCuyGameLevels;
    }

    [System.Serializable]
    public class Level
    {
        public int gameLevel;
        public string title;
        public string imageUrl;
        public string bigImageUrl;
        public int availableMode; // 0 touch, 1 voice, 2 both
        public string imageResult;
    }

    [System.Serializable]
    public class AssetPublicParent
    {
        public List<AssetPublic> list = new List<AssetPublic>();
    }

    [System.Serializable]
    public class AssetPublic
    {
        public string name;
        public int size;
    }

    [System.Serializable]
    public class SenderInfoCustomerGamePrize : SenderInfoCustomer
    {
        public string type;
    }

    [System.Serializable]
    public class SenderInfoCustomerGamePrizeRoute : SenderInfoCustomerGamePrize
    {
        public string codeGame;
        public int level;
    }

    [System.Serializable]
    public class ResponseGamePrize : Response
    {
        public ConfigPrize[] gameConfigPrizes;
    }

    [System.Serializable]
    public class ConfigPrize
    {
        public int id;
        public GamePrize gamePrize;
    }

    [System.Serializable]
    public class GamePrize
    {
        public string imgUrl;
        public string name;

    }

    [System.Serializable]
    public class ResponseGamePrizeRoute : Response
    {
        public ConfigPrizeRoute[] gameConfigPrizes;
    }

    [System.Serializable]
    public class ConfigPrizeRoute
    {
        public int id;
        public string prizeCode;
        public GamePrizeRoute gamePrize;
    }

    [System.Serializable]
    public class GamePrizeRoute
    {
        public int id;
        public string name;
        public string codePrize;
        public int type;
    }

    [System.Serializable]
    public class ResponseMostSelling : Response
    {
        public MostSellingResult responseResult;
    }

    [System.Serializable]
    public class MostSellingResult
    {
        public MostSellingBalance balance;
        public VasPackage[] listVasPackage;
    }

    [System.Serializable]
    public class MostSellingBalance
    {
        public int amount;
        public string dueDate;
    }

    [System.Serializable]
    public class VasPackage
    {
        public string codePackage;
        public string name;
        public string description;
        public string days;
        public string total;
        public string categoryName;
        public string priceInBpuntos;
        public int categoryPriority;
        public int isCanBuyWithCredit;
        public int packageOrigin;
        public string bannerUrl;
        public string priceInLoyaltyPoint;
        public bool favorite;
    }

    [System.Serializable]
    public class SenderPurchasePackage : SenderInfoCustomer
    {
        public PurchasePackage[] listPackage = new PurchasePackage[1];
    }

    [System.Serializable]
    public class PurchasePackage
    {
        public string codePackage;
    }

    [System.Serializable]
    public class ResponseWelcome : Response
    {
        public ConfigWelcome[] cuyConfigWelcomeList;
    }

    [System.Serializable]
    public class ConfigWelcome
    {
        public int id;
        public string image1;
        public string image2;
        public string text1;
        public string text2;
    }

    [System.Serializable]
    public class SenderSubscriptionVIP : SenderInfoCustomer
    {
        public string serviceCode;
        public int actionType = 0;
    }

    [System.Serializable]
    public class ReferenceUI
    {
        public Image image;
        public Sprite sprite_1;
        public Sprite sprite_2;
    }

    [System.Serializable]
    public class ReferenceGO
    {
        public GameObject object_1;
        public GameObject object_2;
    }

    [System.Serializable]
    public class ReferenceTextC
    {
        public Text text;
        public Color color_1;
        public Color color_2;
    }

    [System.Serializable]
    public class ResponseOcsMsisdn : Response
    {
        public OcsMsisdn[] data;
    }

    [System.Serializable]
    public class OcsMsisdn
    {
        public int type;
        public float benefit;
    }

}
