namespace EnhancedDIAttempt.Health
{
    public interface IHealth : IDamageable
    {
        public float CurrentHealth { get; }
        public float MaxHealth { get; }
    }
}