using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour {

    private float fX;
    private float fY;
    [SerializeField]
    private int m_iBlockNum = 0;

    private GameObject goTileManager;
    private SaveNLoad m_SaveNLoad;

    [SerializeField]
    private List<GameObject> m_listBlock;

    // Use this for initialization
    void Start()
    {
        goTileManager = new GameObject("TileManager");
        m_SaveNLoad = GetComponent<SaveNLoad>();
        for (int i = 0; i < 29; ++i)
        {
            for (int j = 0; j < 11; ++j)
            {
                fX = (0.485f / 2) + (j * 0.485f);
                fY = (0.245f / 2) + (i * 0.245f);

                GameObject tile = MonoBehaviour.Instantiate(Resources.Load("Tile/MapTile(White)")) as GameObject;
                tile.name = "MapTile(White)" + (i * 11 +j);
                tile.transform.parent = goTileManager.transform;
                tile.transform.position = new Vector2(fX - 2.67f, fY - 2.5f);
            }
        }
  
    }
	
	// Update is called once per frame
	void Update () {

        //임시 ui만들기전 Test
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            m_iBlockNum = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_iBlockNum = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_iBlockNum = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_iBlockNum = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            m_iBlockNum = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            m_iBlockNum = 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            m_iBlockNum = 6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            m_iBlockNum = 99;

        //클릭 시 타일 위치에 raycast를 통해 오브젝트 생성
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(worldPos.x, worldPos.y);
            Collider2D mouseCol = Physics2D.OverlapPoint(mousePos);
            Debug.Log(mouseCol.gameObject);
            if(mouseCol.gameObject.tag == "Tile")
            {
                GameObject goBlock = mouseCol.gameObject.GetComponent<TileManager>().CreateBlock(m_iBlockNum);
                if (goBlock != null)
                    m_listBlock.Add(goBlock);
                else
                    m_listBlock.Remove(goBlock);
            }
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            m_SaveNLoad.SaveMap();
        }
    }

    public int GetBlockNum()
    {
        return m_iBlockNum;
    }

    public void RemoveList(GameObject goBlock)
    {
        m_listBlock.Remove(goBlock);
    }

    public List<GameObject> GetListBlock()
    {
        return m_listBlock;
    }
}
