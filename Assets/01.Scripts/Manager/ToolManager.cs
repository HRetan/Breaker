﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolManager : MonoBehaviour {

    private static ToolManager Instance = null;

    public static ToolManager GetInstance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType(typeof(ToolManager)) as ToolManager;
                if (Instance == null)
                {
                    Debug.LogError("UIController     생성 실패");
                }
            }
            return Instance;
        }
    }

    enum SPEED_LEVEL
    {
        LEVEL_ONE,
        LEVEL_TWO,
        LEVEL_THREE,
        LEVEL_NONE
    }

    enum CATEGORY
    {
        BRICK,
        ITEM,
        NOCATEGORY
    }

    private float fX;
    private float fY;
    [SerializeField]
    private int m_iBlockNum = 0;
    private int m_iItemNum = 0;

    private GameObject goTileManager;
    private GameObject m_goBlockScroll;
    private GameObject m_goItemScroll;
    private TextMeshProUGUI m_textCount; 

    [SerializeField]
    private List<GameObject> m_listBlock;
    private List<GameObject> m_listItem;

    private List<GameObject> m_listTile = new List<GameObject>();

    [SerializeField]
    private CATEGORY m_eCategory = CATEGORY.BRICK;

    [SerializeField]
    private SPEED_LEVEL m_eSpeed = SPEED_LEVEL.LEVEL_NONE;

    // Use this for initialization
    void Start()
    {
        goTileManager = new GameObject("TileManager");
        m_textCount = GameObject.Find("CountText").GetComponent<TextMeshProUGUI>();

        for (int i = 0; i < 26; ++i)
        {
            for (int j = 0; j < 11; ++j)
            {
                fX = (0.485f / 2) + (j * 0.485f);
                fY = (0.245f / 2) + (i * 0.245f);

                GameObject tile = MonoBehaviour.Instantiate(Resources.Load("Tile/MapTile(White)")) as GameObject;
                tile.name = "MapTile(White)" + (i * 11 + j);
                tile.transform.parent = goTileManager.transform;
                tile.transform.position = new Vector2(fX - 2.67f, fY - 3.0f);
                tile.GetComponent<TileManager>().SetIndex(i * 11 + j);

                m_listTile.Add(tile);
            }
        }

        m_goBlockScroll = GameObject.Find("BlockScroll");
        m_goItemScroll = GameObject.Find("ItemScroll");
        m_goItemScroll.SetActive(false);

        if (NetWorkManager.Instance.GetNet())
        {
            StartCoroutine(NetWorkManager.Instance.LoadMapTool());
        }

    }

    // Update is called once per frame
    void Update () {
        //Index Pos 저장용
        if (Input.GetKeyDown(KeyCode.F9))
            SaveNLoad.GetInstance.SavePos();

        //클릭 시 타일 위치에 raycast를 통해 오브젝트 생성
        if (!UIController.GetInstance.GetUI())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos = new Vector2(worldPos.x, worldPos.y);
                Collider2D mouseCol = Physics2D.OverlapPoint(mousePos);

                if (mouseCol == null)
                    return;

                if (mouseCol.gameObject.tag == "Tile")
                {
                    if (m_eCategory == CATEGORY.BRICK)
                    {
                        GameObject goBlock = mouseCol.gameObject.GetComponent<TileManager>().CreateBlock(m_iBlockNum);
                        if (goBlock != null)
                            m_listBlock.Add(goBlock);
                        else
                            m_listBlock.Remove(goBlock);
                        m_textCount.text = m_listBlock.Count.ToString();
                    }
                    else if (m_eCategory == CATEGORY.ITEM)
                    {
                        mouseCol.gameObject.GetComponent<TileManager>().SetItemID(m_iItemNum);
                    }
                }
            }
        }
        //세이브 파일 생성
        if (Input.GetKeyDown(KeyCode.F6))
        {
            SaveNLoad.GetInstance.SavePos();
        }
    }

    public int GetBlockNum()
    {
        return m_iBlockNum;
    }

    public void SetBlockNum(int iBlockNum)
    {
        m_iBlockNum = iBlockNum;
    }

    public int GetItemNum()
    {
        return m_iItemNum;
    }

    public void SetItemNum(int iItemNum)
    {
        m_iItemNum = iItemNum;
    }

    public void RemoveList(GameObject goBlock)
    {
        m_listBlock.Remove(goBlock);
    }

    public void AddItemList(GameObject goItem)
    {
        m_listItem.Add(goItem);
    }

    public void RemoveItemList(GameObject goBlock)
    {
        m_listItem.Remove(goBlock);
    }

    public List<GameObject> GetListBlock()
    {
        return m_listBlock;
    }

    public List<GameObject> GetListTile()
    {
        return m_listTile;
    }

    public void SetBrick()
    {
        m_eCategory = CATEGORY.BRICK;
        m_goBlockScroll.SetActive(true);
        m_goItemScroll.SetActive(false);
    }

    public void SetItem()
    {
        m_eCategory = CATEGORY.ITEM;
        m_goBlockScroll.SetActive(false);
        m_goItemScroll.SetActive(true);
    }

    public void SetLevel(int iLevel)
    {
        switch (iLevel)
        {
            case 0:
                m_eSpeed = SPEED_LEVEL.LEVEL_ONE;
                break;
            case 1:
                m_eSpeed = SPEED_LEVEL.LEVEL_TWO;
                break;
            case 2:
                m_eSpeed = SPEED_LEVEL.LEVEL_THREE;
                break;
        }
    }

    public int GetLevel()
    {
        switch (m_eSpeed)
        {
            case SPEED_LEVEL.LEVEL_ONE:
                return 1;
            case SPEED_LEVEL.LEVEL_TWO:
                return 2;
            case SPEED_LEVEL.LEVEL_THREE:
                return 3;
        }

        return 0;
    }
}
