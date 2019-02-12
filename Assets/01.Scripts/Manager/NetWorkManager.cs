using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;

public class PlayerMap
{
    public string Id;
    public string Title;
    public string Owner;
}

public class NetWorkManager : MonoBehaviour {

    public static NetWorkManager Instance = null;

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
        Debug.Log("들어온다");
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
        Debug.Log("들어온다");
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
        if (data.error != null)
        {
            Debug.Log("There was an error sending request: " + data.error);
        }
        else
        {
            Debug.Log("WWW Request: " + data.text);
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

        goFile.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(LoadNetUserMap(m_listPlayerMap[iIndex].Id)));
    }
    
    public IEnumerator LoadNetUserMap(string strID)
    {
        Debug.Log("들어오11나");
        string strUrl = "http://54.180.153.218:7436/api/maps/" + strID;
        WWW www = new WWW(strUrl);

        StartCoroutine(SendCheck(www));
        yield return www;

        if (www.error == null)
        {
            BlockManager m_blockManager;
            List<Vector2> listPos = SaveNLoad.GetInstance.GetListPos();

            m_blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();

            JsonData mapData = JsonMapper.ToObject(www.text);

            GameObject goBlockManager = new GameObject("BlockManager");

            for (int i = 0; i < mapData.Count; ++i)
            {
                m_blockManager.CreateBlock(goBlockManager
                    , listPos[int.Parse(mapData[i]["iIndex"].ToString())]
                    , int.Parse(mapData[i]["blockID"].ToString())
                    , 8);
            }
        }
        else
        {
            Debug.Log("Net Load Failed");
        }
    }
}
