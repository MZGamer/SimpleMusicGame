using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NoteEditorManager : MonoBehaviour {
    [Header("關卡物件")]
    public AudioSource Audio;
    public List<GameObject> Note = new List<GameObject>();
    public List<GameObject> HoldNote = new List<GameObject>();
    public GameObject TestLine, TestLineX4;
    public List<newSensor> NewSensors = new List<newSensor>(); 

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
    public static float SecondPerBeat;
    public float TestLineT;
    public static float Offset;
    public int FullCombo;
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

    [Header("編輯器")]
    public GameObject EditLine;
    public Scrollbar Scroll;
    public Text time;
    public bool pause;
    public List<EditorSensor> Line = new List<EditorSensor>();
    public Text CT,ET,ishold;
    public GameObject EndTimeEdit,lineclose;
    public int EditingLine;
    public int EditingNote;

    public EditorSensor Edit;
    public static int slectingHold;
    public static bool UpdateComplete;


    // Use this for initialization
    void Start () {
        UpdateComplete = true;
        TestMode = true;
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
        //Debug.Log(MovingNotes.Speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (MaxCombo < Combo)
        {
            MaxCombo = Combo;
        }

        Score = (int)(((Perfect * 1.0 + Good * 0.6) / FullCombo) * 920000 + ((MaxCombo * 1.0F) / (FullCombo * 1.0F)) * 80000) + HoldBouns;
        if (!isDemo)
            ComboT.text = Combo.ToString();


        if (EndScore < Score)
        {
            EndScore = Score;
            AddScore = (EndScore - VisableScore) * 2 * Time.deltaTime;
        }
        if (VisableScore + AddScore > EndScore)
        {
            VisableScore = EndScore;
        }
        else
        {
            VisableScore += AddScore;
        }
        int S, count = 0;
        S = (int)VisableScore;
        string Zero = "";
        while (S >= 10)
        {
            count++;
            S = S / 10;
        }
        count++;
        for (int i = 0; i < 7 - count; i++)
        {
            Zero = Zero + "0";
        }
        ScoreT.text = Zero + ((int)VisableScore).ToString();
        //if((int)(Time.deltaTime/TestLineT)>=0) {Debug.Log(Time.deltaTime); }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log(passtime);
        }
        if (GameStarted && !pause)
        {
            StageTime += Time.deltaTime;
            Scroll.value = (StageTime / BGM.length);
        }
        else if (pause)
        {
            StageTime = Scroll.value * BGM.length;
            Audio.time = StageTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pause)
            {
                ReStart();
            }
            else
            {
                Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!pause)
            {
                Pause();
                LineGet();
            }
        }

        //TimeText Update
        string min, sec;
        min = ((int)StageTime / 60).ToString();
        sec = ((int)StageTime % 60).ToString();
        if ((int.Parse(min) / 10) == 0)
            min = "0" + min;
        if ((int.Parse(sec) / 10) == 0)
            sec = "0" + sec;
        time.text = "Time:" + min + ":" + sec + ":" + "00";


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



        for (int i = 0; i < Notes.Count; i++)
        {


            //Debug.Log(Notes[i].Line1.Hold);
             StartCoroutine(EditLineCreate(Notes[i],i));


        }
        GameStarted = true;

    }

    IEnumerator EditLineCreate(Note note, int ID)
    {


        EditorSensor Sensor = EditLine.GetComponent<EditorSensor>();
        Sensor.Data = note;
        Sensor.ID = ID;


        GameObject ob = Instantiate(EditLine, EditLine.transform.position, Quaternion.identity);
        Line.Add(ob.GetComponent<EditorSensor>());
        yield return true;
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

    public void Pause()
    {
        pause = true;
        Audio.Pause();
    }
    public void ReStart()
    {
        pause = false;
        Audio.Play();
    }
    public void LineGet(){
        float t = 100f;
        int IDGet = 0;
        for(int i = Stage.Stage.Notes.Count-1;i>=0;i--){
            float temp = (Stage.Stage.Notes[i].time * SecondPerBeat +Offset) - StageTime;
            if(temp<=t&&temp>=0){
                IDGet=i;
                t = temp;
            }
        }
      EditingLine = IDGet;

        DataUpdate();
    }
    public void DataUpdate(){
            UpdateComplete = true;
            Edit =   Line[EditingLine].gameObject.GetComponent<EditorSensor>();
            CT.text = Edit.Correcttime.ToString();
    }

    public void NoteSlect(int Line){
        lineclose.SetActive(true);
        Edit.isActive[Line] = true;
        EditingNote = Line;
        if(Edit.isHold[Line]){
            ishold.text = "True";
            EndTimeEdit.SetActive(true);
            ET.text = Edit.Endtime[Line].ToString();
        }
        else
        {
            ishold.text = "False";
            EndTimeEdit.SetActive(false);
        }
    }
    public void NoteClose(){
        lineclose.SetActive(false);
        Edit.isActive[EditingNote] = false;
        Edit.isHold[EditingNote] = false;
        ishold.text = "False";
        EndTimeEdit.SetActive(false);
    }
    public void HoldSet(){
        if(!Edit.isHold[EditingNote]){
            Edit.isHold[EditingNote] = true;
            ishold.text = "True";
            EndTimeEdit.SetActive(true);
            Edit.Endtime[EditingNote] = Edit.Correcttime + 0.25f;
            ET.text = Edit.Endtime[EditingNote].ToString();
        }
        else
        {
            Edit.isHold[EditingNote] = false;
            ishold.text = "False";
            EndTimeEdit.SetActive(false);
            Edit.Endtime[EditingNote] = 0;
            ET.text = Edit.Endtime[EditingNote].ToString();
        }
    }





}
