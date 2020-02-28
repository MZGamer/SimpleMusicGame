using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Note {
    public NoteData Line1;
    public NoteData Line2;
    public NoteData Line3;
    public NoteData Line4;

    public float time;
    




}

[System.Serializable]
public struct NoteData
{
    public bool active;
    public bool Hold;


    public float endtime;

}