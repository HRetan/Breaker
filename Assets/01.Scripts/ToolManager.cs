using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour {

    private float fX;
    private float fY;
    [SerializeField]
    private int m_iBlockNum = 0;

    private GameObject goTileManager;

    [SerializeField]
    private List<GameObject> m_listBlock;

    private List<GameObject> m_listTile = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        goTileManager = new GameObject("TileManager");

        for (int i = 0; i < 25; ++i)
        {
            for (int j = 0; j < 11; ++j)
            {
                fX = (0.485f / 2) + (j * 0.485f);
                fY = (0.245f / 2) + (i * 0.245f);

                GameObject tile = MonoBehaviour.Instantiate(Resources.Load("Tile/MapTile(White)")) as GameObject;
                tile.name = "MapTile(White)" + (i * 11 +j);
                tile.transform.parent = goTileManager.transform;
                tile.transform.position = new Vector2(fX - 2.67f, fY - 2.5f);
                tile.GetComponent<TileManager>().SetIndex(i * 11 + j);

                m_listTile.Add(tile);
            }
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
                    GameObject goBlock = mouseCol.gameObject.GetComponent<TileManager>().CreateBlock(m_iBlockNum);
                    if (goBlock != null)
                        m_listBlock.Add(goBlock);
                    else
                        m_listBlock.Remove(goBlock);
                }
            }
        }
        //세이브 파일 생성
        if (Input.GetKeyDown(KeyCode.F6))
        {
            SaveNLoad.GetInstance.SaveMap("Stage" + SaveNLoad.GetInstance.GetStaticStageNum().ToString());
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

    public void RemoveList(GameObject goBlock)
    {
        m_listBlock.Remove(goBlock);
    }

    public List<GameObject> GetListBlock()
    {
        return m_listBlock;
    }

    public List<GameObject> GetListTile()
    {
        return m_listTile;
    }
}
