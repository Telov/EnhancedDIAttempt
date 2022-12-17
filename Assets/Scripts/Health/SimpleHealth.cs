namespace EnhancedDIAttempt.Health
{
    public class SimpleHealth : IHealthController
    {
        public SimpleHealth(float curHealth, float maxHealth)
        {
            CurrentHealth = curHealth;
            MaxHealth = maxHealth;
        }

        private bool _working;

        public void Activate()
        {
            _working = true;
        }

        public void Deactivate()
        {
            _working = false;
        }
        
        public void GetDamage(float damageAmount)
        {
            if (!_working) return;
            CurrentHealth -= damageAmount;
            if (CurrentHealth <= 0f) Die();
        }

        private void Die()
        {
            
        }

        public float CurrentHealth { get; private set; }

        public float MaxHealth { get; }
    }
}