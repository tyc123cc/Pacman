using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioText : MonoBehaviour {
    public Slider m_audioSlider;
    public Text m_text;
	// Use this for initialization
	void Start () {
        m_text = GetComponent<Text>();
        if (PlayerPrefs.HasKey("AudioSize"))
        {
            m_audioSlider.value = PlayerPrefs.GetInt("AudioSize") / 100f;
        }
        else
        {
            m_audioSlider.value = 1;
        }
        m_text.text = ((int)(m_audioSlider.value * 100)).ToString();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowAudioSize()
    {
        int audioSize = ((int)(m_audioSlider.value * 100));
        m_text.text = audioSize.ToString();
        PlayerPrefs.SetInt("AudioSize", audioSize);
        BackgrounMusic.m_audioSize = audioSize;
    }
}
