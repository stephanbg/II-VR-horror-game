using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Asegúrate de tener esta directiva using

public class OpenOtherDoor : MonoBehaviour
{
    public GameObject animation;
    public GameObject thisTrigger;
    //public AudioSource doorOpenSound;
    public bool action = false;
    private bool isDoorOpen = false;

    void OnTriggerEnter(Collider collision)
    {
        if (
            collision.transform.tag == "Player" &&
            gameObject.layer == LayerMask.NameToLayer("InteractableElements")
        )
        {
            action = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        action = false;
    }

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("No gamepad connected.");
            return; // No gamepad connected.
        }

        if (gamepad.buttonWest.wasPressedThisFrame && action == true)
        {
            if (!isDoorOpen)
            {
                animation.GetComponent<Animator>().Play("OpenOtherDoor"); 
            }
            else
            {
                animation.GetComponent<Animator>().Play("CloseOtherDoor");
            }

            isDoorOpen = !isDoorOpen;
            action = false;
        }
    }
}
