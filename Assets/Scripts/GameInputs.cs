using UnityEditor;
using UnityEngine;
using System;

public class GameInputs : MonoBehaviour

   
{

    public event EventHandler OnInteractAction;
    private PlayerInputActions playerInputActions;

    private void Awake() {
          playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMoventVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        return inputVector;
    }

 
}
