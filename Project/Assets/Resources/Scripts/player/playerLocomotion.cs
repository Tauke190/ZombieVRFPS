using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLocomotion : MonoBehaviour
{
    public Transform playerCamera;
    [Space(10)]
    public float movementSpeed;
    public float rotationSpeed;
    [Space(10)]
    public float defaultHeight = 1.75f;


    public static bool enableMovement = true; //allow player to move and rotate
    public static bool enableRotation = true; //


    private Vector3 bodyAngle;
    private Vector3 headAngle;

    void Start()
    {
        if (inputManager.viewMode == 0)
        {
            float headsetPlacement = defaultHeight - 0.12f;
            playerCamera.Translate(Vector3.up * headsetPlacement);
        }
    }

    void Update()
    {
        if (enableMovement) //check if controls are enabled
        {
            ControlMovement(); //control players movement
        }

        if (enableRotation)
        {
            ControlRotation(); //control players rotation
        }
    }

    //Movement

    void ControlMovement()
    {
        float movementInputX = 0f; //clear movement inputs for new assignment
        float movementInputY = 0f; //

        switch (inputManager.controlMode)
        {
            case 0:
                if (Input.GetKey("d")) { movementInputX = 1; } else if (Input.GetKey("a")) { movementInputX = -1; } //set the movement inputs to the keyboard's WASD input values
                if (Input.GetKey("w")) { movementInputY = 1; } else if (Input.GetKey("s")) { movementInputY = -1; } //
                break;

            case 1:
                movementInputX = Input.GetAxis("Left Stick X Axis"); //set the movement inputs to the controller's left stick axis values
                movementInputY = Input.GetAxis("Left Stick Y Axis"); //
                break;

            case 2:
                //steamVR takes care of the rest
                break;
        }

        Vector2 movementInput = new Vector2(movementInputX, movementInputY); //store the movement inputs in a vector
        ApplyMovement(movementInput);
    }

    void ApplyMovement(Vector2 movementInput) //applies movement input
    {
        Quaternion playerDirection = Quaternion.Euler(0f, transform.eulerAngles.y, 0f); //find what direction the player is facing
        Vector3 movementVector = playerDirection * new Vector3(movementInput.x, 0f, movementInput.y); //set the vector the player will move in
        Vector3.ClampMagnitude(movementVector, 1f); //clamp vector magnitude so movement is never faster than 1

        transform.position += movementVector / 50f; //apply movement vector to the player's position
    }

    //Rotation

    void ControlRotation() //gets rotation input and applies it
    {
        float rotationInput_x = 0; //clear rotation inputs for new assignment
        float rotationInput_y = 0; //

        switch (inputManager.controlMode) //check if joypad is connected
        {
            case 0:
                rotationInput_x = Input.GetAxis("Mouse X"); //set the movement inputs to the mouse's axis values
                rotationInput_y = -Input.GetAxis("Mouse Y"); //
                break;

            case 1:
                rotationInput_x = Input.GetAxis("Right Stick X Axis"); //set the rotation inputs to the controller's right stick axis values
                rotationInput_y = Input.GetAxis("Right Stick Y Axis"); //
                break;

            case 2:

                break;
        }

        Vector2 rotationInput = new Vector2(rotationInput_x, rotationInput_y); //store the rotation inputs in a vector
        ApplyRotation(rotationInput); //apply rotation input
    }

    void ApplyRotation(Vector2 rotationInput) //applies movement input
    {
        bodyAngle += new Vector3(0f, rotationInput.x, 0f);  //add rotation input
        headAngle += new Vector3(rotationInput.y, 0f, 0f);  //add rotation input

        if (inputManager.viewMode == 1) //check if VR headset is connected
        {
            headAngle.x = Mathf.Clamp(headAngle.x, 0f, 0f); //clamp rotation axis x to 0 (locks up and down inputs to not effect rotation)
        }


        transform.localEulerAngles = bodyAngle;
        playerCamera.localEulerAngles = headAngle; //set player head's rotation to the new angle
    }
}