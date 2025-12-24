using UnityEngine;

namespace Assets.Scripts.StateMovement
{
    internal class StateMovementGrappling : StateMovement
    {
        private float grapplingDelay = 0.1f;
        private float overShoot = 2;

        public Vector3 target;
        private Vector3 velocityToSet;



        public StateMovementGrappling(StateMovementManager stateMovementManager) : base(stateMovementManager)
        {

        }

        public override void EnterState()
        {
            velocityToSet = CalculateJumpVelocity(transform.position, target);
            //Invoke(nameof(SetVelocity), grapplingDelay);
            SetVelocity();
        }

        public override void UpdateState()
        {

        }


        private void SetVelocity()
        {
            velocity = velocityToSet;
        }

        private Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint)
        {
            float trajectoryHeight = CalculateOverShoot();

            float displacementY = endPoint.y - startPoint.y;
            Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

            Vector3 velocityY = Vector3.up * Mathf.Sqrt(displacementY-2 * gravity * trajectoryHeight);
            Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
                + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

            return velocityXZ + velocityY;
        }

        private float CalculateOverShoot()
        {
            Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

            float grapplePointRelativeYPos = target.y - lowestPoint.y;
            float highestPointOnArc = grapplePointRelativeYPos + overShoot;

            if (grapplePointRelativeYPos < 0) highestPointOnArc = overShoot;

            return highestPointOnArc;
        }
    }
}
