using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions
{
    public class InstantaneousAttackAction : AttackActionBase
    {
        public InstantaneousAttackAction(InputAction attackAction, IAttackTargetsProvider targetsProvider, AttackInfo info)
            : base(targetsProvider, attackAction, info)
        {
        }

        protected override void AttackInner(InputAction.CallbackContext callbackContext)
        {
            DealDamageToEach(TargetsProvider.GetAttackTargets());
            
            SetCooldownPoint();
        }
    }
}