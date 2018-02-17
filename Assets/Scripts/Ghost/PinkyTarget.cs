using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyTarget : MonoBehaviour {
    public Transform m_pacman;
    public Transform m_dirPos;
    private GhostTrack m_track;
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
        if (Vector2.Distance(m_pacman.position, transform.position) > 15)
        {
            m_track.m_target = m_dirPos;
        }
        else
        {
            m_track.m_target = m_pacman;
        }
	}
}
