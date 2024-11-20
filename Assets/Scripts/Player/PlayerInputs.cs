using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternativeAction;
    public event EventHandler onGameOverAction;
    public event EventHandler onMainMenuLoadIn;
    public event EventHandler onMainMenuQuit;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternative.performed += InteractAlternative_performed;
        playerInputActions.Player.GameOver.performed += GameOver_performed;
        playerInputActions.Player.MainMenuLoadOn.performed += MainMenuLoadOn_performed;
        playerInputActions.Player.MainMenuQuit.performed += MainMenuQuit_performed;
    }

    private void MainMenuQuit_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        onMainMenuQuit?.Invoke(this, EventArgs.Empty);
    }

    private void MainMenuLoadOn_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        onMainMenuLoadIn?.Invoke(this, EventArgs.Empty);    
    }

    private void GameOver_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        onGameOverAction?.Invoke(this, EventArgs.Empty);    
    }

    private void InteractAlternative_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternativeAction?.Invoke(this, EventArgs.Empty);
    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementNormalized()
    {
        Vector2 inputMovement = playerInputActions.Player.PlayerMovement.ReadValue<Vector2>();
        inputMovement = inputMovement.normalized;
        return inputMovement;
    }
}

