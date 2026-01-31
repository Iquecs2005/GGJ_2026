using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController pc;
    
    [Header("Movement variables")]
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float accelerationRate;
    [SerializeField] private float desaccelerationRate;

    private float currentAcceleration;
    private float currentDesacceleration;

    private Vector2 moveInput;

    private void Start()
    {
        if (pc == null) 
            pc = GetComponent<PlayerController>();

        currentAcceleration = accelerationRate;
        currentDesacceleration = desaccelerationRate;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    public void SetMoveInput(Vector2 value) 
    {
        moveInput = value;

        if (moveInput.sqrMagnitude > 1) 
        {
            moveInput.Normalize();
        }
    }

    private void ApplyMovement()
    {
        Vector2 targetSpeed = moveInput * maxMoveSpeed;

        Vector2 speedDif = targetSpeed - pc.rb.velocity;

        float accelRate;
        if (targetSpeed.magnitude > 0.01f)
        {
            accelRate = currentAcceleration;
        }
        else
        {
            accelRate = currentDesacceleration;
        }

        pc.rb.AddForce(speedDif * accelRate);
    }
}
