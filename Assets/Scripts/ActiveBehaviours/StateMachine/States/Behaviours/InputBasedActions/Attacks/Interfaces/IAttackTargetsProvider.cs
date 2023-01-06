using System.Collections.Generic;
using EnhancedDIAttempt.Damage;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBasedActions
{
    public interface IAttackTargetsProvider
    {
        public IEnumerable<IDamageGetter> GetAttackTargets();
    }
}