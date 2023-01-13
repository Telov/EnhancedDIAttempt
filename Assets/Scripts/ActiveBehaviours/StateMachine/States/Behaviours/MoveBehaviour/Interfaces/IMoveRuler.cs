using System;
using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.Interfaces
{
    public interface IMoveRuler
    {
        public event Action<float, Vector3> OnWantMove;
    }
}