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
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float maxFov = 110f;
    [SerializeField] private float minFov = 60f;

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

        Vector3 moveVec = new Vector3(moveDir.x,0f, moveDir.y).normalized;

        if (moveVec.magnitude>0)
        {
            // transform.forward=moveVec;
            transform.RotateAround(transform.position, Vector3.up, moveVec.x * rotateSpeed);
        }

        moveVec.x=0f;
        moveVec.Normalize();
        

        controller.SimpleMove( transform.forward * moveVec.z *speed);

        UnityEngine.InputSystem.Controls.AxisControl camDelta = Mouse.current.scroll.y;
    
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView-camDelta.ReadValue()* 0.01f, minFov, maxFov);
    }
}
