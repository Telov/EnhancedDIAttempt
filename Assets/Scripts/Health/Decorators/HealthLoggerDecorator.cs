using UnityEngine;

namespace EnhancedDIAttempt.Health
{
    public class HealthLoggerDecorator : IHealthController
    {
        public HealthLoggerDecorator(IHealthController health)
        {
            _health = health;
        }

        private readonly IHealthController _health;

        public void Activate() => _health.Activate();

        public void Deactivate() => _health.Deactivate();
        public void GetDamage(float damageAmount)
        {
            _health.GetDamage(damageAmount);
            Debug.Log("Health left: " + CurrentHealth);
        }

        public float CurrentHealth => _health.CurrentHealth;
        public float MaxHealth => _health.MaxHealth;
    }
}