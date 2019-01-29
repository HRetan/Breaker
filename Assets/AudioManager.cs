using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

    private AudioSource m_asBgm;
    private GameObject m_goAudio;
    private Scene m_Scene;

	// Use this for initialization
	void Start () {
        m_goAudio = GameObject.Find("AudioManager");
        m_Scene = SceneManager.GetActiveScene();
        if (m_goAudio.GetComponent<AudioSource>() != null)
            m_asBgm = m_goAudio.GetComponent<AudioSource>();

        DontDestroyOnLoad(m_asBgm);

    }

    // Update is called once per frame
    void Update () {
        if(m_Scene == SceneManager.GetSceneByName("InGame") && m_asBgm.enabled)
        {
            m_asBgm.enabled = false;
        }
	}
}
