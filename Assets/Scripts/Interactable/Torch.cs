using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour, IInteractableObject
{
    public void Interact()
    {
        transform.parent = GameMaster.Player.transform;
        transform.position = GameMaster.Player.TorchPosition;
    }
}
