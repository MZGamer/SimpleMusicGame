using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerData
{
    public string UserName;
    public string Password;

    public float SpeedSet;
    public bool ReflectionSet;
    public int LastSongCount;
    public int LastDif;

    public int[] EasyHighScore;
    public int[] HardHighScore;
    public int[] ExtraHighScore;
    public int[] PandoraHighScore;
    public bool[] EasyPerfect;
    public bool[] HardPerfect;
    public bool[] ExtraPerfect;
    public bool[] PandoraPerfect;
    public bool[] EasyFC;
    public bool[] HardFC;
    public bool[] ExtraFC;
    public bool[] PandoraFC;
}
