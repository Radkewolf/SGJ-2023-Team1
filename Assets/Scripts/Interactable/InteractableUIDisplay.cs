using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableUIDisplay : MonoBehaviour
{
    private RawImage _Image;
    void Start()
    {
        _Image = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.IsDead)
            return;

        if (Cursor.lockState == CursorLockMode.None)
        {
            _Image.enabled = false;
        }
        else
        {
            _Image.enabled = true;

            var interactable = InteractableObject.RaycastForObject();
            if (interactable != null)
            {
                _Image.enabled = true;
            }
            else
            {
                _Image.enabled = false;
            }
        }
    }
}
