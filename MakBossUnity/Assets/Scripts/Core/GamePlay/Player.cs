using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamagable
{
    public Controls controls;
    Rigidbody2D rd;

    [Header("Jump")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float groundCheckDistance = 1f;

    private bool IsJump;

    public Action<bool> OnFire;

    [SerializeField] public int CurrentHealth { get; set; }

    //[SerializeField] AudioClip AudioClip;
    AudioSource audioSource;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
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

        // audiosource �ҽ��� ������ �� �Ŀ� �����ϵ��� ����� �ڵ�
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if (IsGround() && !IsJump)
        {
            IsJump = true;
            audioSource.clip = Resources.Load<AudioClip>("Sound/Jump");
            audioSource.Play();
            rd.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); //transform.up ȸ�� ��������, Vector2.up �׻���
        }
    }

    private void HandleJumpCancled(InputAction.CallbackContext context)
    {
        IsJump = false;
    }

    private bool IsGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundMask); // 3�� GroundCheckDistance
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position,
            transform.position + (Vector3)(Vector2.down * groundCheckDistance)); // new Vector3(0, -1*3, 0)
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }
}
