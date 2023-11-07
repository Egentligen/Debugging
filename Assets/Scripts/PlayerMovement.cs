using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 10f;
    [SerializeField] float rotaitonSpeed = 180f;

    LevelTime levelTime;

    private void Awake()
    {
        levelTime = FindObjectOfType<LevelTime>();
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        Movement();
    }

    void Movement() 
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);

        movementDirection.Normalize();

        transform.Translate(inputMagnitude * speed * Time.deltaTime * movementDirection, Space.World);

        if (movementDirection != Vector2.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotaitonSpeed * Time.deltaTime);
        }
    }
    public void Death() 
    {
        levelTime.DisplayTimeSurvived();
        Time.timeScale = 0f;
    }
}

