using System;
using UnityEngine;

namespace Assets.Scripts.StateMovement
{
    internal class StateMovementGrounded : StateMovement
    {
        protected float moveSpeed = 5f;
        protected float sprintSpeed = 8f;
        protected float jumpForce = 1f;

        public StateMovementGrounded(StateMovementManager stateMovementManager) : base(stateMovementManager)
        {

        }

        public override void EnterState(StateMovement previousState)
        {
            velocity = previousState.velocity;
        }

        public override void UpdateState()
        {
            bool isGrounded = IsGrounded();
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
            moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

            velocity.x = moveDirection.x * currentSpeed;
            velocity.z = moveDirection.z * currentSpeed;

            characterController.Move(moveDirection * currentSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);

        }


        
    }
}
