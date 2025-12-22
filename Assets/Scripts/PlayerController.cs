using System;
using Assets.Scripts.StateMovement;
using PurrNet;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : NetworkIdentity
{
    [Header("Movement Settings")]
    [SerializeField] private StateMovement stateMovement;

    [Header("Look Settings")]
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private float maxLookAngle = 80f;

    [Header("References")]
    [SerializeField] private CinemachineCamera playerCamera;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Transform cameraPivot;

    private float verticalRotation = 0f;

    protected override void OnSpawned()
    {
        base.OnSpawned();

        enabled = isOwner;
        if (!isOwner)
        {
            Destroy(playerCamera.gameObject);
        } else
        {
            playerCamera.enabled = true;
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        stateMovement = new StateMovementGrounded(transform);

        if (playerCamera == null)
        {
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        stateMovement.HandleMovement();
    }



}