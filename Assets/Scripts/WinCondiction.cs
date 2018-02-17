using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondiction : MonoBehaviour {
    public GameObject m_winCanvas;
    public static bool m_isWin;

    public static WinCondiction Instant;

    public Transform m_SoundTransform;
    public AudioClip m_winAudio;
    public AudioClip m_deathAudio;
    private bool m_winAudioPlayed = false;
	// Use this for initialization
	void Start () {
        Instant = this;
        m_isWin = false;
        //GameObject[] g = GameObject.FindGameObjectsWithTag("Dot");
        //foreach (var item in g)
        //{
        //    Destroy(item);
        //}


	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("Dot") == null)
        {
            m_isWin = true;
            int level = int.Parse(PlayerPrefs.GetString("Scene"));
            if (PlayerPrefs.HasKey("HighestLevel"))
            {
                if (level > PlayerPrefs.GetInt("HighestLevel"))
                {
                    PlayerPrefs.SetInt("HighestLevel", level);
                }
            }
            else
            {
                PlayerPrefs.SetInt("HighestLevel", level);
            }
            
            m_winCanvas.SetActive(true);
            if (m_winAudioPlayed == false)
            {
                AudioSource audio = m_SoundTransform.GetComponent<AudioSource>();
                audio.PlayOneShot(m_winAudio);
                m_winAudioPlayed = true;
            }     
        }
	}

  

    public void Death()
    {
        AudioSource audio = m_SoundTransform.GetComponent<AudioSource>();
        audio.PlayOneShot(m_deathAudio);
    }
}
