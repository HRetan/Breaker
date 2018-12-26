using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {

    [System.Serializable]
    public class Stage
    {
        public int iStageIndex;
        public bool IsOpen;
        public GameObject goStage;
    }

    [SerializeField]
    private List<Stage> m_listStage = new List<Stage>();
    private SaveNLoad m_saveNLoad;

    public static int staticInt = 0;

	// Use this for initialization
	void Start () {
        m_saveNLoad = GetComponent<SaveNLoad>();
        m_saveNLoad.LoadStage();
        for (int i = 0; i < 35; ++i)
        {
            int iIndex = i + 1;
            m_listStage[i].goStage = GameObject.Find("Stage" + (i + 1));
            m_listStage[i].goStage.GetComponent<Button>().onClick.AddListener(() => PlayGameScene(iIndex));
        }
        //GameObject.Find("Stage" + 1).GetComponent<Button>().onClick.AddListener(() => PlayGameScene(1));

    }

    // Update is called once per frame
    void Update () {
    }

    public void SceneChangeTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public List<Stage> GetStageList()
    {
        return m_listStage;
    }

    public void SetAddStageList(int iIndex, bool isCheck)
    {
        Stage stage = new Stage();
        stage.iStageIndex = iIndex;
        stage.IsOpen = isCheck;

        m_listStage.Add(stage);
    }

    public void PlayGameScene(int iIndex)
    {
        Debug.Log(iIndex);
        if (!m_listStage[iIndex - 1].IsOpen)
            return;

        Debug.Log("스테이지" + iIndex);
        m_saveNLoad.SetStaticStageNum(iIndex);
        SceneManager.LoadScene(2);
    }
}
