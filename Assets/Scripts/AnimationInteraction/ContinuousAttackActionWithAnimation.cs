using EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public class ContinuousAttackActionWithAnimation : ContinuousAttackAction
    {
        public ContinuousAttackActionWithAnimation
        (
            InputAction attackAction,
            IAttackTargetsProvider targetsProvider,
            ContinuousAttackInfo attackInfo,
            IAttackStateExitedNotifier attackStateExitedNotifier,
            Animator animator,
            int boolId
        ) 
            : base(attackAction, targetsProvider, attackInfo)
        {
            _attackStateExitedNotifier = attackStateExitedNotifier;
            _animator = animator;
            _boolId = boolId;
        }

        private readonly IAttackStateExitedNotifier _attackStateExitedNotifier;
        private readonly Animator _animator;
        private readonly int _boolId;

        private bool _stopWorking; 

        public override void Activate(StateMachine.StateMachine.CallbackContext callbackContext)
        {
            base.Activate(callbackContext);
            
            _attackStateExitedNotifier.OnAttackStateExited += StopWorking;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            
            _attackStateExitedNotifier.OnAttackStateExited -= StopWorking;
        }

        protected override void AttackInner(InputAction.CallbackContext callbackContext)
        {
            _stopWorking = false;
            _animator.SetBool(_boolId, true);
            base.AttackInner(callbackContext);
        }

        protected override bool ContinueWorkingCondition()
        {
            return base.ContinueWorkingCondition() && !_stopWorking;
        }

        private void StopWorking()
        {
            _stopWorking = true;
            _animator.SetBool(_boolId, false);
        }
    }
}