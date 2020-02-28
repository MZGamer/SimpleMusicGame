using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Networking;


public class LoginManager : MonoBehaviour {
    public static PlayerData Save;
    public static bool Login;
    public bool CanLogin, logincomplete, RegisterCheck;
    public PlayerData Data;
    public Text Username, Password, scret, Message;
    public Animator GateAni;



    // Use this for initialization
    void Start() {
        Login = false;
    }

    // Update is called once per frame
    void Update() {
        if (CanLogin)
        {
            if(Username.text!=""&& Password.text != "")
            {
                if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
                {
                    GateAni.Play("Checking Data");
                    GateAni.SetBool("ChkComplete", false);
                    CanLogin = false;
                    StartCoroutine("OnlineLogin");
                }
                else if (Input.GetKeyDown(KeyCode.RightAlt))
                {
                    GateAni.Play("Checking Data");
                    GateAni.SetBool("ChkComplete", false);
                    CanLogin = false;
                    StartCoroutine("OnlineRegister");
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GateAni.SetBool("Success", true);
                GateAni.SetBool("NewPlayer", true);
                GateAni.Play("LoginSuccess");
            }
        }


        string se = "";
        for(int i= 0; i < Password.text.Length; i++)
        {
            se += "*";
        }
        scret.text = se;
    }

    void DataCreate()
    {
        Save = new PlayerData();
        Save.EasyFC = new bool[25];
        Save.HardFC = new bool[25];
        Save.ExtraFC = new bool[25];
        Save.PandoraFC = new bool[7];

        Save.EasyPerfect = new bool[25];
        Save.HardPerfect = new bool[25];
        Save.ExtraPerfect = new bool[25];
        Save.PandoraPerfect = new bool[7];

        Save.EasyHighScore = new int[25];
        Save.HardHighScore = new int[25];
        Save.ExtraHighScore = new int[25];
        Save.PandoraHighScore = new int[7];

        Save.ReflectionSet = true;
        Save.LastDif = 0;
        Save.LastSongCount = 0;
        Save.SpeedSet = 30;

    }

    IEnumerator OnlineRegister()
    {
        DataCreate();
        WWWForm sent = new WWWForm();
        sent.AddField("user_name", Username.text);
        sent.AddField("user_pw", Password.text);
        sent.AddField("user_data", JsonUtility.ToJson(Save));

        UnityWebRequest ReadyToLogin = UnityWebRequest.Post("http://www.tfcisgamedev.lionfree.net/GameDATA/MusicGameHW/game_server_api-master/index.php?op=user_register", sent);


        yield return ReadyToLogin.SendWebRequest();

        if (ReadyToLogin.isNetworkError || ReadyToLogin.isHttpError)
        {
            Debug.Log(ReadyToLogin.error);
            GateAni.SetBool("ChkComplete", true);
            Message.text = "Network Error";
        }
        else
        {
            if (ReadyToLogin.downloadHandler.text == "1")//用戶已存在
            {
                GateAni.SetBool("ChkComplete", true);
                Message.text = " User Has Exists";
            }
            else
            {
                WWWForm getdata = new WWWForm();
                getdata.AddField("user_name", Username.text);
                ReadyToLogin = UnityWebRequest.Post("http://www.tfcisgamedev.lionfree.net/GameDATA/MusicGameHW/game_server_api-master/index.php?op=get_data", getdata);

                yield return ReadyToLogin.SendWebRequest();

                if (ReadyToLogin.isNetworkError || ReadyToLogin.isHttpError)
                {
                    Debug.Log(ReadyToLogin.error);
                    GateAni.SetBool("ChkComplete", true);
                    Message.text = "Network Error";
                }
                else
                {
                    Debug.Log("Form Get complete!");
                    Save = JsonUtility.FromJson<PlayerData>(ReadyToLogin.downloadHandler.text);
                    GateAni.SetBool("ChkComplete", true);
                    GateAni.SetBool("Success", true);
                    GateAni.SetBool("NewPlayer", true);
                    Message.text = "Success";
                    Login = true;

                }
            }

        }
    }
    IEnumerator OnlineLogin()
    {
        WWWForm sent = new WWWForm();
        sent.AddField("user_name", Username.text);
        sent.AddField("user_pw", Password.text);

        UnityWebRequest ReadyToLogin = UnityWebRequest.Post("http://www.tfcisgamedev.lionfree.net/GameDATA/MusicGameHW/game_server_api-master/index.php?op=user_login", sent);

        yield return ReadyToLogin.SendWebRequest();

        if (ReadyToLogin.isNetworkError || ReadyToLogin.isHttpError)
        {
            Debug.Log(ReadyToLogin.error);
            GateAni.SetBool("ChkComplete", true);
            Message.text = "Network Error";
        }
        else
        {


            if (ReadyToLogin.downloadHandler.text == "0")//找不到帳戶
            {
                Message.text = "User Not Found";
                GateAni.SetBool("ChkComplete", true);

            }

            else if (ReadyToLogin.downloadHandler.text == "1")//密碼錯誤
            {
                Message.text = "Password Error";
                GateAni.SetBool("ChkComplete", true);
            }

            else if (ReadyToLogin.downloadHandler.text == "2")//認證成功
            {
                WWWForm getdata = new WWWForm();
                getdata.AddField("user_name", Username.text);
                ReadyToLogin = UnityWebRequest.Post("http://www.tfcisgamedev.lionfree.net/GameDATA/MusicGameHW/game_server_api-master/index.php?op=get_data", getdata);

                yield return ReadyToLogin.SendWebRequest();

                if (ReadyToLogin.isNetworkError || ReadyToLogin.isHttpError)
                {
                    Debug.Log(ReadyToLogin.error);
                    GateAni.SetBool("ChkComplete", true);
                    Message.text = "Network Error";
                }
                else
                {

                    GateAni.SetBool("ChkComplete", true);
                    GateAni.SetBool("Success", true);
                    Message.text = "Success";
                    Save = JsonUtility.FromJson<PlayerData>(ReadyToLogin.downloadHandler.text);
                    Gate.LoginCom = true;
                    Login = true;

                }
            }

        }
    }



}
