using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfWrite : MonoBehaviour {
    public Image SongImage;
    public Text Author;
    public Text SongName;
    public Text Level;
    public Text BPM;
    public StageCreator DifMap;
    public Text HighScore;
    public Text Score;
    public List<GameObject> Rank = new List<GameObject>();
    public GameObject FullCombo;
    public int dif;
    public bool Perfect;
    public bool FC;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
