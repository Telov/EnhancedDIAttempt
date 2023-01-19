using EnhancedDIAttempt.Health;
using Telov.Utils;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public class SettingAnimatorBoolHealthDecorator : IHealthController
    {
        public SettingAnimatorBoolHealthDecorator
        (
            IHealthController health,
            IAnimatorBoolSetter boolSetter,
            ICommonCharacterAnimationsEventsNotifier animEvents
        )
        {
            _health = health;
            _boolSetter = boolSetter;
            _animEvents = animEvents;
        }

        private readonly IHealthController _health;
        private readonly IAnimatorBoolSetter _boolSetter;
        private readonly ICommonCharacterAnimationsEventsNotifier _animEvents;

        private bool _withdrawn = true;

        public void GetDamage(float damageAmount)
        {
            if (_withdrawn)
            {
                _animEvents.OnHurtEnd += Withdraw;
                _boolSetter.SetBoolToTrue();
            }
            _health.GetDamage(damageAmount);
            if(CurrentHealth == 0) Withdraw();
        }

        private void Withdraw()
        {
            _animEvents.OnHurtEnd -= Withdraw;
            _boolSetter.SetBoolToFalse();
            _withdrawn = true;
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