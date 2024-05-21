using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static Models;

public class ApiService : MonoBehaviour
{
    static string tag = "ApiService";

    static int maxAttempts = 3;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public static IEnumerator getSpriteFromUrl(string url, System.Action<Sprite> callback)
    {
        if (url == null || url.Equals(""))
            callback(null);

        Davinci.get()
            .load(url)
            .withStartAction(() =>
            {
                //statusTxt.text = "Download has been started.";
            })
            .withDownloadProgressChangedAction((progress) =>
            {
                //statusTxt.text = "Download progress: " + progress;
            })
            .withDownloadedAction(() =>
            {
                //statusTxt.text = "Download has been completed.";
            })
            .withLoadedAction(() =>
            {
                //statusTxt.text = "Image has been loaded.";
            })
            .withErrorAction((error) =>
            {
                //statusTxt.text = "Got error : " + error;
                //Debug.Log("withErrorAction " + error);
                callback(null);
            })
            .withImageReadyAction((sprite) =>{
                //Debug.Log("withImageReadyAction");
                if (sprite != null)
                    callback(sprite);
                else
                    callback(null);
            })
            .withEndAction(() =>
            {
                //print("Operation has been finished.");
            })
            .setCached(true)
            .start();

        yield return null;
    }

    public static IEnumerator getSpriteFromUrlOld(string url, System.Action<Sprite> callback)
    {
        if(url == null || url.Equals(""))
            callback(null);


        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Constants.log(tag, www.error);
            callback(null);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            callback(sprite);
        }
    }

    public static UnityWebRequest getResquest(string api, string json)
    {
        string url = Globals.apiBaseUrl + api;
        Constants.log(tag, url + " " + json);
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        return request;
    }

    public static UnityWebRequest getResquestTest(string api, string json)
    {
        string url = Globals.apiBaseUrlTest + api;
        Constants.log(tag, url + " " + json);
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        return request;
    }

    public static IEnumerator getLogin(SenderLogin data, System.Action<ResponseLogin> callback)
    {
        string apiUrl = "services/login";

        var request = new UnityWebRequest(Globals.apiBaseUrl + apiUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            callback(null);
        }
        else
        {
            ResponseLogin res = JsonUtility.FromJson<ResponseLogin>(request.downloadHandler.text);
            Constants.log(tag, "token " + res.responseResult.token);
            callback(res);
        }
    }

    public static IEnumerator getInfoCustomer(SenderInfoCustomer data, System.Action<ResponseInfoCustomer> callback, int attempt = 1)
    {
        string apiUrl = "cuyJump/getInfoCustomer";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            //Debug.Log(request.error);

            if (attempt >= maxAttempts)
                callback(null);
            else
            {
                yield return new WaitForSecondsRealtime(1);
                yield return getInfoCustomer(data, callback, attempt + 1);
            }
        }
        else
        {
            //Debug.Log("-----------> " + request.downloadHandler.text);
            Constants.log("getInfoCustomer", request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponseInfoCustomer>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator getTicketsInfo(SenderInfoCustomer data, System.Action<ResponseTickersInfo> callback, int attempt = 1)
    {
        string apiUrl = "cuyJump/getTicketsInfo";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error);
            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return getTicketsInfo(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponseTickersInfo>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator getGameStructure(SenderInfoCustomerLevel data, System.Action<ResponseGameStructure> callback, int attempt = 1)
    {
        //string apiUrl = "structure.json";
        //var request = getResquestTest(apiUrl, JsonUtility.ToJson(data));
        string apiUrl = "cuyJump/getGameStructure";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error);

            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return getGameStructure(data, callback, attempt + 1);
        }
        else
        {

            //Debug.Log("getGameStructure");
            //Debug.Log(request.downloadHandler.text);

            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponseGameStructure>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator getRandomDoor(SenderInfoCustomer data, System.Action<ResponseRandomDoor> callback, int attempt = 1)
    {
        string apiUrl = "services/randomDoorGame";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error);

            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return getRandomDoor(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponseRandomDoor>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }



    public static IEnumerator getBigPrizeDraw(SenderInfoCustomer data, System.Action<ResponsePrizeBigDraw> callback, int attempt = 1)
    {
        string apiUrl = "cuyJump/listPrizeBigDraw";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error);

            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return getBigPrizeDraw(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponsePrizeBigDraw>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator giveBenefitGame(SenderInfoCustomerBenefit data, System.Action<ResponseGiveBenefit> callback, int attempt = 1)
    {
        data.newValue = Constants.getSha1Sum(data.token+data.userId);
        data.gameMode = Constants.getGameMode();
        string apiUrl = "cuyJump/giveBenefitGame";
        Constants.log(tag, JsonUtility.ToJson(data));
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error);

            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return giveBenefitGame(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponseGiveBenefit>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator changeCoinToTicket(SenderInfoCustomerCoinToTicket data, System.Action<ResponseCoinToTicket> callback, int attempt = 1)
    {
        string apiUrl = "cuyJump/changeCoinToTicket";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error);

            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return changeCoinToTicket(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponseCoinToTicket>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator jumpCuyShare(SenderInfoCustomer data, System.Action<Response> callback, int attempt = 1)
    {
        string apiUrl = "cuyJump/jumpCuyShare";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error);

            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return jumpCuyShare(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<Response>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator listMission(SenderInfoCustomer data, System.Action<ResponseListMission> callback, int attempt = 1)
    {
        string apiUrl = "cuyJump/listMission";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error);

            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return listMission(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponseListMission>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator listWinner(SenderInfoCustomer data, System.Action<ResponseListWinner> callback, int attempt = 1)
    {
        string apiUrl = "cuyJump/cuyJumplistWinner";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error);

            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return listWinner(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponseListWinner>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator listHistory(SenderInfoCustomer data, System.Action<ResponseListHistory> callback, int attempt = 1)
    {
        string apiUrl = "cuyJump/getLifeHistory";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error);

            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return listHistory(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponseListHistory>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator listHistoryAwardsWon(SenderInfoCustomer data, System.Action<ResponseListHistoryAwardsWon> callback, int attempt = 1)
    {
        string apiUrl = "services/getListPrizeOfCustomer";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error);

            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return listHistoryAwardsWon(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponseListHistoryAwardsWon>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator listLevels(SenderInfoCustomer data, System.Action<ResponseLevels> callback, int attempt = 1)
    {
        //string apiUrl = "listLevels.json";
        //var request = getResquestTest(apiUrl, JsonUtility.ToJson(data));
        string apiUrl = "cuyJump/getListLevel";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error + " " + attempt);

            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return listLevels(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponseLevels>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator getGamePrize(SenderInfoCustomer data, System.Action<ResponseGamePrize> callback, int attempt = 1)
    {
        string apiUrl = "services/getGamePrize";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return getGamePrize(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponseGamePrize>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator getMostSelling(SenderInfoCustomer data, System.Action<ResponseMostSelling> callback, int attempt = 1)
    {
        string apiUrl = "services/getMostSellingGame"; //"services/getMostSelling";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error);

            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return getMostSelling(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponseMostSelling>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator getGamePrizeRoute(SenderInfoCustomer data, System.Action<ResponseGamePrizeRoute> callback, int attempt = 1)
    {
        string apiUrl = "services/getGamePrize";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return getGamePrizeRoute(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponseGamePrizeRoute>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator giveBenefitGameDaily(SenderInfoCustomerBenefitGameDaily data, System.Action<ResponseGiveBenefit> callback, int attempt = 1)
    {
        data.newValue = Constants.getSha1Sum(data.token + data.userId);
        string apiUrl = "services/getBenefitGameDaily";
        Constants.log(tag, JsonUtility.ToJson(data));
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        //Debug.Log("******************" + request.ToString());
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error);

            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return giveBenefitGameDaily(data, callback, attempt + 1);
        }
        else
        {
            Constants.log("giveBenefitGameDaily", request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponseGiveBenefit>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator purchasePackages(SenderPurchasePackage data, System.Action<Response> callback, int attempt = 1)
    {
        string apiUrl = "services/purchasePackages";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error);

            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return purchasePackages(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<Response>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator getWelcomePopupGame(SenderInfoCustomer data, System.Action<ResponseWelcome> callback, int attempt = 1)
    {
        string apiUrl = "services/getWelcomePopupGame";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));
        //var request = getResquestTest("onboarding.json", JsonUtility.ToJson(data));
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error);

            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return getWelcomePopupGame(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponseWelcome>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator getSubscriptionVIP(SenderSubscriptionVIP data, System.Action<Response> callback, int attempt = 1)
    {
        string apiUrl = "mps-service/mps-subscription-cancellation";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));

        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error);

            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return getSubscriptionVIP(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<Response>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

    public static IEnumerator getListOcsForMsisdn(SenderInfoCustomer data, System.Action<ResponseOcsMsisdn> callback, int attempt = 1)
    {
        string apiUrl = "services/listOcsForMsisdn";
        var request = getResquest(apiUrl, JsonUtility.ToJson(data));

        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Constants.log(tag, request.error);

            if (attempt >= maxAttempts)
                callback(null);
            else
                yield return getListOcsForMsisdn(data, callback, attempt + 1);
        }
        else
        {
            Constants.log(tag, request.downloadHandler.text);
            callback(JsonUtility.FromJson<ResponseOcsMsisdn>(request.downloadHandler.text));
        }

        request.Dispose();
        request = null;
    }

}
