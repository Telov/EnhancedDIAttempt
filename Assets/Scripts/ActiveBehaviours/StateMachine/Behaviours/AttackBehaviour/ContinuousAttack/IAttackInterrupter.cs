using System;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public interface IAttackInterrupter
    {
        public event Action OnWantToStopAttack;
    }
}