using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyTrackPoint : MonoBehaviour {
    public Transform m_blinky;
    public Transform m_inkyPos;
    public Transform m_pacman;
    private GhostTrack m_track;
    public Transform m_trackPos;
	// Use this for initialization
	void Start () {
        m_track = transform.GetComponent<GhostTrack>();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_pacman == null)
        {
            return;
        }
        if (m_blinky == null)
        {
            m_track.m_target = m_pacman;
        }
        else
        {
            if (Vector2.Distance(m_pacman.position, transform.position) > 15)
            {
                //做InkyPos和Bliky之间的延长线
                float k = (m_inkyPos.position.y - m_blinky.position.y) / (m_inkyPos.position.x - m_blinky.position.x);
                float distance = Vector2.Distance(m_inkyPos.position, m_blinky.position);
                float angle = Mathf.Atan(k);
                float x = distance * Mathf.Cos(angle) + m_inkyPos.position.x;
                float y = distance * Mathf.Sin(angle) + m_inkyPos.position.y;
                m_trackPos.position = new Vector2(x, y);
                m_track.m_target = m_trackPos;
            }
            else
            {
                m_track.m_target = m_pacman;
            }
        }
	}
}
