using System;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public interface ICommonCharacterAnimationsEventsNotifier
    {
        public event Action OnAttackStart;
        public event Action OnAttackEnd;
    }
}