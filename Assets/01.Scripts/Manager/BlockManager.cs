using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> m_listBlock;
    [SerializeField]
    private int m_iBlockCount = 0;
    private int m_iStyle = 0;

    private bool m_bIsPlay = false;
    private float m_fTime = 0f;
    public ScoreUI m_scScore;

    private void Awake()
    {
        m_iStyle = StageManager.GetInstance.GetStyle();
    }
    // Use this for initialization
    void Start()
    {
        if (m_iStyle == 0)
            SaveNLoad.GetInstance.LoadMap(SaveNLoad.GetInstance.GetStaticFileName());
        else if (m_iStyle == 1)
            SaveNLoad.GetInstance.LoadUserMap(SaveNLoad.GetInstance.GetStaticFileName());
        else
            StartCoroutine(NetWorkManager.Instance.LoadNetUserMap());

        SkinManager.GetInstance.MySkin();
        m_scScore = FindObjectOfType(typeof(ScoreUI)) as ScoreUI;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (UIController.GetInstance.GetUI())
            return;

        if (m_fTime <= 1f)
        {
            m_fTime += Time.deltaTime;
        }
        else
            m_bIsPlay = true;


        if(m_iBlockCount == 0 && m_bIsPlay)
        {
            Debug.Log("벌써 들어온다고?!");
            switch(m_iStyle)
            {
                case 0:
                    int iIndex = SaveNLoad.GetInstance.GetStaticStageNum();
                    m_scScore.StarCheck();
                    UIController.GetInstance.ResultUI(m_iBlockCount, m_scScore.GetStarCount()
                        , m_scScore.GetScore(), StageManager.GetInstance.GetStageList()[iIndex - 1].iScore);
                    break;
                case 1:
                    UIController.GetInstance.ResultUI(m_iBlockCount, m_scScore.GetScore());
                    break;
                case 2:
                    UIController.GetInstance.ResultUI(m_iBlockCount, m_scScore.GetScore(), NetWorkManager.Instance.GetPreScore());
                    break;
            }
        }
    }

    void SetBlock(GameObject goBlock, string strName, int iLife, int iCount, int iItemID)
    {
        goBlock.name = strName;
        goBlock.GetComponent<BlockController>().SetLife(iLife);
        goBlock.GetComponent<BlockController>().SetItemID(iItemID);
        m_iBlockCount += iCount;
    }

    public List<GameObject> GetListBlock()
    {
        return m_listBlock;
    }

    public int GetBlockCount()
    {
        return m_iBlockCount;
    }

    public void SetBlockCount()
    {

        m_iBlockCount -= 1;
    }

    public void CreateBlock(GameObject parentObject, Vector3 position, int blockID, int iItemID)
    {
        GameObject goBlock = null;

        if(blockID == 11)
        {
            blockID = Random.Range(0, 10);
        }

        switch (blockID)
        {
            case 0:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Red)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Red)", 1, 1, SetItemIndex(iItemID));
                break;
            case 1:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Orange)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Orange)", 1, 1, SetItemIndex(iItemID));
                break;
            case 2:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Yellow)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Yellow)", 1, 1, SetItemIndex(iItemID));
                break;
            case 3:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Green)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Green)", 1, 1, SetItemIndex(iItemID));
                break;
            case 4:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Blue)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Blue)", 1, 1, SetItemIndex(iItemID));
                break;
            case 5:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Indigo)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Indigo)", 1, 1, SetItemIndex(iItemID));
                break;
            case 6:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Purple)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Purple)", 1, 1, SetItemIndex(iItemID));
                break;
            case 7:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Pink)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Pink)", 1, 1, SetItemIndex(iItemID));
                break;
            case 8:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(SkyBlue)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(SkyBlue)", 2, 1, SetItemIndex(iItemID));
                break;
            case 9:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Gray)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Gray)", 1, 1, SetItemIndex(iItemID));
                break;
            case 10:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Black)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Black)", 1, 0, SetItemIndex(iItemID));
                break;
        }

        if (goBlock != null)
        {
            goBlock.GetComponent<Transform>().position = position;
            goBlock.GetComponent<BlockController>().SetBlockID(blockID);
            goBlock.transform.parent = parentObject.transform;
        }

        m_listBlock.Add(goBlock);
    }

    int SetItemIndex(int iItemID)
    {
        switch(iItemID)
        {
            case 0:
                return 1;
            case 1:
                return 0;
            case 2:
                return 3;
            case 3:
                return 4;
            case 4:
                return 7;
            case 5:
                return 5;
            case 6:
                return 6;
            case 7:
                return 2;
        }

        return Random.Range(0, 50);
    }

}
