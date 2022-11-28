using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class MoveController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerInputActions playerControls;
    [SerializeField] private float speed = 5f;

    private InputAction move;


    private void OnEnable()
    {
        playerControls= new PlayerInputActions();
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    private void Update()
    {
        Vector2 moveDir = move.ReadValue<Vector2>();

        controller.SimpleMove(new Vector3(moveDir.x,0f, moveDir.y) * speed);            
    }
}
