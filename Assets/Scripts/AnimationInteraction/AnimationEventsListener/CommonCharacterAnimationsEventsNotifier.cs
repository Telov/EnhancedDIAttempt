using System;
using UnityEngine;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public class CommonCharacterAnimationsEventsNotifier : MonoBehaviour, ICommonCharacterAnimationsEventsNotifier
    {
        public event Action OnAttackStart = () => { };
        public event Action OnAttackEnd = () => { };

        private void OnAttackStarted()
        {
            OnAttackStart();
        }

        private void OnAttackEnded()
        {
            OnAttackEnd();
        }
    }
}