using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    private GameObject m_goTool;
    private GameObject m_goBlock = null;
    private int m_iBlockNum = 0;
    private int m_iIndex = 0;

	// Use this for initialization
	void Start () {
        m_goTool = GameObject.Find("ToolManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject CreateBlock(int BlockNum)
    {
        m_iBlockNum = BlockNum;

        if (m_goBlock == null)
            CreateObjectBlock(m_iBlockNum);
        else
            ChangeObjectBlock(m_iBlockNum);

        return m_goBlock;
    }

    void CreateObjectBlock(int BlockNum)
    {
        if (m_goBlock != null)
            return;
        
        switch (m_iBlockNum)
        {
            case 0:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Gray)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Gray)";
                break;
            case 1:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Blue)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Blue)";
                break;
            case 2:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Brown)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Brown)";
                break;
            case 3:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Purple)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Purple)";
                break;
            case 4:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Red)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Red)";
                break;
            case 5:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Orange)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Orange)";
                break;
            case 6:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Green)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Green)";
                break;
            case 7:
                break;
        }
        //충돌처리 꺼주고 저장할 때 다시 켜준다
        if (m_goBlock != null)
        {
            m_goBlock.transform.position = transform.position;
            m_goBlock.GetComponent<BlockController>().SetBlockID(m_iBlockNum);
            m_goBlock.GetComponent<BlockController>().SetIndex(m_iIndex);
            m_goBlock.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void ChangeObjectBlock(int BlockNum)
    {
        Destroy(m_goBlock);
        m_goTool.GetComponent<ToolManager>().RemoveList(m_goBlock);
        switch (m_iBlockNum)
        {
            case 0:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Gray)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Gray)";
                break;
            case 1:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Blue)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Blue)";
                break;
            case 2:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Brown)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Brown)";
                break;
            case 3:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Purple)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Purple)";
                break;
            case 4:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Red)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Red)";
                break;
            case 5:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Orange)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Orange)";
                break;
            case 6:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Green)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Green)";
                break;
            case 7:
                m_goBlock = null;
                break;
        }

        if (m_goBlock != null)
        {
            m_goBlock.transform.position = transform.position;
            m_goBlock.GetComponent<BlockController>().SetBlockID(m_iBlockNum);
            m_goBlock.GetComponent<BlockController>().SetIndex(m_iIndex);
            m_goBlock.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void SetIndex(int iIndex)
    {
        m_iIndex = iIndex;
    }
}
