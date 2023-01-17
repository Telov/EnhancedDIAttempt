using System;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public interface IAttackRuler
    {
        public event Action OnWantAttack;
    }
}