using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody _Rigidbody;
    [HideInInspector]
    public GameObject CameraHolder;
    [HideInInspector]
    public GameObject EffectsHolder;
    [HideInInspector]
    public bool IsDead;

    public GameObject DeadCanvas;

    [Range(0f, 1f)]
    public float AccelerationMultiplier;
    public int BaseJumpForceMultiplier;
    public int JumpForceMultiplierSpeedIncrease;
    public int MaxJumpForceMultiplier;
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

    public Vector3 ItemPosition;

    public Equipable Equipment;
    private bool CanJump;
    private float MovementVectorS;
    private float MovementVectorF;
    private float CurrentJumpForce;

    #region MonoBehaviour
    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        CameraHolder = transform.GetChild(0).gameObject;
        EffectsHolder = transform.GetChild(1).gameObject;
        GameMaster.Player = this;
        Cursor.lockState = CursorLockMode.Locked;
        CurrentJumpForce = BaseJumpForceMultiplier;
    }

    void Update()
    {
        if (IsDead)
            return;
        CalculateMovement();
        Interact();
    }

    void FixedUpdate()
    {
        ExecuteMovement();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Plane_invisible")
        {
            collision.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }

        if (collision.gameObject.tag == "Death" && !IsDead)
        {
            Die();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            // Check if the contact normal is upwards. This implies the player is on top of the cube.
            if (contact.normal.y > 0.9f)  // The value 0.9f ensures that only surfaces that are almost completely upward will enable jumping.
            {
                Debug.Log("Can Jump");
                CanJump = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Cannot Jump");
        CanJump = false;
    }
    #endregion

    private void CalculateMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MovementVectorF += AccelerationMultiplier * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MovementVectorF -= AccelerationMultiplier * Time.deltaTime;
        }
        else if (MovementVectorF != 0)
        {
            if (Mathf.Abs(MovementVectorF) < 0.1f)
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

        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.position += transform.right * StrafeMultiplier * Time.deltaTime;
        //}
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    transform.position -= transform.right * StrafeMultiplier * Time.deltaTime;
        //}

        if (Input.GetKey(KeyCode.Space) && CanJump)
        {
            CurrentJumpForce += JumpForceMultiplierSpeedIncrease * Time.deltaTime;
            CurrentJumpForce = CurrentJumpForce > MaxJumpForceMultiplier ? MaxJumpForceMultiplier : CurrentJumpForce;
            Debug.Log(CurrentJumpForce);
        }
        else if(Input.GetKeyUp(KeyCode.Space) && CanJump)
        {
            _Rigidbody.AddForce(Vector3.up * CurrentJumpForce);
            CurrentJumpForce = BaseJumpForceMultiplier;
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
            currentRotation.x -= yDelta * Time.deltaTime * LookMultiplier;

            if (currentRotation.x > 180)
                currentRotation.x -= 360;

            currentRotation.x = Mathf.Clamp(currentRotation.x, 0, 45);
            CameraHolder.transform.eulerAngles = currentRotation;
        }
    }

    private void ExecuteMovement()
    {
        var movement = (transform.forward * MovementVectorF) + (transform.right * MovementVectorS);
        transform.Translate(movement, Space.World);
        if (MovementVectorF != 0)
        {
            //Debug.Log(transform.forward);
            //Debug.Log($"MovementVectorF {MovementVectorF}");
            //Debug.Log($"movement {movement}");
        }
    }

    private void Interact()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0) && Cursor.lockState == CursorLockMode.Locked)
        {
            if(Equipment != null)
            {
                Equipment.ItemAction();
            }
            else
            {
                var interactable = InteractableObject.RaycastForObject();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void SetJump(bool jump)
    {
        CanJump = jump;
    }

    private void Die()
    {
        IsDead = true;
        Instantiate(DeadCanvas);
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSeconds(5f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
