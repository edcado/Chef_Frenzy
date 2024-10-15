using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public event EventHandler <OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;
    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private float speed = 7f;
    [SerializeField] Animator myanimator;
    [SerializeField] PlayerInputs playerInputs;
    [SerializeField] private LayerMask counterLayerMask;

    Vector3 lastInteractDirection;
    private ClearCounter selectedCounter;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerInputs.OnInteractAction += PlayerInputs_OnInteractAction;
    }

    private void PlayerInputs_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }

    void Update()
    {
        Movement();
        Interact();
    }

    void Interact()
    {
        {
            // Obtener la dirección de movimiento normalizada
            Vector2 inputMovement = playerInputs.GetMovementNormalized();

            // Vector de movimiento
            Vector3 moveDirection = new Vector3(inputMovement.x, 0, inputMovement.y);

            // Si el jugador está en movimiento, actualizamos la dirección de interacción
            if (moveDirection != Vector3.zero)
            {
                lastInteractDirection = moveDirection;
            }
            else
            {
                // Si no se está moviendo, usamos la dirección en la que el jugador está mirando
                lastInteractDirection = transform.forward;
            }

            // Definir distancia de interacción
            float interactDistance = 2f;
            RaycastHit raycastHit;

            // Lanzar el Raycast en la dirección de interacción
            if (Physics.Raycast(transform.position, lastInteractDirection, out raycastHit, interactDistance))
            {
                if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
                {
                    if (clearCounter != selectedCounter)
                    {
                        selectedCounter = clearCounter;
                    }
                    else
                    {
                        selectedCounter = null;
                    }
                }

                else
                {
                    selectedCounter = null;
                }
            }
        }
    }

    void Movement()
    {
        Vector2 inputMovement = playerInputs.GetMovementNormalized();

        // Vector de movimiento
        Vector3 moveDirection = new Vector3(inputMovement.x, 0, inputMovement.y);

        float moveDistance = speed * Time.deltaTime;
        float playerCheckRadious = .7f;
        float playerHeight = 2.0f;

        // Raycast para detectar objetos
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerCheckRadious, moveDirection, moveDistance);

        // Si puede moverse, mover al jugador
        if (canMove)
        {
            transform.position += moveDirection * speed * Time.deltaTime;
        }

        // Rotación del jugador
        if (moveDirection != Vector3.zero)
        {
            float rotationSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
        }

        // Actualizar animación de caminar
        bool isWalking = moveDirection.magnitude > 0;
        myanimator.SetBool("IsWalking", isWalking);
    }
}
