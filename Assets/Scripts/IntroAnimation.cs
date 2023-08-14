using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroAnimation : MonoBehaviour
{
    public Vector3 Torque;
    public Rigidbody Body;
    public GameObject Player;
    public AudioSource Audio;
    public Image Black;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.4f;
        Body.AddTorque(Torque);
        StartCoroutine(FadeIn()); 
        GameMaster.Finished = false;
    }

    IEnumerator FadeIn()
    {
        var color = Black.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            color.a = alpha;
            Black.color = color;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1.5f);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        var color = Black.color;
        for (float alpha = 0f; alpha <= 1f; alpha += 0.1f)
        {
            color.a = alpha;
            Black.color = color;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.5f);
        StartGame();
    }

    void StartGame()
    {
        Player.SetActive(true);
        Audio.Play();
        Time.timeScale = 1f;
        Destroy(Black.transform.parent.gameObject);
        Destroy(gameObject);
    }
}
