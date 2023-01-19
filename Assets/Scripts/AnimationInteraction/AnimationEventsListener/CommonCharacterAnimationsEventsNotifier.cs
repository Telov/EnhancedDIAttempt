using System;
using UnityEngine;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public class CommonCharacterAnimationsEventsNotifier : MonoBehaviour, ICommonCharacterAnimationsEventsNotifier
    {
        public event Action OnPrepToAttack = () => { };
        public event Action OnAttackStart = () => { };
        public event Action OnAttackEnd = () => { };
        
        public event Action OnHurtStart = () => { };
        public event Action OnHurtEnd = () => { };

        private void OnPreparingToAttack()
        {
            OnPrepToAttack();
        }

        private void OnAttackStarted()
        {
            OnAttackStart();
        }

        private void OnAttackEnded()
        {
            OnAttackEnd();
        }
        private void OnHurtStarted()
        {
            OnHurtStart();
        }

        private void OnHurtEnded()
        {
            Debug.Log(2);
            OnHurtEnd();
        }
    }
}