using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] Animator myanimator;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 inputMovement = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputMovement.y += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputMovement.y -= 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputMovement.x -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputMovement.x += 1;
        }

        inputMovement = inputMovement.normalized;

        Vector3 moveDirection = new Vector3(inputMovement.x, 0, inputMovement.y);

        // Mueve al jugador
        transform.position += moveDirection * speed * Time.deltaTime;

        // Actualiza la rotación si el jugador se está moviendo
        if (moveDirection != Vector3.zero)
        {
            float rotationSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
        }

        // Lógica de animación: comprueba si el jugador se está moviendo
        bool isWalking = moveDirection.magnitude > 0;
        myanimator.SetBool("IsWalking", isWalking);
    }
}
