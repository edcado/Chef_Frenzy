using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IKitchenObject
{
    public static Player Instance { get; private set; }

    public event EventHandler <OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;
    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float speed = 7f;
    [SerializeField] Animator myanimator;
    [SerializeField] PlayerInputs playerInputs;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] Transform kitchenObjectPoint;

    Vector3 lastInteractDirection;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerInputs.OnInteractAction += PlayerInputs_OnInteractAction;
        playerInputs.OnInteractAlternativeAction += PlayerInputs_OnInteractAlternativeAction;
    }

    private void PlayerInputs_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void PlayerInputs_OnInteractAlternativeAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    void Update()
    {
        Movement();
        Interact();
    }

    void Interact()
    {
        Vector2 inputMovement = playerInputs.GetMovementNormalized();
        Vector3 moveDirection = new Vector3(inputMovement.x, 0, inputMovement.y);

        if (moveDirection != Vector3.zero)
        {
            lastInteractDirection = moveDirection;
        }
        else
        {
            lastInteractDirection = transform.forward;
        }

        float interactDistance = 2f;
        RaycastHit raycastHit;

        if (Physics.Raycast(transform.position, lastInteractDirection, out raycastHit, interactDistance))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                // Solo cambiar si el selectedCounter es diferente
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else if (selectedCounter != null) // Asegúrate de que solo se llame si el selectedCounter ya no es válido
            {
                SetSelectedCounter(null);
            }
        }
        else if (selectedCounter != null)
        {
            SetSelectedCounter(null);
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
            float rotationSpeed = 20f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
        }

        // Actualizar animación de caminar
        bool isWalking = moveDirection.magnitude > 0;
        myanimator.SetBool("IsWalking", isWalking);
    }

    private void SetSelectedCounter(BaseCounter newSelectedCounter)
    {
        if (selectedCounter == newSelectedCounter) return;  // Si ya es el seleccionado, no hacer nada

        selectedCounter = newSelectedCounter;

        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
