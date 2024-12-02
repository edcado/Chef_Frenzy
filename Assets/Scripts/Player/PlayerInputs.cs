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
    public event EventHandler OnGamePaused;

    private PlayerInputActions playerInputActions;

    public static PlayerInputs Instance { get; private set; }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        Instance = this;

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternative.performed += InteractAlternative_performed;
        playerInputActions.Player.GameOver.performed += GameOver_performed;
        playerInputActions.Player.GamePaused.performed += GamePaused_performed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternative.performed -= InteractAlternative_performed;
        playerInputActions.Player.GameOver.performed -= GameOver_performed;
        playerInputActions.Player.GamePaused.performed -= GamePaused_performed;

        playerInputActions.Dispose();
    }

    private void GamePaused_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnGamePaused?.Invoke(this, EventArgs.Empty);
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

