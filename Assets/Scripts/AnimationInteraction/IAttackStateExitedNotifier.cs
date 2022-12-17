using System;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public interface IAttackStateExitedNotifier
    {
        public event Action OnAttackStateExited;
    }
}