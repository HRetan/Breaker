using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockController : MonoBehaviour {
  
    public float fSpeed = 10f;

    private SpriteRenderer sprite;
    private BoxCollider2D boxColl;
    private Transform trans;
    private Color color;
    private bool isBreak = false;
    private float fTime;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        boxColl = GetComponent<BoxCollider2D>();
        trans = GetComponent<Transform>();
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

            if (color.a <= 0.15f)
            {
                Destroy(gameObject);
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
            
            if(gameObject.tag == "BrownBlock")
            {
                GameObject item = MonoBehaviour.Instantiate(Resources.Load("Item/Item(Brown)")) as GameObject;
                item.name = "Item(Brown)";
                item.transform.position = trans.position;
                item.GetComponent<ItemManager>().SetState(0);
            }
            else if (gameObject.tag == "PurpleBlock")
            {
                GameObject item = MonoBehaviour.Instantiate(Resources.Load("Item/Item(Purple)")) as GameObject;
                item.name = "Item(Purple)";
                item.transform.position = trans.position;
                item.GetComponent<ItemManager>().SetState(1);
            }
            else if (gameObject.tag == "YellowBlock")
            {
                GameObject item = MonoBehaviour.Instantiate(Resources.Load("Item/Item(Yellow)")) as GameObject;
                item.name = "Item(Yellow)";
                item.transform.position = trans.position;
                item.GetComponent<ItemManager>().SetState(2);
            }
            else if (gameObject.tag == "GreenBlock")
            {
                GameObject item = MonoBehaviour.Instantiate(Resources.Load("Item/Item(Green)")) as GameObject;
                item.name = "Item(Green)";
                item.transform.position = trans.position;
                item.GetComponent<ItemManager>().SetState(3);
            }
            else if (gameObject.tag == "RedBlock")
            {
                GameObject item = MonoBehaviour.Instantiate(Resources.Load("Item/Item(Red)")) as GameObject;
                item.name = "Item(Red)";
                item.transform.position = trans.position;
                item.GetComponent<ItemManager>().SetState(4);
            }
        }
    }

}
