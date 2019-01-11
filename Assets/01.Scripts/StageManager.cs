﻿using System.Collections;
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
    }

    [SerializeField]
    static private List<Stage> m_listStage = new List<Stage>();

    public static bool w_bStage = false;

	// Use this for initialization
	public void Initialize () {

        for (int i = 0; i < 35; ++i)
        {
            int iIndex = i + 1;

            Stage stage = new Stage();
            stage.iStageIndex = iIndex;
            // True와 False 에 따른 이미지 변경 할 곳.
            if (i == 0)
                stage.IsOpen = true;
            else
                stage.IsOpen = false;

            m_listStage.Add(stage);
        }
        //GameObject.Find("Stage" + 1).GetComponent<Button>().onClick.AddListener(() => PlayGameScene(1));
        SaveNLoad.GetInstance.LoadStage();
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

    public void SetAddStageList(int iIndex, int iBlockIndex, bool isCheck)
    {
        m_listStage[iIndex].IsOpen = isCheck;
        m_listStage[iIndex].iStageIndex = iBlockIndex;
    }

    public void PlayGameScene(int iIndex)
    {
        w_bStage = true;
        if (!m_listStage[iIndex - 1].IsOpen)
            return;

        SaveNLoad.GetInstance.SetStaticStageNum(iIndex);
        SaveNLoad.GetInstance.SetStaticFileName("Stage" + iIndex);
        UIController.GetInstance.SceneChangeSelectStage();
    }

    public void PlayGameScene(string strName)
    {
        w_bStage = false;
        SaveNLoad.GetInstance.SetStaticFileName(strName);
        UIController.GetInstance.SceneChangeSelectStage();
    }
}
