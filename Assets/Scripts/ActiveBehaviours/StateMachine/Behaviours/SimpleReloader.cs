using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class SimpleReloader : IReloader
    {
        public SimpleReloader(float cooldown)
        {
            _cooldown = cooldown;
        }

        private readonly float _cooldown;

        private float _timeSinceCooldownStarted = float.MinValue;

        public void Unload()
        {
            _timeSinceCooldownStarted = Time.time;
        }

        public bool Reloaded => !(Time.time - _timeSinceCooldownStarted < _cooldown);
    }
}