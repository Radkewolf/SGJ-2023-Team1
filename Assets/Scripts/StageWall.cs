using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageWall : MonoBehaviour
{
    public AudioClip Audio;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            PlayVoiceLine();
            StartEffect();
            Destroy(gameObject);
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
}
