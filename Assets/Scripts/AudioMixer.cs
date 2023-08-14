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
        if (CurrentTrack == 4)
            CurrentTrack = 3;
        if (CurrentTrack == 0)
        {
            Audio.volume = 0.2f;
        }
        else if (CurrentTrack == 1)
        {
            Audio.volume = 0.1f;
        }
        else if ( CurrentTrack == 2)
        {
            Audio.volume = 0.2f;
        }
        else if (CurrentTrack == 3)
        {
            Audio.volume = 0.2f;
        }
        else if (CurrentTrack == 4)
        {
            Audio.volume = 0f;
        }
        Audio.clip = Songs[CurrentTrack];
        Audio.Play();
    }

    public void Pause()
    {
        Audio.Pause();
    }
}
