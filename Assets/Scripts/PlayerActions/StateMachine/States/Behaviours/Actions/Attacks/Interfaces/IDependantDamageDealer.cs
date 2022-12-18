using System.Collections.Generic;
using EnhancedDIAttempt.Damage;

namespace EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions
{
    public interface IDependantDamageDealer : IDamageDealer
    {
        public void DealDamage(IEnumerable<IDamageGetter> damageGetters, float amount);
    }
}