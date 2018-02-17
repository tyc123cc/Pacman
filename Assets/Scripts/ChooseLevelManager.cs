using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevelManager : MonoBehaviour
{
    public  int m_highestLevel;
    private int m_nowIndex = 1;
    public GameObject m_advanceButton;
    public GameObject m_retreatButton;
    public Text m_levelText;
    // Use this for initialization
    void Start()
    {
      
        if (PlayerPrefs.HasKey("HighestLevel"))
        {
            m_highestLevel = PlayerPrefs.GetInt("HighestLevel");
        }
        else
        {
            m_highestLevel = 1;
        }
        m_nowIndex = m_highestLevel;
        m_levelText.text = "第" + m_nowIndex + "关";
       
    }

    // Update is called once per frame
    void Update()
    {
        if (m_highestLevel == 1)
        {
            m_advanceButton.SetActive(false);
            m_retreatButton.SetActive(false);
        }
        else if (m_nowIndex == m_highestLevel)
        {
            m_advanceButton.SetActive(false);
            m_retreatButton.SetActive(true);
        }
        else if (m_nowIndex == 1)
        {
            m_retreatButton.SetActive(false);
            m_advanceButton.SetActive(true);
        }
        else
        {
            m_advanceButton.SetActive(true);
            m_retreatButton.SetActive(true);
        }
    }

    public void GoHome()
    {
        SceneManager.LoadScene("Start");
    }

    public void Advanve()
    {
        if (m_nowIndex < m_highestLevel)
        {
            m_nowIndex++;
            m_levelText.text = "第" + m_nowIndex + "关";
            if (m_nowIndex == m_highestLevel)
            {
                m_advanceButton.SetActive(false);
            }
        }
    }

    public void Retreat()
    {
        if (m_nowIndex > 1)
        {
            m_nowIndex--;
            m_levelText.text = "第" + m_nowIndex + "关";
          
        }
    }

    public void OK()
    {
        PlayerPrefs.SetString("Scene", m_nowIndex.ToString().PadLeft(2,'0'));
        GameManager.m_score = 0;
        SceneManager.LoadScene("Load");
    }
}
