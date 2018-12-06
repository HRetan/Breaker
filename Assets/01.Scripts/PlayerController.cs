using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float fSpeed;

    private Vector3 vector;
    private Transform trans;

    bool isRight = false;

    // Use this for initialization
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        Debug.Log("호출");
    }

    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            vector.Set(Input.GetAxisRaw("Horizontal"), 0, transform.position.z);

            if (vector.x != 0)
            {
                transform.Translate(vector.x * fSpeed, 0, 0);
            }
            else if (vector.y != 0)
            {
                transform.Translate(0, vector.y * fSpeed, 0);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Wall")
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                vector.Set(Input.GetAxisRaw("Horizontal"), 0, transform.position.z);

                if (vector.x != 0)
                {
                    transform.Translate(-vector.x * fSpeed, 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * fSpeed, 0);
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {

    }
}
