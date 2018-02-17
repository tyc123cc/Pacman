using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperDot : MonoBehaviour {
    public PacmanMove m_pacman;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.CompareTo("Player") == 0)
        {
            GameManager.Instant.AddScore(10);
            m_pacman.ChangeState(PacmanMove.Pacman_Invicible);
            Destroy(gameObject);
        }
    }
}
