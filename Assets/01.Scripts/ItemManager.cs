using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemManager : MonoBehaviour{

    enum ITEMSTATE
    {
        BROWN,
        PURPLE,
        YELLOW,
        GREEN,
        RED,
        NOITEM
    }

    private BallController goBall;
    private PlayerController goBar;

    [SerializeField]
    private ITEMSTATE eState = ITEMSTATE.NOITEM;
    private Transform trans;

    [SerializeField]
    private float fSpeed = 0.5f;

    // Use this for initialization
    void Start () {
        trans = GetComponent<Transform>();
        goBall = GameObject.FindWithTag("Ball").GetComponent<BallController>();
        goBar = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        trans.Translate(-Vector3.up * fSpeed * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            ItemApply();
            Destroy(gameObject);
            Debug.Log("충돌1");
        }
    }

    void ItemApply()
    {
        switch(eState)
        {
            case ITEMSTATE.BROWN:
                goBar.SetSize(new Vector3(0.5f, 0f, 0f));
                break;
            case ITEMSTATE.PURPLE:
                goBar.SetSpeed(0.025f);
                break;
            case ITEMSTATE.YELLOW:
                goBar.SetSpeed(-0.025f);
                break;
            case ITEMSTATE.GREEN:
                goBar.SetSize(new Vector3(-0.5f, 0f, 0f));
                break;
            case ITEMSTATE.RED:
                goBall.SetSpeed(1f);
                break;
        }
    }

    public void SetState(int state)
    {
        if (eState != ITEMSTATE.NOITEM)
            return;
        switch (state)
        {
            case 0:
                eState = ITEMSTATE.BROWN;
                break;
            case 1:
                eState = ITEMSTATE.PURPLE;
                break;
            case 2:
                eState = ITEMSTATE.YELLOW;
                break;
            case 3:
                eState = ITEMSTATE.GREEN;
                break;
            case 4:
                eState = ITEMSTATE.RED;
                break;
        }
}
}
