using System;
using System.Collections.Generic;
using EnhancedDIAttempt.Damage;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class BlockingContinuousDamageDealerDecorator : IContinuousDamageDealer
    {
        public BlockingContinuousDamageDealerDecorator(IContinuousDamageDealer damageDealer, IBlocker blocker)
        {
            _damageDealer = damageDealer;
            _blocker = blocker;
        }

        private readonly IContinuousDamageDealer _damageDealer;
        private readonly IBlocker _blocker;

        public void DealDamage(Context context, IEnumerable<IDamageGetter> damageGetters, float amount)
        {
            _damageDealer.OnAttackStarted += OnAttackStarted;
            _damageDealer.OnAttackEnded += OnAttackEnded;
            
            _damageDealer.OnAttackStarted += StartFunction;
            _damageDealer.OnAttackEnded += EndFunction;
            
            _damageDealer.DealDamage(context, damageGetters, amount);
        }

        public event Action OnAttackStarted;
        public event Action OnAttackEnded;

        private void StartFunction()
        {
            _blocker.Block();
        }

        private void EndFunction()
        {
            _damageDealer.OnAttackStarted -= OnAttackStarted;
            _damageDealer.OnAttackEnded -= OnAttackEnded;
            
            _damageDealer.OnAttackStarted -= StartFunction;
            _damageDealer.OnAttackEnded -= EndFunction;
            
            _blocker.Unblock();
        }
    }
}