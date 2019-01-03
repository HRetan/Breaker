using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LitJson;

public class Map
{
    public double positionX;
    public double positionY;
    public double positionZ;

    public int blockID;
    public int iIndex;

    public Map(double posX, double posY, double posZ, int ID, int Index)
    {
        positionX = posX;
        positionY = posY;
        positionZ = posZ;
        blockID = ID;
        iIndex = Index;
    }
}

public class SaveNLoad : MonoBehaviour
{
    private static SaveNLoad Instance = null;

    public static SaveNLoad GetInstance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType(typeof(SaveNLoad)) as SaveNLoad;

                if (Instance == null)
                {
                    Debug.LogError("싱글톤 인스턴스 생성 실패");
                }
            }
            return Instance;
        }
    }

    private static int staticStageNum = 0;

    [System.Serializable]
    public class Stage
    {
        public List<bool> clearCheck;
        public List<int> stageIndex;
    }

    public void SaveStage()
    {
        StageManager m_stageManager;

        Stage m_save = new Stage();
        m_stageManager = GameObject.Find("Main Camera").GetComponent<StageManager>();
        Debug.Log(m_stageManager);
        for (int i = 0; i < m_stageManager.GetStageList().Count; ++i)
        {
            m_save.clearCheck.Add(m_stageManager.GetStageList()[i].IsOpen);
            m_save.stageIndex.Add(m_stageManager.GetStageList()[i].iStageIndex);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.streamingAssetsPath + "/StageCheck.dat");

        bf.Serialize(file, m_save);
        file.Close();

        Debug.Log("저장완료");
    }

    public void LoadStage()
    {
        StageManager m_stageManager;
        Stage m_save = new Stage();

        m_stageManager = GameObject.Find("Main Camera").GetComponent<StageManager>();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.streamingAssetsPath + "/StageCheck.dat", FileMode.Open);

        Debug.Log("세이브파일을 찾습니다");
        if (file != null && file.Length > 0)
        {
            m_save = (Stage)bf.Deserialize(file);

            for (int i = 0; i < m_save.stageIndex.Count; ++i)
            {
                m_stageManager.SetAddStageList(m_save.stageIndex[i], m_save.clearCheck[i]);
            }
        }
        else
        {
            Debug.Log("저장된 세이브 파일이 없습니다.");
        }

        file.Close();
    }

    public void SaveMap(string strFileName)
    {
        List<Map> m_listMap = new List<Map>();

        ToolManager m_toolManager;

        m_toolManager = GameObject.Find("ToolManager").GetComponent<ToolManager>();
        Debug.Log(m_toolManager);
        for (int i = 0; i < m_toolManager.GetListBlock().Count; ++i)
        {
            Transform ts = m_toolManager.GetListBlock()[i].GetComponent<Transform>();
            m_listMap.Add(new Map(ts.position.x, ts.position.y, ts.position.z, m_toolManager.GetListBlock()[i].GetComponent<BlockController>().GetBlockID(), m_toolManager.GetListBlock()[i].GetComponent<BlockController>().GetIndex()));
        }

        JsonData mapJson = JsonMapper.ToJson(m_listMap);

        File.WriteAllText(Application.streamingAssetsPath + "/" + strFileName + ".json", mapJson.ToString());

//#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
//        File.WriteAllText(Application.streamingAssetsPath + "/" + strFileName + ".json", mapJson.ToString());
//#elif UNITY_ANDROID
//        File.WriteAllText("jar: file://" + Application.dataPath + "!/assets/" + strFileName + ".json", mapJson.ToString());
//#elif UNITY_IOS
//#endif

        Debug.Log("저장완료");
    }

    public void LoadMap(string strFileName)
    {
        BlockManager m_blockManager;

        m_blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();

        string strPath = Application.streamingAssetsPath + "/" + strFileName + ".json";

        string strJson;

#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)

        strJson = File.ReadAllText(strPath);


#elif UNITY_ANDROID
        WWW reader = new WWW(strPath);
        while(!reader.isDone) {}

        strJson = reader.text;

#elif UNITY_IOS
#endif

        JsonData mapData = JsonMapper.ToObject(strJson);

        GameObject goBlockManager = new GameObject("BlockManager");

        for (int i = 0; i < mapData.Count; ++i)
        {
            Vector3 vecPos = new Vector3(float.Parse(mapData[i]["positionX"].ToString()), float.Parse(mapData[i]["positionY"].ToString())
                , float.Parse(mapData[i]["positionZ"].ToString()));

            m_blockManager.CreateBlock(goBlockManager, vecPos, int.Parse(mapData[i]["blockID"].ToString()));
        }

    }

    public void LoadToolMap(string strFileName)
    {
        ToolManager m_toolManager;

        m_toolManager = GameObject.Find("ToolManager").GetComponent<ToolManager>();

        string strPath = Application.streamingAssetsPath + "/" + strFileName + ".json";

        string strJson;

#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)

        strJson = File.ReadAllText(strPath);


#elif UNITY_ANDROID
        WWW reader = new WWW(strPath);
        while(!reader.isDone) {}

        strJson = reader.text;

#elif UNITY_IOS
#endif

        JsonData mapData = JsonMapper.ToObject(strJson);

        if (m_toolManager.GetListBlock().Count != 0)
        {
            for(int i = 0; i < m_toolManager.GetListBlock().Count; ++i)
            {
                Destroy(m_toolManager.GetListBlock()[i]);
            }
            m_toolManager.GetListBlock().Clear();
        }

        for (int i = 0; i < mapData.Count; ++i)
        {
            GameObject goBlock = GameObject.Find("MapTile(White)" + mapData[i]["iIndex"].ToString()).GetComponent<TileManager>().CreateBlock(int.Parse(mapData[i]["blockID"].ToString()));
            m_toolManager.GetListBlock().Add(goBlock);
        }
    }

    public int GetStaticStageNum()
    {
        return staticStageNum;
    }

    public void SetStaticStageNum(int iStageNum)
    {
        staticStageNum = iStageNum;
    }
}
