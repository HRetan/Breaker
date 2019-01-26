using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour {

    private static SkinManager Instance = null;

    public static SkinManager GetInstance
    {
        get
        {
            if(Instance == null)
            {
                Instance = FindObjectOfType(typeof(SkinManager)) as SkinManager;

                if (Instance == null)
                    Debug.LogError("SkinManager 생성 실패");
            }
        return Instance;
        }
    }

    enum SKINSTATE
    {
        SKIN1,
        SKIN2,
        SKIN3,
        SKIN4,
        SKIN5,
        SKIN6,
        SKIN7,
        NOSKIN
    }

    private static SKINSTATE w_eState = SKINSTATE.SKIN6;
    private int m_iTestSkin = 6;

    public void MySkin()
    {
        GameObject goSkin = Instantiate(Resources.Load("BackGround/BackGround")) as GameObject;
        Sprite spt = null;
        goSkin.name = "BackGround";

        switch (w_eState)
        {
            case SKINSTATE.SKIN1:
                spt = Resources.Load("BackGround/BackGround(Cake)", typeof(Sprite)) as Sprite;
                break;
            case SKINSTATE.SKIN2:
                spt = Resources.Load("BackGround/BackGround(Castle)", typeof(Sprite)) as Sprite;
                break;
            case SKINSTATE.SKIN3:
                spt = Resources.Load("BackGround/BackGround(DarkCity)", typeof(Sprite)) as Sprite;
                break;
            case SKINSTATE.SKIN4:
                spt = Resources.Load("BackGround/BackGround(Mountain)", typeof(Sprite)) as Sprite;
                break;
            case SKINSTATE.SKIN5:
                spt = Resources.Load("BackGround/BackGround(Star)", typeof(Sprite)) as Sprite;
                break;
            case SKINSTATE.SKIN6:
                spt = Resources.Load("BackGround/BackGround(Black)", typeof(Sprite)) as Sprite;
                break;
            case SKINSTATE.SKIN7:
                spt = Resources.Load("BackGround/BackGround(White)", typeof(Sprite)) as Sprite;
                break;
        }

        goSkin.GetComponent<SpriteRenderer>().sprite = spt;
    }

    public void SelectSkin()
    {
        switch(m_iTestSkin)
        {
            case 1:
                w_eState = SKINSTATE.SKIN1;
                break;
            case 2:
                w_eState = SKINSTATE.SKIN2;
                break;
            case 3:
                w_eState = SKINSTATE.SKIN3;
                break;
            case 4:
                w_eState = SKINSTATE.SKIN4;
                break;
            case 5:
                w_eState = SKINSTATE.SKIN5;
                break;
            case 6:
                w_eState = SKINSTATE.SKIN6;
                break;
            case 7:
                w_eState = SKINSTATE.SKIN7;
                break;
        }
    }

    public void SetTestSkin(int iTestIndex)
    {
        m_iTestSkin = iTestIndex;

        SpriteRenderer renderer = GameObject.Find("BackGround").GetComponent<SpriteRenderer>();
        switch (m_iTestSkin)
        {
            case 1:
                renderer.sprite = Resources.Load("BackGround/BackGround(Cake)", typeof(Sprite)) as Sprite;
                break;
            case 2:
                renderer.sprite = Resources.Load("BackGround/BackGround(Castle)", typeof(Sprite)) as Sprite;
                break;
            case 3:
                renderer.sprite = Resources.Load("BackGround/BackGround(DarkCity)", typeof(Sprite)) as Sprite;
                break;
            case 4:
                renderer.sprite = Resources.Load("BackGround/BackGround(Mountain)", typeof(Sprite)) as Sprite;
                break;
            case 5:
                renderer.sprite = Resources.Load("BackGround/BackGround(Star)", typeof(Sprite)) as Sprite;
                break;
            case 6:
                renderer.sprite = Resources.Load("BackGround/BackGround(Black)", typeof(Sprite)) as Sprite;
                break;
            case 7:
                renderer.sprite = Resources.Load("BackGround/BackGround(White)", typeof(Sprite)) as Sprite;
                break;
        }
    }

    public void SetTestSkin(GameObject goSkin)
    {
        GameObject goSelect = GameObject.Find("Selector");

        goSelect.transform.parent = goSkin.transform;
        goSelect.GetComponent<RectTransform>().position = goSkin.GetComponent<RectTransform>().position;
    }
}
