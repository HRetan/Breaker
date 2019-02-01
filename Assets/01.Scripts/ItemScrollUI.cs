using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScrollUI : MonoBehaviour {

    enum SCROLLSTATE
    {
        DRAG,
        NOTOUCH,
        BUTTON
    }

    private RectTransform m_rtPanel;    // ItemScrollPanel 좌표
    private Button[] m_rtItem;          // 현재 가지고 있는 아이템 블락
    private RectTransform m_rtCenter;   // 고정시킬 가운데 좌표
    private RectTransform m_rtSelectItem;

    private float[] m_fDis;
    private int m_iBttnDis;
    private int m_iMiniItemNum;
    private int m_iItemCount = 9;

    [SerializeField]
    private SCROLLSTATE m_eScroll = SCROLLSTATE.NOTOUCH;

    private ToolManager m_scToolManager;
    
	// Use this for initialization
	void Start () {
        m_rtItem = new Button[m_iItemCount];
        for (int i = 0; i < m_iItemCount; ++i)
        {
            int iIndex = i + 1;
            int iIndex2 = i;

            m_rtItem[i] = GameObject.Find("Item_" + iIndex.ToString()).GetComponent<Button>();
            m_rtItem[i].onClick.AddListener(() => FindScrollBlock(iIndex2));
        }

        m_rtPanel = GameObject.Find("ItemScrollPanel").GetComponent<RectTransform>();
        m_rtCenter = GameObject.Find("ItemCenterToCompare").GetComponent<RectTransform>();
        m_rtSelectItem = GameObject.Find("SelectItem").GetComponent<RectTransform>();

        int bttnLenght = m_rtItem.Length;
        m_fDis = new float[bttnLenght];

        m_iBttnDis = (int)Mathf.Abs(m_rtItem[1].GetComponent<RectTransform>().anchoredPosition.x - m_rtItem[0].GetComponent<RectTransform>().anchoredPosition.x);
        m_scToolManager = GameObject.Find("ToolManager").GetComponent<ToolManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (m_eScroll != SCROLLSTATE.BUTTON)
        {
            for (int i = 0; i < m_rtItem.Length; ++i)
            {
                m_fDis[i] = Mathf.Abs(m_rtCenter.transform.position.x - m_rtItem[i].transform.position.x);
            }

            float minDistance = Mathf.Min(m_fDis);

            for (int i = 0; i < m_rtItem.Length; ++i)
            {
                if (minDistance == m_fDis[i])
                {
                    m_iMiniItemNum = i;
                }
            }
        }

        switch (m_eScroll)
        {
            case SCROLLSTATE.DRAG:
                break;
            case SCROLLSTATE.NOTOUCH:
                LerpToBttn(m_iMiniItemNum * -m_iBttnDis);
                //  LerpToSelect(m_iMinBttnNum * m_iBttnDis);
                m_scToolManager.SetItemNum(m_iMiniItemNum);
                break;
            case SCROLLSTATE.BUTTON:
                LerpToBttn(m_iMiniItemNum * -m_iBttnDis);
                m_scToolManager.SetItemNum(m_iMiniItemNum);
                break;
        }

        LerpToSelect(m_iMiniItemNum * m_iBttnDis);
    }

    void LerpToBttn(int position)
    {
        float newX = Mathf.Lerp(m_rtPanel.anchoredPosition.x, position, Time.deltaTime * 2f);
        Vector2 newPosition = new Vector2(newX, m_rtPanel.anchoredPosition.y);

        m_rtPanel.anchoredPosition = newPosition;
    }

    void LerpToSelect(int position)
    {
        float newX = Mathf.Lerp(m_rtSelectItem.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(newX, m_rtPanel.anchoredPosition.y);

        m_rtSelectItem.anchoredPosition = newPosition;
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
        m_iMiniItemNum = iIndex;
        m_eScroll = SCROLLSTATE.BUTTON;
    }
}
