using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Light FlashLight;

    private void Awake() {
        Instance = this;
    }

    public void UseFlashLight() {
        if (InventoryManager.Instance.HasItem(Item.ItemType.Light)) {
            FlashLight.enabled = !FlashLight.enabled;
        } else {
            Debug.Log("Flashlight is not in inventory.");
        }
    }
}
