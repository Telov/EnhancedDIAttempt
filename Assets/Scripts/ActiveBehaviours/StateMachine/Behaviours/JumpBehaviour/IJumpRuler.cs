using System;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public interface IJumpRuler
    {
        public event Action OnWantToJump;
    }
}