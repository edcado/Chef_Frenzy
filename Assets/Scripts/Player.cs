using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] Animator myanimator;
    [SerializeField] PlayerInputs playerInputs;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 inputMovement = playerInputs.GetMovementNormalized();

        Vector3 moveDirection = new Vector3(inputMovement.x, 0, inputMovement.y);

        // Mueve al jugador
        transform.position += moveDirection * speed * Time.deltaTime;

        // Actualiza la rotaci�n si el jugador se est� moviendo
        if (moveDirection != Vector3.zero)
        {
            float rotationSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
        }

        // L�gica de animaci�n: comprueba si el jugador se est� moviendo
        bool isWalking = moveDirection.magnitude > 0;
        myanimator.SetBool("IsWalking", isWalking);
    }
}
