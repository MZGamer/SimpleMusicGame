﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLines : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TestLine0000"))
            {
                Destroy(other.gameObject);
                //other.gameObject.SetActive(false);   
            }
    }
}
