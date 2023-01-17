using System;
using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public interface IMoveRuler
    {
        public event Action<Vector3> OnWantMove;
    }
}