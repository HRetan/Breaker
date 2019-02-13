using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {

    private static StageManager Instance = null;

    public static StageManager GetInstance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType(typeof(StageManager)) as StageManager;

                if (Instance == null)
                {
                    Debug.LogError("싱글톤 인스턴스 생성 실패");
                }
            }
            return Instance;
        }
    }

    [System.Serializable]
    public class Stage
    {
        public bool IsOpen;
        public int iStageIndex;
        public int iStarCount;
        public int iScore;
    }

    public enum PLAYSTYLE
    {
        STAGE,
        MYMAP,
        SERVER
    }

    [SerializeField]
    static private List<Stage> m_listStage = new List<Stage>();

    public static bool w_bStage = false;
    public static PLAYSTYLE w_eStyle = PLAYSTYLE.STAGE;

	// Use this for initialization
	public void Initialize (List<GameObject> listBlock) {

        for (int i = 0; i < 35; ++i)
        {
            int iIndex = i + 1;

            Stage stage = new Stage();
            stage.iStageIndex = iIndex;
            stage.iStarCount = 0;
            stage.iScore = 0;
            // True와 False 에 따른 이미지 변경 할 곳.
            if (i == 0)
            {
                stage.IsOpen = true;
                listBlock[0].transform.Find("Lock").gameObject.SetActive(false);
            }
            else
                stage.IsOpen = false;

            m_listStage.Add(stage);
        }
        SaveNLoad.GetInstance.LoadStage(listBlock);
    }

    public void SceneChangeTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public List<Stage> GetStageList()
    {
        return m_listStage;
    }

    public bool GetStage()
    {
        return w_bStage;
    }

    public int GetStyle()
    {
        switch(w_eStyle)
        {
            case PLAYSTYLE.STAGE:
                return 0;
            case PLAYSTYLE.MYMAP:
                return 1;
            case PLAYSTYLE.SERVER:
                return 2;
        }
        return 0;
    }

    public void SetAddStageList(int iIndex, bool isCheck, int iBlockIndex, int iStarCount, int iScore, GameObject goBlock)
    {
        m_listStage[iIndex].IsOpen = isCheck;
        m_listStage[iIndex].iStageIndex = iBlockIndex;
        m_listStage[iIndex].iStarCount = iStarCount;
        m_listStage[iIndex].iScore = iScore;
        if (isCheck)
        {
            goBlock.transform.Find("Lock").gameObject.SetActive(false);
            Sprite spt = null;
            switch(iStarCount)
            {
                case 0:
                    spt = Resources.Load("Score/Star_0", typeof(Sprite)) as Sprite;
                    break;
                case 1:
                    spt = Resources.Load("Score/Star_1", typeof(Sprite)) as Sprite;
                    break;
                case 2:
                    spt = Resources.Load("Score/Star_2", typeof(Sprite)) as Sprite;
                    break;
                case 3:
                    spt = Resources.Load("Score/Star_3", typeof(Sprite)) as Sprite;
                    break;
            }
            goBlock.transform.Find("Star").gameObject.GetComponent<Image>().sprite = spt;
        }

    }

    public void PlayGameScene(int iIndex)
    {
        w_bStage = true;
        w_eStyle = PLAYSTYLE.STAGE;
        if (!m_listStage[iIndex - 1].IsOpen)
            return;

        SaveNLoad.GetInstance.SetStaticStageNum(iIndex);
        SaveNLoad.GetInstance.SetStaticFileName("Stage" + iIndex);
        UIController.GetInstance.SceneChangeSelectStage();
    }

    public void PlayGameScene(string strName)
    {
        w_bStage = false;
        w_eStyle = PLAYSTYLE.MYMAP;

        SaveNLoad.GetInstance.SetStaticFileName(strName);
        UIController.GetInstance.SceneChangeSelectStage();
    }

    public void PlayGameScene()
    {
        w_eStyle = PLAYSTYLE.SERVER;
        UIController.GetInstance.SceneChangeSelectStage();
    }
}
