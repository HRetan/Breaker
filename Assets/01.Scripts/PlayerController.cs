using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private enum PLAYERMOVE
    {
        LEFTMOVE,
        RIGHTMOVE,
        NOMOVE
    }

    [SerializeField]
    private float fSpeed = 2f;

    private Vector3 vector;
    private Scrollbar m_Scroll;
    private BallController m_scBall;
    private float m_fSize = 4.4f;
    private PLAYERMOVE m_eMove = PLAYERMOVE.NOMOVE;

    private GameObject[] m_goBulletPoint = new GameObject[2];

    private bool m_bBullet = false;
    private float m_fBulletTime = 0f;
    private float m_fItem = 0f;

    // Use this for initialization
    void Start()
    {
        m_scBall = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallController>();
        m_Scroll = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
        m_goBulletPoint[0] = transform.Find("BulletPoint1").gameObject;
        m_goBulletPoint[1] = transform.Find("BulletPoint2").gameObject;
        vector = transform.position;

        m_scBall.SetBallLife();
    }

    void FixedUpdate()
    {
        if (UIController.GetInstance.GetUI())
            return;

        switch (m_eMove)
        {
            case PLAYERMOVE.LEFTMOVE:
            transform.Translate(-fSpeed * Time.deltaTime, 0, 0);
                break;
            case PLAYERMOVE.RIGHTMOVE:
            transform.Translate(fSpeed * Time.deltaTime, 0, 0);
                break;
            case PLAYERMOVE.NOMOVE:
                break;
        }

        vector.x = m_Scroll.value * m_fSize - m_fSize * 0.5f;
        transform.position = vector;
    }

    // Update is called once per frame
    void Update()
    {
        if (UIController.GetInstance.GetUI())
            return;

        if(m_bBullet)
        {
            if(m_fBulletTime >= 0.3f)
            {
                m_fBulletTime = 0;
                CreateBullet();
            }
            if(m_fItem >= 3f)
            {
                m_fItem = 0f;
                m_bBullet = false;
            }
            m_fBulletTime += Time.deltaTime;
            m_fItem += Time.deltaTime;
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Wall")
        {
            switch (m_eMove)
            {
                case PLAYERMOVE.LEFTMOVE:
                    transform.Translate(fSpeed * Time.deltaTime, 0, 0);
                    break;
                case PLAYERMOVE.RIGHTMOVE:
                    transform.Translate(-fSpeed * Time.deltaTime, 0, 0);
                    break;
                case PLAYERMOVE.NOMOVE:
                    break;
            }
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {

    }

    public void SetSpeed(float Speed)
    {
        if (fSpeed >= 3f)
            return;
        if (fSpeed <= 1.5f)
            return;

        fSpeed += Speed;
    }

    public void SetSize(Vector3 vSize)
    {
        if (transform.localScale.x == 3f)
            return;
        if (transform.localScale.x == 0.5f)
            return;

        transform.localScale += vSize;
    }

    public void LeftMoveOn()
    {
        m_eMove = PLAYERMOVE.LEFTMOVE;
    }

    public void MoveOff()
    {
        m_eMove = PLAYERMOVE.NOMOVE;
    }

    public void RightMoveOn()
    {
        m_eMove = PLAYERMOVE.RIGHTMOVE;
    }

    public int GetPlayerMove()
    {
        return (int)m_eMove;
    }

    public void SetBulletPlay()
    {
        m_bBullet = true;
    }

    void CreateBullet()
    {
        for(int i = 0; i < 2; ++i)
        {
            GameObject goBullet = MonoBehaviour.Instantiate(Resources.Load("Bullet/Bullet")) as GameObject;
            goBullet.name = "Bullet";
            goBullet.transform.position = m_goBulletPoint[i].transform.position;
        }
    }
}
