using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float maxSpeed = 100f;
    public float rotationSpeed = 100f;
    private Vector2 movementVector;

    private void Awake()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;
    }
    public void FixedUpdate()
    {
        rb2d.velocity = transform.up * movementVector.y * maxSpeed * Time.fixedDeltaTime;
        rb2d.MoveRotation(transform.rotation *
                          Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.fixedDeltaTime));
    }
}
