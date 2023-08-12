using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpable : MonoBehaviour, IInteractableObject
{
    private bool PickedUp;
    private Rigidbody _Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Interact()
    {
        if(PickedUp)
        {
            transform.parent = null;
            PickedUp = false;
            _Rigidbody.isKinematic = false;
            _Rigidbody.useGravity = true;
        }
        else
        {
            transform.parent = GameMaster.Player.CameraHolder.transform;
            PickedUp = true;
            _Rigidbody.isKinematic = true;
            _Rigidbody.useGravity = false;
        }
    }
}
