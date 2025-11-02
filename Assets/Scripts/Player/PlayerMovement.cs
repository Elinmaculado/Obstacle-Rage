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
        Transform camTransform = Camera.main.transform;
        
        Vector3 camForward = camTransform.forward;
        Vector3 camRight = camTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();
        
        Vector3 movement = camForward * _movementInput.y + camRight * _movementInput.x;
        
        if (movement.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        
        Vector3 finalMove = (movement * speed) + (_playerVelocity.y * Vector3.up);
        _controller.Move(finalMove * Time.deltaTime);
    }
    
}
