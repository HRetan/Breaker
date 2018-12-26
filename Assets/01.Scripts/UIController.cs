using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    private List<GameObject> m_listButton;
    private string m_strButton;

	// Use this for initialization
	void Start () {
        m_strButton = "Stage";
        for(int i = 1; i < 36; ++i)
        m_listButton.Add(GameObject.Find(m_strButton + i));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SceneChangeStart()
    {
        SceneManager.LoadScene("Title_Stage");
    }
    public void SceneChangeMapTool()
    {
        SceneManager.LoadScene("MapTool");
    }
    public void SceneChangeSkin()
    {
        SceneManager.LoadScene("Title_Skin");
    }
    public void SceneChangeRank()
    {
        SceneManager.LoadScene("Title_Rank");
    }

    public void SceneChangeSelectStage()
    {
       
    }
}
