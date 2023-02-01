using System.Collections.Generic;
using EnhancedDIAttempt.Health;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public interface IDamager
    {
        public void DealDamage(Context context, IEnumerable<IDamageable> damageGetters, float amount);
    }
}