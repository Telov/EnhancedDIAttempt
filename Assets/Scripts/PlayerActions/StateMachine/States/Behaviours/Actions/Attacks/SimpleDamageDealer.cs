using System.Collections.Generic;
using EnhancedDIAttempt.Damage;

namespace EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions
{
    public class SimpleDamageDealer : IDependantDamageDealer
    {
        public void DealDamage(IAttackTargetsProvider targetsProvider, float amount)
        {
            DealDamage(targetsProvider.GetAttackTargets(), amount);
        }

        public void DealDamage(IEnumerable<IDamageGetter> damageGetters, float amount)
        {
            foreach (var damageGetter in damageGetters)
            {
                damageGetter.GetDamage(amount);
            }
        }
    }
}