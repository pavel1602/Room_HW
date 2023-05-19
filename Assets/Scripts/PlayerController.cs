using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _inventory;
    
    private InteractableItem _lastInteractableItem;
    private InteractableItem _lastPictedUpItem;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HighLightSelectedObject();

        if (Input.GetMouseButtonDown(0) && _lastInteractableItem != null)
        {
            _lastInteractableItem.ThrowAway(transform.forward);
            _lastInteractableItem = null;
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickUpItem();
            
            var door = RaycastUtils.GetSelectedObject<Door>();
            if (door != null)
            {
                door.SwitchDoorState();
            }
        }
    }

    private void TryPickUpItem()
    {
        var interactableItemObject = RaycastUtils.GetSelectedObject<InteractableItem>();
        if (interactableItemObject != _lastPictedUpItem)
        {
            if (_lastPictedUpItem != null)
            {
                _lastPictedUpItem.Drop();
            }

            if (interactableItemObject != null)
            {
                interactableItemObject.PickUp(_inventory);
            }
            _lastPictedUpItem = interactableItemObject;
        }
    }

    private void HighLightSelectedObject()
    {
        var interactableItemObject = RaycastUtils.GetSelectedObject<InteractableItem>();

        if (_lastInteractableItem != interactableItemObject)
        {
            if (_lastInteractableItem != null)
            {
                _lastInteractableItem.RemoveFocus();
            }

            if (interactableItemObject != null)
            {
                interactableItemObject.SetFocus();
            }
            _lastInteractableItem = interactableItemObject;
        }
    }
}