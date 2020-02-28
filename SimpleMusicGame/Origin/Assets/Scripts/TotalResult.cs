using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class TotalResult : MonoBehaviour {
    public static List<StageInfWrite> Totals = new List<StageInfWrite>();
    public List<StageInfWrite> Total = new List<StageInfWrite>();
    public Animator TotalResultAni;
    public Text Message;
    public float cd;
    bool TotalResultEnd;
    int Retry;


    // Use this for initialization
    void Start () {
        Retry = 0;
        TotalResultEnd = false;
        cd = 0;
		for(int i = 0; i <= 1; i++)
        {
            Total[i].Author.text = Totals[i].Author.text;
            Total[i].BPM.text = Totals[i].BPM.text;
            Total[i].dif = Totals[i].dif;
            Total[i].Level.text = Totals[i].Level.text;
            Total[i].Score.text = Totals[i].Score.text;
            Total[i].SongImage.sprite = Totals[i].SongImage.sprite;
            Total[i].SongName.text = Totals[i].SongName.text;
            Total[i].Perfect = Totals[i].Perfect;
            Total[i].FC = Totals[i].FC;

            if (Total[i].dif == 0)
            {
                Total[i].gameObject.GetComponent<Image>().color = new Color(0.4862745f, 1, 0, 0.454902f);
            }
            else if(Total[i].dif == 1)
            {
                Total[i].gameObject.GetComponent<Image>().color = new Color(1, 0, 0, 0.454902f);
            }
            else
            {
                Total[i].gameObject.GetComponent<Image>().color = new Color(0.8531394f, 0, 1, 0.454902f);
            }
            int Score = int.Parse(Total[i].Score.text);
            if (Total[i].Perfect)
            {
                Total[i].Rank[0].SetActive(true);
            }
            else if (Score >= 980000)
            {
                Total[i].Rank[1].SetActive(true);
            }
            else if (Score >= 920000)
            {
                Total[i].Rank[2].SetActive(true);
            }
            else if (Score >= 840000)
            {
                Total[i].Rank[3].SetActive(true);
            }
            else if (Score >= 780000)
            {
                Total[i].Rank[4].SetActive(true);
            }
            else if (Score >= 700000)
            {
                Total[i].Rank[5].SetActive(true);
            }
            else if (Score >= 600000)
            {
                Total[i].Rank[6].SetActive(true);
            }
            else
            {
                Total[i].Rank[7].SetActive(true);
            }

            if (Total[i].FC)
            {
                Total[i].FullCombo.SetActive(true);
            }


        }
	}
	
	// Update is called once per frame
	void Update () {
        cd += Time.deltaTime;
        if (cd >= 4.333f&& Input.GetKey(KeyCode.Space) && !TotalResultEnd)
        {
            if (LoginManager.Login)
            {
                TotalResultAni.SetBool("TotalResultEnd", true);
                TotalResultEnd = true;
                TotalResultAni.SetBool("Login", true);
                StartCoroutine("SentPlayerData");
            }
            else
            {
                TotalResultAni.SetBool("TotalResultEnd", true);
                TotalResultEnd = true;
                Invoke("GotoStart", 9.5f);
            }

        }
	}


    void GotoStart()
    {
        SceneManager.LoadScene("Start");
    }
    
    IEnumerator SentPlayerData()
    {
        string JsonData = JsonUtility.ToJson(LoginManager.Save);



        WWWForm sent = new WWWForm();
        sent.AddField("user_name", LoginManager.Save.UserName);
        sent.AddField("user_data", JsonData);

        UnityWebRequest ReadyToLogin = UnityWebRequest.Post("http://www.tfcisgamedev.lionfree.net/GameDATA/MusicGameHW/game_server_api-master/index.php?op=modify_data", sent);

        yield return ReadyToLogin.SendWebRequest();
        TotalResultAni.SetBool("HasConnected", true);
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
                TotalResultAni.SetBool("Success", true);
                Message.text = "Success";
                Debug.Log("Form upload complete!");
                Debug.Log(sent.data);
                Debug.Log(ReadyToLogin.downloadHandler.text);
                yield return new WaitForSeconds(13.5f);
                GotoStart();
            }

        }
    }
}
