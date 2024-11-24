using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour, IKitchenObject
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;
    public event EventHandler OnPickUpSomething;

    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float speed = 7f;
    [SerializeField] Animator myanimator;
    [SerializeField] PlayerInputs playerInputs;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] Transform kitchenObjectPoint;

    private Vector3 lastInteractDirection;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;
    public bool isMoving;

    [SerializeField] private Image image;
    [SerializeField] private Image image2;


    private void Awake()
    {
        Instance = this;
        image.gameObject.SetActive(false);
        image2.gameObject.SetActive(false);
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
        if (!KitchenGameManager.Instance.isPlayingGame())
            return;

        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void PlayerInputs_OnInteractAlternativeAction(object sender, System.EventArgs e)
    {
        if (!KitchenGameManager.Instance.isPlayingGame())
            return;

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

        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);                                  
                }            
            }
            else
            {
                image.gameObject.SetActive(false);
                SetSelectedCounter(null);
            }
        }
        else
        {
            image.gameObject.SetActive(false);
            SetSelectedCounter(null);
        }
    }

    void Movement()
    {
        Vector2 inputMovement = playerInputs.GetMovementNormalized();
        Vector3 moveDirection = new Vector3(inputMovement.x, 0f, inputMovement.y);

        float moveDistance = speed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = (moveDirection.x < -.5f || moveDirection.x > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDirection = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = (moveDirection.z < -.5f || moveDirection.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDirection = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        isMoving = moveDirection != Vector3.zero;
        myanimator.SetBool("IsWalking", moveDirection != Vector3.zero);

        if (moveDirection != Vector3.zero)
        {
            float rotationSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
        }
    }

    private void SetSelectedCounter(BaseCounter newSelectedCounter)
    {
        if (selectedCounter == newSelectedCounter) return;

        selectedCounter = newSelectedCounter;

        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs
        {
            selectedCounter = selectedCounter,
        }) ;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnPickUpSomething?.Invoke(this, EventArgs.Empty);
        }
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
