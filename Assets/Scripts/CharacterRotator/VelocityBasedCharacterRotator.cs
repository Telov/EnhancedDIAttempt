using UnityEngine;

namespace EnhancedDIAttempt.CharacterRotator
{
    public class VelocityBasedCharacterRotator : ICharacterRotator
    {
        public VelocityBasedCharacterRotator
        (
            IUpdatesController updatesController,
            Transform rotatedObject,
            Rigidbody2D rigidbody,
            Vector3 rotationAxis
        )
        {
            _updatesController = updatesController;
            _rotatedObject = rotatedObject;
            _rigidbody = rigidbody;
            _rotationAxis = rotationAxis;
        }

        private readonly IUpdatesController _updatesController;
        private readonly Transform _rotatedObject;
        private readonly Rigidbody2D _rigidbody;
        private readonly Vector3 _rotationAxis;

        private bool _rotated;

        public void Activate()
        {
            _updatesController.AddUpdateCallback(RotateIfTooFast);
        }

        public void Deactivate()
        {
            _updatesController.RemoveUpdateCallback(RotateIfTooFast);
        }

        private void RotateIfTooFast()
        {
            if (_rigidbody.velocity.x < 0f)
            {
                if (_rotated) return;
                _rotated = true;
                _rotatedObject.Rotate(_rotationAxis, 180f);
            }
            else
            {
                if (!_rotated || _rigidbody.velocity.sqrMagnitude - 0.0000001f < 0f) return;
                _rotated = false;
                _rotatedObject.Rotate(_rotationAxis, -180f);
            }
        }
    }
}