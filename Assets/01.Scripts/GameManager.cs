using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private SaveNLoad m_saveNLoad;
	// Use this for initialization
	void Start () {
        m_saveNLoad = GameObject.Find("GameManager").GetComponent<SaveNLoad>();
        m_saveNLoad.BuildLoad();
        //m_saveNLoad.LoadMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
