using System.Collections.Generic;
using EnhancedDIAttempt.Damage;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public interface IDamageDealer
    {
        public void DealDamage(Context context, IEnumerable<IDamageGetter> damageGetters, float amount);
    }
}