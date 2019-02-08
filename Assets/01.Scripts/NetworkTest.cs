using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using System.IO;

class PlayerMap
{
    public string Id;
    public string Title;
    public string Owner;
}


public class NetworkTest : MonoBehaviour {

    private PlayerMap m_player = new PlayerMap();

	// Use this for initialization
	void Start () {
        StartCoroutine(SaveNetData());
        //SaveNetData();
    }

    // Update is called once per frame
    void Update () {

    }

    IEnumerator Upload()
    {
        Debug.Log("들어온다");
        string strUrl = "http://54.180.153.218:7436/api/maps?owner=retan";
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

        string strJson;

        strJson = File.ReadAllText(strPath);

        var formData = System.Text.Encoding.UTF8.GetBytes(strJson);
        Debug.Log(strJson);

        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("title", "Map Test2");
        dic.Add("owner", "Retan");
        dic.Add("mapData", strJson);
        JsonData data = JsonMapper.ToJson(dic);
        Debug.Log("이거봐라" + data.ToString());
        //Debug.Log("이거도 봐라" + System.Text.Encoding.UTF8.GetBytes(strJson));

        Dictionary<string, string> dic2 = new Dictionary<string, string>();
        dic.Add("Content-Type", "application/json");

        //Hashtable table = new Hashtable();
        //table.Add("Content-Type", "application/json");

        //WWWForm form = new WWWForm();
        // form.headers.Add("Content-Type", "application/json");
        //form.AddField("title", strJson);

        //UnityWebRequest www = UnityWebRequest.Post("http://54.180.153.218:7436/api/maps", form);
        WWW www = new WWW("http://54.180.153.218:7436/api/maps", System.Text.Encoding.UTF8.GetBytes(data.ToString()), dic2);
        //StartCoroutine(wait(www));
        yield return www;

        //if (www.isNetworkError || www.isHttpError)
        //{
        //    Debug.Log(www.error);
        //}
        //else
        //{
        //    Debug.Log("Form upload complete!");
        //}
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
        playerMap.Title = jsonvale["title"].ToString();
        playerMap.Owner = jsonvale["owner"].ToString();

        Debug.Log(playerMap.Id);
        Debug.Log(playerMap.Title);
        Debug.Log(playerMap.Owner);
    }
}
