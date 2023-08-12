using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableUIDisplay : MonoBehaviour
{
    private Image _Image;
    void Start()
    {
        _Image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.Player.IsDead)
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
                _Image.color = Color.blue;
            }
            else
            {
                _Image.color = Color.white;
            }
        }
    }
}
