using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageWall : MonoBehaviour
{
    private AudioSource Source;
    private bool Activated;
    public AudioClip Voiceline;
    public GameObject ClipGameObject;

    private void Start()
    {
        Source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(!Activated && other.gameObject.tag == "Player")
        {
            Activated = true;
            StartEffect();
            StartCoroutine(AudioQue());
        }
    }

    void PlayVoiceLine()
    {
        var go = Instantiate(ClipGameObject);
        var audio = go.GetComponent<AudioSource>();
        audio.clip = Voiceline;
        audio.Play();
    }

    void StartEffect()
    {
        GameMaster.Player.EffectsHolder.transform.GetChild(0).gameObject.SetActive(true);
        GameMaster.Player.EffectsHolder.transform.GetChild(1).gameObject.SetActive(true);
    }
    IEnumerator AudioQue()
    {
        GameMaster.Mixer.Pause();
        Source.Play();
        yield return new WaitForSeconds(1.5f);
        PlayVoiceLine();
        yield return new WaitForSeconds(1f);
        GameMaster.Mixer.NextSong();
        Destroy(gameObject);
    }
}
