using System;
using System.Collections.Generic;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours;
using EnhancedDIAttempt.Damage;
using Telov.Utils;
using UnityEngine;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public class DependantOnAnimationContinuousDamageDealerDecorator : IContinuousDamageDealer
    {
        public DependantOnAnimationContinuousDamageDealerDecorator
        (
            IContinuousDamageDealer continuousDamageDealer,
            IUsableInterrupter interrupter,
            ICommonCharacterAnimationsEventsNotifier animEventsNotifier,
            IAnimatorBoolSetter animatorBoolSetter,
            float allowedTimeBeforeAttackPreparation
        )
        {
            _continuousDamageDealer = continuousDamageDealer;
            _interrupter = interrupter;
            _animEventsNotifier = animEventsNotifier;
            _animatorBoolSetter = animatorBoolSetter;
            _allowedTimeBeforeAttackPreparation = allowedTimeBeforeAttackPreparation;
        }

        private readonly IContinuousDamageDealer _continuousDamageDealer;
        private readonly IUsableInterrupter _interrupter;
        private readonly ICommonCharacterAnimationsEventsNotifier _animEventsNotifier;
        private readonly IAnimatorBoolSetter _animatorBoolSetter;
        private readonly float _allowedTimeBeforeAttackPreparation;

        public event Action OnAttackStarted;
        public event Action OnAttackEnded;

        private Context _context;
        private IEnumerable<IDamageGetter> _damageGetters;
        private float _amount;

        private float _requestTime;

        public void DealDamage(Context context, IEnumerable<IDamageGetter> damageGetters, float amount)
        {
            _damageGetters = damageGetters;
            _amount = amount;
            _context = context;

            _requestTime = Time.time;
            
            _animEventsNotifier.OnPrepToAttack += CancelAttackIfTooLate;
            
            _context.OnDeactivated += _animatorBoolSetter.SetBoolToFalse;

            _animEventsNotifier.OnAttackStart += StartDealingDamage;
            _animEventsNotifier.OnAttackEnd += StopFromAnimEvent;

            _continuousDamageDealer.OnAttackEnded += StopFromOriginal;

            _animatorBoolSetter.SetBoolToTrue();
        }

        private void CancelAttackIfTooLate()
        {
            if (Time.time - _requestTime >= _allowedTimeBeforeAttackPreparation)
            {
                _context.OnDeactivated -= _animatorBoolSetter.SetBoolToFalse;

                _animEventsNotifier.OnAttackStart -= StartDealingDamage;
                _animEventsNotifier.OnAttackEnd -= StopFromAnimEvent;

                _continuousDamageDealer.OnAttackEnded -= StopFromOriginal;
            
                _animatorBoolSetter.SetBoolToFalse();
                
                _animEventsNotifier.OnPrepToAttack -= CancelAttackIfTooLate;
            }
        }

        private void StartDealingDamage()
        {
            Debug.Log("Starting");
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