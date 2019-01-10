using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BALLSTATE
{
    START,
    MOVE,
    TOUCH,
    GRAP,
    DEAD
}

public class BallController : MonoBehaviour
{

    private float fMoveSpeed = 5f;
    private Transform trans;
    [SerializeField]
    private BALLSTATE state;

    private Rigidbody2D temp;

    private GameObject BarObject;
    private GameObject ArrowObject;
    private Transform BarTrans;

    private BlockManager blockManager;

    private Vector3 vector;
    private Vector3 vecDir;
    private Vector3 mousePt;

    private Vector3 incidence;
    private Vector3 normalVec;
    private Vector3 startPosition;

    [SerializeField]
    private bool m_bIsGrap = false;
    [SerializeField]
    private float m_fItemTime = 0f;
    [SerializeField]
    private float m_fColX = 0f;

    // Use this for initialization
    void Start()
    {
        BarObject = GameObject.FindGameObjectWithTag("Player");
        blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();
        BarTrans = BarObject.GetComponent<Transform>();
        trans = GetComponent<Transform>();
        state = BALLSTATE.START;
        ArrowObject = GameObject.FindGameObjectWithTag("Arrow");
        temp = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (UIController.GetInstance.GetUI())
        {
            temp.simulated = false;
            return;
        }

        temp.simulated = true;

        if(m_bIsGrap)
        {
            m_fItemTime += Time.deltaTime;
            if (m_fItemTime >= 10f)
                m_bIsGrap = false;
        }

        switch (state)
        {
            case BALLSTATE.START:
            case BALLSTATE.TOUCH:
            case BALLSTATE.GRAP:
                StartBall();
                break;
            case BALLSTATE.MOVE:
                Move();
                break;
            case BALLSTATE.DEAD:
                break;
        }
    }

    void StartBall()
    {
        if (!m_bIsGrap)
            vector.Set(BarTrans.position.x, BarTrans.position.y + 0.25f, BarTrans.position.z);
        else
            vector.Set(BarTrans.position.x + m_fColX, BarTrans.position.y + 0.25f, BarTrans.position.z);

        trans.position = vector;
        Debug.Log("기본");
        if (BarObject.GetComponent<PlayerController>().GetPlayerMove() == 2)
            DirBall();

    }

    void Move()
    {
        temp.velocity = vecDir * fMoveSpeed;
    }

    void DirBall()
    {
        //마우스 조종
        if (Input.GetMouseButton(0))
        {
            mousePt = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            vecDir = mousePt - trans.position;
            vecDir = vecDir.normalized;
            float angle = Mathf.Atan2(vecDir.y, vecDir.x) * 180f / Mathf.PI;

            if (angle <= 170f && angle >= 10f)
            {

                Vector3 angleVec = trans.rotation.eulerAngles;
                trans.rotation = Quaternion.Euler(angleVec.x, angleVec.y, angle);
            }
            
            state = BALLSTATE.TOUCH;
        }

        if (Input.GetMouseButtonUp(0) && state == BALLSTATE.TOUCH)
        {
            state = BALLSTATE.MOVE;
            ArrowObject.SetActive(false);
            startPosition = trans.position;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && m_bIsGrap)
        {
            m_fColX = Mathf.Abs(BarTrans.position.x - coll.contacts[0].point.x);
            if (BarTrans.position.x >= coll.contacts[0].point.x)
                m_fColX *= -1;

            temp.AddForce(Vector2.zero);
            ArrowObject.SetActive(true);
            state = BALLSTATE.GRAP;
            Debug.Log(m_fColX);
        }

        Vector3 angleVec = trans.rotation.eulerAngles;
        Vector2 point = coll.contacts[0].point;
        temp.AddForce(Vector2.zero);

        incidence = (Vector3)point - startPosition;
        normalVec = coll.contacts[0].normal;
        vecDir = Vector3.Reflect(incidence, normalVec).normalized;
        startPosition = point;

        float angle = Mathf.Atan2(vecDir.y, vecDir.x) * 180f / Mathf.PI;
        trans.rotation = Quaternion.Euler(angleVec.x, angleVec.y, angle);

    }


    //void OnCollisionStay2D(Collision2D coll)
    //{
    //    if (coll.gameObject.tag == "Player" && m_bIsGrap)
    //    {
    //        m_fColX = coll.contacts[0].point.x;
    //        state = BALLSTATE.GRAP;
    //    }
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeadZone")
        {
            UIController.GetInstance.ResultUI(blockManager.GetListBlock());
        }
    }

    public void SetSpeed(float fSpeed)
    {
        if (fMoveSpeed >= 10f)
            return;
        if (fMoveSpeed <= 3f)
            return;

        fMoveSpeed += fSpeed;
    }

    public void SetGrap()
    {
        m_bIsGrap = true;
    }
}
