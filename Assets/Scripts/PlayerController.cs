using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody _Rigidbody;


    public int JumpForceMultiplier;
    public int StrafeMultiplier;
    public int ForwardMultiplayer;
    public int BackwardMultiplayer;
    public int LookMultiplier;

    public Vector3 TorchPosition;

    private bool CanJump;

    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        GameMaster.Player = this;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * ForwardMultiplayer * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * BackwardMultiplayer * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * StrafeMultiplier * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * StrafeMultiplier * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) && CanJump)
        {
            _Rigidbody.AddForce(Vector3.up * JumpForceMultiplier);
        }

        var xDelta = Input.GetAxis("Mouse X");
        if (xDelta != 0)
        {
            transform.Rotate(0, xDelta * Time.deltaTime * LookMultiplier, 0);
        }

        var yDelta = Input.GetAxis("Mouse Y");
        if (yDelta != 0)
        {
            GameMaster.Camera.transform.RotateAround(transform.position, Vector3.right, yDelta * LookMultiplier * Time.deltaTime);

            Vector3 dir = (GameMaster.Camera.transform.position - transform.position).normalized;
            GameMaster.Camera.transform.position = transform.position + dir * 10;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            CanJump = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        CanJump = false;
    }
}
