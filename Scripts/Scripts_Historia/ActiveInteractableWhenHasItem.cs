using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInteractableWhenHasItem : MonoBehaviour
{
    public Item.ItemType itemType = Item.ItemType.Heart;
    private bool hasChange;
    private Color originalEmissionColor; // Para almacenar el color de emisión original
    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        hasChange = false;
        // Verificar si el material tiene la propiedad de emisión
        if (objectRenderer.material.HasProperty("_EmissionColor"))
        {
            originalEmissionColor = objectRenderer.material.GetColor("_EmissionColor");
        }
    }

    public void OnPointerEnter() {

    }

    public void OnPointerExit() {
      
    }
    private void Update()
    {
        // Cuando el inventario NO tiene el ítem
        if (!InventoryManager.Instance.HasItem(itemType))
        {
            // Restaurar capa a "Default"
            if (gameObject.layer != LayerMask.NameToLayer("Default"))
            {
                gameObject.layer = LayerMask.NameToLayer("Default");
            }

            // Restaurar el brillo original de la emisión
            if (objectRenderer.material.HasProperty("_EmissionColor"))
            {
                objectRenderer.material.SetColor("_EmissionColor", originalEmissionColor);
            }

            hasChange = false;
        }
        // Cuando el inventario TIENE el ítem
        else if (InventoryManager.Instance.HasItem(itemType) && !hasChange)
        {
            // Cambiar capa a "InteractableElements"
            if (gameObject.layer != LayerMask.NameToLayer("InteractableElements"))
            {
                gameObject.layer = LayerMask.NameToLayer("InteractableElements");
            }

            hasChange = true;
        }
    }
}