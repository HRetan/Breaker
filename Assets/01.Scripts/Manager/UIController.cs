﻿using System;
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

    private void Awake()
    {
    }

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
        NetWorkManager.Instance.SetNet(false);
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

        GameObject.Find("Cancel").GetComponent<Button>().onClick.AddListener(() => DestroyUI(goUI.name));
        //GameObject.Find("Apply").GetComponent<Button>().onClick.AddListener(() => SaveApply());

        m_bUI = true;
    }

    public void LoadUI()
    {
        GameObject goUI;
        goUI = MonoBehaviour.Instantiate(Resources.Load("UI/LoadUI")) as GameObject;
        goUI.name = "dynamicUI";

        GameObject.Find("Cancel").GetComponent<Button>().onClick.AddListener(() => DestroyUI(goUI.name));
        GameObject.Find("Apply").GetComponent<Button>().onClick.AddListener(() => LoadApply());

        m_bUI = true;
    }

    public void InputTextUI(string strStyle)
    {
        GameObject goUI;
        goUI = MonoBehaviour.Instantiate(Resources.Load("UI/DynamicUI")) as GameObject;
        goUI.name = "dynamicUI";

        if(strStyle == "Load")
        {
            GameObject.Find("Cancel").GetComponent<Button>().onClick.AddListener(() => DestroyUI(goUI.name));
            GameObject.Find("Apply").GetComponent<Button>().onClick.AddListener(() => LoadApply());
        }
        else if(strStyle == "Modify")
        {
            InputField ttName = GameObject.Find("InputField").GetComponent<InputField>();
            ttName.contentType = InputField.ContentType.Password;
            GameObject.Find("Cancel").GetComponent<Button>().onClick.AddListener(() => DestroyUI(goUI.name));
            GameObject.Find("Apply").GetComponent<Button>().onClick.AddListener(() => ServerModify());
        }
        else if (strStyle == "Delete")
        {
            InputField ttName = GameObject.Find("InputField").GetComponent<InputField>();
            ttName.contentType = InputField.ContentType.Password;
            GameObject.Find("Cancel").GetComponent<Button>().onClick.AddListener(() => DestroyUI(goUI.name));
            GameObject.Find("Apply").GetComponent<Button>().onClick.AddListener(() => ServerDelete());
        }

        m_bUI = true;
    }

    public void OnSaveUI()
    {
        GameObject goUI;
        goUI = MonoBehaviour.Instantiate(Resources.Load("UI/PasswordUI")) as GameObject;
        goUI.name = "PasswordUI";
        
        GameObject.Find("Cancel").GetComponent<Button>().onClick.AddListener(() => DestroyUI(goUI.name));
        GameObject.Find("Apply").GetComponent<Button>().onClick.AddListener(() => SaveApply());

        m_bUI = true;

    }

    public void ReadLoadFilePath()
    {
        Debug.Log("로드 추가");
        InputField ttName = GameObject.Find("InputField").GetComponent<InputField>();
        ttName.text = SaveNLoad.GetInstance.GetStaticFileName();
        Debug.Log(ttName.text);
        Debug.Log(SaveNLoad.GetInstance.GetStaticFileName());
    }

    private void LoadApply()
    {
        Text ttName = GameObject.Find("TextName").GetComponent<Text>();
        //Debug.Log(ttName.text);

        SaveNLoad.GetInstance.LoadToolMap(ttName.text);
        Destroy(GameObject.Find("dynamicUI"));
        m_bUI = false;
    }

    public void DestroyUI(string strName)
    {
        Destroy(GameObject.Find(strName));
        m_bUI = false;
    }

    public void SaveApply()
    {
        if (!NetWorkManager.Instance.GetNet())
        {
            Text[] text = new Text[2];

            text[0] = GameObject.Find("Text(Title)").GetComponent<Text>();
            text[1] = GameObject.Find("Text(NickName)").GetComponent<Text>();

            InputField[] password = new InputField[2];

            password[0] = GameObject.Find("Password").GetComponent<InputField>();
            password[1] = GameObject.Find("ConfirmCheck").GetComponent<InputField>();

            if (password[0].text != password[1].text || password[0].text == "")
            {
                SendMessage("실패");
                return;
            }

            SaveNLoad.GetInstance.SaveMap(text[0].text);
            StartCoroutine(NetWorkManager.Instance.SaveNetData(text[0].text, text[1].text, password[0].text));
            Destroy(GameObject.Find("PasswordUI"));
            m_bUI = false;
            SceneChangeUserMap();
        }
        else
        {
            StartCoroutine(NetWorkManager.Instance.PutModifyMap());
        }
    }

    public void ServerModify()
    {
        //선택한 MapData의 패스워드와 입력한 패스워드가 같을 때 실행
        //ToolManager에서 실행 여부를 통해 확인후 불러오기 실행
        InputField ttName = GameObject.Find("InputField").GetComponent<InputField>();

        StartCoroutine(NetWorkManager.Instance.ModifyMap(ttName.text));
        Destroy(GameObject.Find("dynamicUI"));
        m_bUI = false;
    }

    public void ServerDelete()
    {
        //선택한 MapData의 패스워드와 입력한 패스워드가 같을 때 실행
        //같은면 삭제
        InputField ttName = GameObject.Find("InputField").GetComponent<InputField>();

        StartCoroutine(NetWorkManager.Instance.DeleteMap(ttName.text));
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

        GameObject.Find("Cancel").GetComponent<Button>().onClick.AddListener(() => DestroyUI(goDynamic.name));
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

    public void ClearGame(int iBlockCount, int iScore, int iPreScore = 0)
    {
        int iStyle = StageManager.GetInstance.GetStyle();

        switch(iStyle)
        {
            case 0:
                if (iBlockCount == 0)
                {
                    int iIndex = SaveNLoad.GetInstance.GetStaticStageNum();

                    StageManager.GetInstance.GetStageList()[iIndex].IsOpen = true;

                    if (iPreScore <= iScore)
                        StageManager.GetInstance.GetStageList()[iIndex - 1].iScore = iScore;

                    SaveNLoad.GetInstance.SaveStage();
                }
                SceneChangeStart();
                break;
            case 1:
            SceneChangeUserMap();
                break;
            case 2:
                if (iBlockCount == 0)
                {
                    Debug.Log("결과");
                    if (iPreScore <= iScore)
                    {
                    Debug.Log("PreScore : " + iPreScore + " Score : " + iScore);
                        StartCoroutine(NetWorkManager.Instance.PutNetData(NetWorkManager.Instance.GetID(), iScore.ToString()));
                    }
                }
                SceneChangeUserMap();
                break;
        }
    }

    public void ClearGame(int iBlockCount, int iStarCount, int iScore, int iPreScore = 0)
    {
        if (StageManager.GetInstance.GetStyle() == 0)
        {
            if (iBlockCount == 0)
            {
                int iIndex = SaveNLoad.GetInstance.GetStaticStageNum();

                StageManager.GetInstance.GetStageList()[iIndex].IsOpen = true;

                if(StageManager.GetInstance.GetStageList()[iIndex - 1].iStarCount <= iStarCount)
                 StageManager.GetInstance.GetStageList()[iIndex - 1].iStarCount = iStarCount;
                if (iPreScore <= iScore)
                    StageManager.GetInstance.GetStageList()[iIndex - 1].iScore = iScore;

                SaveNLoad.GetInstance.SaveStage();
            }
            SceneChangeStart();
        }
        else if(StageManager.GetInstance.GetStyle() == 1)
        {
            SceneChangeUserMap();
        }
    }

    public void ResultUI(int iBlockCount, int iStarCount, int iScore, int iPreScore = 0)
    {
        GameObject goDynamic = MonoBehaviour.Instantiate(Resources.Load("UI/GameOverUI")) as GameObject;
        m_bUI = true;

        goDynamic.name = "dynamicUI";

        if (iBlockCount == 0)
            GameObject.Find("Result").GetComponent<Text>().text = "Clear";
        else
            GameObject.Find("Result").GetComponent<Text>().text = "Failed";

        GameObject.Find("Check").GetComponent<Button>().onClick.AddListener(() => ClearGame(iBlockCount, iStarCount, iScore, iPreScore));
    }

    public void ResultUI(int iBlockCount, int iScore, int iPreScore = 0)
    {
        GameObject goDynamic = MonoBehaviour.Instantiate(Resources.Load("UI/GameOverUI")) as GameObject;
        m_bUI = true;

        goDynamic.name = "dynamicUI";

        if (iBlockCount == 0)
            GameObject.Find("Result").GetComponent<Text>().text = "Clear";
        else
            GameObject.Find("Result").GetComponent<Text>().text = "Failed";

        GameObject.Find("Check").GetComponent<Button>().onClick.AddListener(() => ClearGame(iBlockCount, iScore, iPreScore));
    }

    public void MenuUI()
    {
        GameObject goDynamic = MonoBehaviour.Instantiate(Resources.Load("UI/MenuUI")) as GameObject;
        m_bUI = true;

        goDynamic.name = "dynamicUI";

        GameObject.Find("Menu").GetComponent<Button>().onClick.AddListener(() => SceneChangeStart());
        GameObject.Find("Retry").GetComponent<Button>().onClick.AddListener(() => SceneChangeSelectStage());
        GameObject.Find("Return").GetComponent<Button>().onClick.AddListener(() => DestroyUI(goDynamic.name));
    }

    public void MapList()
    {
        GameObject goDynamic = MonoBehaviour.Instantiate(Resources.Load("UI/MapList")) as GameObject;
        m_bUI = true;

        goDynamic.name = "MyMapList";

        GameObject.Find("Return").GetComponent<Button>().onClick.AddListener(() => DestroyUI(goDynamic.name));
        GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(() => StageManager.GetInstance.PlayGameScene(SaveNLoad.GetInstance.GetStaticFileName()));
        GameObject.Find("Delete").GetComponent<Button>().onClick.AddListener(() => DeleteFile(SaveNLoad.GetInstance.GetStaticFileName()));
    }

    public void ServerMapList()
    {
        GameObject goDynamic = MonoBehaviour.Instantiate(Resources.Load("UI/NetMapList")) as GameObject;
        m_bUI = true;

        goDynamic.name = "NetMapList";

        GameObject.Find("Return").GetComponent<Button>().onClick.AddListener(() => DestroyUI(goDynamic.name));
        GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(() => StageManager.GetInstance.PlayGameScene());
        GameObject.Find("Delete").GetComponent<Button>().onClick.AddListener(() => InputTextUI("Delete"));
        GameObject.Find("Modify").GetComponent<Button>().onClick.AddListener(() => InputTextUI("Modify"));
    }

    public void EnableSelector()
    {

    }
}
