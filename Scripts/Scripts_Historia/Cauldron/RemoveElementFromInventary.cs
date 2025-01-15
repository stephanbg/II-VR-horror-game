using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Asegúrate de tener esta directiva using

public class RemoveElementFromInventary : MonoBehaviour
{
    
    public CauldronColliderNotifier notifier;
    public Item.ItemType itemType = Item.ItemType.Heart;
    
    private void OnEnable()
    {
        if (notifier != null)
        {
            notifier.OnCharacterCollision += Interaction; // Suscribir al evento de colisión
        }
    }

    private void OnDisable()
    {
        if (notifier != null)
        {
            notifier.OnCharacterCollision -= Interaction; // Cancelar suscripción al evento de colisión
        }
    }

    private void Interaction()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            return; // No gamepad connected.
        }

        // Verificar si el botón East está presionado
        if (gamepad.buttonWest.isPressed && InventoryManager.Instance.HasItem(itemType))
        {
            // Eliminar el ítem "Heart" del inventario
            Item heartItem = InventoryManager.Instance.Items.Find(item => item.itemType == itemType);
            if (heartItem != null)
            {
                InventoryManager.Instance.Remove(heartItem);
            }
        }
    }
}