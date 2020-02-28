using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stage
{

    public List<Note> Notes;
    public AudioClip BGM;
    public int songtime;
    public float BPM;
    public int FullCombo;
    public float Offset;

    public string SongName;
    public string Author;
    public Sprite SongImage;
    public string level;
}
