using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    public static GameInputManager Instance { get; private set; }

    private GameInput gameInput;

    public static event EventHandler onPauseAction;

    private void Awake()
    {
        if (Instance != null)
        {
            throw new Exception("PlayerInput already exists!");
        }
        Instance = this;

        gameInput = new GameInput();
        gameInput.Player.Enable();

        gameInput.Player.Pause.performed += TriggerPauseEvent;
    }

    private void OnDestroy()
    {
        gameInput.Player.Pause.performed -= TriggerPauseEvent;
        gameInput.Dispose();
    }

    private void TriggerPauseEvent(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        onPauseAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetNormalizedInputVector()
    {
        Vector2 inputVector = gameInput.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }
}
