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
        public List<bool> clearCheck = new List<bool>();
        public List<int> stageIndex = new List<int>();
    }

    //현재 클리어한 Stage를 저장하기 위한 Func
    //플랫폼 별 저장 경로를 다르게 지정해준다.
    public void SaveStage()
    {

        Stage m_save = new Stage();

        for (int i = 0; i < StageManager.GetInstance.GetStageList().Count; ++i)
        {
            Debug.Log(i);
            Debug.Log(StageManager.GetInstance.GetStageList().Count);
            m_save.clearCheck.Add(StageManager.GetInstance.GetStageList()[i].IsOpen);
            m_save.stageIndex.Add(StageManager.GetInstance.GetStageList()[i].iStageIndex);
        }

        BinaryFormatter bf = new BinaryFormatter();

#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
        FileStream file = File.Create(Application.streamingAssetsPath + "/StageCheck.dat");
#elif UNITY_ANDROID
        FileStream file = File.Create(Application.persistentDataPath + "/StageCheck.dat");
#elif UNITY_IOS
#endif

        bf.Serialize(file, m_save);
        file.Close();

        Debug.Log("저장완료");
    }

    //현재 클리어한 Stage를 불러오기 위한 Func
    //플랫폼 별 불러오는 경로를 다르게 지정해준다.
    public void LoadStage()
    {
        Stage m_save = new Stage();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        string strPath;
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
        //file = File.Open(Application.streamingAssetsPath + "/StageCheck.dat", FileMode.Open);
        strPath = Application.streamingAssetsPath + "/StageCheck.dat";
#elif UNITY_ANDROID
        //file = File.Open(Application.persistentDataPath + "/StageCheck.dat", FileMode.Open);
        strPath = Application.persistentDataPath + "/StageCheck.dat";
#elif UNITY_IOS
#endif
        try
        {
            file = new FileStream(strPath, FileMode.Open);
        }
        catch (IOException)
        {
            Debug.Log("불러오기 실패");
            //for (int i = 1; i < 36; ++i)
            //{
            //    if(i == 1)
            //        m_stageManager.SetAddStageList(i, true);
            //    else
            //        m_stageManager.SetAddStageList(i, false);
            //}
            return;
        }

        m_save = (Stage)bf.Deserialize(file);

        for (int i = 0; i < m_save.stageIndex.Count; ++i)
        {
            StageManager.GetInstance.SetAddStageList(i, m_save.stageIndex[i], m_save.clearCheck[i]);
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

       // File.WriteAllText(Application.streamingAssetsPath + "/" + strFileName + ".json", mapJson.ToString());

#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
        File.WriteAllText(Application.streamingAssetsPath + "/" + strFileName + ".json", mapJson.ToString());
#elif UNITY_ANDROID
        File.WriteAllText(Application.persistentDataPath + "/" + strFileName + ".json", mapJson.ToString());
#elif UNITY_IOS
#endif

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

    //1.Tool에서 쓸 Func이기 때문에 안드로이드 자체에서 세이브한 경로를 받아와 호출한다.
    //  현재 안드로이드에서만 사용할 수 있는 맵
    //2.네트워크를 통해 세이브 로드하는 Func을 새로 만든다.
    public void LoadToolMap(string strFileName)
    {
        ToolManager m_toolManager;

        m_toolManager = GameObject.Find("ToolManager").GetComponent<ToolManager>();

        string strPath = Application.persistentDataPath + "/" + strFileName + ".json";

        string strJson;
        //Debug.Log(Application.persistentDataPath);
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)

        strJson = File.ReadAllText(strPath);


#elif UNITY_ANDROID
        strJson = File.ReadAllText(strPath);

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
