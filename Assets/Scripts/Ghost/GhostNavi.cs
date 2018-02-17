using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostNavi : Ghost {
    public Transform m_pacman;
    private List<Vector2> path;
    public float m_speed = 0.3f;
    private float m_Timer = 0;
	// Use this for initialization
	void Start () {
        //path = NavMesh2D.GetSmoothedPath(transform.position, m_pacman.position);

	}
	
	// Update is called once per frame
	void Update () {
        if (m_pacman == null)
        {
            return;
        }
        m_Timer += Time.deltaTime;
        if (m_Timer >= 0.5f)
        {
            //path = NavMesh2D.GetSmoothedPath(transform.position, m_pacman.position);
            m_Timer = 0;
            Vector2 dir = path[1] - (Vector2)transform.position;
            if (Mathf.Abs(dir.x) - Mathf.Abs(dir.y) > 0)
            {
                GetComponent<Animator>().SetFloat("DirX", dir.x);
                GetComponent<Animator>().SetFloat("DirY", 0);
            }
            else
            {
                GetComponent<Animator>().SetFloat("DirX", 0);
                GetComponent<Animator>().SetFloat("DirY", dir.y);
            }
        }
	       

        if(path != null && path.Count != 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, path[0], m_speed);
            if(Vector2.Distance(transform.position,path[0]) < 0.01f)
            {
                path.RemoveAt(0);
            }
        }
	}
    //void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.tag == "Player")
    //        Destroy(collider.gameObject);
    //}
	
}
