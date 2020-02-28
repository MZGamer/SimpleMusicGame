using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sensor : MonoBehaviour {
    [Header("判定相關")]
    public bool CanDes;
    public int IDonLine;
    public float Presstime;
    public float Correcttime;
    public float Endtime;
    public int Line;
    public bool Hold;
    public bool HoldStarted;
    public List<Note> Notes= new List<Note>();
    public StageCreator Stage;
    public float stagetime;
    [Header("分數相關")]
    public Text ScoreT;
    public Text ComboT;
    public int Score;
    public int Combo;
    public Text GradeT;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        stagetime = StageManager.StageTime;
 
        if (StageManager.Auto)
        {
            AutoPlay();
        }
        else
        {
        }


    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Note0"))
        {
            Destroy(other.gameObject);
            //other.gameObject.SetActive(false);   
        }
    }




    public void AutoPlay()
    {
        if (!Hold)
        {
            if (Correcttime - stagetime <= 0)
            {
                StageManager.Combo++;
                StageManager.Perfect++;
                Debug.Log("perfect");
                Destroy(gameObject);
            }
        }
        else
        {
            if (!HoldStarted)
            {
                if (Correcttime - stagetime <= 0)
                {
                    StageManager.Combo++;
                    StageManager.Perfect++;
                    Debug.Log("perfect");
                    HoldStarted = true;
                }
            }
            else
            {
                if (Endtime - stagetime <= 0)
                {
                    Debug.Log("done");
                    Destroy(gameObject);
                }
            }

        }
    }
}
