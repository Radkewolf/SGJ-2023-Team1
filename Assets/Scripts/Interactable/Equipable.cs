using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipable : InteractableObject
{
    public GameObject IntactModel;
    public GameObject BrokenModel;

    bool IsEquipped;
    Collider _Collider;
    Rigidbody _Rigidbody;
    int State;
    void Start()
    {
        _Collider = GetComponent<Collider>();
        _Rigidbody = GetComponent<Rigidbody>();
        State = 2;
    }

    public override void Interact()
    {
        transform.parent = GameMaster.Player.transform;
        transform.localPosition = GameMaster.Player.ItemPosition;
        transform.localRotation = Quaternion.Euler(0, -90, 0);
        //_Collider.enabled = false;
        GameMaster.Player.Equipment = this;
        IsEquipped = true;
        _Rigidbody.useGravity = false;
        _Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        _Collider.enabled = false;
    }

    public void ItemAction()
    {
        var interactable = InteractableObject.RaycastForObject();
        if (interactable != null)
        {
            DoAnimation();
            interactable.transform.localScale *= 0.95f;
            UpdateState();
            if(State == 0)
            {
                IntactModel.SetActive(false);
                BrokenModel.SetActive(true);
                DropItem();
            }
        }
    }

    private void DoAnimation()//?
    {
    }

    private void DropItem()
    {
        GameMaster.Player.Equipment = null;
        transform.parent = null;
        _Rigidbody.useGravity = true;
        _Rigidbody.constraints = RigidbodyConstraints.None;
        gameObject.layer = 0;
        _Collider.enabled = true;
    }

    private void UpdateState()
    {
        State -= 1;
    }
}
