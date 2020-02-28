using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MusicInf
{

    public List<StageCreator> Difficulty;
    public AudioClip BGM;
    public float BPM;

    public string SongName;
    public string Author;
    public Sprite SongImage;

    public int songID;
}
