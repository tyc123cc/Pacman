using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : Ghost {
    public Transform[] m_wayPoints;
    public int m_curPos = 0;
    public PacmanMove m_pacman;
    public float m_speed = 0.3f;
    public bool m_isToNear = false;
    public Transform nearWayPoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (m_pacman.m_pacmanState == PacmanMove.Pacman_Normal)
        {
            m_isToNear = false;
            if (transform.position != m_wayPoints[m_curPos].position)
            {
                Vector2 p = Vector2.MoveTowards(transform.position, m_wayPoints[m_curPos].position, m_speed);
                transform.position = p;
            }
            else
            {

                m_curPos = (m_curPos + 1) % m_wayPoints.Length;
            }

            Vector2 dir = m_wayPoints[m_curPos].position - transform.position;
            GetComponent<Animator>().SetFloat("DirX", dir.x);
            GetComponent<Animator>().SetFloat("DirY", dir.y);
        }
        else if(m_pacman.m_pacmanState == PacmanMove.Pacman_Invicible)
        {
            if (m_isToNear == false)
            {
                nearWayPoint = EscapeToNearWayPoint();
                m_isToNear = true;
            }
           Vector2 p = Vector2.MoveTowards(transform.position, nearWayPoint.position, m_speed);
           transform.position = p;
           if (Vector2.Distance(transform.position, nearWayPoint.position) < 0.1f)
           {
               nearWayPoint = ChangeFarWayPoint(m_pacman.transform,nearWayPoint);//到达路点，变更下一个路点
               Vector2 dir = nearWayPoint.position - transform.position;
               GetComponent<Animator>().SetFloat("DirX", dir.x);
               GetComponent<Animator>().SetFloat("DirY", dir.y);
           }
        }
    }

  


    //void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.tag == "Player")
    //        Destroy(collider.gameObject);
    //}
}
