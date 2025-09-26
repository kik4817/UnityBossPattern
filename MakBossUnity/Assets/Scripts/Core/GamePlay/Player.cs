using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Controls controls;
    Rigidbody2D rd;

    [Header("Jump")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float groundCheckDistance = 1f;

    private bool IsJump;

    public Action<bool> OnFire;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls = new Controls();
        controls.Player.Enable();

        controls.Player.Jump.performed += HandleJump;
        controls.Player.Jump.canceled += HandleJumpCancled;
        controls.Player.Fire.performed += OnFirePerformed;
        controls.Player.Fire.canceled += OnFireCancele;
    }

    private void OnDisable()
    {
        controls.Player.Jump.performed -= HandleJump;
        controls.Player.Jump.canceled -= HandleJumpCancled;
        controls.Player.Fire.performed -= OnFirePerformed;
        controls.Player.Fire.canceled -= OnFireCancele;
        controls.Player.Disable();
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        OnFire?.Invoke(true);
    }

    private void OnFireCancele(InputAction.CallbackContext context)
    {
        OnFire?.Invoke(false);
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
            rd.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); //transform.up 회전 방향으로, Vector2.up 항상위
        }
    }

    private void HandleJumpCancled(InputAction.CallbackContext context)
    {
        IsJump = false;
    }

    private bool IsGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundMask); // 3을 GroundCheckDistance
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position,
            transform.position + (Vector3)(Vector2.down * groundCheckDistance)); // new Vector3(0, -1*3, 0)
    }
}
