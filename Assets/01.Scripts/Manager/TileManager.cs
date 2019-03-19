using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public static int w_iItemNum = 0;

    private GameObject m_goTool;
    private GameObject m_goBlock = null;
    private GameObject m_goItem = null;
    
    private int m_iBlockNum = 0;
    private int m_iItemNum = 0;
    private int m_iIndex = 0;

	// Use this for initialization
	void Start () {
        m_goTool = GameObject.Find("ToolManager");
        w_iItemNum = 8;
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

        CheckItemID(w_iItemNum);
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

        CheckItemID(w_iItemNum);
    }

    void CheckBlockId()
    {
        switch (m_iBlockNum)
        {
            case 0:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Red)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Red)";
                break;
            case 1:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Orange)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Orange)";
                break;
            case 2:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Yellow)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Yellow)";
                break;
            case 3:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Green)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Green)";
                break;
            case 4:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Blue)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Blue)";
                break;
            case 5:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Indigo)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Indigo)";
                break;
            case 6:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Purple)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Purple)";
                break;
            case 7:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Pink)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Pink)";
                break;
            case 8:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(SkyBlue)")) as GameObject;
                m_goBlock.name = "Breaker_Block(SkyBlue)";
                break;
            case 9:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Gray)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Gray)";
                break;
            case 10:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Black)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Black)";
                break;
            case 11:
                m_goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Random)")) as GameObject;
                m_goBlock.name = "Breaker_Block(Random)";
                break;
            case 12:
                m_goBlock = null;
                break;
        }
    }

    void CheckItemID(int itemID)
    {
        switch (itemID)
        {
            case 0:
                m_goItem = MonoBehaviour.Instantiate(Resources.Load("Item/ItemSprite/ItemImage(Orange)")) as GameObject;
                m_goItem.name = "ItemImage(Orange)";
                m_goItem.transform.parent = m_goBlock.transform;
                break;
            case 1:
                m_goItem = MonoBehaviour.Instantiate(Resources.Load("Item/ItemSprite/ItemImage(Blue)")) as GameObject;
                m_goItem.name = "ItemImage(Blue)";
                m_goItem.transform.parent = m_goBlock.transform;
                break;
            case 2:
                m_goItem = MonoBehaviour.Instantiate(Resources.Load("Item/ItemSprite/ItemImage(Red)")) as GameObject;
                m_goItem.name = "ItemImage(Red)";
                m_goItem.transform.parent = m_goBlock.transform;
                break;
            case 3:
                m_goItem = MonoBehaviour.Instantiate(Resources.Load("Item/ItemSprite/ItemImage(Yellow)")) as GameObject;
                m_goItem.name = "ItemImage(Yellow)";
                m_goItem.transform.parent = m_goBlock.transform;
                break;
            case 4:
                m_goItem = MonoBehaviour.Instantiate(Resources.Load("Item/ItemSprite/ItemImage(SkyBlue)")) as GameObject;
                m_goItem.name = "ItemImage(SkyBlue)";
                m_goItem.transform.parent = m_goBlock.transform;
                break;
            case 5:
                m_goItem = MonoBehaviour.Instantiate(Resources.Load("Item/ItemSprite/ItemImage(Green)")) as GameObject;
                m_goItem.name = "ItemImage(Green)";
                m_goItem.transform.parent = m_goBlock.transform;
                break;
            case 6:
                m_goItem = MonoBehaviour.Instantiate(Resources.Load("Item/ItemSprite/ItemImage(Pink)")) as GameObject;
                m_goItem.name = "ItemImage(Pink)";
                m_goItem.transform.parent = m_goBlock.transform;
                break;
            case 7:
                m_goItem = MonoBehaviour.Instantiate(Resources.Load("Item/ItemSprite/ItemImage(Purple)")) as GameObject;
                m_goItem.name = "ItemImage(Purple)";
                m_goItem.transform.parent = m_goBlock.transform;
                break;
            case 8:
                m_goItem = MonoBehaviour.Instantiate(Resources.Load("Item/ItemSprite/ItemImage(Random)")) as GameObject;
                m_goItem.name = "ItemImage(Random)";
                m_goItem.transform.parent = m_goBlock.transform;
                break;
        }

        if (m_goItem != null)
        {
            m_goItem.transform.position = m_goBlock.transform.position;
            m_goItem.transform.localScale = new Vector3(0.3f, 0.4f);
        }
    }

    public void SetItemID(int iItemNum)
    {
        if (m_goBlock == null)
            return;
        w_iItemNum = iItemNum;

        Destroy(m_goItem);
        m_goTool.GetComponent<ToolManager>().RemoveList(m_goItem);

        CheckItemID(iItemNum);
        m_goBlock.GetComponent<BlockController>().SetItemID(iItemNum);
    }

    public void SetIndex(int iIndex)
    {
        m_iIndex = iIndex;
    }

    public int GetIndex()
    {
        return m_iIndex;   
    }

    public void SetItemIndex(int iIndex)
    {
        m_iItemNum = iIndex;
    }
}
