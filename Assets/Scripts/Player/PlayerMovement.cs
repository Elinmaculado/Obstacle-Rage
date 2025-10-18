using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions _actions;
    private CharacterController _controller;
    private Vector2 _movementInput;

    public float speed = 5;

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
    }

    void MovePlayer()
    {
        
        _movementInput = _actions.Player.Move.ReadValue<Vector2>();
        Vector3 movement = new Vector3(_movementInput.x, 0, _movementInput.y);
        _controller.Move(movement * Time.deltaTime * speed);
    }
    
}
