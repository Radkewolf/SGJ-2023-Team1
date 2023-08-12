using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : InteractableObject
{
    Collider _Collider;
    void Start()
    {
        _Collider = GetComponent<Collider>();
    }

    public override void Interact()
    {
        transform.parent = GameMaster.Player.transform;
        transform.localPosition = GameMaster.Player.TorchPosition;
        _Collider.enabled = false;
    }
}
