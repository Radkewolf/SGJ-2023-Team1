using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody _Rigidbody;
    [HideInInspector]
    public GameObject CameraHolder;

    [Range(0f, 1f)]
    public float AccelerationMultiplier;
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
    private float MovementVectorS;
    private float MovementVectorF;

    #region MonoBehaviour
    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        CameraHolder = transform.GetChild(0).gameObject;
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
            Debug.Log("Can Jump");
            CanJump = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Cannot Jump");
            CanJump = false;
        }
    }
    #endregion

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MovementVectorF += AccelerationMultiplier * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MovementVectorF -= AccelerationMultiplier * Time.deltaTime;
        }
        else if(MovementVectorF != 0)
        {
            if(Mathf.Abs(MovementVectorF) < 0.1f)
                MovementVectorF = 0;
            else
                MovementVectorF -= Mathf.Sign(MovementVectorF) * AccelerationMultiplier * 2 * Time.deltaTime;
        }

        MovementVectorF = Mathf.Clamp(MovementVectorF, -BackwardMaxSpeed, ForwardMaxSpeed);


        if (Input.GetKey(KeyCode.D))
        {
            MovementVectorS += AccelerationMultiplier * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            MovementVectorS -= AccelerationMultiplier * Time.deltaTime;
        }
        else if (MovementVectorS != 0)
        {
            if (Mathf.Abs(MovementVectorS) < 0.1f)
                MovementVectorS = 0;
            else
                MovementVectorS -= Mathf.Sign(MovementVectorS) * AccelerationMultiplier * 2 * Time.deltaTime;
        }

        MovementVectorS = Mathf.Clamp(MovementVectorS, -StrafeMaxSpeed, StrafeMaxSpeed);

        var movement = (transform.forward * MovementVectorF) + (transform.right * MovementVectorS);
        transform.Translate(movement, Space.World);
        if(MovementVectorF != 0)
        {
            Debug.Log(transform.forward);
            Debug.Log($"MovementVectorF {MovementVectorF}");
            Debug.Log($"movement {movement}");
        }
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
            Vector3 currentRotation = CameraHolder.transform.eulerAngles;
            currentRotation.x += yDelta * Time.deltaTime * LookMultiplier;

            if (currentRotation.x > 180)
                currentRotation.x -= 360;

            currentRotation.x = Mathf.Clamp(currentRotation.x, 0, 45);
            CameraHolder.transform.eulerAngles = currentRotation;
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
