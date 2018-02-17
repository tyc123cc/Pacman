using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostOut : MonoBehaviour {
    public Transform[] m_wanderPoint;
    public int m_outCount;
    int index = 0;
    public float m_speed = 0.1f;
	// Use this for initialization
	void Start () {
        GetComponent<GhostTrack>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instant.m_eatedDot < m_outCount)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, m_wanderPoint[index].position, m_speed);
            transform.position = p;
            if (Vector2.Distance(transform.position, m_wanderPoint[index].position) < 0.1f)
            {
                index = (index + 1) % m_wanderPoint.Length;
                GetComponent<Animator>().SetFloat("DirX", m_wanderPoint[index].position.x - transform.position.x);
            }
        }
        else
        {
            GetComponent<GhostTrack>().enabled = true;
            
            this.enabled = false;
        }
	}
}
