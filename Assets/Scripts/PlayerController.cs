using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody _Rigidbody;
    public int ForceMultiplier;

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
            transform.position += transform.forward * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) && CanJump)
        {
            _Rigidbody.AddForce(Vector3.up * ForceMultiplier);
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
