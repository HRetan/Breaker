using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//using GoogleMobileAds.Api;

public class GameManager : MonoBehaviour {

    private Scene m_Scene;
  //  static bool m_bIsAdsBanner = false;

    void Start()
    {
        m_Scene = SceneManager.GetActiveScene();

        //if(m_Scene == SceneManager.GetSceneByName("Title_Stage"))
        //{
        //    if (!m_bIsAdsBanner)
        //        RequestBanner();
        //}
    }

    void Update()
    {
        if (m_Scene == SceneManager.GetSceneByName("Title"))
            QuitApp();
        else if (m_Scene == SceneManager.GetSceneByName("Title_Stage") || m_Scene == SceneManager.GetSceneByName("MapTool"))
            ReturnTitle();
        else if (m_Scene == SceneManager.GetSceneByName("InGame"))
            Option();
    }

    void QuitApp()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIController.GetInstance.TitleQuitGame();
        }
    }

    void ReturnTitle()
    {
        //백버튼 누를시 돌아가기

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIController.GetInstance.SceneChangeTitle();
        }
    }

    void Option()
    {
        //백버튼 누를시 돌아가기

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!UIController.GetInstance.GetUI())
                UIController.GetInstance.MenuUI();
            else
            {
                //UIController.GetInstance.DestroyUI();
                Debug.Log("종료");
            }
        }
    }

//    // 애드몹 설정
//    private void RequestBanner()

//    {
//        Debug.Log("들어오나여");
//#if UNITY_ANDROID

//        string AdUnitID = "ca-app-pub-8475559446490862/7818072623";

//#else

//        string AdUnitID = "unDefind";

//#endif

//        BannerView banner = new BannerView(AdUnitID, AdSize.Banner, AdPosition.Top);



//        AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("0DE6974EAB3FD00430C1BD4763A64F21").Build();

//        banner.LoadAd(request);

//        m_bIsAdsBanner = true;

//    }
}

