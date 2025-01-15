using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    private bool isPointerOver = false;
    public Transform playerTransform; // Referencia al transform del jugador
    public float pickupRange = 2.0f;

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("No gamepad connected.");
            return; // No gamepad connected.
        }

        // Verificar si el botón X está presionado y la retícula está sobre el objeto
        if (isPointerOver && gamepad.buttonWest.wasPressedThisFrame && IsPlayerInRange())
        {
            Pickup();
        }
    }

    public void OnPointerEnter()
    {
        isPointerOver = true;
    }

    public void OnPointerExit()
    {
        isPointerOver = false;
    }
    void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        gameObject.layer = 0;
        gameObject.SetActive(false);
    }
    bool IsPlayerInRange()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("Player transform is not assigned.");
            return false;
        }

        float distance = Vector3.Distance(playerTransform.position, transform.position);
        return distance <= pickupRange;
    }
}
