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

    private float fSpeed = 60f;
    private float fMoveSpeed = 0.1f;
    private Transform trans;
    private BALLSTATE state;

    private Rigidbody2D temp;

    private GameObject BarObject;
    private GameObject ArrowObject;
    private Transform BarTrans;
    private Vector3 vector;
    private Vector3 dirVec;

    private Vector3 angle;
    private Vector3 rotVec = new Vector3(0f, 90f, 0f);

    private float rotZ = 90f;

    private bool isFire = false;

    // Use this for initialization
    void Start()
    {
        BarObject = GameObject.FindGameObjectWithTag("Player");
        BarTrans = BarObject.GetComponent<Transform>();
        trans = GetComponent<Transform>();
        state = BALLSTATE.START;
        ArrowObject = GameObject.FindGameObjectWithTag("Arrow");
        temp = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirVec = -Vector3.up;
        switch (state)
        {
            case BALLSTATE.START:
                StartBall();
                break;
            case BALLSTATE.MOVE:
                temp.AddForce(trans.right * fMoveSpeed);
                //trans.Translate(trans.TransformDirection(dirVec) * fMoveSpeed * Time.deltaTime);
                break;
            case BALLSTATE.TOUCH:
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

        DirBall();

        if (isFire)
        {
            state = BALLSTATE.MOVE;
        }
    }

    void Move()
    {

    }

    void DirBall()
    {
        if (Input.GetKey(KeyCode.UpArrow) && trans.rotation.eulerAngles.z <= 170f)
        {
            rotZ = Time.deltaTime * fSpeed;

            trans.Rotate(0f, 0f, rotZ);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && trans.rotation.eulerAngles.z >= 10f)
        {
            rotZ = -Time.deltaTime * fSpeed;
            trans.Rotate(0f, 0f, rotZ);
        }

        //if (Input.GetKey(KeyCode.UpArrow) && rotVec.z <= 170f)
        //{
        //    rotVec.z += Time.deltaTime * fSpeed;
        //    //trans.Rotate(0f, 0f, rotZ);
        //    angle = rotVec - dirVec;
        //    angle.Normalize();
        //    rotVec.Normalize();
        //    ArrowObject.GetComponent<Transform>().Rotate(rotVec);
        //    Debug.Log(rotVec.z);
        //}
        //else if (Input.GetKey(KeyCode.DownArrow) && rotVec.z >= 10f)
        //{
        //    rotVec.z -= Time.deltaTime * fSpeed;
        //    //trans.Rotate(0f, 0f, rotZ);
        //    angle = rotVec - dirVec;
        //    angle.Normalize();
        //    rotVec.Normalize();
        //    ArrowObject.GetComponent<Transform>().Rotate(rotVec);
        //    Debug.Log(rotVec.z);

        //}

        if (Input.GetKey(KeyCode.Space))
        {
            state = BALLSTATE.MOVE;
            //ArrowObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(trans.rotation.eulerAngles.z);

        Vector3 angleVec = trans.rotation.eulerAngles;

        trans.rotation = Quaternion.Euler(angleVec.x, angleVec.y, 180f - angleVec.z);
    }
}
