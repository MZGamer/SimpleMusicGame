using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newSensor : MonoBehaviour {
    public int lineID;
    public bool HoldStarted = false;
    public KeyCode code;
    public List<sensor> Line = new List<sensor>();

    public GameObject PerfectPar, GoodPar, HoldPar;
    public static bool Holding;


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Line.Count > StageManager.LCountS[lineID - 1] &&!StageManager.Auto)
        {
            KeyDown();
        }

    }


    public void KeyDown()
    {
        sensor s = Line[StageManager.LCountS[lineID - 1]];
        float Correcttime = s.Correcttime;
        float stagetime = StageManager.StageTime;
        float Endtime = s.Endtime;

        GameObject Note = s.gameObject;


        if (!s.Hold)
        {


            if (Correcttime - stagetime <= -StageManager.MissTs)
            {
                Debug.Log("MISS");
                StageManager.Combo = 0;
                StageManager.Miss++;
                Destroy(Note);
                StageManager.LCountS[lineID - 1]++;

            }
            if (Input.GetKeyDown(code))
            {
                //Debug.Log(Correcttime + "," + stagetime );
                if (Mathf.Abs(Correcttime - stagetime) <= StageManager.MissTs)
                {
                    if (Mathf.Abs(Correcttime - stagetime) <= StageManager.PerfectTs)
                    {
                        StageManager.Combo++;
                        StageManager.Perfect++;
                        Destroy(Note);
                        StageManager.LCountS[lineID - 1]++;
                        GameObject Par=  Instantiate(PerfectPar, transform.position, Quaternion.identity);
                        Par.transform.position = transform.localPosition;

                    }
                    else if (Mathf.Abs(Correcttime - stagetime) <= StageManager.GoodTs)
                    {
                        StageManager.Combo++;
                        StageManager.Good++;
                        Destroy(Note);
                        StageManager.LCountS[lineID - 1]++;
                        GameObject Par =  Instantiate(GoodPar, transform.position, Quaternion.identity);
                        Par.transform.position = transform.localPosition;
                    }
                }
                else if (Correcttime - stagetime < -StageManager.MissTs)
                {
                    StageManager.Combo = 0;
                    StageManager.Miss++;
                    Destroy(Note);
                    StageManager.LCountS[lineID - 1]++;
                }

            }



        }
        else if (s.Hold)
        {
            Debug.Log(HoldStarted);
            if (Endtime - stagetime <= -StageManager.MissTs)
            {
                StageManager.Combo = 0;
                StageManager.Miss++;
                Destroy(Note);
                StageManager.LCountS[lineID - 1]++;
            }
            if (!HoldStarted)
            {
                if (Correcttime - stagetime <= -StageManager.MissTs)
                {
                    StageManager.Combo = 0;
                    StageManager.Miss++;
                    StageManager.LCountS[lineID - 1]++;
                    s.gameObject.GetComponent<Animator>().SetBool("Miss", true);
                }
                if (Input.GetKeyDown(code))
                {
                    if (Correcttime - stagetime <= StageManager.MissTs)
                    {
                        if (Mathf.Abs(Correcttime - stagetime) <= StageManager.PerfectTs)
                        {
                            HoldStarted = true;
                            StageManager.Combo++;
                            StageManager.Perfect++;

                            GameObject Par = Instantiate(PerfectPar, transform.position, Quaternion.identity);
                            Par.transform.position = transform.localPosition;
                        }
                        else if (Mathf.Abs(Correcttime - stagetime) <= StageManager.GoodTs)
                        {
                            HoldStarted = true;
                            StageManager.Combo++;
                            StageManager.Good++;

                            GameObject Par = Instantiate(GoodPar, transform.position, Quaternion.identity);
                            Par.transform.position = transform.localPosition;


                        }


                    }
                }

            }

            else
            {
                if (HoldStarted)
                {
                    if (Endtime - stagetime > StageManager.MissTs)
                    {
                        if (Input.GetKeyUp(code))
                        {
                            //StageManager.Miss++;
                            StageManager.Combo = 0;
                            StageManager.LCountS[lineID - 1]++;
                            s.gameObject.GetComponent<Animator>().SetBool("Miss", true);
                            HoldStarted = false;

                        }
                        else
                        {
                            StageManager.HoldBouns++;
                            GameObject Par = Instantiate(HoldPar, transform.position, Quaternion.identity);
                            Par.transform.position = transform.localPosition;
                        }
                    }


                    else if (Endtime - stagetime <= 0)
                    {

                        Destroy(Note);
                        StageManager.LCountS[lineID - 1]++;
                        HoldStarted = false;
                    }


                }
            }
        }
    }

}
