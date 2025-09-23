using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Controls controls;
    Rigidbody2D rd;

    [Header("Jump")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float JumpPower;
    [SerializeField] float GroundCheckDistance;

    private bool IsJump;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls = new Controls();
        controls.Player.Enable();

        controls.Player.Jump.performed += HandleJump;
        controls.Player.Jump.performed += HandleJumpCancled;
    }

    private void OnDisable()
    {
        controls.Player.Jump.performed -= HandleJump;
        controls.Player.Jump.performed -= HandleJumpCancled;
        controls.Player.Disable();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float dir = controls.Player.Move.ReadValue<float>();

        rd.linearVelocity = new Vector2(dir * 5, rd.linearVelocityY);
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if (IsGround() && !IsJump)
        {
            IsJump = true;
            rd.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse); //transform.up 회전 방향으로, Vector2.up 항상위
        }
    }

    private void HandleJumpCancled(InputAction.CallbackContext context)
    {
        IsJump = false;
    }

    private bool IsGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, GroundCheckDistance, groundMask); // 3을 GroundCheckDistance
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position,
            transform.position + (Vector3)(Vector2.down * GroundCheckDistance)); // new Vector3(0, -1*3, 0)
    }
}
