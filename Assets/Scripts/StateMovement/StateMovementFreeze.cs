using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StateMovement
{
    internal class StateMovementFreeze : StateMovement
    {

        public StateMovementFreeze(StateMovementManager stateMovementManager) : base(stateMovementManager)
        {

        }

        public override void EnterState(StateMovement previousState)
        {
            velocity = previousState.velocity;

        }

        public override void UpdateState()
        {
            velocity = Vector3.zero;

        }

    }
}
