using Telov.Utils;
using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours.ConstantMoveDownBehaviour
{
    public class ConstantMoveDownBehaviour : IBehaviour
    {
        public ConstantMoveDownBehaviour
        (
            Rigidbody2D rb,
            IUpdatesController updatesController,
            float speedChangePerTick,
            float powerRiseRate,
            float speedLimit
        )
        {
            _rb = rb;
            _updatesController = updatesController;
            _speedChangePerTick = speedChangePerTick;
            _powerRiseRate = powerRiseRate;
            _speedLimit = speedLimit;
        }

        private readonly Rigidbody2D _rb;
        private readonly IUpdatesController _updatesController;
        private readonly float _speedChangePerTick;
        private readonly float _powerRiseRate;
        private readonly float _speedLimit;

        private float _multiplier = 1f;

        public void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            _updatesController.AddFixedUpdateCallback(MoveDown);
        }

        public void Deactivate()
        {
            _updatesController.RemoveFixedUpdateCallback(MoveDown);
            _multiplier = 1f;
        }

        private void MoveDown()
        {
            var velocity = _rb.velocity;
            float appliedForce = 1 * _speedChangePerTick * _rb.mass * _multiplier;
            appliedForce = Mathf.Clamp(appliedForce, 0, velocity.y - _speedLimit);
            Debug.Log(appliedForce);
            _rb.AddForce(Vector2.down * appliedForce, ForceMode2D.Impulse);
            _multiplier *= _powerRiseRate;
        }
    }
}