﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;
    private bool isSprinting => canSprint && Input.GetKey(sprintKey);
    private bool shouldJump => characterController.isGrounded && Input.GetKeyDown(jumpKey);

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
   

    [Header("Control")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("Movement Parameters")]
    [SerializeField] private float walkspeed = 3.0f;
    [SerializeField] private float sprintspeed = 6.0f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float LookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float LookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float UpperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float LowerLookLimit = 80.0f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    private Camera playerCamera;
    private CharacterController characterController;
    private Animator anim;

    private Vector3 MoveDirection;
    private Vector2 CurrentInput;

    private float rotationX;
    private float finalSpeed;
    public static float weaponAnimationSpeed;
   
    void Start()
    {
        anim = GetComponent<Animator>();
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if(CanMove)
        {
            HandleMovement();
            HandleMouseLook();
            if (canJump)
                HandleJump();
            ApplyFinalMovement();
        }
        finalSpeed = walkspeed + Modifiers.instance.Speed;
        weaponAnimationSpeed = characterController.velocity.magnitude / finalSpeed;
        if(weaponAnimationSpeed>1)
        {
            weaponAnimationSpeed = 1;
        }
    }

    private void HandleMovement() //Handle Player Movement
    {
        CurrentInput = new Vector2((isSprinting ? sprintspeed : finalSpeed) * Input.GetAxis("Vertical"), (isSprinting ? sprintspeed : finalSpeed) * Input.GetAxis("Horizontal"));
        float moveDirectionY = MoveDirection.y;
        MoveDirection = (transform.TransformDirection(Vector3.forward) * CurrentInput.x) + (transform.TransformDirection(Vector3.right) * CurrentInput.y);
        MoveDirection.y = moveDirectionY;
    
    }

    private void HandleMouseLook() // Handle Mouse movement
    {
        rotationX -= Input.GetAxis("Mouse Y") * LookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -UpperLookLimit, LowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * LookSpeedX, 0); 
    }

    private void HandleJump() // Handles Player jump
    {
        if (shouldJump)
            MoveDirection.y = jumpForce;
    }

    private void ApplyFinalMovement() // Applies all the movement  values calculated above
    {
        if(!characterController.isGrounded)
        {
            MoveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(MoveDirection * Time.deltaTime);
    }
}
