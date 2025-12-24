using PurrNet;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.StateMovement
{
    internal class StateMovementManager : NetworkIdentity
    {
        [SerializeField] public StateMovement currentState; 

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
        }

        public void SwitchState(StateMovement stateMovement)
        {
            currentState = stateMovement;
            currentState.EnterState();
        }

    }
}
