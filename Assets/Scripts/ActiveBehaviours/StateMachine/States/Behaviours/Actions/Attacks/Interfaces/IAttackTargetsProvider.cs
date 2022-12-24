using System.Collections.Generic;
using EnhancedDIAttempt.Damage;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.Actions
{
    public interface IAttackTargetsProvider
    {
        public IEnumerable<IDamageGetter> GetAttackTargets();
    }
}