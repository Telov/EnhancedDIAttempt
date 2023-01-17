using System.Collections.Generic;
using EnhancedDIAttempt.Damage;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class SimpleDamageDealer : IDamageDealer
    {
        public void DealDamage(Context context, IEnumerable<IDamageGetter> damageGetters, float amount)
        {
            foreach (var damageGetter in damageGetters)
            {
                damageGetter.GetDamage(amount);
            }
        }
    }
}