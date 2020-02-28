using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Gate : MonoBehaviour {
    public Animator GateAni;
    public bool CanStart;
    public static bool LoginCom;


	// Use this for initialization
	void Start () {
        LoginManager.Login = false;
        KeyDownEffect.Reflection = true;
        StageSlect.Dif = 0;
        StageSlect.NowSongID = 0;
        StageSlect.Track = 1;
        TotalResult.Totals = new List<StageInfWrite>();
        StageManager.isDemo = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (CanStart && Input.GetKeyDown(KeyCode.Space))
        {
            GateAni.Play("EndHowtoPlay");
            Invoke("gotoSongSlect", 7);
        }
        if (LoginCom)
        {
            LoginCom = false;
            Invoke("gotoSongSlect", 11);
        }
        if (LoginManager.Login)
        {
            StageSlect.NowSongID = LoginManager.Save.LastSongCount;
            StageSlect.Dif = LoginManager.Save.LastDif;
            StageManager.NoteSpeed = LoginManager.Save.SpeedSet;
            KeyDownEffect.Reflection = LoginManager.Save.ReflectionSet;
        }
        }
        void gotoSongSlect()
    {
        SceneManager.LoadScene("SongSlect");
    }
}
