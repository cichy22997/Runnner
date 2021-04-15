using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource collectSub;
    public AudioSource waveCompleted;
    public AudioSource levelCompleted;
    public AudioSource dyingSound;
    public AudioSource mainMusic;



    public static Sounds Instance { set; get; }

    private void Start()
    {
        Instance = this;

        PlayMainMusic();
    }


    public void PlayCollect()
    {
        if (MenuMenager.soundsOn)
        {
            collectSub.Play();
        }

    }
    public void PlayWave()
    {
        if (MenuMenager.soundsOn)
        {
            waveCompleted.Play();
        }
    }
    public void PlayLevel()
    {
        if (MenuMenager.soundsOn)
        {
            levelCompleted.Play();
        }
    }
    public void PlayDying()
    {
        if (MenuMenager.soundsOn)
        {
            dyingSound.Play();
        }
    }

    public void PlayMainMusic()
    {
        if (MenuMenager.musicOn)
        {
            mainMusic.Play();
        }
    }


}

