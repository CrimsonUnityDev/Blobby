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
    [SerializeField] private Animator anim;

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
        if (Mathf.Approximately(moveDir.magnitude, 0f))
        {
            anim.Play("Stand");
        }
        else
        {
            anim.Play("Jump");
        }

        Vector3 moveVec = new Vector3(moveDir.x,0f, moveDir.y).normalized;

        Vector3 camForward = Camera.main.transform.forward;
        camForward.y=0f;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y=0f;
        camRight.Normalize();

        Vector3 prevPos = transform.position;
        Vector3 nextPos = transform.position + ((camForward) * moveVec.z + (camRight) * moveVec.x);
        Vector3 dir = (nextPos-prevPos).normalized;

        dir.y=0f;
        transform.forward = Vector3.Slerp(transform.forward, dir, 0.25f);

        controller.SimpleMove( ((camForward) * moveVec.z + (camRight) * moveVec.x) *speed);

        UnityEngine.InputSystem.Controls.AxisControl camDelta = Mouse.current.scroll.y;
    
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView-camDelta.ReadValue()* 0.01f, minFov, maxFov);
    }
}
