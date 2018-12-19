using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveNLoad : MonoBehaviour {

    [System.Serializable]
    public class Data
    {
        public List<float> positionX;
        public List<float> positionY;
        public List<float> positionZ;

        public List<int> blockID;
    }

    public Data m_data;
    [SerializeField]
    private ToolManager m_toolManager;
    private BlockManager m_blockManager;

    public void SaveMap()
    {
        m_toolManager = GameObject.Find("ToolManager").GetComponent<ToolManager>();
        Debug.Log(m_toolManager);
        for(int i = 0; i < m_toolManager.GetListBlock().Count; ++i)
        {
            Transform ts = m_toolManager.GetListBlock()[i].GetComponent<Transform>();
            m_data.positionX.Add(ts.position.x);
            m_data.positionY.Add(ts.position.y);
            m_data.positionZ.Add(ts.position.z);
            m_data.blockID.Add(m_toolManager.GetListBlock()[i].GetComponent<BlockController>().GetBlockID());
        }

        Debug.Log(m_data.positionX[0]);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/SaveFile.dat");

        bf.Serialize(file, m_data);
        file.Close();

        Debug.Log("저장완료");
    }

    public void LoadMap()
    {
        m_blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/SaveFile.dat", FileMode.Open);
        Debug.Log("세이브파일을 찾습니다");
        if (file != null && file.Length > 0)
        {
            m_data = (Data)bf.Deserialize(file);

            for (int i = 0; i < m_data.blockID.Count; ++i)
            {
                Vector3 vecPos = new Vector3(m_data.positionX[i], m_data.positionY[i], m_data.positionZ[i]);

                m_blockManager.CreateBlock(vecPos, m_data.blockID[i]);
            }
        }
        else
        {
            Debug.Log("저장된 세이브 파일이 없습니다.");
        }

        file.Close();
    }
}
