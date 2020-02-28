using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class ResultManager : MonoBehaviour {


    [Header("UI")]
    public Image SongImage;
    public Image Dif;
    public Text SongName;
    public Text Author;
    public Text Score;
    public Text Perfect;
    public Text Good;
    public Text Miss;
    public Text MaxCombo;
    public Text HoldBouns;
    


    [Header("Animation")]
    public float Zeroto1;
    public float CD;
    public Animator ResultAni;
    public bool ResultFade;


    [Header("Rank")]
    public GameObject F;
    public GameObject D;
    public GameObject C;
    public GameObject B;
    public GameObject A;
    public GameObject S;
    public GameObject SS;
    public GameObject P;
    public GameObject FC;

    [Header("SendPlayerData")]
    public int Retry;
    public Text Message;
    public AudioSource BGM;
    public bool DataUpdate;
    


    public static MusicInfCreator Music;
    public List<StageInfWrite> TotalNeed = new List<StageInfWrite>();
    public int Difficult;

    // Use this for initialization
    void Start () {
        Retry = 0;
        //TotalResult.Totals.Add(TotalNeed[StageSlect.Track - 1]);
        CD = 6;
        StageCreator s = StageManager.MusicStage;
        if(Music.musicInf.songID == 0)
        {
            Dif.color = new Color(0.8531394f, 0, 1, 0.454902f);
            //TotalResult.Totals[StageSlect.Track - 1].dif = 2;
        }
        else
        {
            if (s == Music.musicInf.Difficulty[0])
            {
                Dif.color = new Color(0.4862745f, 1, 0, 0.454902f);
                //TotalResult.Totals[StageSlect.Track - 1].dif = 0;
                Difficult = 0;
            }
            else if (s == Music.musicInf.Difficulty[1])
            {
                Dif.color = new Color(1, 0, 0, 0.454902f);
                //TotalResult.Totals[StageSlect.Track - 1].dif = 1;
                Difficult = 1;

            }
            else
            {
                Dif.color = new Color(0.8531394f, 0, 1, 0.454902f);
                //TotalResult.Totals[StageSlect.Track - 1].dif = 2;
                Difficult = 2;
            }
        }

        SongImage.sprite = s.Stage.SongImage;
        SongName.text = s.Stage.SongName;
        Author.text = s.Stage.Author;
        Score.text = ((int)(StageManager.Score* Zeroto1)).ToString();
        Perfect.text = "Perfect : " +(int)(StageManager.Perfect * Zeroto1);
        Good.text = "Good : " + (int)(StageManager.Good* Zeroto1);
        Miss.text = "Miss : " + (int)(StageManager.Miss* Zeroto1);
        MaxCombo.text = "MaxCombo : " + (int)(StageManager.MaxCombo* Zeroto1);
        HoldBouns.text = "HoldBouns : " + (int)(StageManager.HoldBouns* Zeroto1);


        /*
        TotalResult.Totals[StageSlect.Track - 1].Author.text = Author.text;
        TotalResult.Totals[StageSlect.Track - 1].BPM.text = "BPM " + s.Stage.BPM.ToString();

        TotalResult.Totals[StageSlect.Track - 1].Level.text = "LV " + s.Stage.level.ToString();
        TotalResult.Totals[StageSlect.Track - 1].Score.text = StageManager.Score.ToString();
        TotalResult.Totals[StageSlect.Track - 1].SongImage.sprite = SongImage.sprite;
        TotalResult.Totals[StageSlect.Track - 1].SongName.text = SongName.text;
        */

        if(StageManager.Miss == 0)
        {
            FC.SetActive(true);
            //TotalResult.Totals[StageSlect.Track - 1].FC = true;
            if (LoginManager.Login)
            {
                if (Music.musicInf.songID == 0 && !LoginManager.Save.PandoraFC[StageSlect.Pan])
                {
                    LoginManager.Save.PandoraFC[StageSlect.Pan] = true;
                    DataUpdate = true;
                }
                else
                {
                    if(Difficult == 0 && !LoginManager.Save.EasyFC[Music.musicInf.songID])
                    {                       
                            LoginManager.Save.EasyFC[Music.musicInf.songID] = true;
                        DataUpdate = true;
                    }
                    if(Difficult == 1 && !LoginManager.Save.HardFC[Music.musicInf.songID])
                    {
                            LoginManager.Save.HardFC[Music.musicInf.songID] = true;
                        DataUpdate = true;
                    }
                    if(Difficult == 2 && !LoginManager.Save.ExtraFC[Music.musicInf.songID])
                    {
                            LoginManager.Save.ExtraFC[Music.musicInf.songID] = true;
                        DataUpdate = true;
                    }
                }
            }
        }

        if (StageManager.Miss == 0 && StageManager.Good == 0)
        {
            P.SetActive(true);
            //TotalResult.Totals[StageSlect.Track - 1].Perfect = true;
            if (LoginManager.Login)
            {
                if (Music.musicInf.songID == 0 && !LoginManager.Save.PandoraPerfect[StageSlect.Pan])
                {
                    LoginManager.Save.PandoraPerfect[StageSlect.Pan] = true;
                }
                else
                {
                    if (Difficult == 0 && !LoginManager.Save.EasyPerfect[Music.musicInf.songID])
                    {
                        LoginManager.Save.EasyPerfect[Music.musicInf.songID] = true;
                        DataUpdate = true;
                    }
                    if (Difficult == 1 && !LoginManager.Save.HardPerfect[Music.musicInf.songID])
                    {
                        LoginManager.Save.HardPerfect[Music.musicInf.songID] = true;
                        DataUpdate = true;
                    }
                    if (Difficult == 2 && !LoginManager.Save.ExtraPerfect[Music.musicInf.songID])
                    {
                        LoginManager.Save.ExtraPerfect[Music.musicInf.songID] = true;
                        DataUpdate = true;
                    }
                }
            }


        }
        else
        {
            int Score = StageManager.Score;
            if(Score >= 980000)
            {
                SS.SetActive(true);
            }
            else if (Score >= 920000)
            {
                S.SetActive(true);
            }
            else if(Score >= 840000)
            {
                A.SetActive(true);
            }
            else if(Score >= 780000)
            {
                B.SetActive(true);
            }
            else if(Score >= 700000)
            {
                C.SetActive(true);
            }
            else if(Score >= 600000)
            {
                D.SetActive(true);
            }
            else
            {
                F.SetActive(true);
            }

        }

        if (LoginManager.Login)
        {


            if (Music.musicInf.songID == 0)
            {
                if (LoginManager.Save.PandoraHighScore[StageSlect.Pan] < StageManager.Score)
                {
                    LoginManager.Save.PandoraHighScore[StageSlect.Pan] = StageManager.Score;
                    DataUpdate = true;
                }

            }
            else
            {
                if (Difficult == 0)
                {
                    if (LoginManager.Save.EasyHighScore[Music.musicInf.songID] < StageManager.Score)
                    {
                        LoginManager.Save.EasyHighScore[Music.musicInf.songID] = StageManager.Score;
                        DataUpdate = true;
                    }

                }
                if (Difficult == 1)
                {
                    if (LoginManager.Save.HardHighScore[Music.musicInf.songID] < StageManager.Score)
                    {
                        LoginManager.Save.HardHighScore[Music.musicInf.songID] = StageManager.Score;
                        DataUpdate = true;
                    }

                }
                if (Difficult == 2)
                {
                    if (LoginManager.Save.ExtraHighScore[Music.musicInf.songID] < StageManager.Score)
                    {
                        LoginManager.Save.ExtraHighScore[Music.musicInf.songID] = StageManager.Score;
                        DataUpdate = true;
                    }

                }
            }
        }




    }
	
	// Update is called once per frame
	void Update () {
        Score.text = ((int)(StageManager.Score* Zeroto1)).ToString();
        Perfect.text = "Perfect : " +(int)(StageManager.Perfect * Zeroto1);
        Good.text = "Good : " + (int)(StageManager.Good* Zeroto1);
        Miss.text = "Miss : " + (int)(StageManager.Miss* Zeroto1);
        MaxCombo.text = "MaxCombo : " + (int)(StageManager.MaxCombo* Zeroto1);
        HoldBouns.text = "HoldBouns : " + (int)(StageManager.HoldBouns* Zeroto1);
        CD-=Time.deltaTime;
        if (ResultFade)
            BGM.volume -= Time.deltaTime;

        if (CD<=0 && Input.GetKeyDown(KeyCode.Space)){
            ResultAni.SetBool("ResultDone" , true);
            if (!LoginManager.Login)
            {
                Invoke("ChangeScene", 1f);
                ResultFade = true;

            }

            else
            {
                Invoke("ReadySend", 2f);
            }

        }
	}

    void ReadySend()
    {
        ResultAni.Play("DataSending");
        StartCoroutine("SentPlayerData");
    }

    void ChangeScene(){
        //StageSlect.Track++;
        if(StageSlect.Track>2)
            SceneManager.LoadScene("TotalResult");
        else
            SceneManager.LoadScene("SongSlect");


    }



    IEnumerator SentPlayerData()
    {
        string JsonData = JsonUtility.ToJson(LoginManager.Save);



        WWWForm sent = new WWWForm();
        sent.AddField("user_name", LoginManager.Save.UserName);
        sent.AddField("user_data", JsonData);

        UnityWebRequest ReadyToLogin = UnityWebRequest.Post("http://www.tfcisgamedev.lionfree.net/GameDATA/MusicGameHW/game_server_api-master/index.php?op=modify_data", sent);

        yield return ReadyToLogin.SendWebRequest();
        ResultAni.SetBool("HasConnected", true);
        if (ReadyToLogin.isNetworkError || ReadyToLogin.isHttpError)
        {
            Debug.Log(ReadyToLogin.error);
            Message.text = "NetworkError";
            Retry++;
            if (Retry < 3)
            {
                StartCoroutine("SentPlayerData");
            }
        }
        else
        {

            if (ReadyToLogin.downloadHandler.text == "2")//上傳成功
            {
                ResultAni.SetBool("Success", true);
                Message.text = "Success";
                Debug.Log("Form upload complete!");
                Debug.Log(sent.data);
                Debug.Log(ReadyToLogin.downloadHandler.text);
                yield return new WaitForSeconds(6f);
                ChangeScene();
            }

        }
    }
}
