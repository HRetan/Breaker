using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class UIController : MonoBehaviour
{

    private static UIController Instance = null;

    public static UIController GetInstance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType(typeof(UIController)) as UIController;

                if (Instance == null)
                {
                    Debug.LogError("UIController     생성 실패");
                }
            }
            return Instance;
        }
    }

    static private string w_strBlockPath = "Block/Ver2/Breaker_Block_Ver2";

    public string GetPath()
    {
        return w_strBlockPath;
    }

    private bool m_bUI = false;

    void Update()
    {
        //Block Image 변경
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (w_strBlockPath == "Block/Ver1/Breaker_Block")
                w_strBlockPath = "Block/Ver2/Breaker_Block_Ver2";
            else
                w_strBlockPath = "Block/Ver1/Breaker_Block";
        }
    }

    public void SceneChangeTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void SceneChangeStart()
    {
        SceneManager.LoadScene("Title_Stage");
    }
    public void SceneChangeUserMap()
    {
        SceneManager.LoadScene("Title_UserMap");
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
        SceneManager.LoadScene("InGame");
    }

    public void SaveUI()
    {
        GameObject goUI;
        goUI = MonoBehaviour.Instantiate(Resources.Load("UI/SaveUI")) as GameObject;
        goUI.name = "dynamicUI";

        GameObject.Find("Cancel").GetComponent<Button>().onClick.AddListener(() => DestroyUI());
        GameObject.Find("Apply").GetComponent<Button>().onClick.AddListener(() => SaveApply());

        m_bUI = true;
    }

    public void LoadUI()
    {
        GameObject goUI;
        goUI = MonoBehaviour.Instantiate(Resources.Load("UI/LoadUI")) as GameObject;
        goUI.name = "dynamicUI";

        GameObject.Find("Cancel").GetComponent<Button>().onClick.AddListener(() => DestroyUI());
        GameObject.Find("Apply").GetComponent<Button>().onClick.AddListener(() => LoadApply());

        m_bUI = true;
    }

    private void LoadApply()
    {
        Text ttName = GameObject.Find("LoadName").GetComponent<Text>();
        //Debug.Log(ttName.text);

        SaveNLoad.GetInstance.LoadToolMap(ttName.text);
        Destroy(GameObject.Find("dynamicUI"));
        m_bUI = false;
    }

    public void DestroyUI()
    {
        Destroy(GameObject.Find("dynamicUI"));
        m_bUI = false;
    }

    public void SaveApply()
    {
        Text ttName = GameObject.Find("SaveName").GetComponent<Text>();

        SaveNLoad.GetInstance.SaveMap(ttName.text);
        Destroy(GameObject.Find("dynamicUI"));
        m_bUI = false;
    }

    public void SetUI(bool bUI)
    {
        m_bUI = bUI;
    }

    public bool GetUI()
    {
        return m_bUI;
    }

    public void TitleQuitGame()
    {
        GameObject goDynamic = MonoBehaviour.Instantiate(Resources.Load("UI/QuitApp")) as GameObject;

        goDynamic.name = "dynamicUI";

        m_bUI = true;

        GameObject.Find("Cancel").GetComponent<Button>().onClick.AddListener(() => DestroyUI());
        GameObject.Find("Apply").GetComponent<Button>().onClick.AddListener(() => EndGame());
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void DeleteFile(string strFileName)
    {
        FileInfo fileDel;

#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
        fileDel = new FileInfo(Application.streamingAssetsPath + "/" + strFileName + ".json");
#elif UNITY_ANDROID
        fileDel = new FileInfo(Application.persistentDataPath + "/" + strFileName + ".json");
#elif UNITY_IOS
#endif

        if (fileDel.Exists) // 삭제할 파일이 있는지
        {
            fileDel.Delete(); // 없어도 에러안남
            Destroy(GameObject.Find(strFileName));
            Debug.Log("파일 삭제");
        }
        else
            Debug.Log("파일 삭제 실패");
    }

    public void ClearGame(int iBlockCount, int iScore)
    {
        if (StageManager.GetInstance.GetStage())
        {
            if (iBlockCount == 0)
            {
                int iIndex = SaveNLoad.GetInstance.GetStaticStageNum();

                StageManager.GetInstance.GetStageList()[iIndex].IsOpen = true;

                if (StageManager.GetInstance.GetStageList()[iIndex - 1].iScore <= iScore)
                    StageManager.GetInstance.GetStageList()[iIndex - 1].iScore = iScore;

                SaveNLoad.GetInstance.SaveStage();
            }
            SceneChangeStart();
        }
        else
        {
            SceneChangeUserMap();
        }
    }

    public void ClearGame(int iBlockCount, int iStarCount, int iScore)
    {
        if (StageManager.GetInstance.GetStage())
        {
            if (iBlockCount == 0)
            {
                int iIndex = SaveNLoad.GetInstance.GetStaticStageNum();

                StageManager.GetInstance.GetStageList()[iIndex].IsOpen = true;

                if(StageManager.GetInstance.GetStageList()[iIndex - 1].iStarCount <= iStarCount)
                 StageManager.GetInstance.GetStageList()[iIndex - 1].iStarCount = iStarCount;
                if (StageManager.GetInstance.GetStageList()[iIndex - 1].iScore <= iScore)
                    StageManager.GetInstance.GetStageList()[iIndex - 1].iScore = iScore;

                SaveNLoad.GetInstance.SaveStage();
            }
            SceneChangeStart();
        }
        else
        {
            SceneChangeUserMap();
        }
    }

    public void ResultUI(int iBlockCount, int iStarCount, int iScore)
    {
        GameObject goDynamic = MonoBehaviour.Instantiate(Resources.Load("UI/GameOverUI")) as GameObject;
        m_bUI = true;

        goDynamic.name = "dynamicUI";

        if (iBlockCount == 0)
            GameObject.Find("Result").GetComponent<Text>().text = "Clear";
        else
            GameObject.Find("Result").GetComponent<Text>().text = "Failed";

        GameObject.Find("Check").GetComponent<Button>().onClick.AddListener(() => ClearGame(iBlockCount, iStarCount, iScore));
    }

    public void ResultUI(int iBlockCount, int iScore)
    {
        GameObject goDynamic = MonoBehaviour.Instantiate(Resources.Load("UI/GameOverUI")) as GameObject;
        m_bUI = true;

        goDynamic.name = "dynamicUI";

        if (iBlockCount == 0)
            GameObject.Find("Result").GetComponent<Text>().text = "Clear";
        else
            GameObject.Find("Result").GetComponent<Text>().text = "Failed";

        GameObject.Find("Check").GetComponent<Button>().onClick.AddListener(() => ClearGame(iBlockCount, iScore));
    }

    public void MenuUI()
    {
        GameObject goDynamic = MonoBehaviour.Instantiate(Resources.Load("UI/MenuUI")) as GameObject;
        m_bUI = true;

        goDynamic.name = "dynamicUI";

        GameObject.Find("Menu").GetComponent<Button>().onClick.AddListener(() => SceneChangeStart());
        GameObject.Find("Retry").GetComponent<Button>().onClick.AddListener(() => SceneChangeSelectStage());
        GameObject.Find("Return").GetComponent<Button>().onClick.AddListener(() => DestroyUI());
    }

    public void MapList()
    {
        GameObject goDynamic = MonoBehaviour.Instantiate(Resources.Load("UI/MapList")) as GameObject;
        m_bUI = true;

        goDynamic.name = "dynamicUI";

        GameObject.Find("Return").GetComponent<Button>().onClick.AddListener(() => DestroyUI());
        GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(() => StageManager.GetInstance.PlayGameScene(SaveNLoad.GetInstance.GetStaticFileName()));
        GameObject.Find("Delete").GetComponent<Button>().onClick.AddListener(() => DeleteFile(SaveNLoad.GetInstance.GetStaticFileName()));
    }

   public void EnableSelector()
    {

    }
}
