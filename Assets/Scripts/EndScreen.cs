using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public GameObject EndCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameMaster.Finished && Input.GetKeyUp(KeyCode.Mouse0))
        {
            SceneManager.LoadScene(0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DisplayEndScreen();
            GameMaster.IsDead = true;
            StartCoroutine(DelayForFinish());
        }
    }

    private void DisplayEndScreen()
    {
        Instantiate(EndCanvas);
    }

    IEnumerator DelayForFinish()
    {
        yield return new WaitForSeconds(3f);
        GameMaster.Finished = true;
    }
}
