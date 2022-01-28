using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


//Reference video by Brackeys on Audio Managers = https://www.youtube.com/watch?v=6OT43pvUyfY

public class AudioSystem : MonoBehaviour
{
    public Sounds[] sounds;

    private void Awake()
    {
        foreach (Sounds audio in sounds)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;

            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;
            audio.source.loop = audio.loop;
        }
    }

    private void Start()
    {

        Scene current = SceneManager.GetActiveScene();

        string sceneName = current.name;

        if (sceneName != "GameOver")
        {

            if (sceneName == "BossFight")
            {
                Play("BossTheme");
            }
            else if(sceneName == "StartMenu")
            {
                Play("StartMenuTheme");
            }
            else if (sceneName == "WinScene")
            {
                //Play("ThankYouTheme"); //no sound at the moment
            }

            Play("SceneTheme");
        }

        //Keeping this part of code commented for future implementation
        //else
        //{
            //no sound at the moment
            //Play("GameOver");
        //}

    }

    public void Play(string name)
    {
        Sounds audio = Array.Find(sounds, sound => sound.name == name);
        audio.source.Play();
    }
}
