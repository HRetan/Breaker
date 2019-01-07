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

    public float fMoveSpeed = 3f;
    private Transform trans;
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

        switch (state)
        {
            case BALLSTATE.START:
            case BALLSTATE.TOUCH:
                StartBall();
                break;
            case BALLSTATE.MOVE:
                Move();
                break;
            case BALLSTATE.GRAP:
                break;
            case BALLSTATE.DEAD:
                break;
        }
    }

    void StartBall()
    {
        vector.Set(BarTrans.position.x, BarTrans.position.y + 0.25f, BarTrans.position.z);
        trans.position = vector;

        if(BarObject.GetComponent<PlayerController>().GetPlayerMove() == 2)
            DirBall();
    }

    void Move()
    {
        temp.velocity = vecDir * fMoveSpeed;
    }

    void DirBall()
    {
        //마우스 조종
        if(Input.GetMouseButton(0))
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "DeadZone")
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
}
