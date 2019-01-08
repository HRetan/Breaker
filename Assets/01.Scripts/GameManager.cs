using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    void Start()
    {
    }

    void Update()
    {
        if (UIController.GetInstance.GetUI())
            return;

        Scene scene = SceneManager.GetActiveScene();
        if (scene == SceneManager.GetSceneByName("Title"))
            QuitApp();
        else if (scene == SceneManager.GetSceneByName("Title_Stage") || scene == SceneManager.GetSceneByName("MapTool"))
            ReturnTitle();
        else if (scene == SceneManager.GetSceneByName("InGame"))
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
            UIController.GetInstance.MenuUI();
        }
    }
}

