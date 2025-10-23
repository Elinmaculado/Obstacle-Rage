using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions _actions;
    private CharacterController _controller;
    private Vector2 _movementInput;

    public float speed = 5;
    private Vector3 _playerVelocity;
    public float jumpHeight = 1.5f;
    public float gravity = -9.8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _actions = new InputSystem_Actions();
        _actions.Player.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        
        if(_controller.isGrounded)
            Debug.Log("Character is grounded");
        else
            Debug.Log("Character NOT grounded");
            
    }

    void MovePlayer()
    {

        if (_controller.isGrounded && _actions.Player.Jump.WasPerformedThisFrame())
        {
            Debug.Log("Jump");
            _playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
        
        _playerVelocity.y += gravity * Time.deltaTime;
        
        _movementInput = _actions.Player.Move.ReadValue<Vector2>();
        Vector3 movement = new Vector3(_movementInput.x, 0, _movementInput.y);
        Vector3 finalMove = (movement * speed) + (_playerVelocity.y * Vector3.up);
        _controller.Move(finalMove * Time.deltaTime * speed);
    }
    
}
