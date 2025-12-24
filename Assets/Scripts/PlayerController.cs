using System;
using PurrNet;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : NetworkIdentity
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundCheckDistance = 0.2f;

    [Header("Look Settings")]
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private float maxLookAngle = 80f;

    [Header("References")]
    [SerializeField] private CinemachineCamera playerCamera;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Transform cameraPivot;

    
    public float sensitivity = 3f;
    public float minPitch = -40f;
    public float maxPitch = 70f;

    public CharacterController characterController;
    private Vector3 velocity;

    public float currentGravity;

    protected override void OnSpawned()
    {
        base.OnSpawned();

        enabled = isOwner;
        if (!isOwner)
        {
            Destroy(playerCamera.gameObject);
        }
        else
        {
            playerCamera.enabled = true;
            currentGravity = gravity;
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();

        if (playerCamera == null)
        {
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        bool isGrounded = IsGrounded();
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            velocity.x = 0f;
            velocity.z = 0f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * currentGravity);
        }

        velocity.y += currentGravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.03f, Vector3.down, groundCheckDistance);
    }

    public void Rotate(float yaw)
    {
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }

    public void SetGravity(float newGravity = 0f)
    {
        if (newGravity == 0f)
        {
            newGravity = gravity;
        }
        currentGravity = newGravity;
    }

    public void SetVelocity(Vector3 newVelocity = default)
    {
        if (newVelocity != default)
        {
            Debug.Log("Setting velocity to: " + newVelocity);
            velocity = newVelocity;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * 0.03f, Vector3.down * groundCheckDistance);
    }
#endif
}