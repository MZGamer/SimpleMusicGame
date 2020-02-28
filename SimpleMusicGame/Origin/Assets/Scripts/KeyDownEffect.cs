using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDownEffect : MonoBehaviour {
    public GameObject Line1, Line2, Line3, Line4;
    public Transform[] test;
    public static bool Reflection;
    // Use this for initialization
    void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        if (Reflection)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Instantiate(Line1, test[0].position, Quaternion.identity);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                Instantiate(Line2, test[1].position, Quaternion.identity);
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                Instantiate(Line3, test[2].position, Quaternion.identity);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                Instantiate(Line4, test[3].position, Quaternion.identity);
            }
        }

    }
}
