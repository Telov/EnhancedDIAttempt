using System.Collections.Generic;
using EnhancedDIAttempt.Damage;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public interface IAttackTargetsProvider
    {
        public ICollection<IDamageGetter> GetAttackTargets();
    }
}