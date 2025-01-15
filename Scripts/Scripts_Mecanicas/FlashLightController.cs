using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLightController : MonoBehaviour
{
    public Light FlashLight;

    private void Start()
    {
        if (FlashLight != null) FlashLight.enabled = false;
    }

    private void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("No gamepad connected.");
            return; // No gamepad connected.
        }
        if (!InventoryManager.Instance.HasItem(Item.ItemType.Light))
        {
            FlashLight.enabled = false;
        }
        else if (gamepad.buttonEast.wasPressedThisFrame && (CreditScreen.creditsActive == false && GameOverScreen.gameoverActive == false))
        {
            ToggleFlashLight();
        }
    }

    public void ToggleFlashLight()
    {
        FlashLight.enabled = !FlashLight.enabled;
    }
}
