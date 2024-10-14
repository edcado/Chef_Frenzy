using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }
    public Vector2 GetMovementNormalized()
    {
        Vector2 inputMovement = playerInputActions.Player.PlayerMovement.ReadValue<Vector2>();


        inputMovement = inputMovement.normalized;

        return inputMovement;
    }
}
