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
    private Transform trans;
    private BALLSTATE state;

    private GameObject BarObject;
    private Transform BarTrans;
    private Vector3 vector;
    private float rotZ = 90f;

    private bool isFire = false;

    // Use this for initialization
    void Start()
    {
        BarObject = GameObject.FindGameObjectWithTag("Player");
        BarTrans = BarObject.GetComponent<Transform>();
        trans = GetComponent<Transform>();
        state = BALLSTATE.START;
        trans.Rotate(0f, 0f, rotZ);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case BALLSTATE.START:
                StartBall();
                break;
            case BALLSTATE.MOVE:
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
        if (Input.GetKey(KeyCode.UpArrow) && trans.rotation.z <= 160f)
        {
            rotZ = Time.deltaTime * fSpeed;
            trans.Rotate(0f, 0f, rotZ);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && trans.rotation.z >= 0f)
        {
            rotZ = -Time.deltaTime * fSpeed;
            trans.Rotate(0f, 0f, rotZ);
        }
        Debug.Log(trans.rotation.z);
    }
}
