using EnhancedDIAttempt.Health;
using Telov.Utils;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public class TriggeringAnimationHealthDecorator : IHealthController
    {
        public TriggeringAnimationHealthDecorator(IHealthController health, IAnimatorTriggerSetter triggerSetter)
        {
            _health = health;
            _triggerSetter = triggerSetter;
        }

        private readonly IHealthController _health;
        private readonly IAnimatorTriggerSetter _triggerSetter;

        public void GetDamage(float damageAmount)
        {
            _triggerSetter.SetTrigger();
            _health.GetDamage(damageAmount);
        }

        public float CurrentHealth => _health.CurrentHealth;
        public float MaxHealth => _health.MaxHealth;
        public void Activate()
        {
            _health.Activate();
        }

        public void Deactivate()
        {
            _health.Deactivate();
        }
    }
}