using PurrNet;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.StateMovement
{
    internal class StateMovementManager : NetworkIdentity
    {
        [SerializeField] private StateMovement currentState;

        [SerializeField] public Vector3 veloc;

        public StateMovement stateFreeze;
        public StateMovement stateGrappling;
        public StateMovement stateGrounded;

        public void Start()
        {
            stateFreeze = new StateMovementFreeze(this);
            stateGrappling = new StateMovementGrappling(this);
            stateGrounded = new StateMovementGrounded(this);

            currentState = stateGrounded;
            currentState.EnterState();
        }

        public void Update()
        {
            currentState.UpdateState();
            veloc = currentState.velocity;
        }

        private void SwitchState(StateMovement state)
        {
            currentState = state;

        }

        public void SetStateFreeze()
        {
            SwitchState(stateFreeze);
            currentState.EnterState();
        }

        public void SetStateGrappling(Vector3 endpoint)
        {
            SwitchState(stateGrappling);
            currentState.EnterState();
            ((StateMovementGrappling) currentState).target = endpoint;
        }

        public void SetStateGrounded()
        {
            SwitchState(stateGrounded);
            currentState.EnterState();
        }

    }
}
