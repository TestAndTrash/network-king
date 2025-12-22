using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StateMovement
{
    internal class StateMovementGrounded : StateMovement
    {

        public StateMovementGrounded(Transform transform) : base(transform) { }

        public override void HandleMovement()
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
