using EnhancedDIAttempt.Damage;

namespace EnhancedDIAttempt.Health
{
    public interface IHealth : IDamageGetter
    {
        public float CurrentHealth { get; }
        public float MaxHealth { get; }
    }
}