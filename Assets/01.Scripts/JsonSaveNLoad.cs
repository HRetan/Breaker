using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

//public class Map
//{
//    public double positionX;
//    public double positionY;
//    public double positionZ;
           
//    public int blockID;

//    public Map(double posX, double posY, double posZ, int ID)
//    {
//        positionX = posX;
//        positionY = posY;
//        positionZ = posZ;
//        blockID = ID;
//    }
//}

public class JsonSaveNLoad : MonoBehaviour {


    //public void SaveMap(string strFileName)
    //{
    //    List<Map> m_listMap = new List<Map>();

    //    ToolManager m_toolManager;

    //    m_toolManager = GameObject.Find("ToolManager").GetComponent<ToolManager>();
    //    Debug.Log(m_toolManager);
    //    for (int i = 0; i < m_toolManager.GetListBlock().Count; ++i)
    //    {
    //        Transform ts = m_toolManager.GetListBlock()[i].GetComponent<Transform>();
    //        m_listMap.Add(new Map(ts.position.x, ts.position.y, ts.position.z, m_toolManager.GetListBlock()[i].GetComponent<BlockController>().GetBlockID()));
    //    }

    //    JsonData mapJson = JsonMapper.ToJson(m_listMap);

    //    File.WriteAllText(Application.streamingAssetsPath + "/" + strFileName + ".json", mapJson.ToString());

    //    Debug.Log("저장완료");
    //}

    //public void LoadMap(string strFileName)
    //{
    //    BlockManager m_blockManager;

    //    m_blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();
    //    //TextAsset textAsset = Resources.Load("Json/" + strFileName, typeof(TextAsset)) as TextAsset;

    //    string strJson = File.ReadAllText(Application.streamingAssetsPath + "/" + strFileName + ".json");
    //        //textAsset.text;

    //    JsonData mapData = JsonMapper.ToObject(strJson);

    //    GameObject goBlockManager = new GameObject("BlockManager");
        
    //    for (int i = 0; i < mapData.Count; ++i)
    //    {
    //        Vector3 vecPos = new Vector3(float.Parse(mapData[i]["positionX"].ToString()), float.Parse(mapData[i]["positionY"].ToString())
    //            , float.Parse(mapData[i]["positionZ"].ToString()));

    //        m_blockManager.CreateBlock(goBlockManager, vecPos, int.Parse(mapData[i]["blockID"].ToString()));
    //    }

    //}
}
