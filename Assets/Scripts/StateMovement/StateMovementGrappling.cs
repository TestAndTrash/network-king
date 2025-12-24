using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Assets.Scripts.StateMovement
{
    internal class StateMovementGrappling : StateMovement
    {
        public Vector3 target;

        protected float moveSpeed = 20f;




        public StateMovementGrappling(StateMovementManager stateMovementManager) : base(stateMovementManager)
        {

        }

        public override void EnterState(StateMovement previousState)
        {
            velocity = previousState.velocity;
        }

        public override void UpdateState()
        {
            velocity = CalculateJumpVelocity(transform.position, target);
            characterController.Move(velocity * Time.deltaTime);
        }

        private Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint)
        {

            Vector3 direction = (endPoint - startPoint).normalized * moveSpeed;

            return direction;
        }

        
    }
}
