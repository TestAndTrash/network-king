using System;
using UnityEngine;

namespace Assets.Scripts.StateMovement
{
    internal abstract class StateMovement
    {
        protected float moveSpeed = 5f;
        protected float sprintSpeed = 8f;
        protected float jumpForce = 1f;
        protected float gravity = -9.81f;
        protected float groundCheckDistance = 0.2f;
        protected Transform transform;
        protected CharacterController characterController;
        protected Vector3 velocity;


        public StateMovement(Transform transform)
        {
            this.transform = transform;
            characterController = this.transform.GetComponent<CharacterController>();
        }

        public virtual void HandleMovement()
        {
            throw new NotImplementedException();
        }

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
