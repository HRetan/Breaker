using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapList : MonoBehaviour
{
    enum SearchType
    {
        TITLE,
        OWNER,
        END
    }

    private List<string> m_listFile = new List<string>();
    private List<GameObject> m_listFileOb = new List<GameObject>();

    private GameObject m_goSelector = null;
    [SerializeField]
    private int m_iFileIndex = 0;
    [SerializeField]
    private int m_iCurIndex = 0;
    private SearchType m_eSearchType = SearchType.TITLE;

    public Text m_goSearchText;

    // Use this for initialization
    void Start()
    {
        if (gameObject.name == "MyMapList")
        {
            SaveNLoad.GetInstance.FindFileStage(m_listFile);

            for (int i = 0; i < m_listFile.Count; ++i)
            {
                CreateFile(m_listFile[i], i);
            }

            if (m_listFile.Count != 0)
            {
                SaveNLoad.GetInstance.SetStaticFileName(m_listFileOb[m_iFileIndex].name);
                m_goSelector = Instantiate(Resources.Load("UI/FileSelector")) as GameObject;
                MoveSelector(m_goSelector);
            }
        }
        else
        {
            StartCoroutine(NetWorkManager.Instance.AllMapLoad());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_listFile.Count == 0)
            return;

        if(m_iCurIndex != m_iFileIndex)
        {
            MoveSelector(m_goSelector);
            m_iCurIndex = m_iFileIndex;
        }
    }

    void CreateFile(string strName, int iIndex)
    {
        GameObject goFile = MonoBehaviour.Instantiate(Resources.Load("UI/ListName")) as GameObject;

        goFile.transform.SetParent(GameObject.Find("Contents").transform);
        goFile.name = strName;
        Debug.Log(goFile.name);
        goFile.GetComponentInChildren<Text>().text = strName;
        goFile.transform.localScale = new Vector3(1, 1, 1);
        m_listFileOb.Add(goFile);

        goFile.GetComponent<Button>().onClick.AddListener(() => SaveNLoad.GetInstance.SetStaticFileName(goFile.name));
        goFile.GetComponent<Button>().onClick.AddListener(() => FileIndex(iIndex));

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MapTool"))
            goFile.GetComponent<Button>().onClick.AddListener(() => UIController.GetInstance.ReadLoadFilePath());
    }

    void FileIndex(int iIndex)
    {
        m_iFileIndex = iIndex;
    }

    void MoveSelector(GameObject goSelector)
    {
        goSelector.transform.parent = m_listFileOb[m_iCurIndex].transform;
        goSelector.name = "FileSelector";
        goSelector.GetComponent<RectTransform>().position = m_listFileOb[m_iFileIndex].GetComponent<RectTransform>().position;
        goSelector.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
    }

    public void TitleCheck(bool bIsTitle)
    {
        if (bIsTitle)
            m_eSearchType = SearchType.TITLE;
    }

    public void OwnerCheck(bool bIsOnwer)
    {
        if (bIsOnwer)
            m_eSearchType = SearchType.OWNER;
    }

    public void SearchBtn()
    {
        if(m_eSearchType == SearchType.TITLE)
        {

        }
        else if(m_eSearchType == SearchType.OWNER)
        {
            StartCoroutine(NetWorkManager.Instance.SearchMapLoad(m_goSearchText.text));
        }
    }
}
