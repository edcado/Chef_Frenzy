using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] Animator myanimator;
    [SerializeField] PlayerInputs playerInputs;
    [SerializeField] private LayerMask counterLayerMask;

    Vector3 lastInteractDirection;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerInputs.OnInteractAction += PlayerInputs_OnInteractAction;
    }

    private void PlayerInputs_OnInteractAction(object sender, System.EventArgs e)
    {
        //Planteo hacer el raycast mas ancho porque desde el lado no detecta bien la mesa

        Vector2 inputMovement = playerInputs.GetMovementNormalized();

        //Movement Vector
        Vector3 moveDirection = new Vector3(inputMovement.x, 0, inputMovement.y);

        if (moveDirection != Vector3.zero)
        {
            lastInteractDirection = moveDirection;
        }

        float interactDistance = 2f;
        RaycastHit raycastHit;

        if (Physics.Raycast(transform.position, lastInteractDirection, out raycastHit, interactDistance))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //Has ClearCounter
                clearCounter.Interact();
            }
        }
    }

    void Update()
    {
        Movement();

    }

    void Movement()
    {
        Vector2 inputMovement = playerInputs.GetMovementNormalized();

        //Movement Vector
        Vector3 moveDirection = new Vector3(inputMovement.x, 0, inputMovement.y);

        float moveDistance = speed * Time.deltaTime;
        float playerCheckRadious = .7f;
        float playerHeight = 2.0f;

        //Rycast for the objects detection
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerCheckRadious, moveDirection, moveDistance);

        //if can move, move the player
        if (canMove)
        {
            transform.position += moveDirection * speed * Time.deltaTime;
        }


        //PlayerRotation
        if (moveDirection != Vector3.zero)
        {
            float rotationSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
        }

        //IsWalking
        bool isWalking = moveDirection.magnitude > 0;
        myanimator.SetBool("IsWalking", isWalking);
    }

}
