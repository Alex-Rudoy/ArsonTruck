using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public static event EventHandler<OnMilestoneReachedEventArgs> OnMilestoneReached;

    public class OnMilestoneReachedEventArgs : EventArgs
    {
        public int milestone;
    }

    private float rotation = 0;
    private const float rotationSpeed = 40;
    private const float maxRotation = 30;

    private float speed = 20;
    private const float accelerationSpeed = 10;
    private const float minSpeed = 10;
    private const float defaultSpeed = 20;
    private const float maxSpeed = 30;

    private int milestone = 0;

    private void Start() { }

    private void Update()
    {
        HandleRotation();
        HandleAcceleration();
        Move();
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rotation -= 2 * Time.deltaTime * rotationSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rotation += 2 * Time.deltaTime * rotationSpeed;
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            if (rotation > 0)
            {
                rotation -= 1 * Time.deltaTime * rotationSpeed;
            }

            if (rotation < 0)
            {
                rotation += 1 * Time.deltaTime * rotationSpeed;
            }
        }

        if (transform.position.x > 13 && rotation > 0)
        {
            rotation -= 5 * Time.deltaTime * rotationSpeed;
        }

        if (transform.position.x < -13 && rotation < 0)
        {
            rotation += 5 * Time.deltaTime * rotationSpeed;
        }

        rotation = Mathf.Clamp(rotation, -maxRotation, maxRotation);
    }

    private void HandleAcceleration()
    {
        if (Input.GetKey(KeyCode.W))
        {
            speed += 2 * Time.deltaTime * accelerationSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            speed -= 2 * Time.deltaTime * accelerationSpeed;
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            if (speed > defaultSpeed)
            {
                speed -= 1 * Time.deltaTime * accelerationSpeed;
            }

            if (speed < defaultSpeed)
            {
                speed += 1 * Time.deltaTime * accelerationSpeed;
            }
        }

        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
    }

    private void Move()
    {
        transform.rotation = Quaternion.Euler(0, rotation, 0);
        transform.position += transform.forward * Time.deltaTime * speed;
        if (transform.position.z > milestone * 30)
        {
            milestone++;
            OnMilestoneReached(
                this,
                new OnMilestoneReachedEventArgs { milestone = (int)milestone }
            );
        }
    }
}
