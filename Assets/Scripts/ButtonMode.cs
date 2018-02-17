using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMode : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GameManager.m_ButtonMode == true)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
