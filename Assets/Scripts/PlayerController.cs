using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody _Rigidbody;

    public int AccelerationMultiplier;
    public int JumpForceMultiplier;
    //public int StrafeMultiplier;
    //public int ForwardMultiplier;
    //public int BackwardMultiplier;
    [Range(0f, 1f)]
    public float StrafeMaxSpeed;
    [Range(0f, 1f)]
    public float ForwardMaxSpeed;
    [Range(0f, 1f)]
    public float BackwardMaxSpeed;
    public float LookMultiplier;

    public Vector3 TorchPosition;

    private bool CanJump;
    private float MovementVectorX;
    private float MovementVectorZ;

    #region MonoBehaviour
    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        GameMaster.Player = this;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movement();
        Interact();
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
    #endregion

    private void Movement()
    {
        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.position += transform.forward * ForwardMultiplier * Time.deltaTime;
        //}
        //else if (Input.GetKey(KeyCode.S))
        //{
        //    transform.position -= transform.forward * BackwardMultiplier * Time.deltaTime;
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.position += transform.right * StrafeMultiplier * Time.deltaTime;
        //}
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    transform.position -= transform.right * StrafeMultiplier * Time.deltaTime;
        //}

        if (Input.GetKey(KeyCode.W))
        {
            MovementVectorZ += AccelerationMultiplier * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MovementVectorZ -= AccelerationMultiplier * Time.deltaTime;
        }
        else if(MovementVectorZ != 0)
        {
            if(Mathf.Abs(MovementVectorZ) < 0.1f)
                MovementVectorZ = 0;
            else
                MovementVectorZ -= Mathf.Sign(MovementVectorZ) * AccelerationMultiplier * 2 * Time.deltaTime;
        }

        MovementVectorZ = Mathf.Clamp(MovementVectorZ, -BackwardMaxSpeed, ForwardMaxSpeed);


        if (Input.GetKey(KeyCode.D))
        {
            MovementVectorX += AccelerationMultiplier * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            MovementVectorX -= AccelerationMultiplier * Time.deltaTime;
        }
        else if (MovementVectorX != 0)
        {
            if (Mathf.Abs(MovementVectorX) < 0.1f)
                MovementVectorX = 0;
            else
                MovementVectorX -= Mathf.Sign(MovementVectorX) * AccelerationMultiplier * 2 * Time.deltaTime;
        }

        MovementVectorX = Mathf.Clamp(MovementVectorX, -StrafeMaxSpeed, StrafeMaxSpeed);

        transform.position += transform.forward * MovementVectorZ + transform.right * MovementVectorX;
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.position += transform.right * StrafeMultiplier * Time.deltaTime;
        //}
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    transform.position -= transform.right * StrafeMultiplier * Time.deltaTime;
        //}

        if (Input.GetKeyDown(KeyCode.Space) && CanJump)
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
            GameMaster.Camera.transform.RotateAround(transform.position + (Vector3.up * 2), Vector3.right, yDelta * LookMultiplier * Time.deltaTime);

            Vector3 dir = (GameMaster.Camera.transform.position - transform.position).normalized;
            GameMaster.Camera.transform.position = transform.position + dir * 5;
        }
    }

    private void Interact()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var interactable = hit.collider.gameObject.GetComponent<IInteractableObject>();
                if (interactable != null)
                    interactable.Interact();
            }
        }
    }
}
