using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeTrack : MonoBehaviour {
    public Transform m_pacman;
    public List<Transform> m_corner;
    private GhostTrack m_track;
    private bool m_caculateCorner = false;
	// Use this for initialization
	void Start () {
        m_track = GetComponent<GhostTrack>();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_pacman == null)
        {
            return;
        }
        if (Vector2.Distance(m_pacman.position, transform.position) > 15)//距离大于15时，以吃豆人为目标
        {
            m_track.m_target = m_pacman;
            m_caculateCorner = false;
        }
        else if(m_caculateCorner == false)//距离小于15时，走向最近的角落
        {
            float distance = float.MaxValue;
            Transform nearCorner = transform;
            foreach (var corner in m_corner)
            {
                if(Vector2.Distance(corner.position,transform.position) < distance){
                    distance = Vector2.Distance(corner.position,transform.position);
                    nearCorner = corner;
                }
            }
            m_track.m_target = nearCorner;
            m_caculateCorner = true;
        }
	}
}
