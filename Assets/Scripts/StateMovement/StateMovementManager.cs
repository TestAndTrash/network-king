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
            StateMovement previousState = currentState;
            SwitchState(stateFreeze);
            currentState.EnterState(previousState);
        }

        public void SetStateGrappling(Vector3 endpoint)
        {
            StateMovement previousState = currentState;
            SwitchState(stateGrappling);
            currentState.EnterState(previousState);
            ((StateMovementGrappling) currentState).target = endpoint;
        }

        public void SetStateGrounded()
        {
            StateMovement previousState = currentState;
            SwitchState(stateGrounded);
            currentState.EnterState(previousState);
        }

    }
}
