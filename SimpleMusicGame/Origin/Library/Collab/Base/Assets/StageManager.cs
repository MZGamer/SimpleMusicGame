using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {
    [Header("關卡物件")]
    public AudioSource Audio;
    public GameObject Note1;
    public GameObject Note2;
    public GameObject Note3;
    public GameObject Note4;
    public GameObject HoldNote1;
    public GameObject HoldNote2;
    public GameObject HoldNote3;
    public GameObject HoldNote4;
    public Text ScoreT;
    public Text ComboT;

    [Header("音樂資料")]
    public static StageCreator Stage;
    public AudioClip BGM;
    public List<Note> Notes = new List<Note>();
    public float falltime;

    [Header("玩家資料")]
    public int Score;
    public int Combo;

    [Header("神的恩惠")]
    public float st_time;
    public float passtime;






    // Use this for initialization
    void Start () {
        Audio.clip = Stage.Stage.BGM;



        falltime = 25f / (float)MovingNotes.Speed;
        Audio.time = st_time;
        passtime = st_time;
        Audio.Play();
        for (int i = 0; i < Notes.Count; i++)
        {
            if (Notes[i].time - falltime>= st_time)
            {
                if (Notes[i].Line1.active)
                {
                    if (!Notes[i].Line1.Hold)
                    {

                    }
                    else
                    {

                    }
                }

            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        passtime += Time.deltaTime;





    }
}
