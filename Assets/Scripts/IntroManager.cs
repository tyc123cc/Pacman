using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public Transform[] m_canvas;
    public int m_curIndex = 0;
    public Transform m_advanceButton;
    public Transform m_retreateButton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (m_curIndex == 0)
        {
            m_retreateButton.gameObject.SetActive(false);
        }
        else if (m_curIndex == m_canvas.Length - 1)
        {
            m_advanceButton.gameObject.SetActive(false);
        }
        else
        {
            m_retreateButton.gameObject.SetActive(true);
            m_advanceButton.gameObject.SetActive(true);

        }
	}

    public void Advance()
    {
        m_curIndex++;
        ShowCanvas(m_curIndex);
    }

    public void Retreat(){
        m_curIndex--;
        ShowCanvas(m_curIndex);
    }

    private void ShowCanvas(int curIndex)
    {
        for (int i = 0; i < m_canvas.Length; i++)
        {
            if (curIndex == i)
            {
                m_canvas[i].gameObject.SetActive(true);
            }
            else
            {
                m_canvas[i].gameObject.SetActive(false);
            }
        }
    }

    public void GoHome()
    {
        SceneManager.LoadScene("Start");
    }
}
