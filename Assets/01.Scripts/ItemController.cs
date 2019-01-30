using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

    enum ITEMSTATE
    {
        BROWN,
        PURPLE,
        YELLOW,
        GREEN,
        RED,
        BLUE,
        PINK,
        SKYBLUE,
        NOITEM
    }
    public AudioClip m_acEffectSound;

    private BallController goBall;
    private PlayerController goBar;

    private ITEMSTATE eState = ITEMSTATE.NOITEM;
    private Transform trans;

    private float fSpeed = 1f;

    // Use this for initialization
    void Start()
    {
        trans = GetComponent<Transform>();
        goBall = GameObject.FindWithTag("Ball").GetComponent<BallController>();
        goBar = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UIController.GetInstance.GetUI())
        {
            return;
        }

        trans.Translate(-Vector3.up * fSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if (m_acEffectSound != null)
                AudioSource.PlayClipAtPoint(m_acEffectSound, transform.position);

            ItemApply();
            Destroy(gameObject);
        }
        else if (coll.tag == "DeadZone")
        {
            Destroy(this.gameObject);
        }
    }

    void ItemApply()
    {
        switch (eState)
        {
            case ITEMSTATE.BROWN:
                goBall.SetSpeed(-1f);
                break;
            case ITEMSTATE.PURPLE:
                goBall.SetItemPlay(1);
                break;
            case ITEMSTATE.YELLOW:
                goBar.SetSize(new Vector3(-0.5f, 0f, 0f));
                break;
            case ITEMSTATE.GREEN:
                goBall.CreateBall();
                break;
            case ITEMSTATE.RED:
                goBar.SetSize(new Vector3(0.5f, 0f, 0f));
                break;
            case ITEMSTATE.BLUE:
                goBall.SetSpeed(1f);
                break;
            case ITEMSTATE.PINK:
                goBall.SetItemPlay(0);
                break;
            case ITEMSTATE.SKYBLUE:
                goBar.SetBulletPlay();
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
                eState = ITEMSTATE.BLUE;
                break;
            case 1:
                eState = ITEMSTATE.BROWN;
                break;
            case 2:
                eState = ITEMSTATE.PURPLE;
                break;
            case 3:
                eState = ITEMSTATE.RED;
                break;
            case 4:
                eState = ITEMSTATE.YELLOW;
                break;
            case 5:
                eState = ITEMSTATE.GREEN;
                break;
            case 6:
                eState = ITEMSTATE.PINK;
                break;
            case 7:
                eState = ITEMSTATE.SKYBLUE;
                break;
        }
    }
}
