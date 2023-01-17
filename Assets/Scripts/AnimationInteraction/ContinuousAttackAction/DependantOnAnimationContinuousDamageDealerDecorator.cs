using System;
using System.Collections.Generic;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours;
using EnhancedDIAttempt.Damage;
using Telov.Utils;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public class DependantOnAnimationContinuousDamageDealerDecorator : IContinuousDamageDealer
    {
        public DependantOnAnimationContinuousDamageDealerDecorator
        (
            IContinuousDamageDealer continuousDamageDealer,
            IUsableInterrupter interrupter,
            ICommonCharacterAnimationsEventsNotifier animEventsNotifier,
            IAnimatorBoolSetter animatorBoolSetter
        )
        {
            _continuousDamageDealer = continuousDamageDealer;
            _interrupter = interrupter;
            _animEventsNotifier = animEventsNotifier;
            _animatorBoolSetter = animatorBoolSetter;
        }

        private readonly IContinuousDamageDealer _continuousDamageDealer;
        private readonly IUsableInterrupter _interrupter;
        private readonly ICommonCharacterAnimationsEventsNotifier _animEventsNotifier;
        private readonly IAnimatorBoolSetter _animatorBoolSetter;

        public event Action OnAttackStarted;
        public event Action OnAttackEnded;

        private Context _context;
        private IEnumerable<IDamageGetter> _damageGetters;
        private float _amount;

        public void DealDamage(Context context, IEnumerable<IDamageGetter> damageGetters, float amount)
        {
            _damageGetters = damageGetters;
            _amount = amount;
            _context = context;

            _context.OnDeactivated += _animatorBoolSetter.SetBoolToFalse;

            _animEventsNotifier.OnAttackStart += StartDealingDamage;
            _animEventsNotifier.OnAttackEnd += StopFromAnimEvent;

            _continuousDamageDealer.OnAttackEnded += StopFromOriginal;

            _animatorBoolSetter.SetBoolToTrue();
        }

        private void StartDealingDamage()
        {
            _context.OnDeactivated -= _animatorBoolSetter.SetBoolToFalse;
            
            _continuousDamageDealer.OnAttackStarted += OnAttackStarted;
            _continuousDamageDealer.OnAttackEnded += OnAttackEnded;
            _continuousDamageDealer.DealDamage(_context, _damageGetters, _amount);
        }

        private void StopFromAnimEvent()
        {
            StopFromOriginal();
            _interrupter.Interrupt();
            OnAttackEnded?.Invoke();
        }

        private void StopFromOriginal()
        {
            _animEventsNotifier.OnAttackStart -= StartDealingDamage;
            _animEventsNotifier.OnAttackEnd -= StopFromAnimEvent;
            _continuousDamageDealer.OnAttackEnded -= StopFromOriginal;

            _animatorBoolSetter.SetBoolToFalse();

            _continuousDamageDealer.OnAttackStarted -= OnAttackStarted;
            _continuousDamageDealer.OnAttackEnded -= OnAttackEnded;
        }
    }
}