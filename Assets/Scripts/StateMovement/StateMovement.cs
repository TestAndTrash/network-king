using System;
using PurrNet;
using UnityEngine;

namespace Assets.Scripts.StateMovement
{
    internal abstract class StateMovement
    {
        protected float gravity = Physics.gravity.y;
        protected float groundCheckDistance = 0.2f;
        public Vector3 velocity;

        protected CharacterController characterController;
        protected StateMovementManager stateManager;
        protected Transform transform;

        public StateMovement(StateMovementManager stateMovementManager)
        {
            stateManager = stateMovementManager;
            characterController = stateManager.GetComponent<CharacterController>();
            transform = characterController.transform;
        }

        public abstract void EnterState(StateMovement previousState);

        public abstract void UpdateState();

        protected bool IsGrounded()
        {
            return Physics.Raycast(transform.position + Vector3.up * 0.03f, Vector3.down, groundCheckDistance);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position + Vector3.up * 0.03f, Vector3.down * groundCheckDistance);
        }
#endif

    }
}
