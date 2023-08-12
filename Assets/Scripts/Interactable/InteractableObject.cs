using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public abstract void Interact();

    public static InteractableObject RaycastForObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var layer_mask = LayerMask.GetMask("PickUp");
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 19f, layer_mask))
        {
            var interactable = hit.collider.gameObject.GetComponent<InteractableObject>();
            return interactable;
        }

        return null;
    }
}
