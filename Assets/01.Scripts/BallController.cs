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

public class BallController : MonoBehaviour {
  
    private float fSpeed = 0f;
    private Transform trans;
    private BALLSTATE state;

    private GameObject BarObject;
    private Transform BarTrans;
    private Vector3 vector;

    private bool isFire = false;
    
    // Use this for initialization
	void Start () {
        BarObject = GameObject.FindGameObjectWithTag("Player");
        BarTrans = BarObject.GetComponent<Transform>();
        trans = GetComponent<Transform>();
        state = BALLSTATE.START;
	}
	
	// Update is called once per frame
	void Update () {
		switch(state)
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

        if (isFire)
            state = BALLSTATE.MOVE;
    }

    void Move()
    {

    }
}
