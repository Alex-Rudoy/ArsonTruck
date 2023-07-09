using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private CarDestroyer carDestroyer;

    public static event EventHandler<OnMilestoneReachedEventArgs> OnMilestoneReached;

    public class OnMilestoneReachedEventArgs : EventArgs
    {
        public int milestone;

        public OnMilestoneReachedEventArgs(int milestone)
        {
            this.milestone = milestone;
        }
    }

    private float rotation = 0;
    private const float rotationSpeed = 40;
    private const float maxRotation = 30;

    private float speed = 20;
    public float Speed
    {
        get { return speed; }
    }
    private const float accelerationSpeed = 10;
    private const float minSpeed = 15;
    private const float defaultSpeed = 20;
    private const float maxSpeed = 30;

    private int milestone = 1;

    private void Start()
    {
        GameOverUI.onRestartClick += HandleRestartClick;
        ResetPlayer();
    }

    private void Update()
    {
        Vector2 inputVector = GameInputManager.Instance.GetNormalizedInputVector();
        HandleRotation(inputVector);
        HandleAcceleration(inputVector);
        Move();
    }

    private void HandleRotation(Vector2 inputVector)
    {
        if (inputVector.x < 0)
        {
            rotation -= 2 * Time.deltaTime * rotationSpeed;
        }

        if (inputVector.x > 0)
        {
            rotation += 2 * Time.deltaTime * rotationSpeed;
        }

        if (inputVector.x == 0)
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

    private void HandleAcceleration(Vector2 inputVector)
    {
        if (inputVector.y > 0)
        {
            speed += 2 * Time.deltaTime * accelerationSpeed;
        }

        if (inputVector.y < 0)
        {
            speed -= 2 * Time.deltaTime * accelerationSpeed;
        }

        if (inputVector.y == 0)
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
            OnMilestoneReached(this, new OnMilestoneReachedEventArgs(milestone));
        }

        carDestroyer.transform.position = new Vector3(0, 0, transform.position.z - 30);
    }

    private void HandleRestartClick(object sender, System.EventArgs e)
    {
        ResetPlayer();
    }

    private void ResetPlayer()
    {
        milestone = 1;
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        speed = defaultSpeed;
        rotation = 0;
        carDestroyer.transform.position = new Vector3(0, 0, transform.position.z - 30);
    }
}
