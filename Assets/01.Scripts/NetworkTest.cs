using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Newtonsoft.Json.Linq;
using LitJson;
using System.IO;
using UnityEngine.Networking;
using Newtonsoft.Json;


public class NetworkTest : MonoBehaviour {

    private PlayerMap m_player = new PlayerMap();

	// Use this for initialization
	void Start () {
        StartCoroutine(Upload());
        //SaveNetData();
    }

    // Update is called once per frame
    void Update () {
        
    }

    IEnumerator Upload()
    {
        Debug.Log("들어온다");
        string strUrl = "http://54.180.153.218:7436/api/maps";
        WWW www = new WWW(strUrl);
        yield return www;

        if(www.error == null)
        {
            Debug.Log(www.text);
            ProcessJson(www.text);
        }
        else
        {
            Debug.Log("Error" + www.error);
        }
    }

    IEnumerator SaveNetData()
    {
        string strPath = Application.streamingAssetsPath + "/" + "Stage4" + ".json";

        string strJson = string.Empty;
        
        strJson = File.ReadAllText(strPath);
        string strTest = "{\"title\":\"title123\", \"owner\":\"retan\",\"mapData\":" + strJson + "}";

        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("Content-Type", "application/json");

        byte[] body = System.Text.Encoding.UTF8.GetBytes(strTest);
        WWW www = new WWW("http://54.180.153.218:7436/api/maps", body, dic);
        StartCoroutine(wait(www));
        yield return www;

    }

    IEnumerator wait(WWW data)
    {
        yield return data; // Wait until the download is done 
        if (data.error != null)
        {
            Debug.Log("There was an error sending request: " + data.error);
        }
        else
        {
            Debug.Log("WWW Request: " + data.text);
        }
    }

    void ProcessJson(string strJson)
    {
        JsonData jsonvale = JsonMapper.ToObject(strJson);

        Debug.Log(jsonvale[0]["title"]);
        PlayerMap playerMap;
        playerMap = new PlayerMap();

        for (int i = 0; i < jsonvale.Count; ++i)
        {
            playerMap.Title = jsonvale[i]["title"].ToString();
            playerMap.Owner = jsonvale[i]["owner"].ToString();

            Debug.Log("타이틀 : " + playerMap.Title);
            Debug.Log("유저 : " + playerMap.Owner);
        }
    }
}
