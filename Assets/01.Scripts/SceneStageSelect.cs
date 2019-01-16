using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneStageSelect : MonoBehaviour {

    private List<GameObject> m_listBlock = new List<GameObject>();

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 35; ++i)
        {
            int iIndex = i + 1; 
            m_listBlock.Add(GameObject.Find("Stage" + (i + 1)));

            m_listBlock[i].GetComponent<Button>().onClick.AddListener(() => StageManager.GetInstance.PlayGameScene(iIndex));
        }

        StageManager.GetInstance.Initialize(m_listBlock);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
