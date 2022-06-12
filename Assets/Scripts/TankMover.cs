using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float maxSpeed = 100f;
    public float minSpeed = -20f;
    public float rotationSpeed = 100f;
    private Vector2 movementVector;

    public float forwardAcceleration = 100f;
    public float backwardAcceleration = 100f;
    public float brakeAcceleration = 200f;
    public float currentSpeed = 0f;

    // public float currentForewardDirection = 1f;
    private void Awake()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;
        CalculateSpeed(movementVector);
    }

    private void CalculateSpeed(Vector2 movementVector)
    {
        if (IsBrakingIfInputOpositeDirection() == false)
        {
            if (movementVector.y > 0)
            {
                currentSpeed += forwardAcceleration * Time.deltaTime;
            }
            else if (movementVector.y < 0)
            {
                currentSpeed -= backwardAcceleration * Time.deltaTime;
            }

            currentSpeed = Math.Clamp(currentSpeed, minSpeed, maxSpeed);
        }
    }

    private bool IsBrakingIfInputOpositeDirection()
    {
        if (movementVector.y > 0 && currentSpeed < 0)
        {
            currentSpeed += brakeAcceleration * Time.deltaTime;
            return true;
        }
        if (movementVector.y < 0 && currentSpeed > 0)
        {
            currentSpeed -= brakeAcceleration * Time.deltaTime;
            return true;
        }

        if (movementVector.y == 0)
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= brakeAcceleration * Time.deltaTime;
                currentSpeed = Math.Clamp(currentSpeed, 0, maxSpeed);

            }
            else
            {
                currentSpeed += brakeAcceleration * Time.deltaTime;
                currentSpeed = Math.Clamp(currentSpeed, minSpeed, 0);
            }
            return true;
        }
        return false;
    }

    public void FixedUpdate()
    {
        rb2d.velocity = transform.up * currentSpeed * Time.fixedDeltaTime;
        rb2d.MoveRotation(transform.rotation *
                          Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.fixedDeltaTime));
    }
}