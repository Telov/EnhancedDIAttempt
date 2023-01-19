using EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours;
using EnhancedDIAttempt.AnimationInteraction;

namespace EnhancedDIAttempt.Health
{
    public class BlockingHealthDecorator : IHealthController
    {
        public BlockingHealthDecorator(IHealthController health, ICommonCharacterAnimationsEventsNotifier animEvents, IBlocker blocker)
        {
            _health = health;
            _animEvents = animEvents;
            _blocker = blocker;
        }
        
        private readonly IHealthController _health;
        private readonly ICommonCharacterAnimationsEventsNotifier _animEvents;
        private readonly IBlocker _blocker;
        
        public void GetDamage(float damageAmount)
        {
            _animEvents.OnHurtStart += StartFunction;
            _animEvents.OnHurtEnd += EndFunction;
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

        private void StartFunction()
        {
            _blocker.Block();
        }
        
        private void EndFunction()
        {
            _animEvents.OnHurtStart -= StartFunction;
            _animEvents.OnHurtEnd -= EndFunction;
            
            _blocker.Unblock();
        }
    }
}