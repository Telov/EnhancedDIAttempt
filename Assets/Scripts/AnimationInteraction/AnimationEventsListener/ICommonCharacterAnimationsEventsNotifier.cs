using System;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public interface ICommonCharacterAnimationsEventsNotifier
    {
        public event Action OnPrepToAttack;
        public event Action OnAttackStart;
        public event Action OnAttackEnd;
        public event Action OnHurtStart;
        public event Action OnHurtEnd;
    }
}