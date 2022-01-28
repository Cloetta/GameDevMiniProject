﻿//Reference video Brackeys = https://www.youtube.com/watch?v=6OT43pvUyfY
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sounds
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;


}
