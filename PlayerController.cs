using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, Interfaces.IAction
{
    // players speed
    [SerializeField] float speed = 14.0f;

    // max range for player movement on the X axis
    private float xRange = 24.0f;

    // max range for player movement on the Z axis
    private float zRange = 12f;

    private Vector2 touchStartPos;
    private bool isTouching = false;


    void Update()
    {
        ContainPlayer();

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == UnityEngine.TouchPhase.Began)
            {
                touchStartPos = touch.position;
                isTouching = true;
            }
            else if (touch.phase == UnityEngine.TouchPhase.Moved)
            {
                Vector2 touchDelta = touch.position - touchStartPos;
                float moveDirection = Mathf.Sign(touchDelta.x);

                if (moveDirection > 0)
                {
                    MoveRight();
                }
                else if (moveDirection < 0)
                {
                    MoveLeft();
                }
            }
            else if (touch.phase == UnityEngine.TouchPhase.Ended)
            {
                StopMoving();
                isTouching = false;
            }
        }
        else if (!isTouching)
        {
            StopMoving();
        }
    }
    public void MoveLeft()
    {
        Debug.Log("Move Left");
        Vector3 movement = Vector3.left * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    public void MoveRight()
    {
        Debug.Log("Move Right");
        Vector3 movement = Vector3.right * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    public void StopMoving()
    {

    }

    // Methods to handle button events
    public void OnLeftButtonPressed()
    {
        MoveLeft(); // Move the player left
    }

    public void OnRightButtonPressed()
    {
        MoveRight(); // Move the player right
    }


    // if statements to keep player in bounds
    void ContainPlayer()
    {
        // if statement to keep player in bounds
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
    }
}