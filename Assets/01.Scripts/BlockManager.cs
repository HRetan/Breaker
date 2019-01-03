using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> m_listBlock;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F5))
        //{
        //    m_SaveNLoad.LoadMap();
        //}
    }

    public List<GameObject> GetListBlock()
    {
        return m_listBlock;
    }

    public void CreateBlock(GameObject parentObject, Vector3 position, int blockID)
    {
        GameObject goBlock = null;

        switch (blockID)
        {
            case 0:
                goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Gray)")) as GameObject;
                goBlock.name = "Breaker_Block(Gray)";
                break;
            case 1:
                goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Blue)")) as GameObject;
                goBlock.name = "Breaker_Block(Blue)";
                break;
            case 2:
                goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Brown)")) as GameObject;
                goBlock.name = "Breaker_Block(Brown)";
                break;
            case 3:
                goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Purple)")) as GameObject;
                goBlock.name = "Breaker_Block(Purple)";
                break;
            case 4:
                goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Red)")) as GameObject;
                goBlock.name = "Breaker_Block(Red)";
                break;
            case 5:
                goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Orange)")) as GameObject;
                goBlock.name = "Breaker_Block(Orange)";
                break;
            case 6:
                goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Green)")) as GameObject;
                goBlock.name = "Breaker_Block(Green)";
                break;
        }

        if (goBlock != null)
        {
            goBlock.GetComponent<Transform>().position = position;
            goBlock.GetComponent<BlockController>().SetBlockID(blockID);
            goBlock.transform.parent = parentObject.transform;
        }
    }
}
