using UnityEngine;

namespace EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions
{
    public class SimpleCooldownController : ICooldownController
    {
        public SimpleCooldownController(float cooldown)
        {
            _cooldown = cooldown;
        }

        private readonly float _cooldown;

        private float _timeSinceCooldownStarted = float.MinValue;

        public void StartCooldown()
        {
            _timeSinceCooldownStarted = Time.time;
        }

        public bool CooldownPassed => !(Time.time - _timeSinceCooldownStarted < _cooldown);
    }
}