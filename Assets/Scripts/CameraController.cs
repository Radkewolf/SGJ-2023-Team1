using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    void Start()
    {
        GameMaster.Camera = this;
        Player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(Player.transform.position);
    }
}
