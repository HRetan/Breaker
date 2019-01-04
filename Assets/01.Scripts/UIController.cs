using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

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
                    Debug.LogError("싱글톤 인스턴스 생성 실패");
                }
            }
            return Instance;
        }
    }

    private bool m_bUI = false;

    public void SceneChangeTitle()
    {
        SceneManager.LoadScene("Title");
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
        Debug.Log(ttName.text);

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

        GameObject.Find("Cancel").GetComponent<Button>().onClick.AddListener(() => DestroyUI());
        GameObject.Find("Apply").GetComponent<Button>().onClick.AddListener(() => EndGame());
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void ClearGame()
    {
        StageManager.GetInstance.GetStageList()[SaveNLoad.GetInstance.GetStaticStageNum()].IsOpen = true;
        SaveNLoad.GetInstance.SaveStage();
        SceneChangeStart();
    }
}
