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
            CreateObjectBlock();
        else
            ChangeObjectBlock();

        return m_goBlock;
    }

    void CreateObjectBlock()
    {
        if (m_goBlock != null)
            return;

        CheckBlockId();

        //충돌처리 꺼주고 저장할 때 다시 켜준다
        if (m_goBlock != null)
        {
            m_goBlock.transform.position = transform.position;
            m_goBlock.GetComponent<BlockController>().SetBlockID(m_iBlockNum);
            m_goBlock.GetComponent<BlockController>().SetIndex(m_iIndex);
            m_goBlock.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void ChangeObjectBlock()
    {
        Destroy(m_goBlock);
        m_goTool.GetComponent<ToolManager>().RemoveList(m_goBlock);

        CheckBlockId();

        if (m_goBlock != null)
        {
            m_goBlock.transform.position = transform.position;
            m_goBlock.GetComponent<BlockController>().SetBlockID(m_iBlockNum);
            m_goBlock.GetComponent<BlockController>().SetIndex(m_iIndex);
            m_goBlock.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void CheckBlockId()
    {
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
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Pink)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Pink)";
                break;
            case 8:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(SkyBlue)")) as GameObject;
                m_goBlock.name = "Breaker_Block(SkyBlue)";
                break;
            case 9:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Yellow)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Yellow)";
                break;
            case 10:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Black)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Black)";
                break;
            case 11:
                m_goBlock = null;
                break;
         
        }
    }

    public void SetIndex(int iIndex)
    {
        m_iIndex = iIndex;
    }
}
