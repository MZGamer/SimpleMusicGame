using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorSensor : MonoBehaviour {
    [Header("判定相關")]


    public float Correcttime;
    public float[] Endtime = new float[4];
    public bool[] isHold = new bool[4];
    public bool[] isActive = new bool[4];
    public List<GameObject> Note = new List<GameObject>();
    public List<GameObject> Hold = new List<GameObject>();
    public float stagetime;

    public float Realtime;
    public float[] RealEndtime = new float[4];
    [Header("編輯線")]
    public int ID;
    public Note Data;
    // Use this for initialization
    void Start () {
        Correcttime = Data.time;
        isActive[0] = Data.Line1.active;
        isActive[1] = Data.Line2.active;
        isActive[2] = Data.Line3.active;
        isActive[3] = Data.Line4.active;
        isHold[0] = Data.Line1.Hold;
        isHold[1] = Data.Line2.Hold;
        isHold[2] = Data.Line3.Hold;
        isHold[3] = Data.Line4.Hold;
        Endtime[0] = Data.Line1.endtime;
        Endtime[1] = Data.Line2.endtime;
        Endtime[2] = Data.Line3.endtime;
        Endtime[3] = Data.Line4.endtime;

        Realtime = Correcttime * NoteEditorManager.SecondPerBeat + NoteEditorManager.Offset;
        for (int i = 0; i < 4; i++)
        {
            RealEndtime[i] = Endtime[i] * NoteEditorManager.SecondPerBeat + NoteEditorManager.Offset;
            Hold[i].transform.localScale = new Vector3(0.2165127f, NoteEditorManager.NoteSpeed * (RealEndtime[i] - Realtime) * 3.03f, 1);
            if (isActive[i])
            {
                if (isHold[i])
                {
                    Hold[i].SetActive(true);
                }
                else
                {
                    Note[i].SetActive(true);
                }
            }
        }

        transform.localPosition = new Vector3(0, 42f *  (Realtime - stagetime) / (42f / NoteEditorManager.NoteSpeed), 0);
    }
	
	// Update is called once per frame
	void Update () {


        for (int i = 0; i < 4; i++)
        {
            if (isActive[i])
            { 
                if (isHold[i])
                {
                    RealEndtime[i] = Endtime[i] * NoteEditorManager.SecondPerBeat + NoteEditorManager.Offset;
                    Hold[i].transform.localScale = new Vector3(0.2165127f, NoteEditorManager.NoteSpeed * (RealEndtime[i] - Realtime) * 3.03f, 1);
                    Note[i].SetActive(false);
                    Hold[i].SetActive(true);
                }
                else
                {
                    Hold[i].SetActive(false);
                    Note[i].SetActive(true);
                }
            }
            else{
                 Hold[i].SetActive(false);
                 Note[i].SetActive(false);
            }
        }
        stagetime = NoteEditorManager.StageTime;

        //transform.Translate(Vector2.down * Time.deltaTime * NoteEditorManager.NoteSpeed);
        transform.localPosition = new Vector3(0, 42f * (Realtime - stagetime) / (42f / NoteEditorManager.NoteSpeed), 0);



    }





}
