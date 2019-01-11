﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> m_listBlock;

    // Use this for initialization
    void Start()
    {
        if(StageManager.GetInstance.GetStage())
            SaveNLoad.GetInstance.LoadMap(SaveNLoad.GetInstance.GetStaticFileName());
        else
            SaveNLoad.GetInstance.LoadUserMap(SaveNLoad.GetInstance.GetStaticFileName());

    }

    // Update is called once per frame
    void Update()
    {
        if (UIController.GetInstance.GetUI())
            return;

        if(m_listBlock.Count == 0)
        {
            UIController.GetInstance.ResultUI(m_listBlock);
        }

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
            case 7:
                goBlock = MonoBehaviour.Instantiate(Resources.Load("Block/Breaker_Block(Pink)")) as GameObject;
                goBlock.name = "Breaker_Block(Pink)";
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
