using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanDot : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    { 
        if (collider.tag.CompareTo("Player") == 0)
        {
            GameManager.Instant.AddScore(1);
            GameManager.Instant.EatDot();
            Destroy(gameObject);
        }
    }
}
