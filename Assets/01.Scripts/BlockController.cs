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
    private float fTime;

    private int m_iBlockID = 0;
    private int m_iIndex = 0;
    private int m_iBlockLife = 1;

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

        if (m_iBlockLife <= 0)
        {
            if(boxColl.enabled)
            boxColl.enabled = false;

            fTime += Time.deltaTime * fSpeed;

            color.a = Mathf.Lerp(1, 0, fTime);
            
            if (fTime >= 1f)
            {
                blockManager.GetListBlock().Remove(this.gameObject);
                blockManager.SetBlockCount();
                Destroy(this.gameObject);
            }
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Ball" || coll.gameObject.tag == "Bullet")
        {
            BlockState();

            if (coll.gameObject.tag == "Bullet")
                Destroy(coll.gameObject);
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
        else if (name.ToString() == "Breaker_Block(Pink)")
            m_iBlockID = 7;
        else if (name.ToString() == "Breaker_Block(SkyBlue)")
            m_iBlockID = 8;
        else if (name.ToString() == "Breaker_Block(Yellow)")
            m_iBlockID = 9;
        else if (name.ToString() == "Breaker_Block(Black)")
            m_iBlockID = 10;
    }

    void BlockState()
    {
        if (m_iBlockID != 10)
        {
            Damage();
            GameObject Effect = MonoBehaviour.Instantiate(Resources.Load("Effect/Broken_Effect")) as GameObject;
            Effect.name = "Broken_Effect";
            Effect.transform.position = trans.position;
        }

        switch (m_iBlockID)
        {
            case 0:
                break;
            case 1:
                CreateItem("Item(Blue)", 5);
                break;
            case 2:
                CreateItem("Item(Brown)", 0);
                break;
            case 3:
                CreateItem("Item(Purple)", 1);
                break;
            case 4:
                CreateItem("Item(Red)", 4);
                break;
            case 5:
                CreateItem("Item(Yellow)", 2);
                break;
            case 6:
                CreateItem("Item(Green)", 3);
                break;
            case 7:
                CreateItem("Item(Pink)", 6);
                break;
            case 8:
                DamageBlock();
                break;
            case 9:
                break;
            case 10:
                break;
        }
    }

    void CreateItem(string strName, int iIndex)
    {
        GameObject item = MonoBehaviour.Instantiate(Resources.Load("Item/" + strName)) as GameObject;
        item.name = strName;
        item.transform.position = trans.position;
        item.GetComponent<ItemManager>().SetState(iIndex);
    }

    void DamageBlock()
    {
        if(m_iBlockLife == 1)
        {
            Sprite spt = Resources.Load(UIController.GetInstance.GetPath() + "(SkyBlue)_Damage", typeof(Sprite)) as Sprite;
            gameObject.GetComponent<SpriteRenderer>().sprite = spt;
        }
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

    void Damage()
    {
        m_iBlockLife -= 1;
    }

    public void SetLife(int iLife)
    {
        m_iBlockLife = iLife;
    }
}
