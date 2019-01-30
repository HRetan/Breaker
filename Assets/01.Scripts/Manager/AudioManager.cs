using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioSource m_asBgm;
    [SerializeField] private Scene m_Scene;

    // Use this for initialization
    void Start () {
        m_Scene = SceneManager.GetActiveScene();
        if (GetComponent<AudioSource>() != null)
            m_asBgm = GetComponent<AudioSource>();

        DontDestroyOnLoad(this);

    }

    // Update is called once per frame
    void Update () {
        if (m_Scene != SceneManager.GetActiveScene())
            m_Scene = SceneManager.GetActiveScene();

        if(m_Scene == SceneManager.GetSceneByName("InGame"))
        {
            m_asBgm.enabled = false;
        }
        else
        {
            if (!m_asBgm.enabled)
                m_asBgm.enabled = true;
        }
    }
}
