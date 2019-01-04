using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float fSpeed = 2f;

    private Vector3 vector;

    private bool m_bLeft = false;
    private bool m_bRight = false;


    // Use this for initialization
    void Start()
    {
    }

    void FixedUpdate()
    {
        //if (Input.GetAxisRaw("Horizontal") != 0)
        //{
        //    vector.Set(Input.GetAxisRaw("Horizontal"), 0, transform.position.z);

        //    if (vector.x != 0)
        //    {
        //        transform.Translate(vector.x * fSpeed, 0, 0);
        //    }
        //    else if (vector.y != 0)
        //    {
        //        transform.Translate(0, vector.y * fSpeed, 0);
        //    }
        //}
        if (m_bLeft)
        {
            transform.Translate(-fSpeed * Time.deltaTime, 0, 0);
        }

        if (m_bRight)
        {
            transform.Translate(fSpeed * Time.deltaTime, 0, 0);
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
            //if (Input.GetAxisRaw("Horizontal") != 0)
            //{
            //    vector.Set(Input.GetAxisRaw("Horizontal"), 0, transform.position.z);

            //    if (vector.x != 0)
            //    {
            //        transform.Translate(-vector.x * fSpeed, 0, 0);
            //    }
            //    else if (vector.y != 0)
            //    {
            //        transform.Translate(0, vector.y * fSpeed, 0);
            //    }
            //}
            if (m_bLeft)
            {
                transform.Translate(fSpeed * Time.deltaTime, 0, 0);
            }

            if (m_bRight)
            {
                transform.Translate(-fSpeed * Time.deltaTime, 0, 0);
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
        Debug.Log("사이즈 증가");
    }

    public void LeftMoveOn()
    {
        m_bLeft = true;
        m_bRight = false;
    }

    public void MoveOff()
    {
        m_bLeft = false;
        m_bRight = false;
    }

    public void RightMoveOn()
    {
        m_bLeft = false;
        m_bRight = true;
    }

}
