using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageWall : MonoBehaviour
{
    private AudioSource Source;
    private bool Activated;

    private void Start()
    {
        Source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(!Activated && other.gameObject.name == "Player")
        {
            Activated = true;
            StartEffect();
            StartCoroutine(AudioQue());
        }
    }

    void PlayVoiceLine()
    {

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
