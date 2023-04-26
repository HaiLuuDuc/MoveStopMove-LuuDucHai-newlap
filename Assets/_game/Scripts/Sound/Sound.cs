using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    MouseClick = 0,
    Die = 1,
    Win = 2,
    Lose = 3,
    Throw = 4
}
[Serializable]
public class Sound
{
    public AudioClip clip;
    public SoundType soundType;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 3f)]
    public float pitch;

}
