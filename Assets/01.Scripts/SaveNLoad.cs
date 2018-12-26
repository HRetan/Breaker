using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveNLoad : MonoBehaviour
{
    private static int staticStageNum = 0;

    [System.Serializable]
    public class Data
    {
        public List<float> positionX;
        public List<float> positionY;
        public List<float> positionZ;

        public List<int> blockID;
    }

    [System.Serializable]
    public class Stage
    {
        public List<bool> clearCheck;
        public List<int> stageIndex;
    }

    public Stage m_save = new Stage();
    public Data m_data = new Data();

    public void SaveMap()
    {
        //m_data = new Data();
        ToolManager m_toolManager;

        m_toolManager = GameObject.Find("ToolManager").GetComponent<ToolManager>();
        Debug.Log(m_toolManager);
        for (int i = 0; i < m_toolManager.GetListBlock().Count; ++i)
        {
            Debug.Log(m_data);
            Transform ts = m_toolManager.GetListBlock()[i].GetComponent<Transform>();
            m_data.positionX.Add(ts.position.x);
            m_data.positionY.Add(ts.position.y);
            m_data.positionZ.Add(ts.position.z);
            m_data.blockID.Add(m_toolManager.GetListBlock()[i].GetComponent<BlockController>().GetBlockID());
        }

        Debug.Log(m_data.positionX[0]);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/Resources/Save/SaveFile1.txt");

        bf.Serialize(file, m_data);
        file.Close();

        Debug.Log("저장완료");
    }

    public void LoadMap()
    {
        BlockManager m_blockManager;
        
        m_blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/Resources/Save/Stage" + staticStageNum.ToString() + ".txt", FileMode.Open);
        Debug.Log("세이브파일을 찾습니다");
        if (file != null && file.Length > 0)
        {
            m_data = (Data)bf.Deserialize(file);
            GameObject goBlockManager = new GameObject("BlockManager");

            for (int i = 0; i < m_data.blockID.Count; ++i)
            {
                Vector3 vecPos = new Vector3(m_data.positionX[i], m_data.positionY[i], m_data.positionZ[i]);

                m_blockManager.CreateBlock(goBlockManager, vecPos, m_data.blockID[i]);
            }
        }
        else
        {
            Debug.Log("저장된 세이브 파일이 없습니다.");
        }

        file.Close();
    }

    public void BuildLoad()
    {
        BlockManager m_blockManager;

        m_blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();
        TextAsset textAsset = Resources.Load("Save/Stage" + staticStageNum.ToString()) as TextAsset;
        MemoryStream stream = new MemoryStream(textAsset.bytes);
        BinaryFormatter bf = new BinaryFormatter();

        if(stream != null && stream.Length > 0)
        {
            m_data = (Data)bf.Deserialize(stream);
            GameObject goBlockManager = new GameObject("BlockManager");

            for (int i = 0; i < m_data.blockID.Count; ++i)
            {
                Vector3 vecPos = new Vector3(m_data.positionX[i], m_data.positionY[i], m_data.positionZ[i]);

                m_blockManager.CreateBlock(goBlockManager, vecPos, m_data.blockID[i]);
            }
        }
        else
        {
            Debug.Log("저장된 세이브 파일이 없습니다.");
        }

        stream.Close();
    }

    public void SaveStage()
    {
        StageManager m_stageManager;

        m_stageManager = GameObject.Find("Main Camera").GetComponent<StageManager>();
        Debug.Log(m_stageManager);
        for (int i = 0; i < m_stageManager.GetStageList().Count; ++i)
        {
            m_save.clearCheck.Add(m_stageManager.GetStageList()[i].IsOpen);
            m_save.stageIndex.Add(m_stageManager.GetStageList()[i].iStageIndex);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/Resources/Save/StageCheck.dat");

        bf.Serialize(file, m_save);
        file.Close();

        Debug.Log("저장완료");
    }

    public void LoadStage()
    {
        StageManager m_stageManager;

        m_stageManager = GameObject.Find("Main Camera").GetComponent<StageManager>();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/Resources/Save/StageCheck.dat", FileMode.Open);
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

    public static int GetStaticStageNum()
    {
        return staticStageNum;
    }

    public void SetStaticStageNum(int iStageNum)
    {
        staticStageNum = iStageNum;
    }
}
