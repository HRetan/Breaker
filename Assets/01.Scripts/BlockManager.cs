using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> m_listBlock;
    [SerializeField]
    private int m_iBlockCount = 0;
    public ScoreUI m_scScore;

    // Use this for initialization
    void Start()
    {
        if(StageManager.GetInstance.GetStage())
            SaveNLoad.GetInstance.LoadMap(SaveNLoad.GetInstance.GetStaticFileName());
        else
            SaveNLoad.GetInstance.LoadUserMap(SaveNLoad.GetInstance.GetStaticFileName());

        SkinManager.GetInstance.MySkin();
        m_scScore = FindObjectOfType(typeof(ScoreUI)) as ScoreUI;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (UIController.GetInstance.GetUI())
            return;

        if(m_iBlockCount == 0)
        {
            if (StageManager.GetInstance.GetStage())
            {
                m_scScore.StarCheck();
                UIController.GetInstance.ResultUI(m_iBlockCount, m_scScore.GetStarCount(), m_scScore.GetScore());
            }
            else
                UIController.GetInstance.ResultUI(m_iBlockCount, m_scScore.GetScore());

        }

    }

    void SetBlock(GameObject goBlock, string strName, int iLife, int iCount)
    {
        goBlock.name = strName;
        goBlock.GetComponent<BlockController>().SetLife(iLife);
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

    public void CreateBlock(GameObject parentObject, Vector3 position, int blockID)
    {
        GameObject goBlock = null;

        switch (blockID)
        {
            case 0:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Gray)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Gray)", 1, 1);
                break;
            case 1:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Blue)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Blue)", 1, 1);
                break;
            case 2:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Brown)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Brown)", 1, 1);
                break;
            case 3:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Purple)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Purple)", 1, 1);
                break;
            case 4:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Red)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Red)", 1, 1);
                break;
            case 5:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Orange)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Orange)", 1, 1);
                break;
            case 6:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Green)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Green)", 1, 1);
                break;
            case 7:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Pink)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Pink)", 1, 1);
                break;
            case 8:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(SkyBlue)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(SkyBlue)", 2, 1);
                break;
            case 9:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Yellow)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Yellow)", 1, 1);
                break;
            case 10:
                goBlock = MonoBehaviour.Instantiate(Resources.Load(UIController.GetInstance.GetPath() + "(Black)")) as GameObject;
                SetBlock(goBlock, "Breaker_Block(Black)", 1, 0);
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
}
