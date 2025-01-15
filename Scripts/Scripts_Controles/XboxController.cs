using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class XboxController : MonoBehaviour
{
    public float speed = 2.0f;
    public float runSpeed = 4.0f;
    public float rotationSpeed = 15.0f;
    public GameObject vrCamera;
    private CharacterController characterController;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float gravity = -9.81f;
    private Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("No gamepad connected.");
            return; // No gamepad connected.
        }

        // Obtener la dirección de movimiento basada en la entrada del mando
        moveInput = gamepad.leftStick.ReadValue();
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);

        // Convertir la dirección de movimiento a la dirección en la que está mirando la cámara
        direction = vrCamera.transform.TransformDirection(direction);
        direction.y = 0; // Asegurarse de que el movimiento sea solo en el plano XZ

        // Verificar si el joystick izquierdo está presionado para correr
        float currentSpeed = gamepad.leftStickButton.isPressed ? runSpeed : speed;

        // Mover al jugador
        characterController.Move(direction * currentSpeed * Time.deltaTime);

        // Aplicar gravedad
        if (characterController.isGrounded)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);

        // Girar al jugador basado en la entrada del joystick derecho
        lookInput = gamepad.rightStick.ReadValue();
        if (lookInput.x > 0.5f)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        else if (lookInput.x < -0.5f)
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
    }
}