using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBased
{
    public class AttackAction : IBehaviour
    {
        public AttackAction
        (
            IAttackTargetsProvider targetsProvider,
            ICooldownController cooldownController,
            IDamageDealer damageDealer,
            InputAction attackAction,
            float attackDamage
        )
        {
            _targetsProvider = targetsProvider;
            _cooldownController = cooldownController;
            _damageDealer = damageDealer;
            _attackAction = attackAction;
            _attackDamage = attackDamage;
        }

        private readonly IAttackTargetsProvider _targetsProvider;
        private readonly ICooldownController _cooldownController;
        private readonly IDamageDealer _damageDealer;
        private readonly InputAction _attackAction;
        private readonly float _attackDamage;

        private float _lastAttackTime;

        public void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            _attackAction.performed += Attack;
        }

        public void Deactivate()
        {
            _attackAction.performed -= Attack;
        }
        
        private void Attack(InputAction.CallbackContext callbackContext)
        {
            if (!_cooldownController.CooldownPassed) return;
            _damageDealer.DealDamage(_targetsProvider, _attackDamage);
            _cooldownController.StartCooldown();
        }
    }
}