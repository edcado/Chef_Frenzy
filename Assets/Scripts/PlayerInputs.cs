using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = GetComponent<PlayerInputActions>();
    }
    public Vector2 GetMovementNormalized()
    {
        Vector2 inputMovement = new Vector2(0, 0);


        inputMovement = inputMovement.normalized;

        return inputMovement;
    }
}
