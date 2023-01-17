namespace EnhancedDIAttempt.Health
{
    public class DyingHealthDecorator : IHealthController
    {
        public DyingHealthDecorator(IHealthController health, IDeathHandler deathHandler)
        {
            _health = health;
            _deathHandler = deathHandler;
        }

        private readonly IHealthController _health;
        private readonly IDeathHandler _deathHandler;

        public void GetDamage(float damageAmount)
        {
            _health.GetDamage(damageAmount);
            if (!(CurrentHealth > 0f)) _deathHandler.Trigger();
        }

        public void Activate()
        {
            _health.Activate();
        }

        public void Deactivate()
        {
            _health.Deactivate();
        }

        public float CurrentHealth => _health.CurrentHealth;
        public float MaxHealth => _health.MaxHealth;
    }
}