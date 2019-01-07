using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool m_bLeft = false;
    private bool m_bRight = false;
    private PLAYERMOVE m_eMove = PLAYERMOVE.NOMOVE;

    // Use this for initialization
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (UIController.GetInstance.GetUI())
            return;
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
        Debug.Log("사이즈 증가");
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
}
