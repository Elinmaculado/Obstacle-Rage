using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions _actions;
    private CharacterController _controller;
    private Vector2 _movementInput;

    public float speed = 10;
    private Vector3 _playerVelocity;
    public float jumpHeight = 5;
    public float gravity = -40f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _actions = new InputSystem_Actions();
        _actions.Player.Enable();
    }


    private void Update()
    {
        _movementInput = _actions.Player.Move.ReadValue<Vector2>();
        if (_controller.isGrounded && _actions.Player.Jump.WasPressedThisFrame())
        {
            Debug.Log("Jump");
            _playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
        MovePlayer();
        
    }

    void MovePlayer()
    {
        _playerVelocity.y += gravity * Time.deltaTime;
        Vector3 movement = new Vector3(_movementInput.x, 0, _movementInput.y);
        Vector3 finalMove = (movement * speed) + (_playerVelocity.y * Vector3.up);
        _controller.Move(finalMove * Time.deltaTime);
    }
    
}
