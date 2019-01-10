using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LitJson;

public class Map
{
    public int blockID;
    public int iIndex;

    public Map(int ID, int Index)
    {
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
    private static string w_strFileName = "";
    private static List<Vector2> m_listPos = new List<Vector2>();

    [System.Serializable]
    public class Stage
    {
        public List<bool> clearCheck = new List<bool>();
        public List<int> stageIndex = new List<int>();
    }

    [System.Serializable]
    public class BlockPos
    {
        public List<float> posX = new List<float>();
        public List<float> posY = new List<float>();
    }

    //현재 클리어한 Stage를 저장하기 위한 Func
    //플랫폼 별 저장 경로를 다르게 지정해준다.
    public void SaveStage()
    {

        Stage m_save = new Stage();

        for (int i = 0; i < StageManager.GetInstance.GetStageList().Count; ++i)
        {
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

        for (int i = 0; i < m_toolManager.GetListBlock().Count; ++i)
        {
            Transform ts = m_toolManager.GetListBlock()[i].GetComponent<Transform>();
            m_listMap.Add(new Map(m_toolManager.GetListBlock()[i].GetComponent<BlockController>().GetBlockID(), m_toolManager.GetListBlock()[i].GetComponent<BlockController>().GetIndex()));
        }

        JsonData mapJson = JsonMapper.ToJson(m_listMap);

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
            m_blockManager.CreateBlock(goBlockManager, m_listPos[int.Parse(mapData[i]["iIndex"].ToString())], int.Parse(mapData[i]["blockID"].ToString()));
        }

    }

    public void LoadUserMap(string strFileName)
    {
        BlockManager m_blockManager;

        m_blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();

        string strPath = Application.persistentDataPath + "/" + strFileName + ".json";

        string strJson;

#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)

        strJson = File.ReadAllText(strPath);


#elif UNITY_ANDROID

         strJson = File.ReadAllText(strPath);

#elif UNITY_IOS
#endif

        JsonData mapData = JsonMapper.ToObject(strJson);

        GameObject goBlockManager = new GameObject("BlockManager");

        for (int i = 0; i < mapData.Count; ++i)
        {
            m_blockManager.CreateBlock(goBlockManager, m_listPos[int.Parse(mapData[i]["iIndex"].ToString())], int.Parse(mapData[i]["blockID"].ToString()));
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

    public void SavePos()
    {
        BlockPos save = new BlockPos();
        ToolManager toolManager = GameObject.Find("ToolManager").GetComponent<ToolManager>();

        for (int i = 0; i < toolManager.GetListTile().Count; ++i)
        {
            save.posX.Add(toolManager.GetListTile()[i].transform.position.x);
            save.posY.Add(toolManager.GetListTile()[i].transform.position.y);
        }

        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Application.streamingAssetsPath + "/IndexPos.dat");

        bf.Serialize(file, save);
        file.Close();

        Debug.Log("저장완료");
    }

    public void LoadPos()
    {
        BlockPos save = new BlockPos();

        BinaryFormatter bf = new BinaryFormatter();

        string strPath = Application.streamingAssetsPath + "/IndexPos.dat";

#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
        FileStream file = File.Open(strPath, FileMode.Open);

        if (file != null && file.Length > 0)
        {
            save = (BlockPos)bf.Deserialize(file);

            for (int i = 0; i < save.posY.Count; ++i)
            {
                m_listPos.Add(new Vector2(save.posX[i], save.posY[i]));
            }
        }
        else

        file.Close();
#elif UNITY_ANDROID
        WWW reader = new WWW(strPath);
        while (!reader.isDone) { }
        MemoryStream ms = new MemoryStream(reader.bytes);

        save = (BlockPos)bf.Deserialize(ms);
        
        for (int i = 0; i < save.posY.Count; ++i)
        {
            m_listPos.Add(new Vector2(save.posX[i], save.posY[i]));
        }

        ms.Close();
#elif UNITY_IOS
#endif
    }

    public void FindFileStage(List<string> listFile)
    {
        string dirPath;

#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)

        dirPath = Application.streamingAssetsPath;

#elif UNITY_ANDROID
        dirPath = Application.persistentDataPath;

#elif UNITY_IOS
#endif

        if (Directory.Exists(dirPath))
        {
            DirectoryInfo di = new DirectoryInfo(dirPath);

            foreach(var file in di.GetFiles())
            {
                if (file.Extension == ".json")
                    listFile.Add(file.Name.Substring(0, file.Name.Length - 5));
            }
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

    public string GetStaticFileName()
    {
        return w_strFileName;
    }

    public void SetStaticFileName(string strFileName)
    {
        w_strFileName = strFileName;
    }
}
