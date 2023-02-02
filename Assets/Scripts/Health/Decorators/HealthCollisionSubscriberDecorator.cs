
using Telov.Utils;

namespace EnhancedDIAttempt.Health
{
    public class HealthCollisionSubscriberDecorator : IHealthController
    {
        public HealthCollisionSubscriberDecorator(IHealthController health, ICollisionManager collisionManager)
        {
            _health = health;
            _collisionManager = collisionManager;
        }

        private readonly IHealthController _health;
        private readonly ICollisionManager _collisionManager;

        public void Activate()
        {
            _health.Activate();
            _collisionManager.SubscribeAsListener(this);
        }

        public void Deactivate()
        {
            _health.Deactivate();
            _collisionManager.UnsubscribeAsListener(this);
        }

        public void GetDamage(float damageAmount) => _health.GetDamage(damageAmount);

        public float CurrentHealth => _health.CurrentHealth;
        public float MaxHealth => _health.MaxHealth;
    }
}