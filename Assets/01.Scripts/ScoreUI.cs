using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour {

    public BlockManager m_scBlockManager;

    private TextMeshProUGUI m_tmpScore;
    private TextMeshProUGUI m_tmpMaxScore;
    private TextMeshProUGUI m_tmpTime;
    private float m_fMaxTime = 60;
    private int m_iScore = 0;
    private int m_iMaxScore = 0;
    private int m_iBlockCount;

    private int m_iStar = 3;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start () {
        m_scBlockManager = FindObjectOfType(typeof(BlockManager)) as BlockManager;
        m_tmpScore = GameObject.Find("Score(Point)").GetComponent<TextMeshProUGUI>();
        m_tmpMaxScore = GameObject.Find("MaxScore(Point)").GetComponent<TextMeshProUGUI>();
        m_tmpTime = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        m_iBlockCount = m_scBlockManager.GetBlockCount();

        m_iMaxScore = StageManager.GetInstance.GetStageList()[SaveNLoad.GetInstance.GetStaticStageNum() - 1].iScore;
    }
	
	// Update is called once per frame
	void Update () {
        if (UIController.GetInstance.GetUI())
            return;

        if (m_fMaxTime <= 0)
            m_fMaxTime = 0f;
        else
            m_fMaxTime -= Time.deltaTime;

        m_tmpTime.text = ((int)m_fMaxTime).ToString();
        m_tmpScore.text = m_iScore.ToString();
        m_tmpMaxScore.text = m_iMaxScore.ToString();

    }

    public void ScoreCheck(int iCount)
    {
        m_iScore += 100 * iCount;
        if (m_iMaxScore <= m_iScore)
            m_iMaxScore = m_iScore;
    }

    public void StarCheck()
    {
        if (m_fMaxTime == 0)
            m_iStar -= 1;
        if (m_iBlockCount * 150 >= m_iScore)
            m_iStar -= 1;

        Debug.Log("max : " + m_iBlockCount * 150);
        Debug.Log("Current : " + m_iScore);
        Debug.Log("Star : " + m_iStar);
    }

    public int GetStarCount()
    {
        return m_iStar;
    }

    public int GetMaxScore()
    {
        return m_iMaxScore;
    }

    public int GetScore()
    {
        return m_iScore;
    }

    public void SetScore(int iMaxScore)
    {
        m_iMaxScore = iMaxScore;
    }

}
