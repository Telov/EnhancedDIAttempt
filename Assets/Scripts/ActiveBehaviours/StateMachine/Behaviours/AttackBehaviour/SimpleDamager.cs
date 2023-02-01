using System.Collections.Generic;
using EnhancedDIAttempt.Health;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class SimpleDamager : IDamager
    {
        public void DealDamage(Context context, IEnumerable<IDamageable> damageGetters, float amount)
        {
            foreach (var damageGetter in damageGetters)
            {
                damageGetter.GetDamage(amount);
            }
        }
    }
}