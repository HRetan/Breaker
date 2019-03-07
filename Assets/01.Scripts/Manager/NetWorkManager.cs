﻿using System.Collections;
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
    public string BestScore;
    public string BestUser;

    public PlayerMap()
    {
    }

    public PlayerMap(string id, string title, string owner, string bestScore, string bestUser)
    {
        Id = id;
        Title = title;
        Owner = owner;
        BestScore = bestScore;
        BestUser = bestUser;
    }
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
    private List<GameObject> m_listFile = new List<GameObject>();
    
    public List<PlayerMap> GetListPlayerMap()
    {
        return m_listPlayerMap;
    }

    //모든 맵리스트 불러오기
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
    //유저명으로 맵리스트 불러오기
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

        if(m_listFile.Count != 0)
        {
            for(int i = 0; i < m_listFile.Count; ++i)
            {
                Destroy(m_listFile[i]);
            }
            m_listFile.Clear();
        }

        JsonData jsonvale = JsonMapper.ToObject(strJson);

        for(int i = 0; i < jsonvale.Count; ++i)
        {
            PlayerMap playermap = new PlayerMap(jsonvale[i]["_id"].ToString(),
                jsonvale[i]["title"].ToString(),
                jsonvale[i]["owner"].ToString(),
                jsonvale[i]["bestScore"]["score"].ToString(),
                jsonvale[i]["bestScore"]["user"].ToString());

            m_listPlayerMap.Add(playermap);

            CreateFile(i);
        }

        Debug.Log("오브젝트 수 : " + m_listPlayerMap.Count);
    }

    //맵 저장
    public IEnumerator SaveNetData(string strTitle)
    {
        List<Map> m_listMap = new List<Map>();

        List<GameObject> m_listBlock = GameObject.Find("ToolManager").GetComponent<ToolManager>().GetListBlock();

        for (int i = 0; i < m_listBlock.Count; ++i)
        {
            m_listMap.Add(new Map(m_listBlock[i].GetComponent<BlockController>().GetBlockID()
                , m_listBlock[i].GetComponent<BlockController>().GetIndex()
                , m_listBlock[i].GetComponent<BlockController>().GetItemID()));
        }

        JsonData mapJson = JsonMapper.ToJson(m_listMap);

        string strJson = mapJson.ToString();
        Debug.Log(strJson);

        string strTest = "{\"title\":\"" + strTitle + "\", \"owner\":\"retan\",\"mapData\":" + strJson + "}";

        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("Content-Type", "application/json");

        byte[] body = System.Text.Encoding.UTF8.GetBytes(strTest);
        WWW www = new WWW("http://54.180.153.218:7436/api/maps", body, dic);
        StartCoroutine(SendCheck2(www));
        yield return www;
    }

    IEnumerator SendCheck2(WWW data)
    {
        yield return data; // Wait until the download is done 

        if (data.error == null)
        {
            Debug.Log(data.text);
        }
        else
        {
            Debug.Log("Net Load Failed");
        }
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
                    , int.Parse(mapData["mapData"][i]["itemID"].ToString())); //아이템 ID 넘길것
            }
        }
        else
        {
            Debug.Log("Net Load Failed");
        }
    }
    //최고 점수 변경
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

    //맵리스트 파일 생성
    void CreateFile(int iIndex)
    {
        Debug.Log("파일 생성");
        GameObject goFile = MonoBehaviour.Instantiate(Resources.Load("UI/NetListName")) as GameObject;
        PlayerMap playerMap = NetWorkManager.Instance.GetListPlayerMap()[iIndex];

        goFile.transform.SetParent(GameObject.Find("Contents").transform);
        goFile.name = "Data";

        goFile.GetComponentInChildren<Text>().text = "No \r\n제목 " + playerMap.Title
            + "\r\nOwner " + playerMap.Owner + "\r\nCount\r\nBest Score " + playerMap.BestScore + "\r\nBest User " + playerMap.BestUser;
        goFile.transform.localScale = new Vector3(1, 1, 1);

        goFile.GetComponent<Button>().onClick.AddListener(() => SetID(m_listPlayerMap[iIndex].Id));
        m_listFile.Add(goFile);
    }
    
    //유저 맵 실행
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
