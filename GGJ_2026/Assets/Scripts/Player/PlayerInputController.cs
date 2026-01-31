using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector2> OnMoveEvent;
    [SerializeField] private UnityEvent OnInteractionEvent;

    public void OnMove(InputAction.CallbackContext context) 
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        OnMoveEvent.Invoke(moveInput);
    }

    public void OnInteract(InputAction.CallbackContext context) 
    {
        if (context.performed)
            OnInteractionEvent.Invoke();
    }

    public void OnBlame(InputAction.CallbackContext context)
    {
        if (context.performed)
            print("Blame");
    }

    public void OnNotebook(InputAction.CallbackContext context)
    {
        if (context.performed)
            print("Notebook");
    }
}
