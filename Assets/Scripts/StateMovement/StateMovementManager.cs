using PurrNet;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.StateMovement
{
    internal class StateMovementManager : NetworkIdentity
    {
        [SerializeField] private StateMovement currentState;

        private StateMovement stateFreeze;
        private StateMovement stateGrappling;
        private StateMovement stateGrounded;

        public void Start()
        {
            stateFreeze = new StateMovementFreeze(this);
            stateGrappling = new StateMovementGrappling(this);
            stateGrounded = new StateMovementGrounded(this);

            currentState = stateGrounded;
        }

        public void Update()
        {
            currentState.UpdateState();
        }

        private void SwitchState(StateMovement state)
        {
            StateMovement previousState = currentState;
            currentState = state;
            currentState.EnterState(previousState);
        }

        public void SetStateFreeze()
        {
            SwitchState(stateFreeze);
        }

        public void SetStateGrappling(Vector3 endpoint)
        {
            SwitchState(stateGrappling);
            ((StateMovementGrappling) currentState).target = endpoint;
        }

        public void SetStateGrounded()
        {
            SwitchState(stateGrounded);
        }

    }
}
