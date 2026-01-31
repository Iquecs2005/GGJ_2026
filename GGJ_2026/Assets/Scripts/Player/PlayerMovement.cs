using System;
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
    [SerializeField] private float minInputForTurning;

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

        CalculateFacingDir();
    }

    private void CalculateFacingDir() 
    {
        if (Mathf.Abs(moveInput.x) >= Mathf.Abs(moveInput.y))
        {
            if (moveInput.x > minInputForTurning)
            {
                pc.SetFacingDirection(Vector2.right);
            }
            else if (moveInput.x < -minInputForTurning)
            {
                pc.SetFacingDirection(Vector2.left);
            }
        }
        else
        {
            if (moveInput.y > minInputForTurning)
            {
                pc.SetFacingDirection(Vector2.up);
            }
            else if (moveInput.y < -minInputForTurning)
            {
                pc.SetFacingDirection(Vector2.down);
            }
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
