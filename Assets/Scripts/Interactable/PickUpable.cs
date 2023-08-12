using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpable : MonoBehaviour, IInteractableObject
{
    private bool PickedUp;
    // Start is called before the first frame update
    void Start()
    {
        
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
        }
        else
        {
            transform.parent = GameMaster.Player.CameraHolder.transform;
            PickedUp = true;
        }
    }
}
