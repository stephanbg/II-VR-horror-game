using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class OpenDoor : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public Transform playerTransform; // Referencia al transform del jugador
    public float interactionRange = 3.0f; // Distancia máxima para interactuar con la puerta

    private bool isOpen = false;
    private bool isPointerOver = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
    }

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("No gamepad connected.");
            return; // No gamepad connected.
        }

        // Verificar si el botón X está presionado, la retícula está sobre la puerta y el jugador está cerca
        if (isPointerOver && gamepad.buttonWest.wasPressedThisFrame && IsPlayerInRange())
        {
            isOpen = !isOpen;
        }

        if (isOpen)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, Time.deltaTime * openSpeed);
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

    bool IsPlayerInRange()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("Player transform is not assigned.");
            return false;
        }

        float distance = Vector3.Distance(playerTransform.position, transform.position);
        return distance <= interactionRange;
    }
}