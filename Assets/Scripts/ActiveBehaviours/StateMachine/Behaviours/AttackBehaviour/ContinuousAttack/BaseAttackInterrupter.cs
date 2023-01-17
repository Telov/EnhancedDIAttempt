using System;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class BaseAttackInterrupter : IUsableInterrupter
    {
        public event Action OnWantToStopAttack = () => { };
        public void Interrupt()
        {
            OnWantToStopAttack();
        }
    }
}