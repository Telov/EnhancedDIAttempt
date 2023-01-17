using System;
using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class AttackOnInputAttackRuler : IAttackRuler
    {
        public AttackOnInputAttackRuler(InputAction attackAction)
        {
            _attackAction = attackAction;
        }
        
        private readonly InputAction _attackAction;
        
        public event Action OnWantAttack
        {
            add
            {
                _innerOnWantAttack += value;
                _listenersCount++;
                if (_listenersCount == 1) _attackAction.performed += RequestAttack;
            }
            remove
            {
                _innerOnWantAttack -= value;
                _listenersCount--;
                if (_listenersCount == 0) _attackAction.performed -= RequestAttack;
            }
        }

        private Action _innerOnWantAttack = () => { };

        private int _listenersCount;

        private void RequestAttack(InputAction.CallbackContext ctx)
        {
            _innerOnWantAttack();
        }
    }
}