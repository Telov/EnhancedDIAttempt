using System.Collections.Generic;
using EnhancedDIAttempt.Health;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public interface IDamageablesProvider
    {
        public ICollection<IDamageable> GetAttackTargets();
    }
}