using System;
using System.Collections.Generic;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours;
using EnhancedDIAttempt.Health;
using Telov.Utils;
using UnityEngine;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public class DependantOnAnimationContinuousDamagerDecorator : IContinuousDamager
    {
        public DependantOnAnimationContinuousDamagerDecorator
        (
            IContinuousDamager continuousDamager,
            AnimatorBoolSetter boolSetter,
            ICommonCharacterAnimationsEventsNotifier animEvents,
            IMecanimStateExitedNotifier animStateInfoProvider,
            float allowedTimeBeforeAttackPrep,
            IStopper originalsStopper
        )
        {
            _continuousDamager = continuousDamager;
            _boolSetter = boolSetter;
            _animEvents = animEvents;
            _animStateInfoProvider = animStateInfoProvider;
            _allowedTimeBeforeAttackPrep = allowedTimeBeforeAttackPrep;
            _originalsStopper = originalsStopper;
        }

        private readonly IContinuousDamager _continuousDamager;
        private readonly AnimatorBoolSetter _boolSetter;
        private readonly ICommonCharacterAnimationsEventsNotifier _animEvents;
        private readonly IMecanimStateExitedNotifier _animStateInfoProvider;
        private readonly float _allowedTimeBeforeAttackPrep;
        private readonly IStopper _originalsStopper;

        public event Action OnDamagingStarted;
        public event Action OnDamagingEnded;

        private (Context context, IEnumerable<IDamageable> damageGetters, float amount) _parameters;
        private float _boolSetTime;
        
        public void DealDamage(Context context, IEnumerable<IDamageable> damageGetters, float amount)
        {
            _parameters = (context, damageGetters, amount);

            StartThis();
        }

        private void StartThis()
        {
            _continuousDamager.OnDamagingStarted += OnDamagingStarted;
            _continuousDamager.OnDamagingEnded += OnDamagingEnded;
            
            _continuousDamager.OnDamagingEnded += StopThis;

            _animEvents.OnPrepToAttack += CancelAttackIfPrepTooLate;

            _parameters.context.OnDeactivated += StopThis;
            
            _animEvents.OnAttackStart += OnAttackStartedAnimEvent;
            _boolSetTime = Time.time;
            _boolSetter.SetBoolToTrue();
        }

        private void CancelAttackIfPrepTooLate()
        {
            if (Time.time - _boolSetTime > _allowedTimeBeforeAttackPrep)
            {
                StopThis();
            }
        }

        private void OnAttackStartedAnimEvent()
        {
            _animEvents.OnAttackStart -= OnAttackStartedAnimEvent;

            _animStateInfoProvider.OnStateExited += OnAttackStoppedAnimEvent;
            
            _continuousDamager.DealDamage(_parameters.context, _parameters.damageGetters, _parameters.amount);
        }

        private void OnAttackStoppedAnimEvent()
        {
            OnStopFromThis();
        }

        private void OnStopFromThis()
        {
            StopThis();
            _originalsStopper.Stop();
        }

        private void StopThis()
        {
            _continuousDamager.OnDamagingStarted -= OnDamagingStarted;
            _continuousDamager.OnDamagingEnded -= OnDamagingEnded;
            _continuousDamager.OnDamagingEnded -= StopThis;
            _animStateInfoProvider.OnStateExited -= OnAttackStoppedAnimEvent;
            _parameters.context.OnDeactivated -= StopThis;
            _animEvents.OnPrepToAttack -= CancelAttackIfPrepTooLate;

            _boolSetter.SetBoolToFalse();
        }
    }
}