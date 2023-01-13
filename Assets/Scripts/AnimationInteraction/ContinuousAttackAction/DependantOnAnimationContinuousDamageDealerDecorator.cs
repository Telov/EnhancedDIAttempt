using System;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBased;
using EnhancedDIAttempt.Utils.MecanimStateMachine;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public class DependantOnAnimationContinuousDamageDealerDecorator :  IContinuousDamageDealer
    {
        public DependantOnAnimationContinuousDamageDealerDecorator
        (
            IContinuousDamageDealer continuousDamageDealer,
            IToggleableAttackAllower toggleableAttackAllower,
            IMecanimStateExitedNotifier stateExitedNotifier,
            IAnimatorBoolSetter animatorBoolSetter
        )
        {
            _continuousDamageDealer = continuousDamageDealer;
            _toggleableAttackAllower = toggleableAttackAllower;
            _stateExitedNotifier = stateExitedNotifier;
            _animatorBoolSetter = animatorBoolSetter;
        }
        
        private readonly IContinuousDamageDealer _continuousDamageDealer;
        private readonly IToggleableAttackAllower _toggleableAttackAllower;
        private readonly IMecanimStateExitedNotifier _stateExitedNotifier;
        private readonly IAnimatorBoolSetter _animatorBoolSetter;

        public event Action OnAttackStarted;
        public event Action OnAttackEnded;
        
        public void DealDamage(IAttackTargetsProvider targetsProvider, float amount)
        {
            _continuousDamageDealer.OnAttackStarted += StartWorking;
            _continuousDamageDealer.OnAttackStarted += OnAttackStarted;
            _continuousDamageDealer.OnAttackEnded += StopWorking;
            _continuousDamageDealer.OnAttackEnded += OnAttackEnded;
            
            _continuousDamageDealer.DealDamage(targetsProvider, amount);
        }

        private void StartWorking()
        {
            _toggleableAttackAllower.ToggleOn();
            _stateExitedNotifier.OnStateExited += StopWorking;
            _animatorBoolSetter.SetBoolToTrue();
        }
        
        private void StopWorking()
        {
            _toggleableAttackAllower.ToggleOff();
            _stateExitedNotifier.OnStateExited -= StopWorking;
            
            _animatorBoolSetter.SetBoolToFalse();
            
            _continuousDamageDealer.OnAttackStarted -= StartWorking;
            _continuousDamageDealer.OnAttackStarted -= OnAttackStarted;
            _continuousDamageDealer.OnAttackEnded -= StopWorking;
            _continuousDamageDealer.OnAttackEnded -= OnAttackEnded;
        }
    }
}