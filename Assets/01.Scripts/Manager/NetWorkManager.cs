using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerMap
{
    public string Id;
    public string Title;
    public string Owner;
}

public class NetWorkManager : MonoBehaviour {

    public static NetWorkManager Instance = null;

    public static string m_strID = "";
    public static int m_iPreScore = 0;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    private List<PlayerMap> m_listPlayerMap = new List<PlayerMap>();

    public List<PlayerMap> GetListPlayerMap()
    {
        return m_listPlayerMap;
    }

    public IEnumerator AllMapLoad()
    {
        string strUrl = "http://54.180.153.218:7436/api/maps";
        WWW www = new WWW(strUrl);
        yield return www;

        if (www.error == null)
        {
            Debug.Log(www.text);
            ProcessJson(www.text);
        }
        else
        {
            Debug.Log("Error" + www.error);
        }
    }

    public IEnumerator SearchMapLoad(string strUserName)
    {
        string strUrl = "http://54.180.153.218:7436/api/maps?owner=" + strUserName;
        WWW www = new WWW(strUrl);
        yield return www;

        if (www.error == null)
        {
            Debug.Log(www.text);
            ProcessJson(www.text);
        }
        else
        {
            Debug.Log("Error" + www.error);
        }
    }

    void ProcessJson(string strJson)
    {
        m_listPlayerMap.Clear();

        JsonData jsonvale = JsonMapper.ToObject(strJson);

        for(int i = 0; i < jsonvale.Count; ++i)
        {
            PlayerMap playermap = new PlayerMap();

            playermap.Id = jsonvale[i]["_id"].ToString();
            playermap.Title = jsonvale[i]["title"].ToString();
            playermap.Owner = jsonvale[i]["owner"].ToString();

            m_listPlayerMap.Add(playermap);

            CreateFile(i);
        }

        Debug.Log("오브젝트 수 : " + m_listPlayerMap.Count);
    }


    public IEnumerator SaveNetData(string strTitle)
    {
        string strPath = Application.streamingAssetsPath + "/" + strTitle + ".json";

        string strJson = string.Empty;

        strJson = File.ReadAllText(strPath);
        string strTest = "{\"title\":\"" + strTitle+ "\", \"owner\":\"retan\",\"mapData\":" + strJson + "}";

        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("Content-Type", "application/json");

        byte[] body = System.Text.Encoding.UTF8.GetBytes(strTest);
        WWW www = new WWW("http://54.180.153.218:7436/api/maps", body, dic);
        StartCoroutine(SendCheck(www));
        yield return www;

    }

    IEnumerator SendCheck(WWW data)
    {
        yield return data; // Wait until the download is done 
        if (data.error == null)
        {
            BlockManager m_blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();
            ScoreUI scoreUI = GameObject.Find("ScoreUI").GetComponent<ScoreUI>();
            List<Vector2> listPos = SaveNLoad.GetInstance.GetListPos();

            JsonData mapData = JsonMapper.ToObject(data.text);

            GameObject goBlockManager = new GameObject("BlockManager");
            m_iPreScore = int.Parse(mapData["bestScore"]["score"].ToString());
            scoreUI.SetScore(m_iPreScore);

            Debug.Log(mapData.Count);
            for (int i = 0; i < mapData.Count; ++i)
            {
                m_blockManager.CreateBlock(goBlockManager
                    , listPos[int.Parse(mapData["mapData"][i]["iIndex"].ToString())]
                    , int.Parse(mapData["mapData"][i]["blockID"].ToString())
                    , 8);
            }
        }
        else
        {
            Debug.Log("Net Load Failed");
        }
    }

    public IEnumerator PutNetData(string strID, string strScore)
    {
        string strTest = "{\"bestScore\":{\"user\":\"Kairencer\",\"score\":\"" + strScore + "\"}}";

        byte[] body = System.Text.Encoding.UTF8.GetBytes(strTest);

        UnityWebRequest www = UnityWebRequest.Put("http://54.180.153.218:7436/api/maps/" + strID, body);

        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }

    void CreateFile(int iIndex)
    {
        Debug.Log("파일 생성");
        GameObject goFile = MonoBehaviour.Instantiate(Resources.Load("UI/NetListName")) as GameObject;
        PlayerMap playerMap = NetWorkManager.Instance.GetListPlayerMap()[iIndex];

        goFile.transform.SetParent(GameObject.Find("Contents").transform);
        goFile.name = "Data";

        goFile.GetComponentInChildren<Text>().text = "No \r\nTitle " + playerMap.Title
            + "\r\nOwner " + playerMap.Owner + "\r\nCount\r\nBest Score\r\nBest User";
        goFile.transform.localScale = new Vector3(1, 1, 1);

        goFile.GetComponent<Button>().onClick.AddListener(() => SetID(m_listPlayerMap[iIndex].Id));
    }
    
    public IEnumerator LoadNetUserMap()
    {
        string strUrl = "http://54.180.153.218:7436/api/maps/" + m_strID;
        Debug.Log(strUrl);
        WWW www = new WWW(strUrl);
        
        yield return StartCoroutine(SendCheck(www));
    }

    public void SetID(string ID)
    {
        m_strID = ID;
    }

    public string GetID()
    {
        return m_strID;
    }

    public int GetPreScore()
    {
        return m_iPreScore;
    }
}
