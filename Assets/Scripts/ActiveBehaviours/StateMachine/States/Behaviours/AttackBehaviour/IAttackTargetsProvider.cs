using System.Collections.Generic;
using EnhancedDIAttempt.Damage;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBased
{
    public interface IAttackTargetsProvider
    {
        public ICollection<IDamageGetter> GetAttackTargets();
    }
}