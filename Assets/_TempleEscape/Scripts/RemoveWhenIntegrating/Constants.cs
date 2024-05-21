using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using static Models;

public class Constants : MonoBehaviour
{
    static string tag = "Constants";

    public static string NO_THING = "NO_THING";

    public static string CODE_CANGREJO = "CANGREJO";
    public static string CODE_CONCHA = "CONCHA";
    public static string CODE_MEDUSA = "MEDUSA";
    public static string CODE_ESTRELLA = "ESTRELLA";
    public static string CODE_SIPAN_HEAD = "SIPAN_HEAD";
    public static string CODE_PULPO = "PULPO";
    public static string CODE_MASCARA = "MASCARA";
    public static string CODE_PAVITA = "PAVITA";
    public static string CODE_CABEZA_CLAVA = "CABEZA_CLAVA";
    public static string CODE_SURI = "SURI";
    public static string CODE_MOSQUITO = "MOSQUITO";
    public static string CODE_LECHUZA = "LECHUZA";
    public static string CODE_PLATFORM_GOAL = "PLATFORM_GOAL";//"PLATFORM_GOAL"; LETRERO
    public static string CODE_20_MB = "20_MB_BENEFIT";
    public static string CODE_50_MB = "50_MB_BENEFIT";
    public static string CODE_75_MB = "75_MB_BENEFIT";
    public static string CODE_100_MB = "100_MB_BENEFIT";
    public static string CODE_150_MB = "150_MB_BENEFIT";
    public static string CODE_25_MB = "25_MB_BENEFIT";
    public static string CODE_35_MB = "35_MB_BENEFIT";
    public static string CODE_5_MIN_TDN = "5_MIN_TDN_BENEFIT";
    public static string CODE_10_MIN_TDN = "10_MIN_TDN_BENEFIT";
    public static string CODE_15_MIN_TDN = "15_MIN_TDN_BENEFIT";
    public static string CODE_20_MIN_TDN = "20_MIN_TDN_BENEFIT";
    public static string CODE_1_LIVE = "1_LIFE_BENEFIT";
    public static string CODE_SI5_PACKAGE = "PACKAGE_SI_5_BENEFIT";
    public static string CODE_COIN = "COIN_BENEFIT";



    public static string CODE_PLATFORM_ARENA = "PLATFORM_ARENA";
    public static string CODE_PLATFORM_PLAYA = "PLATFORM_PLAYA";
    public static string CODE_PLATFORM_ORO = "PLATFORM_ORO";
    public static string CODE_PLATFORM_ARENA2 = "PLATFORM_ARENA2";
    public static string CODE_PLATFORM_NIEVE = "PLATFORM_NIEVE";
    public static string CODE_TIME = "TIME";
    public static string CODE_KEY = "KEY";

    public static string CODE_WIN_GAME = "WIN";

    public const string CODE_1_LIFE_ROUTE = "1_LIFE_BG_ROUTE";
    public const string CODE_10_MIN_ROUTE = "10_MIN_TDN_BG_ROUTE";
    public const string CODE_15_MIN_ROUTE = "15_MIN_TDN_BG_ROUTE";
    public const string CODE_20_MIN_ROUTE = "20_MIN_TDN_BG_ROUTE";
    public const string CODE_25_MB_ROUTE = "25_MB_BG_ROUTE";
    public const string CODE_35_MB_ROUTE = "35_MB_BG_ROUTE";
    public const string CODE_50_MB_ROUTE = "50_MB_BG_ROUTE";
    public const string CODE_75_MB_ROUTE = "75_MB_BG_ROUTE";
    public const string CODE_100_MB_ROUTE = "100_MB_BG_ROUTE";
    public const string CODE_150_MB_ROUTE = "150_MB_BG_ROUTE";
    public const string CODE_GAME_BUBBLES = "BUBBLE_GAME";
    public const string CODE_GAME_HELIX = "EARTH_GAME";
    public const string CODE_GAME_JELLY = "JELLY_CRUSH";
    public const string CODE_GAME_ROBOT = "ROBOT_DEFENSE";
    public const string CODE_GAME_HOVERSHIFT = "HOVERSHIFT";
    public const string CODE_GAME_JETPACK = "JET_PACK";
    public const string CODE_GAME_COLOR_JUMP = "COLOR_JUMP";


    public static string SCENE_ONBOARDING = "SCENE_ONBOARDING";
    public static string SCENE_HOME = "SCENE_HOME";
    public static string SCENE_LEVELS = "SCENE_LEVELS";
    public static string SCENE_GAME_JUMP = "SCENE_GAME_JUMP";
    public static string SCENE_GAME_DOORS = "SCENE_GAME_DOORS";
    public static string SCENE_GAME_BURBUJAS_LEVELS = "SCENE_GAME_BURBUJAS_LEVELS";
    public static string SCENE_GAME_BURBUJAS_GAME = "SCENE_GAME_BURBUJAS_GAME";
    public static string SCENE_GAME_CANDY = "SCENE_GAME_CANDY";
    public static string SCENE_GAME_BUBBLE = "SCENE_GAME_BUBBLE";
    public static string SCENE_GAME_TIERRA_MAP = "SCENE_GAME_TIERRA_MAP";
    public static string SCENE_GAME_TIERRA_GAME = "SCENE_GAME_TIERRA_GAME";
    public static string SCENE_GAME_ROBOT_MAP = "SCENE_GAME_ROBOT_MAP";
    public static string SCENE_GAME_ROBOT_GAME = "SCENE_GAME_ROBOT_GAME";
    public static string SCENE_GAME_COLOR_JUMP_MAP = "SCENE_GAME_COLOR_JUMP_MAP";
    public static string SCENE_GAME_COLOR_JUMP_GAME = "SCENE_GAME_COLOR_JUMP_GAME";
    public static string SCENE_GAME_SPACE_RUNNER = "SCENE_GAME_SPACE_RUNNER";
    public static string SCENE_GAME_JETPACK = "SCENE_GAME_JETPACK";


    public static string SCENE_STORE = "SCENE_STORE";
    public static string SCENE_OTHER = "SCENE_OTHER";

    public static string SCENE_WINNERS = "SCENE_WINNERS";
    public static string SCENE_HISTORY_AWARDS_WON = "SCENE_HISTORY_AWARDS_WON";
    public static string SCENE_HISTORY_LIVES = "SCENE_HISTORY_LIVES";
    public static string SCENE_TERMS = "SCENE_TERMS";

    public static string SCENE_ACTUAL = "";
    public static string SCENE_LAST_GAME_PLAYED = "";

    public static string SUBSCRIPTION_1_CODE = "BAVENTURAS_DAILY";
    public static string SUBSCRIPTION_2_CODE = "BAVENTURAS_WEEKLY";
    public static string SUBSCRIPTION_3_CODE = "BAVENTURAS_MONTHLY";

    public static float SUBSCRIPTION_1_PRICE = 1.80f;
    public static float SUBSCRIPTION_2_PRICE = 4.99f;
    public static float SUBSCRIPTION_3_PRICE = 9.99f;

    public static string SUBSCRIPTION_1_DESC = "Diario por S/ 1.80";
    public static string SUBSCRIPTION_2_DESC = "Semanal por S/ 4.99";
    public static string SUBSCRIPTION_3_DESC = "Mensual por S/ 9.99";

    /*public static string getUserNsisdn()
    {
        return "910248192";
    }

    public static string getUserPassword()
    {
        return "2345";
    }*/

    public static void setLoginData(ResponseLogin data)
    {
        PlayerPrefs.SetString("login_data", JsonUtility.ToJson(data));
    }

    public static ResponseLogin getLoginData()
    {
        string data = PlayerPrefs.GetString("login_data");
        if (data == null || data.Equals(""))
            return null;
        return JsonUtility.FromJson<ResponseLogin>(data);
    }

    public static void setAppInfo(AppInfo data)
    {
        PlayerPrefs.SetString("app_info", JsonUtility.ToJson(data));
    }

    public static AppInfo getAppInfo()
    {
        if(Constants.inDevelopMode())
        {
            AppInfo data = new AppInfo();
            data.os = 1;
            data.versionApp = "0.0";
            data.versionOs = "0";
            return data;
        }
        else
        {
            string data = PlayerPrefs.GetString("app_info");
            if (data == null || data.Equals(""))
                return null;
            return JsonUtility.FromJson<AppInfo>(data);
        }
    }

    public static void log(string tag, string message)
    {
        if(Globals.showDebugMessages)
            Debug.Log("unity_log_" + tag + " " + message);
    }

    public static void setInfoCustomer(ResponseInfoCustomer data)
    {
        PlayerPrefs.SetString("info_customer", JsonUtility.ToJson(data));
    }

    public static ResponseInfoCustomer getInfoCustomer()
    {
        string data = PlayerPrefs.GetString("info_customer");
        if (data == null || data.Equals(""))
            return null;
        return JsonUtility.FromJson<ResponseInfoCustomer>(data);
    }

    public static void setSenderInfoCustomer(SenderInfoCustomer data)
    {
        PlayerPrefs.SetString("sender_info_customer", JsonUtility.ToJson(data));
    }

    public static SenderInfoCustomer getSenderInfoCustomer()
    {
        string data = PlayerPrefs.GetString("sender_info_customer");
        if (data == null || data.Equals(""))
            return null;
        return JsonUtility.FromJson<SenderInfoCustomer>(data);
    }

    public static BenefitWin getBenefitsWinn()
    {
        string data = PlayerPrefs.GetString("list_benefits_winn");
        if (data == null || data.Equals(""))
            return null;
        return JsonUtility.FromJson<BenefitWin>(data);
    }

    public static void setBenefitsWinn(BenefitWin data)
    {
        if(data == null)
        {
            PlayerPrefs.SetString("list_benefits_winn", "");
        }
        else
        {
            //Debug.Log(JsonUtility.ToJson(data));
            PlayerPrefs.SetString("list_benefits_winn", JsonUtility.ToJson(data));
        }
        
    }

    public static SenderInfoCustomerBenefitGameDaily getLevelProgressInfo()
    {
        string data = PlayerPrefs.GetString("level_progress_route");
        if (data == null || data.Equals(""))
            return null;
        return JsonUtility.FromJson<SenderInfoCustomerBenefitGameDaily>(data);
    }

    public static void setLevelProgressInfo(SenderInfoCustomerBenefitGameDaily data)
    {
        if (data == null)
        {
            PlayerPrefs.SetString("level_progress_route", "");
        }
        else
        {
            PlayerPrefs.SetString("level_progress_route", JsonUtility.ToJson(data));
        }

    }

    public static void setLevelSelected(Level data)
    {
        PlayerPrefs.SetString("level_selected", JsonUtility.ToJson(data));
    }

    public static Level getLevelSeletec()
    {
        string data = PlayerPrefs.GetString("level_selected");
        if (data == null || data.Equals(""))
            return null;
        return JsonUtility.FromJson<Level>(data);
    }

    public static void setLevels(ResponseLevels data)
    {
        PlayerPrefs.SetString("levels_service", JsonUtility.ToJson(data));
    }

    public static ResponseLevels getLevels()
    {
        string data = PlayerPrefs.GetString("levels_service");
        if (data == null || data.Equals(""))
            return null;
        return JsonUtility.FromJson<ResponseLevels>(data);
    }

    /*public static void setLevelSelected(int id)
    {
        PlayerPrefs.SetInt("level_selected", id);
    }

    public static int getLevelSeletec()
    {
        return PlayerPrefs.GetInt("level_selected");
    }*/

    public static void setLevelStructureId(int id)
    {
        PlayerPrefs.SetInt("level_structure_id", id);
    }

    public static int getLevelStructureId()
    {
        return PlayerPrefs.GetInt("level_structure_id");
    }

    public static int isComesFromOnBoarding()
    {
        return PlayerPrefs.GetInt("is_comes_from_onBoarding");
    }

    public static void setComesFromOnBoarding(int val)
    {
        PlayerPrefs.SetInt("is_comes_from_onBoarding", val);
    }

    public static void setGameMode(int data)
    {
        // 0 touch 1 voice
        PlayerPrefs.SetInt("game_mode", data);
    }

    public static int getGameMode()
    {
        return PlayerPrefs.GetInt("game_mode");
    }

    public static string getPrizeCodeReference(int id)
    {
        Dictionary<int, string> currentCodes = stringToDictionary(PlayerPrefs.GetString("prizes_code_reference"));
        return currentCodes[id];
    }
    public static void addPrizeCodeReference(int id, string code)
    {
        //Debug.Log("addPrizeCodeReference " + id + " " + code);
        Dictionary<int, string> currentCodes = stringToDictionary(PlayerPrefs.GetString("prizes_code_reference"));
        currentCodes.Add(id, code);
        PlayerPrefs.SetString("prizes_code_reference", dictionaryToString(currentCodes));
    }
    public static void clearPrizeCodeReference()
    {
        Dictionary<int, string> currentCodes = new Dictionary<int, string>();
        PlayerPrefs.SetString("prizes_code_reference", dictionaryToString(currentCodes));
    }

    public static void setAudioConfig(int val)
    {
        // 0 off 1 on
        PlayerPrefs.SetInt("audio_config", val);
        setAudioDefault("default_changed");
    }

    public static int getAudioConfig()
    {
        int val = PlayerPrefs.GetInt("audio_config");
        string def = getAudioDefault();
        if(def == null || !def.Equals("default_changed"))
        {
            return 1;
        }
        else
        {
            return val;
        }
    }

    public static void setAudioDefault(string val)
    {
        PlayerPrefs.SetString("audio_config_default", val);
    }

    public static string getAudioDefault()
    {
        return PlayerPrefs.GetString("audio_config_default");
    }

    public static void isVIP(bool val)
    {
        // 0 off 1 on
        PlayerPrefs.SetInt("vip_config", val ? 1 : 0);
    }

    public static bool isVIP()
    {
        return PlayerPrefs.GetInt("vip_config") == 1;
    }

    public static void setLastCache(string data)
    {
        PlayerPrefs.SetString("last_cache_date", data);
    }

    public static string getLastCache()
    {
        return PlayerPrefs.GetString("last_cache_date");
    }

    public static AssetPublicParent getAssetPublicParent()
    {
        string data = PlayerPrefs.GetString("list_asset_public");
        if (data == null || data.Equals(""))
            return new AssetPublicParent();
        return JsonUtility.FromJson<AssetPublicParent>(data);
    }

    public static void setAssetPublicParent(AssetPublicParent data)
    {
        if (data == null)
        {
            PlayerPrefs.SetString("list_asset_public", "");
        }
        else
        {
            //Debug.Log(JsonUtility.ToJson(data));
            PlayerPrefs.SetString("list_asset_public", JsonUtility.ToJson(data));
        }

    }

    public static string getSha1Sum(string strToEncrypt)
    {
        UTF8Encoding ue = new UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        SHA1 sha = new SHA1CryptoServiceProvider();
        byte[] hashBytes = sha.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    public static bool inDevelopMode()
    {
        return Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor;
    }

    /* Utils */
    private static string dictionaryToString(Dictionary<int, string> data)
    {
        string result = "";
        foreach (KeyValuePair<int, string> value in data)
        {
            result += value.Key.ToString() + "," + value.Value + "|";
        }
        return result;
    }
    public static Dictionary<int, string> stringToDictionary(string data)
    {
        Dictionary<int, string> result = new Dictionary<int, string>();
        if (String.IsNullOrEmpty(data))
            return result;
        string[] pairs = data.Split('|');
        foreach (string pair in pairs)
        {
            if (!String.IsNullOrEmpty(pair))
            { 
                string[] values = pair.Split(',');
                result.Add(int.Parse(values[0]), values[1]);
            }
        }
        return result;
    }
}
