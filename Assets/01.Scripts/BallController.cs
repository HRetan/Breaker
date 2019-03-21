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

enum BALLITEM
{
    GRAP,
    PENETRATE,
    ITEMEND
}

public struct itemState
{
    public bool bIsPlay;
    public float fTime;

    public itemState(bool play, float time)
    {
        bIsPlay = play;
        fTime = time;
    }
}


public class BallController : MonoBehaviour
{
    private static int m_iBallLive = 1;
    private static int w_iBallAtk = 1;
    private static int w_iCount = 0;
    [SerializeField] private static List<GameObject> w_listBall = new List<GameObject>();


    private float fMoveSpeed = 5f;
    private Transform trans;
    private BALLSTATE state = BALLSTATE.START;

    private Rigidbody2D temp;

    private GameObject BarObject;
    private GameObject ArrowObject;
    private Transform BarTrans;

    private BlockManager blockManager;
    private ScoreUI m_scScoreUI;

    private Vector3 vector;
    private Vector3 vecDir;
    private Vector3 mousePt;

    private Vector3 incidence;
    private Vector3 normalVec;
    private Vector3 startPosition;

    private float m_fColX = 0f;
    public float m_fTestAngle = 0f;

    private itemState[] m_tState = new itemState[(int)BALLITEM.ITEMEND];

    void Awake()
    {
        BarObject = GameObject.FindGameObjectWithTag("Player");
        blockManager = GameObject.Find("GameManager").GetComponent<BlockManager>();
        BarTrans = BarObject.GetComponent<Transform>();
        trans = GetComponent<Transform>();
        ArrowObject = transform.Find("Arrow").gameObject;
        temp = GetComponent<Rigidbody2D>();
        for (int i = 0; i < 2; ++i)
        {
            m_tState[i].bIsPlay = false;
            m_tState[i].fTime = 0f;
        }

        w_listBall.Add(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        //Debug.Log(m_iBallLive);
        m_scScoreUI = GameObject.Find("ScoreUI").GetComponent<ScoreUI>();
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

        for (int i = 0; i < 2; ++i)
        {
            if (m_tState[i].bIsPlay)
            {
                if (m_tState[i].fTime >= 4f)
                {
                    m_tState[i].bIsPlay = false;
                    m_tState[i].fTime = 0f;
                }
                m_tState[i].fTime += Time.deltaTime;
            }
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
        if (!m_tState[0].bIsPlay)
            vector.Set(BarTrans.position.x, BarTrans.position.y + 0.25f, BarTrans.position.z);
        else
            vector.Set(BarTrans.position.x + m_fColX, BarTrans.position.y + 0.25f, BarTrans.position.z);

        trans.position = vector;
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
        if (coll.gameObject.tag == "Block")
        {
            w_iCount += 1;
            m_scScoreUI.ScoreCheck(w_iCount);

            if(m_tState[(int)BALLITEM.PENETRATE].bIsPlay)
            {
                coll.gameObject.GetComponent<BlockController>().SetLife(0);
                coll.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                return;
            }
        }
        else
            w_iCount = 0;
        
        Vector3 angleVec = trans.rotation.eulerAngles;

        trans.rotation = Quaternion.Euler(angleVec.x, angleVec.y, SetAngle(coll));

    }

    float SetAngle(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && m_tState[(int)BALLITEM.GRAP].bIsPlay)
        {
            m_fColX = Mathf.Abs(BarTrans.position.x - coll.contacts[0].point.x);
            if (BarTrans.position.x >= coll.contacts[0].point.x)
                m_fColX *= -1;

            temp.AddForce(Vector2.zero);
            ArrowObject.SetActive(true);
            state = BALLSTATE.GRAP;
            //Debug.Log(m_fColX);
        }
        float angle = 0f;
        Vector2 point = coll.contacts[0].point;
        temp.AddForce(Vector2.zero);

        incidence = (Vector3)point - startPosition;
        normalVec = coll.contacts[0].normal;
        vecDir = Vector3.Reflect(incidence, normalVec);

        if (coll.gameObject.tag == "Block" && coll.gameObject.GetComponent<BlockController>().GetBlockID() == 9)
        {
            vecDir = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), 0f);
        }
        vecDir = vecDir.normalized;
        startPosition = point;

        angle = Mathf.Atan2(vecDir.y, vecDir.x) * 180f / Mathf.PI;

        if (Mathf.Abs(angle) >= 160 || Mathf.Abs(angle) <= 20)
        {
            if (angle < 0)
            {
                angle += 50;
            }
            else
                angle -= 50;
        }

        return m_fTestAngle = angle;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeadZone")
        {
            m_iBallLive -= 1;

            if (m_iBallLive == 0)
            {
                UIController.GetInstance.ResultUI(blockManager.GetBlockCount(), m_scScoreUI.GetScore());
                m_iBallLive = 1;
            }
            else
            {
                w_listBall.Remove(this.gameObject);
                //Debug.Log("삭제후 볼 갯수 : " + w_listBall.Count);
                Destroy(this.gameObject);
            }
        }
    }

    public void CreateBall()
    {
        GameObject[] goBall = new GameObject[2];
        for (int i = 0; i < 2; ++i)
        {
            ++m_iBallLive;
            goBall[i] = Instantiate(Resources.Load("Ball/Breaker_Ball(Red)")) as GameObject;
            BallController ball = goBall[i].GetComponent<BallController>();
            CloneVar(goBall[i], ball);
        }

        goBall[0].transform.rotation = Quaternion.Euler(trans.rotation.x, trans.rotation.y, trans.rotation.z + 45f);
        goBall[1].transform.rotation = Quaternion.Euler(trans.rotation.x, trans.rotation.y, trans.rotation.z - 45f);
        goBall[0].GetComponent<BallController>().vecDir = goBall[0].transform.rotation * vecDir;
        goBall[1].GetComponent<BallController>().vecDir = goBall[1].transform.rotation * vecDir;
    }

    void CloneVar(GameObject goBall, BallController ballCon)
    {
        goBall.name = "Breaker_Ball(Red)";
        goBall.transform.position = trans.position;
        ballCon.state = BALLSTATE.MOVE;
        ballCon.ArrowObject.SetActive(false);
        ballCon.startPosition = trans.position;
        ballCon.m_fColX = m_fColX;

        for (int i = 0; i < 2; ++i)
        {
            ballCon.m_tState[i] = m_tState[i];
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

    public void SetItemPlay(int iIndex)
    {
       Debug.Log("볼 갯수 : " + w_listBall.Count);
        for(int i = 0; i < w_listBall.Count; ++i)
        {
            w_listBall[i].GetComponent<BallController>().m_tState[iIndex].bIsPlay = true;
        }
    }

    public int GetBallAtk()
    {
        return w_iBallAtk;
    }

    public void SetBallLife()
    {
        m_iBallLive = 1;
    }

    public void SetListBall()
    {
        w_listBall.Clear();
    }

    public void SetBallSpeedLevel(int iLevel)
    {
        switch(iLevel)
        {
            case 1:
                fMoveSpeed = 5f;
                break;
            case 2:
                fMoveSpeed = 6f;
                break;
            case 3:
                fMoveSpeed = 7f;
                break;
        }
    }
}
