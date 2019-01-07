using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockController : MonoBehaviour {
  
    private float fSpeed = 5f;

    private SpriteRenderer sprite;
    private BoxCollider2D boxColl;
    private BlockManager blockManager;
    private Transform trans;
    private Color color;
    private bool isBreak = false;
    private float fTime;

    [SerializeField]
    private int m_iBlockID = 0;
    [SerializeField]
    private int m_iIndex = 0;

    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        boxColl = GetComponent<BoxCollider2D>();
        trans = GetComponent<Transform>();
        color = new Color();
        color = sprite.color;
        blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();
    }

    // Update is called once per frame
    void Update () {
        if (UIController.GetInstance.GetUI())
            return;

        sprite.color = color;
        if (isBreak)
        {
            fTime += Time.deltaTime * fSpeed;

            color.a = Mathf.Lerp(1, 0, fTime);
            
            if (fTime >= 1f)
            {
                blockManager.GetListBlock().Remove(this.gameObject);
                Destroy(gameObject);
            }

            Debug.Log(fTime);
            Debug.Log(color.a);
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Ball")
        {
            isBreak = true;
            boxColl.enabled = false;
            Debug.Log(Time.deltaTime);
            
            if(m_iBlockID == 2)
            {
                GameObject item = MonoBehaviour.Instantiate(Resources.Load("Item/Item(Brown)")) as GameObject;
                item.name = "Item(Brown)";
                item.transform.position = trans.position;
                item.GetComponent<ItemManager>().SetState(0);
            }
            else if (m_iBlockID == 3)
            {
                GameObject item = MonoBehaviour.Instantiate(Resources.Load("Item/Item(Purple)")) as GameObject;
                item.name = "Item(Purple)";
                item.transform.position = trans.position;
                item.GetComponent<ItemManager>().SetState(1);
            }
            else if (m_iBlockID == 5)
            {
                GameObject item = MonoBehaviour.Instantiate(Resources.Load("Item/Item(Yellow)")) as GameObject;
                item.name = "Item(Yellow)";
                item.transform.position = trans.position;
                item.GetComponent<ItemManager>().SetState(2);
            }
            else if (m_iBlockID == 6)
            {
                GameObject item = MonoBehaviour.Instantiate(Resources.Load("Item/Item(Green)")) as GameObject;
                item.name = "Item(Green)";
                item.transform.position = trans.position;
                item.GetComponent<ItemManager>().SetState(3);
            }
            else if (m_iBlockID == 4)
            {
                GameObject item = MonoBehaviour.Instantiate(Resources.Load("Item/Item(Red)")) as GameObject;
                item.name = "Item(Red)";
                item.transform.position = trans.position;
                item.GetComponent<ItemManager>().SetState(4);
            }
        }
    }

    void SetBlockID()
    {
        if (name.ToString() == "Breaker_Block(Gray)")
            m_iBlockID = 0;
        else if (name.ToString() == "Breaker_Block(Blue)")
            m_iBlockID = 1;
        else if (name.ToString() == "Breaker_Block(Brown)")
            m_iBlockID = 2;
        else if (name.ToString() == "Breaker_Block(Purple)")
            m_iBlockID = 3;
        else if (name.ToString() == "Breaker_Block(Red)")
            m_iBlockID = 4;
        else if (name.ToString() == "Breaker_Block(Orange)")
            m_iBlockID = 5;
        else if (name.ToString() == "Breaker_Block(Green)")
            m_iBlockID = 6;
    }

    public int GetBlockID()
    {
        return m_iBlockID;
    }

    public void SetBlockID(int iBlockID)
    {
        m_iBlockID = iBlockID;  
    }

    public int GetIndex()
    {
        return m_iIndex;
    }

    public void SetIndex(int iIndex)
    {
        m_iIndex = iIndex;
    }
}
