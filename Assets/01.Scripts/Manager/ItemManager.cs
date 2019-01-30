using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemManager : MonoBehaviour
{

    private static ItemManager Instance = null;

    public static ItemManager GetInstance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType(typeof(ItemManager)) as ItemManager;

                if (Instance == null)
                {
                    Debug.LogError("싱글톤 인스턴스 생성 실패");
                }
            }
            return Instance;
        }
    }
    
    public void CreateItem(int iIndex, Vector3 position)
    {
        GameObject item = null;
        string strName = "";

        switch(iIndex)
        {
            case 0:
                strName = "Item(Blue)";
                break;
            case 1:
                strName = "Item(Brown)";
                break;
            case 2:
                strName = "Item(Purple)";
                break;
            case 3:
                strName = "Item(Red)";
                break;
            case 4:
                strName = "Item(Yellow)";
                break;
            case 5:
                strName = "Item(Green)";
                break;
            case 6:
                strName = "Item(Pink)";
                break;
            case 7:
                strName = "Item(SkyBlue)";
                break;
            default:
                return;
        }

        if(strName != "")
        {
            item = MonoBehaviour.Instantiate(Resources.Load("Item/" + strName)) as GameObject;
            item.name = strName;
            item.transform.position = position;
            item.GetComponent<ItemController>().SetState(iIndex);
        }
    }

}
