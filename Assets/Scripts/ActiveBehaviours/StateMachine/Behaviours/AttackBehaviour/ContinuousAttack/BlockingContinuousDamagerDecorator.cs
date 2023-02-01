using System;
using System.Collections.Generic;
using EnhancedDIAttempt.Health;
using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class BlockingContinuousDamagerDecorator : IContinuousDamager
    {
        public BlockingContinuousDamagerDecorator(IContinuousDamager damager, IBlocker blocker)
        {
            _damager = damager;
            _blocker = blocker;
        }

        private readonly IContinuousDamager _damager;
        private readonly IBlocker _blocker;
        public event Action OnDamagingStarted;
        public event Action OnDamagingEnded;

        public void DealDamage(Context context, IEnumerable<IDamageable> damageGetters, float amount)
        {
            StartThis();

            _damager.DealDamage(context, damageGetters, amount);
        }

        private void StartThis()
        {
            _damager.OnDamagingStarted += OnDamagingStarted;
            _damager.OnDamagingEnded += OnDamagingEnded;
            _damager.OnDamagingEnded += StopThis;

            _damager.OnDamagingStarted += _blocker.Block;
            _damager.OnDamagingEnded += _blocker.Unblock;
        }

        private void StopThis()
        {
            _damager.OnDamagingStarted -= OnDamagingStarted;
            _damager.OnDamagingEnded -= OnDamagingEnded;
            _damager.OnDamagingEnded -= StopThis;

            _damager.OnDamagingStarted -= _blocker.Block;
            _damager.OnDamagingEnded -= _blocker.Unblock;
        }
    }
}