using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float fSpeed = 0.05f;

    private Vector3 vector;


    // Use this for initialization
    void Start()
    {
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

    public void SetSpeed(float Speed)
    {
        if (fSpeed >= 0.15f)
            return;
        if (fSpeed <= 0.025f)
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
        Debug.Log("사이즈 증가");
    }
}
