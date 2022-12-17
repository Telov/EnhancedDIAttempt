using System.Collections.Generic;
using EnhancedDIAttempt.Damage;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions
{
    public abstract class AttackActionBase : IBehaviour
    {
        public AttackActionBase(IAttackTargetsProvider targetsProvider, InputAction attackAction, AttackInfo info)
        {
            TargetsProvider = targetsProvider;
            _attackAction = attackAction;
            Info = info;
        }

        protected readonly IAttackTargetsProvider TargetsProvider;
        protected readonly AttackInfo Info;
        private readonly InputAction _attackAction;

        private float _lastAttackTime;

        public virtual void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            _attackAction.performed += Attack;
        }

        public virtual void Deactivate()
        {
            _attackAction.performed -= Attack;
        }

        protected abstract void AttackInner(InputAction.CallbackContext callbackContext);
        
        protected void DealDamageToEach(IEnumerable<IDamageGetter> damageGetters)
        {
            foreach (IDamageGetter damageGetter in damageGetters)
            {
                damageGetter.GetDamage(Info.attackDamage);
            }
        }
        
        protected void SetCooldownPoint()
        {
            _lastAttackTime = Time.time;
        }

        private void Attack(InputAction.CallbackContext callbackContext)
        {
            if (!CooldownPassed()) return;
            AttackInner(callbackContext);
        }

        private bool CooldownPassed()
        {
            return  Time.time - _lastAttackTime > Info.attackCooldown;
        }

        [System.Serializable]
        public class AttackInfo
        {
            public float attackDamage;
            public float attackCooldown;
        }
    }
}