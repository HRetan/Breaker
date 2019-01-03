using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SaveNLoad.GetInstance.LoadMap("Stage" + SaveNLoad.GetInstance.GetStaticStageNum().ToString());
    }

    // Update is called once per frame
    void Update () {

    }

}
