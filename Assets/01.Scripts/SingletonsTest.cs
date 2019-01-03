using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonsTest : MonoBehaviour {

    private static SingletonsTest instance = null;

    public static SingletonsTest GetInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType(typeof(SingletonsTest)) as SingletonsTest;

                if(instance == null)
                {
                    Debug.LogError("싱글톤 인스턴스 생성 실패");
                }
            }
            return instance;
        }
    }

    public void TestFunc()
    {
        Debug.Log("성공");
    }

}
