using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {
    public float cd;
    public Animator StartAni;
    public bool Starting;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        cd += Time.deltaTime;
        if (cd > 1 && Input.GetKey(KeyCode.Space) && !Starting)
        {
            Starting = true;
            Invoke("ChangeScene", 1.5f);
            StartAni.SetBool("Starting",true);
        }

        if (cd >= 30)
        {
            Invoke("ChangeScene", 1.5f);
            StartAni.SetBool("Starting", true);
        }
	}

    void ChangeScene()
    {
        if (Starting)
            SceneManager.LoadScene("Gate");
        else
        {
            SceneManager.LoadScene("Stage");
            StageManager.isDemo = true;
        }



    }
}
