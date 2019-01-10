using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapList : MonoBehaviour {

    private List<string> m_listFile = new List<string>();
	// Use this for initialization
	void Start () {
        SaveNLoad.GetInstance.FindFileStage(m_listFile);
        for (int i = 0; i < m_listFile.Count; ++i)
        {
            CreateFile(m_listFile[i]);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateFile(string strName)
    {
        GameObject goFile = MonoBehaviour.Instantiate(Resources.Load("UI/ListName")) as GameObject;

        goFile.transform.SetParent(GameObject.Find("Contents").transform);
        goFile.name = strName;
        goFile.GetComponentInChildren<Text>().text = strName;
        goFile.transform.localScale = new Vector3(1, 1, 1);

        goFile.GetComponent<Button>().onClick.AddListener(() => SaveNLoad.GetInstance.SetStaticFileName(goFile.name));
    }


}
