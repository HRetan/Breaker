using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance = null;

    [SerializeField] private AudioSource m_asBgm;
    [SerializeField] private Scene m_Scene;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        m_Scene = SceneManager.GetActiveScene();
        if (GetComponent<AudioSource>() != null)
            m_asBgm = GetComponent<AudioSource>();
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
