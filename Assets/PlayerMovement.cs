using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;

    public float normalSpeed = 6f;
    public float runningSpeed = 12f;
    private float _speed;
    private bool _isRunning;

    public float gravity = -12f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 _velocity;
    private bool _isGrounded;

    void Update()
    {
        GroundCheck();
        HandleMovement();
        HandleJumping();
        ApplyGravity();
        HandleRunning();
        HandleCrouchSlide();
    }

    private void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        Controller.Move(move * _speed * Time.deltaTime);
    }

    private void HandleJumping()
    {
        if (Input.GetButton("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        
    }

    private void ApplyGravity()
    {
        _velocity.y += gravity * Time.deltaTime;
        Controller.Move(_velocity * Time.deltaTime);
    }

    private void HandleRunning()
    {
        // If grounded and LeftShift is pressed, start running
        if (_isGrounded && Input.GetKey(KeyCode.LeftShift))
        {
            _isRunning = true;
            _speed = runningSpeed;
        }
        else if (!_isGrounded && _isRunning)
        {
            
        }
        else if (_isGrounded)
        {
            _isRunning = false;
            _speed = normalSpeed;
        }
    }

    private void HandleCrouchSlide()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (_isRunning)
            {
                Debug.Log("Slide!");
                return;
            }

            Debug.Log("Crouch!"); //default behaviour
        }
    }



}
