using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldScript : MonoBehaviour {

    void Awake()
    {
        QualitySettings.vSyncCount = 0; // vsync 사용안함 
        Application.targetFrameRate = 60; // 30 프레임 고정.
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
