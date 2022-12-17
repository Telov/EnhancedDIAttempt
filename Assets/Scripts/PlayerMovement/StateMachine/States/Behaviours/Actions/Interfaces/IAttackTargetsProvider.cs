using System.Collections.Generic;
using EnhancedDIAttempt.Damage;

namespace EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions
{
    public interface IAttackTargetsProvider
    {
        public IEnumerable<IDamageGetter> GetAttackTargets();
    }
}