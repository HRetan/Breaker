using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollUI : MonoBehaviour {

    enum SCROLLSTATE
    {
        DRAG,
        NOTOUCH,
        BUTTON
    }

    private RectTransform m_rtPanel;     // ScrollPanel 좌표
    private Button[] m_rtBlock;          // 현재 가지고 있는 버튼블락
    private RectTransform m_rtCenter;    // 고정시킬 가운데 좌표
    private RectTransform m_rtSelectBlock;

    private float[] m_fDis;             // 센터와의 거리
    private int m_iBttnDis;             // 블락 간의 거리를 유지
    private int m_iMinBttnNum;          // 고정 시킨 버튼의 인덱스 번호

    [SerializeField]
    private SCROLLSTATE m_eScroll = SCROLLSTATE.NOTOUCH;

    private ToolManager m_scToolManager;

    // Use this for initialization
    void Start () {
        m_rtBlock = new Button[9];
        for (int i = 0; i < 9; ++i)
        {
            int iIndex = i + 1;
            int iIndex2 = i;

            m_rtBlock[i] = GameObject.Find("Block_" + iIndex.ToString()).GetComponent<Button>();
            m_rtBlock[i].onClick.AddListener(() => FindScrollBlock(iIndex2));
        }
        m_rtPanel = GameObject.Find("ScrollPanel").GetComponent<RectTransform>();
        m_rtCenter = GameObject.Find("CenterToCompare").GetComponent<RectTransform>();
        m_rtSelectBlock = GameObject.Find("SelectBlock").GetComponent<RectTransform>();

        int bttnLenght = m_rtBlock.Length;
        m_fDis = new float[bttnLenght];

        m_iBttnDis = (int)Mathf.Abs(m_rtBlock[1].GetComponent<RectTransform>().anchoredPosition.x - m_rtBlock[0].GetComponent<RectTransform>().anchoredPosition.x);
        m_scToolManager = GetComponent<ToolManager>();
	}

    // Update is called once per frame
    void Update()
    {
        if(m_eScroll != SCROLLSTATE.BUTTON)
        {
            for (int i = 0; i < m_rtBlock.Length; ++i)
            {
                m_fDis[i] = Mathf.Abs(m_rtCenter.transform.position.x - m_rtBlock[i].transform.position.x);
            }

            float minDistance = Mathf.Min(m_fDis);

            for (int i = 0; i < m_rtBlock.Length; ++i)
            {
                if (minDistance == m_fDis[i])
                {
                    m_iMinBttnNum = i;
                }
            }
        }

        switch (m_eScroll)
        {
            case SCROLLSTATE.DRAG:
                break;
            case SCROLLSTATE.NOTOUCH:
                LerpToBttn(m_iMinBttnNum * -m_iBttnDis);
              //  LerpToSelect(m_iMinBttnNum * m_iBttnDis);
                m_scToolManager.SetBlockNum(m_iMinBttnNum);
                break;
            case SCROLLSTATE.BUTTON:
                LerpToBttn(m_iMinBttnNum * -m_iBttnDis);
                m_scToolManager.SetBlockNum(m_iMinBttnNum);
                break;
        }

        LerpToSelect(m_iMinBttnNum * m_iBttnDis);

    }

    void LerpToBttn(int position)
    {
        float newX = Mathf.Lerp(m_rtPanel.anchoredPosition.x, position, Time.deltaTime * 2f);
        Vector2 newPosition = new Vector2(newX, m_rtPanel.anchoredPosition.y);

        m_rtPanel.anchoredPosition = newPosition;
    }

    void LerpToSelect(int position)
    {
        float newX = Mathf.Lerp(m_rtSelectBlock.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(newX, m_rtPanel.anchoredPosition.y);

        m_rtSelectBlock.anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        //m_bDragging = true;
        m_eScroll = SCROLLSTATE.DRAG;
    }

    public void EndDrag()
    {
        //m_bDragging = false;
        m_eScroll = SCROLLSTATE.NOTOUCH;
    }

    public void FindScrollBlock(int iIndex)
    {
        m_iMinBttnNum = iIndex;
        m_eScroll = SCROLLSTATE.BUTTON;
    }
}
