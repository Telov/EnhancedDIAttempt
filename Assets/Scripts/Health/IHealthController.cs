namespace EnhancedDIAttempt.Health
{
    public interface IHealthController : IHealth
    {
        public void Activate();
        public void Deactivate();
    }
}