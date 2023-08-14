using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpable : InteractableObject
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
    public override void Interact()
    {
        if (PickedUp)
        {
            GameMaster.Player.Interactable = null;
            transform.parent = null;
            PickedUp = false;
            //_Rigidbody.isKinematic = false;
            _Rigidbody.useGravity = true;
            _Rigidbody.constraints = RigidbodyConstraints.None;
            //_Rigidbody = true;
        }
        else
        {
            GameMaster.Player.Interactable = this;
            transform.parent = GameMaster.Player.CameraHolder.transform;
            PickedUp = true;
            //_Rigidbody.isKinematic = true;
            _Rigidbody.useGravity = false;
            _Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
