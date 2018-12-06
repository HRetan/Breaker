using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

    public float fSpeed = 100f;

    private SpriteRenderer sprite;
    private BoxCollider2D boxColl;
    private Color color;
    private bool isBreak = false;
    private float fTime;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        boxColl = GetComponent<BoxCollider2D>();
        color = new Color();
        color = sprite.color;
	}
	
	// Update is called once per frame
	void Update () {
        sprite.color = color;
        if (isBreak)
        {
            fTime += Time.deltaTime * fSpeed;

            if(fTime <= 1.2f)
                color.a = Mathf.Lerp(1, 0, fTime);

            if (color.a <= 0.1f)
            {
                gameObject.SetActive(false);
                Debug.Log("종료");
            }
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Ball")
        {
            isBreak = true;
            boxColl.enabled = false;
        }
    }

}
