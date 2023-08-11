using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody _Rigidbody;
    public int JumpForceMultiplier;
    public int StrafeMultiplier;
    public int ForwardMultiplayer;
    public int BackwardMultiplayer;

    private bool CanJump;
    // Start is called before the first frame update
    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
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
