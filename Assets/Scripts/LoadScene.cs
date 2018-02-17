using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    public Transform m_pacman;
    public Transform m_startPos;
    public Transform m_endPos;
    public List<Transform> m_eated;
    public float m_speed = 0.1f;
    private bool m_loadEnd = false;
	// Use this for initialization
	void Start () {
        StartCoroutine(Loading());
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 p = Vector2.MoveTowards(m_pacman.position, m_endPos.position, m_speed);
        m_pacman.position = p;
        if (Vector2.Distance(m_pacman.position, m_endPos.position) < 0.1)
        {
            m_pacman.position = m_startPos.position;
            foreach (var item in m_eated)
            {
                item.gameObject.SetActive(true);
            }
            m_loadEnd = true;
        }
	}

    private IEnumerator Loading()
    {
        string SceneName = PlayerPrefs.GetString("Scene");
        print(SceneName);
        AsyncOperation op = SceneManager.LoadSceneAsync(SceneName);
        op.allowSceneActivation = false;
        while (op.progress < 0.9f || m_loadEnd == false)
        {

            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;  
    }
}
