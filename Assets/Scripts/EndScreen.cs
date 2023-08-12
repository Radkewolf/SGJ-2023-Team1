using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            DisplayEndScreen();
            Time.timeScale = 0;
        }
    }

    private void DisplayEndScreen()
    {
        Instantiate(EndCanvas);
    }
}
