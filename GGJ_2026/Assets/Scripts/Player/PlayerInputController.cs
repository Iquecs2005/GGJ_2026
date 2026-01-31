using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector2> OnMoveEvent;

    public void OnMove(InputAction.CallbackContext context) 
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        OnMoveEvent.Invoke(moveInput);
    }
}
