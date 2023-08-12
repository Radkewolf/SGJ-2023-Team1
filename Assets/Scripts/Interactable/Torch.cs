using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour, IInteractableObject
{
    Collider _Collider;
    void Start()
    {
        _Collider = GetComponent<Collider>();
    }

    public void Interact()
    {
        transform.parent = GameMaster.Player.transform;
        transform.localPosition = GameMaster.Player.TorchPosition;
        _Collider.enabled = false;
    }
}
