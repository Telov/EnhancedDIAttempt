using System;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.Interfaces;
using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States
{
    public class EmptyMoveRuler : IMoveRuler
    {
        public event Action<float, Vector3> OnWantMove;
    }
}