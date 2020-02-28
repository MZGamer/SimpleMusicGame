using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {
    [Header("關卡物件")]
    public AudioSource Audio;
    public List<GameObject> Note = new List<GameObject>();
    public List<GameObject> HoldNote = new List<GameObject>();
    public GameObject TestLine, TestLineX4;
    public List<newSensor> NewSensors = new List<newSensor>();
    public Animator YumeHANABIUtage;
    public Animator EffectAni;
    public Animator DemoEnd;
    //public Text ScoreT;
    //public Text ComboT;

    [Header("音樂資料")]
    public StageCreator Stage;
    public static StageCreator MusicStage;
    public AudioClip BGM;
    public string SongName;
    public string SongAuthor;
    public int Level;
    public Sprite SongImage;
    public List<Note> Notes = new List<Note>();
    public float falltime;
    public static float StageTime;
    public float BPM;
    public float SecondPerBeat;
    public float TestLineT;
    public float Offset;
    public int FullCombo;
    public int SongID00;
    [Header("玩家資料")]
    public float VisableScore;
    float AddScore, EndScore;
    public static int Score;

    public static int MaxCombo;
    public static int Combo;
    public static int Perfect;
    public bool FC;
    public static int Good;
    public static int Miss;
    public static int HoldBouns;
    public float Speed;
    public static float NoteSpeed;

    [Header("關卡判定")]
    public float PerfectT;
    public float GoodT;
    public float MissT;
    public static float PerfectTs;
    public static float GoodTs;
    public static float MissTs;
    public static int[] LCountS = new int[4];
    public static int[] RealLCountS = new int[4];
    int[] LCount = new int[4] { 0, 0, 0, 0 };

    [Header("UI")]
    public Text ComboT;
    public Text ScoreT;
    public Text SongNameT;
    public Text SongAuthorT;
    public Text LevelT;
    public Image SongBG;

    [Header("神的恩惠")]
    public bool Autoplay;
    public bool TestMode;
    public bool Fake;
    public static bool Testing;
    public bool GameStarted;
    public static bool Auto;
    public float st_time;
    public float passtime;

    [Header("Demo")]
    public List<StageCreator> Demo = new List<StageCreator>();
    public static bool isDemo;

    [Header("Pause")]
    public GameObject Pause;
    public bool GameFinish;
    public bool Pausing;
    public string Scene;

    // Use this for initialization
    void Start () {
        SongID00 = StageSlect.SongID00;
        Debug.Log(SongID00);
        EndScore = 0;
        if (TestMode)
        {
            KeyDownEffect.Reflection = true;
            MusicStage = Stage;

            Offset += 0.25f;
            NoteSpeed = Speed;
            Debug.Log(NoteSpeed);
            StageSet();
            GameStart();
        }
        else if (isDemo)
        {
            Stage = Demo[Random.Range(0,Demo.Count-1)];
            NoteSpeed = 30;
            Autoplay = true;
            StageSet();
            Invoke("GameStart", 8);
        }
        else
        {
            Stage = MusicStage;
            StageSet();
            Offset += 0.25f;
            Invoke("GameStart", 8);

        }
        //Debug.Log(MovingNotes.Speed);
    }
	
	// Update is called once per frame
	void Update () {
        if (GameStarted)
        {
            passtime += Time.deltaTime;
            StageTime += Time.deltaTime;
            if (!Audio.isPlaying && !Testing && !Pausing)
            {
                StartCoroutine("GotoResult");
                GameFinish = true;
            }
            
            if (GameStarted && !GameFinish && Input.GetKeyDown(KeyCode.Escape) && !isDemo)
            {
                if (!Pause.activeInHierarchy)
                {
                    Pause.SetActive(true);
                    Time.timeScale = 0;
                    Pausing = true;
                    Audio.Pause();
                }

            }
        }
        if (MaxCombo < Combo)
        {
            MaxCombo = Combo;
        }

        Score = (int)(((Perfect * 1.0 + Good * 0.6) / FullCombo) * 920000 + ((MaxCombo * 1.0F) / (FullCombo * 1.0F)) * 80000) + HoldBouns;
        if(!isDemo)
            ComboT.text = Combo.ToString();
        else
            ComboT.text = "Demo";


        if (EndScore < Score)
        {
            EndScore = Score;
            AddScore = (EndScore - VisableScore)*2 * Time.deltaTime;
        }
        if(VisableScore + AddScore > EndScore)
        {
            VisableScore = EndScore;
        }
        else
        {
            VisableScore += AddScore;
        }
        int S,count = 0;
        S = (int)VisableScore;
        string Zero = "";
        while (S >= 10)
        {
            count++;
            S = S / 10;
        }
        count++;
        for(int i = 0;i< 7 -count; i++)
        {
            Zero = Zero + "0";
        }
        ScoreT.text =Zero +  ((int)VisableScore).ToString();
        //if((int)(Time.deltaTime/TestLineT)>=0) {Debug.Log(Time.deltaTime); }
       
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log(passtime);
        }

        if (isDemo && Input.anyKeyDown)
        {
            StartCoroutine("GotoResult");
        }
        if(!NewSensors[0].HoldStarted && !NewSensors[1].HoldStarted && !NewSensors[2].HoldStarted && !NewSensors[3].HoldStarted && !FC && FullCombo == Combo && !Testing && !isDemo)
        {
            FC = true;
            EffectAni.Play("FullCombo");


        }


    }

    void StageSet()
    {
        LCountS[0] = 0;
        LCountS[1] = 0;
        LCountS[2] = 0;
        LCountS[3] = 0;
        Auto = Autoplay;
        BGM = Stage.Stage.BGM;
        Audio.clip = BGM;
        Notes = Stage.Stage.Notes;
        BPM = Stage.Stage.BPM;
        SecondPerBeat = 240f / BPM;
        TestLineT = 60f / BPM;
        FullCombo = Stage.Stage.FullCombo;
        Offset = Stage.Stage.Offset;
        SongNameT.text = Stage.Stage.SongName;
        SongAuthorT.text = Stage.Stage.Author;
        SongBG.sprite = Stage.Stage.SongImage;
        LevelT.text = "LV " + Stage.Stage.level;
        Testing = TestMode;



        PerfectTs = PerfectT;
        GoodTs = GoodT;
        MissTs = MissT;

        Score = 0;
        Perfect = 0;
        Good = 0;
        Miss = 0;
        HoldBouns = 0;
        MaxCombo = 0;
        Combo = 0;


        falltime = 42f / NoteSpeed;
        Audio.time = st_time;
        passtime = st_time;
        StageTime = st_time;
    }

    void GameStart()
    {



        Audio.Play();
        if (st_time != 0)
        {
            Invoke("WaitOffset",0);
        }
        else
            Invoke("WaitOffset", Offset - falltime);
        if (SongID00 == 14)
        {
        Invoke("YumeUAni0", Offset + 43 * SecondPerBeat - st_time);
        Invoke("YumeUAni1", Offset + 51 * SecondPerBeat - st_time);
        Invoke("YumeUAni2", Offset + 16 * SecondPerBeat - st_time);
        Invoke("YumeUAni3", Offset + 20 * SecondPerBeat - st_time);
        Invoke("YumeUAni4", Offset + 32 * SecondPerBeat - st_time);
        Invoke("YumeUAni5", Offset + 59 * SecondPerBeat - st_time);
        }




        for (int i = 0; i < Notes.Count; i++)
        {
            //Debug.Log(i);
            if ((Notes[i].time * SecondPerBeat + Offset) - falltime >= st_time)
            {
                //Debug.Log(i);
                bool[] NA = new bool[4] { Notes[i].Line1.active, Notes[i].Line2.active, Notes[i].Line3.active, Notes[i].Line4.active };

                bool[] HN = new bool[4] { Notes[i].Line1.Hold, Notes[i].Line2.Hold, Notes[i].Line3.Hold, Notes[i].Line4.Hold };

                float[] ET = new float[4] { Notes[i].Line1.endtime * SecondPerBeat, Notes[i].Line2.endtime * SecondPerBeat, Notes[i].Line3.endtime * SecondPerBeat, Notes[i].Line4.endtime * SecondPerBeat };

                for (int j = 0; j <= 3; j++)
                {
                    if (NA[j])
                    {


                        if (!HN[j])
                        {
                            StartCoroutine(Notecreate(j + 1, (((Notes[i].time * SecondPerBeat + Offset) - falltime) - st_time), (Notes[i].time * SecondPerBeat + Offset)));
                        }
                        else
                        {
                            //Debug.Log(Notes[i].Line1.Hold);
                            StartCoroutine(HoldNotecreate(j + 1, (((Notes[i].time * SecondPerBeat + Offset) - falltime) - st_time), ((ET[j] + Offset) - (Notes[i].time * SecondPerBeat + Offset)), (Notes[i].time * SecondPerBeat + Offset), (ET[j] + Offset)));
                        }
                    }

                }

            }
        }
        GameStarted = true;


    }

    IEnumerator Notecreate(int Line, float time, float Notetime)
    {
        yield return new WaitForSeconds(time);

        sensor Sensor = Note[Line].GetComponent<sensor>();
        Sensor.Endtime = 0;
        Sensor.Hold = false;
        Sensor.Correcttime = Notetime;
        Sensor.Line = Line;
        Sensor.IDonLine = LCount[Line -1];
        LCount[Line - 1]++;

        GameObject ob = Instantiate(Note[Line], Note[Line].transform.position, Quaternion.identity);
        NewSensors[Line - 1].Line.Add(ob.GetComponent<sensor>());

    }
    IEnumerator HoldNotecreate(int Line, float time, float holdlong, float Notetime, float endtime)
    {
        yield return new WaitForSeconds(time);
        sensor Sensor = HoldNote[Line].GetComponent<sensor>();

        Sensor.Correcttime = Notetime;
        Sensor.Line = Line;
        Sensor.Hold = true;
        Sensor.Endtime = endtime;
        HoldNote[Line].transform.localScale = new Vector3(3.75f, NoteSpeed * holdlong , 1);
        Sensor.IDonLine = LCount[Line - 1];
        LCount[Line - 1]++;
        GameObject ob =  Instantiate(HoldNote[Line], HoldNote[Line].transform.position, Quaternion.identity);

        NewSensors[Line-1].Line.Add(ob.GetComponent<sensor>());
    }


    void WaitOffset()
    {
        if(st_time == 0){
            InvokeRepeating("TestLineCreate", 0, TestLineT);
            InvokeRepeating("TestLineCreateX4", 0, TestLineT * 4f);
        }
        else
        {
            Invoke("STTestLineCreate", st_time % TestLineT + TestLineT);
        }
    }

    void TestLineCreate()
    {
        Instantiate(TestLine, TestLine.transform.position, Quaternion.identity);
    }
    void TestLineCreateX4()
    {
        Instantiate(TestLineX4, TestLineX4.transform.position, Quaternion.identity);
    }
    void STTestLineCreate()
    {
        Instantiate(TestLine, TestLine.transform.position, Quaternion.identity);
        Instantiate(TestLineX4, TestLineX4.transform.position, Quaternion.identity);
        InvokeRepeating("TestLineCreate", 0, TestLineT);
        InvokeRepeating("TestLineCreateX4", 0, TestLineT * 4f);
    }
    IEnumerator GotoResult(){
        if (isDemo)
        {
            DemoEnd.Play("DemoEnd");
            CancelInvoke();
            yield return new WaitForSeconds(1.5f);

            SceneManager.LoadScene("Start");

        }
        else
        {
            yield return new WaitForSeconds(3);
            CancelInvoke();
            EffectAni.SetBool("StageFinish", true);
            yield return new WaitForSeconds(9f);
            SceneManager.LoadScene("Result");
        }

    }

    public void Restart()
    {
        CancelInvoke();
        Time.timeScale = 1;
        SceneManager.LoadScene(Scene);

    }

    public void BackToSlect()
    {
        CancelInvoke();
        Time.timeScale = 1;
        SceneManager.LoadScene("SongSlect");
    }

    public void Continue()
    {
        Time.timeScale = 1;
        Pause.SetActive(false);
        Audio.Play();
        Pausing = false ;
    }




    public void YumeUAni0() {
        YumeHANABIUtage.Play("YumeHANABI0");
        Debug.Log("turn0");
    }
    public void YumeUAni1()
    {
        YumeHANABIUtage.Play("YumeHANABI001");
        Debug.Log("turn1");
    }
    public void YumeUAni2()
    {
        YumeHANABIUtage.Play("YumeHANABI2");
        Debug.Log("turn2");
    }
    public void YumeUAni3()
    {
        YumeHANABIUtage.Play("YumeHANABI3");
        Debug.Log("turn3");
    }
    public void YumeUAni4()
    {
        YumeHANABIUtage.Play("YumeHANABI4");
        Debug.Log("turn3");
    }
    public void YumeUAni5()
    {
        YumeHANABIUtage.Play("YumeHANABI5");
        Debug.Log("turn5");
    }
}
