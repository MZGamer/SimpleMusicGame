using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSlect : MonoBehaviour {
    [Header("Effect")]
    public TrailRenderer TrailEffect;
    public Animator StandByAni;
    public Animator SlectingAlpha;
    public Animator SettingAlpha;
    float target, dis, targetforsetting, settingdis;
    [Header("SongSlect")]
    public List<MusicInfCreator> Stage = new List<MusicInfCreator>();
    public List<StageInfWrite> StageInfList = new List<StageInfWrite>();
    public GameObject StageInfExample;
    public GameObject SongData;
    public GameObject Settingoption;
    public AudioSource BGM;
    public static int NowSongID;
    public static int Track;
    static public int SongID00;

    [Header("State")]
    public bool Slecting;
    public bool Setting;
    public bool StandBy;
    public float CD;

    [Header("PlayerSetting")]
    public HorizontalLayoutGroup Hor;
    public StageInfWrite Inf;
    public int SettingID;
    public float SpeedRate;
    public Text Speed;
    public Text ReflectSetting;
    public static int Dif;
    public static int Pan;


    [Header("Tips")]
    public Text UPDown;
    public Text LeftRight;
    public Text Space;
    public GameObject Startgame;

    [Header("Testing")]
    public bool TestMode;


    // Use this for initialization
    void Start () {
        if (TestMode)
            Track = 1;
        if (LoginManager.Login)
        {
            SpeedRate = LoginManager.Save.SpeedSet*1.0f / 10;
        }
        else if(Track == 1)
        {
            SpeedRate = 3;

        }

        if(Track == 2)
        {
            SpeedRate = StageManager.NoteSpeed * 1.0f / 10;
        }
        Slecting = true;
        BGM.clip = Stage[NowSongID].musicInf.BGM;
        BGM.Play();

        target = SongData.transform.localPosition.x;
        target -= 500 * NowSongID;
        dis = Vector3.Distance(SongData.transform.localPosition, new Vector3(target, SongData.transform.localPosition.y, SongData.transform.localPosition.z));
        SongData.transform.localPosition = new Vector3(SongData.transform.localPosition.x + dis, SongData.transform.localPosition.y, SongData.transform.localPosition.z);
        dis = Vector3.Distance(SongData.transform.localPosition, new Vector3(target, SongData.transform.localPosition.y, SongData.transform.localPosition.z));
        targetforsetting = Settingoption.transform.localPosition.x;
        for (int i = 0; i < Stage.Count; i++)
        {
            GameObject Inf = Instantiate(StageInfExample, SongData.transform);
            StageInfWrite write = Inf.GetComponent<StageInfWrite>();

            StageInfList.Add(write);
            write.SongName.text = Stage[i].musicInf.SongName;
            write.Author.text = Stage[i].musicInf.Author;
            write.BPM.text = "BPM " + Stage[i].musicInf.BPM.ToString();
            write.SongImage.sprite = Stage[i].musicInf.SongImage;


            if (LoginManager.Login)
            {
                write.HighScore.gameObject.SetActive(true);
            }


            int ID = Stage[i].musicInf.songID;
            if (Stage[i].musicInf.songID == 0)
            {
                StageInfList[i].gameObject.GetComponent<Image>().color = new Color(0.8531394f, 0, 1, 0.454902f);
                Pan = Random.Range(0, Stage[i].musicInf.Difficulty.Count - 1);
                write.Level.text = "LV " + Stage[i].musicInf.Difficulty[Pan].Stage.level;
                write.BPM.text = "BPM " + Stage[i].musicInf.Difficulty[Pan].Stage.BPM;
                write.DifMap = Stage[i].musicInf.Difficulty[Pan];
                if (LoginManager.Login)
                {
                    write.FC = LoginManager.Save.PandoraFC[ID];
                    write.Perfect = LoginManager.Save.PandoraPerfect[ID];
                    write.HighScore.text = "" + LoginManager.Save.PandoraHighScore[Pan];
                    Rankchk(write, LoginManager.Save.PandoraHighScore[Pan], LoginManager.Save.PandoraFC[Pan], LoginManager.Save.PandoraPerfect[Pan]);
                }
            }
            else if (Stage[i].musicInf.Difficulty[Dif] != null)
            {
                write.Level.text = "LV " + Stage[i].musicInf.Difficulty[Dif].Stage.level;
                write.DifMap = Stage[i].musicInf.Difficulty[Dif];
                if (Dif == 0)
                {
                    if (LoginManager.Login)
                    {
                        write.HighScore.text = "" + LoginManager.Save.EasyHighScore[ID];
                        write.FC = LoginManager.Save.EasyFC[ID];
                        write.Perfect = LoginManager.Save.EasyPerfect[ID];
                        Rankchk(write, LoginManager.Save.EasyHighScore[ID], LoginManager.Save.EasyFC[ID], LoginManager.Save.EasyPerfect[ID]);
                    }
                    StageInfList[i].gameObject.GetComponent<Image>().color = new Color(0.4862745f, 1, 0, 0.454902f);
                }
                else if (Dif == 1)
                {
                    if (LoginManager.Login)
                    {
                        write.FC = LoginManager.Save.HardFC[ID];
                        write.Perfect = LoginManager.Save.HardPerfect[ID];
                        write.HighScore.text = "" + LoginManager.Save.HardHighScore[ID];
                        Rankchk(write, LoginManager.Save.HardHighScore[ID], LoginManager.Save.HardFC[ID], LoginManager.Save.HardPerfect[ID]);
                    }
                    StageInfList[i].gameObject.GetComponent<Image>().color = new Color(1, 0, 0, 0.454902f);
                }

                else
                {
                    if (LoginManager.Login)
                    {
                        write.FC = LoginManager.Save.ExtraFC[ID];
                        write.Perfect = LoginManager.Save.ExtraPerfect[ID];
                        write.HighScore.text = "" + LoginManager.Save.ExtraHighScore[ID];
                        Rankchk(write, LoginManager.Save.ExtraHighScore[ID], LoginManager.Save.ExtraFC[ID], LoginManager.Save.ExtraPerfect[ID]);
                    }
                    StageInfList[i].gameObject.GetComponent<Image>().color = new Color(0.8531394f, 0, 1, 0.454902f);
                }

            }
            else
            {
                for (int k = 2; k >= 0; k--)
                {
                    if (k < Stage[i].musicInf.Difficulty.Count)
                    {
                        if (Stage[i].musicInf.Difficulty[k] != null)
                        {
                            write.Level.text = "LV " + Stage[i].musicInf.Difficulty[k].Stage.level;
                            write.DifMap = Stage[i].musicInf.Difficulty[k];
                            if (k == 0)
                            {
                                {
                                    write.HighScore.text = "" + LoginManager.Save.EasyHighScore[ID];
                                    Rankchk(write, LoginManager.Save.EasyHighScore[ID], LoginManager.Save.EasyFC[ID], LoginManager.Save.EasyPerfect[ID]);
                                }
                                StageInfList[i].gameObject.GetComponent<Image>().color = new Color(0.4862745f, 1, 0, 0.454902f);
                            }

                            else if (k == 1)
                            {
                                if (LoginManager.Login)
                                {
                                    write.HighScore.text = "" + LoginManager.Save.HardHighScore[ID];
                                    Rankchk(write, LoginManager.Save.HardHighScore[ID], LoginManager.Save.HardFC[ID], LoginManager.Save.HardPerfect[ID]);
                                }
                                StageInfList[i].gameObject.GetComponent<Image>().color = new Color(1, 0, 0, 0.454902f);
                            }

                            else
                            {
                                if (LoginManager.Login)
                                {
                                    write.HighScore.text = "" + LoginManager.Save.ExtraHighScore[ID];
                                    Rankchk(write, LoginManager.Save.ExtraHighScore[ID], LoginManager.Save.ExtraFC[ID], LoginManager.Save.ExtraPerfect[ID]);
                                }
                                StageInfList[i].gameObject.GetComponent<Image>().color = new Color(0.8531394f, 0, 1, 0.454902f);
                            }

                            break;
                        }
                    }
                }
            }



        }


        InfListChange();
        BGM.clip = Stage[NowSongID].musicInf.BGM;
        BGM.Play();
    }
	
	// Update is called once per frame
	void Update () {
		if(CD != 0)
        {
            CD -= Time.deltaTime;
            if (CD < 0)
                CD = 0;


        }
        else
        {
            TrailEffect.emitting = true;
            TrailEffect.time = 1;
        }
         if(Slecting)
            Slect();
        else if (Setting)
        {
            PlayerSetting();
        }
         else if (StandBy)
        {
            StandingBy();
        }

    }


    void Slect()
    {
        UPDown.text = "Change Difficulty";
        LeftRight.text = "Change Song";
        Space.text = "SlectSong";
        Startgame.SetActive(true);
        SongData.transform.localPosition = Vector2.MoveTowards(SongData.transform.localPosition, new Vector3(target, SongData.transform.localPosition.y, SongData.transform.localPosition.z), (dis / 0.3f) * Time.deltaTime);
        Settingoption.transform.localPosition = Vector2.MoveTowards(Settingoption.transform.localPosition, new Vector3(targetforsetting, Settingoption.transform.localPosition.y, Settingoption.transform.localPosition.z), (settingdis / 0.3f) * Time.deltaTime);
        if(CD == 0)
        {
            Settingoption.SetActive(false);
        }

        if (Hor.spacing + (-375f / 0.2f) * Time.deltaTime > -300)
            Hor.spacing += (-375f / 0.2f) * Time.deltaTime;
        else
            Hor.spacing = -300;
        if (Input.GetKey(KeyCode.RightArrow) && CD == 0 && NowSongID < StageInfList.Count-1)
        {
            TrailEffect.emitting = false;
            TrailEffect.time = 0.1f;
            target -= 500;
            dis = Vector3.Distance(SongData.transform.localPosition, new Vector3(target, SongData.transform.localPosition.y, SongData.transform.localPosition.z));
            CD = 0.3f;
            NowSongID++;
            BGM.clip = Stage[NowSongID].musicInf.BGM;
            BGM.PlayDelayed(CD);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && CD == 0 && NowSongID > 0)
        {
            TrailEffect.emitting = false;
            TrailEffect.time = 0.1f;
            target += 500;
            dis = Vector3.Distance(SongData.transform.localPosition, new Vector3(target, SongData.transform.localPosition.y, SongData.transform.localPosition.z));
            CD = 0.3f;
            NowSongID--;
            BGM.clip = Stage[NowSongID].musicInf.BGM;
            BGM.PlayDelayed(CD);
        }
        if(Input.GetKey(KeyCode.Space) && CD == 0)
        {
            SettingSongInfChange();

            Settingoption.SetActive(true);
            settingdis = Vector3.Distance(Settingoption.transform.localPosition, new Vector3(targetforsetting, Settingoption.transform.localPosition.y, Settingoption.transform.localPosition.z));
            Setting = true;
            Slecting = false;
            CD = 0.2f;
            SlectingAlpha.Play("SettingAlpha");

        }

        if (Input.GetKey(KeyCode.UpArrow) && CD == 0)
        {
            if(Dif < 2)
            {
                CD = 0.1F;
                Dif++;
                InfListChange();
            }

        }
        if (Input.GetKey(KeyCode.DownArrow) && CD == 0)
        {
            if (Dif > 0)
            {
                CD = 0.1F;
                Dif--;
                InfListChange();
            }


        }

    }

    void PlayerSetting()
    {
        UPDown.text = "Change Difficulty";
        LeftRight.text = "Change Setting";
        Space.text = "StartGame";
        Settingoption.transform.localPosition = Vector2.MoveTowards(Settingoption.transform.localPosition, new Vector3(targetforsetting, Settingoption.transform.localPosition.y, Settingoption.transform.localPosition.z), (settingdis / 0.3f) * Time.deltaTime);
        Speed.text = SpeedRate.ToString();
        if (KeyDownEffect.Reflection)
        {
            ReflectSetting.text = "On";
        }
        else
            ReflectSetting.text = "Off";
        if (Hor.spacing < 75)
        {
            if (Hor.spacing + (375f / 0.2f) * Time.deltaTime < 75)
                Hor.spacing += (375f / 0.2f) * Time.deltaTime;
            else
                Hor.spacing = 75;
        }
        if(SettingID == 0)
        {
            Startgame.SetActive(true);
            UPDown.text = "Change Difficulty";
        }
        else
        {
            Startgame.SetActive(false);
            UPDown.text = "Change Setting Value";
        }



        if (Input.GetKey(KeyCode.RightArrow) && CD == 0 && SettingID < 2)
        {
            TrailEffect.emitting = false;
            TrailEffect.time = 0.1f;
            targetforsetting -= 375;
            settingdis = Vector3.Distance(Settingoption.transform.localPosition, new Vector3(targetforsetting, Settingoption.transform.localPosition.y, Settingoption.transform.localPosition.z));
            CD = 0.3f;
            SettingID++;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && CD == 0 && SettingID > 0)
        {
            TrailEffect.emitting = false;
            TrailEffect.time = 0.1f;
            targetforsetting += 375;
            settingdis = Vector3.Distance(Settingoption.transform.localPosition, new Vector3(targetforsetting, Settingoption.transform.localPosition.y, Settingoption.transform.localPosition.z));
            CD = 0.3f;
            SettingID--;
        }
        if (CD == 0 && SettingID == 0)
        {
            if (Input.GetKey(KeyCode.UpArrow) && Dif < 2)
            {
                

                Dif ++;
                Pan = Random.Range(0, Stage[Stage.Count - 1].musicInf.Difficulty.Count);
                
                InfListChange();
                SettingSongInfChange();
                //StageManager.Speed = SpeedRate*10;
                CD = 0.1f;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && Dif>0)
            {


                Dif --;
                Pan = Random.Range(0, Stage[Stage.Count - 1].musicInf.Difficulty.Count);
                
                InfListChange();
                SettingSongInfChange();
                //StageManager.Speed = SpeedRate*10;
                CD = 0.1f;
            }

        }
        if ( CD == 0 && SettingID == 1)
        {
            if (Input.GetKey(KeyCode.UpArrow)&&SpeedRate <10)
            {
                SpeedRate += 0.1f;
                SpeedRate = Mathf.Round(SpeedRate*10)/10;
                Speed.text = SpeedRate.ToString();
                //StageManager.Speed = SpeedRate*10;
                CD = 0.1f;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && SpeedRate > 3f)
            {
                SpeedRate -= 0.1f;
                SpeedRate = Mathf.Round(SpeedRate * 10) / 10;
                Speed.text = SpeedRate.ToString();
               // StageManager.Speed = SpeedRate * 10;
                CD = 0.1f;
            }

        }
        if (CD == 0 && SettingID == 2)
        {
            if (Input.GetKey(KeyCode.UpArrow) && !KeyDownEffect.Reflection)
            {
                KeyDownEffect.Reflection = true;
                ReflectSetting.text = "On";
                StageManager.NoteSpeed = SpeedRate*10;
                CD = 0.1f;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && KeyDownEffect.Reflection)
            {
                KeyDownEffect.Reflection = false;
                ReflectSetting.text = "Off";
                StageManager.NoteSpeed = SpeedRate * 10;
                CD = 0.1f;
            }

        }
        if (Input.GetKeyDown(KeyCode.Space) && CD == 0 && SettingID == 0)
        {
            SlectingAlpha.Play("StandByAlpha");
            ResultManager.Music = Stage[NowSongID];
            Setting = false;
            StandBy = true;
            StandByAni.Play("StandingBy");
            CD = 8;
        }
        if ((Input.GetKey(KeyCode.Backspace) || Input.GetKey(KeyCode.Escape)) && CD == 0)
        {
            SlectingAlpha.Play("SlectingAlpha");
            targetforsetting = 0;
            SettingID = 0;
            Setting = false;
            Slecting = true;
            settingdis = Vector3.Distance(Settingoption.transform.localPosition, new Vector3(targetforsetting, Settingoption.transform.localPosition.y, Settingoption.transform.localPosition.z));

            SettingAlpha.Play("SettingbACKAlphaChange");
            
            CD = 0.2f;
        }
    }

    void SettingSongInfChange()
    {
        Inf.Author.text = StageInfList[NowSongID].Author.text;
        Inf.SongName.text = StageInfList[NowSongID].SongName.text;
        Inf.BPM.text = StageInfList[NowSongID].BPM.text;
        Inf.DifMap = StageInfList[NowSongID].DifMap;
        Inf.Level.text = StageInfList[NowSongID].Level.text;
        Inf.dif = StageInfList[NowSongID].dif;
        Inf.gameObject.GetComponent<Image>().color = StageInfList[NowSongID].gameObject.GetComponent<Image>().color;
        Inf.SongImage.sprite = StageInfList[NowSongID].SongImage.sprite;
        Inf.HighScore.text = StageInfList[NowSongID].HighScore.text;
        Inf.FC = StageInfList[NowSongID].FC;
        Inf.Perfect = StageInfList[NowSongID].Perfect;

        if (LoginManager.Login)
        {
            Rankchk(Inf, int.Parse(Inf.HighScore.text), Inf.FC, Inf.Perfect);
            Inf.HighScore.gameObject.SetActive(true);
        }


    }
    void InfListChange()
    {
        for (int i = 0; i < StageInfList.Count; i++)
        {
            int ID = Stage[i].musicInf.songID;
            if (Stage[i].musicInf.songID == 0)
            {
                Pan = Random.Range(0, Stage[i].musicInf.Difficulty.Count);

                StageInfList[i].gameObject.GetComponent<Image>().color = new Color(0.8531394f, 0, 1, 0.454902f);
                StageInfList[i].Level.text = "LV " + Stage[i].musicInf.Difficulty[Pan].Stage.level;
                StageInfList[i].BPM.text = "BPM " + Stage[i].musicInf.Difficulty[Pan].Stage.BPM;
                StageInfList[i].DifMap = Stage[i].musicInf.Difficulty[Pan];
                if (LoginManager.Login)
                {
                    StageInfList[i].HighScore.text = "" + LoginManager.Save.PandoraHighScore[Pan];
                    StageInfList[i].FC = LoginManager.Save.PandoraFC[Pan];
                    StageInfList[i].Perfect = LoginManager.Save.PandoraPerfect[Pan];
                    Rankchk(StageInfList[i], LoginManager.Save.PandoraHighScore[Pan], StageInfList[i].FC, StageInfList[i].Perfect);
                }
            }
            else
                for (int k = Dif; k >= 0; k--)
                {
                    if (Stage[i].musicInf.Difficulty[k] != null && k < Stage[i].musicInf.Difficulty.Count)
                    {
                        StageInfList[i].Level.text = "LV " + Stage[i].musicInf.Difficulty[k].Stage.level;
                        StageInfList[i].DifMap = Stage[i].musicInf.Difficulty[k];
                        if (k == 0)
                        {
                            StageInfList[i].gameObject.GetComponent<Image>().color = new Color(0.4862745f, 1, 0, 0.454902f);
                            if (LoginManager.Login)
                            {
                                StageInfList[i].FC = LoginManager.Save.EasyFC[ID];
                                StageInfList[i].Perfect = LoginManager.Save.EasyPerfect[ID];
                                StageInfList[i].HighScore.text = "" + LoginManager.Save.EasyHighScore[ID];
                                Rankchk(StageInfList[i], LoginManager.Save.EasyHighScore[ID], LoginManager.Save.EasyFC[ID], LoginManager.Save.EasyPerfect[ID]);
                            }
                        }

                        else if (k == 1)
                        {
                            if (LoginManager.Login)
                            {
                                StageInfList[i].FC = LoginManager.Save.HardFC[ID];
                                StageInfList[i].Perfect = LoginManager.Save.HardPerfect[ID];
                                StageInfList[i].HighScore.text = "" + LoginManager.Save.HardHighScore[ID];
                                Rankchk(StageInfList[i], LoginManager.Save.HardHighScore[ID], LoginManager.Save.HardFC[ID], LoginManager.Save.HardPerfect[ID]);
                            }
                            StageInfList[i].gameObject.GetComponent<Image>().color = new Color(1, 0, 0, 0.454902f);

                        }

                        else
                        {
                            if (LoginManager.Login)
                            {
                                StageInfList[i].FC = LoginManager.Save.ExtraFC[ID];
                                StageInfList[i].Perfect = LoginManager.Save.ExtraPerfect[ID];
                                StageInfList[i].HighScore.text = "" + LoginManager.Save.ExtraHighScore[ID];
                                Rankchk(StageInfList[i], LoginManager.Save.ExtraHighScore[ID], LoginManager.Save.ExtraFC[ID], LoginManager.Save.ExtraPerfect[ID]);
                            }
                            StageInfList[i].gameObject.GetComponent<Image>().color = new Color(0.8531394f, 0, 1, 0.454902f);

                        }


                        break;
                    }
                }
        }
    }

    void StandingBy()
    {
        if (Hor.spacing + (600f / 0.2f) * Time.deltaTime < 600)
            Hor.spacing += (600f / 0.2f) * Time.deltaTime;
        else
            Hor.spacing = 600;
        if (CD <= 1)
        {
            TrailEffect.emitting = false;
            TrailEffect.time = 0.5f;
        }
        if (LoginManager.Login)
        {
            LoginManager.Save.LastSongCount = NowSongID;
            LoginManager.Save.LastDif = Dif;
            LoginManager.Save.SpeedSet = SpeedRate * 10;
        }
        StageManager.NoteSpeed = SpeedRate * 10;
        StageManager.MusicStage = StageInfList[NowSongID].DifMap;
        Invoke("GotoStage", 9f);

    }
    void GotoStage()
    {
        if (Stage[NowSongID].musicInf.songID == 9)
        {
            SceneManager.LoadScene("Stage9");
        }
        else
            SongID00 = Stage[NowSongID].musicInf.songID;
            SceneManager.LoadScene("Stage");
    }

    void Rankchk(StageInfWrite w, int Score , bool FC,bool Perfect)
    {
        w.Rank[0].SetActive(false);
        w.FullCombo.SetActive(false);
        for (int i = 1; i < w.Rank.Count; i++)
        {
            w.Rank[i].SetActive(false);
        }
        if (FC)
        {
            w.FullCombo.SetActive(true);
        }
        if (Perfect)
        {
            w.Rank[0].SetActive(true);
        }
        else if(Score != 0)
        {
            if (Score >= 980000)
            {
                w.Rank[1].SetActive(true);
            }
            else if (Score >= 920000)
            {
                w.Rank[2].SetActive(true);
            }
            else if (Score >= 840000)
            {
                w.Rank[3].SetActive(true);
            }
            else if (Score >= 780000)
            {
                w.Rank[4].SetActive(true);
            }
            else if (Score >= 700000)
            {
                w.Rank[5].SetActive(true);
            }
            else if (Score >= 600000)
            {
                w.Rank[6].SetActive(true);
            }
            else
            {
                w.Rank[7].SetActive(true);
            }
        }
    }


}
