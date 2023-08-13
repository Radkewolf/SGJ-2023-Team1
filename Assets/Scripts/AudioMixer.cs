using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMixer : MonoBehaviour
{
    public AudioClip[] Songs;

    private AudioSource Audio;
    private int CurrentTrack;

    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        GameMaster.Mixer = this;
    }

    public void StartAudio()
    {
        Audio.clip = Songs[CurrentTrack];
        Audio.Play();
    }

    public void NextSong()
    {
        Audio.Stop();
        CurrentTrack++;
        if(CurrentTrack == 1 || CurrentTrack == 3)
        {
            Audio.volume = 0.4f;
        }
        else
        {
            Audio.volume = 0.9f;
        }
        Audio.clip = Songs[CurrentTrack];
        Audio.Play();
    }

    public void Pause()
    {
        Audio.Pause();
    }
}
