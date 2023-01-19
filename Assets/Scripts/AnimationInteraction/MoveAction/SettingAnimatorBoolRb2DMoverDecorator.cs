using System.Collections;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours;
using Telov.Utils;
using UnityEngine;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public class SettingAnimatorBoolRb2DMoverDecorator : IRb2DMover
    {
        public SettingAnimatorBoolRb2DMoverDecorator
        (
            IRb2DMover mover,
            IUpdatesController updatesController,
            IAnimatorBoolSetter boolSetter,
            float animationLengthAfterLastMoveInSec
        )
        {
            _mover = mover;
            _updatesController = updatesController;
            _boolSetter = boolSetter;
            _animationLengthAfterLastMoveInSec = animationLengthAfterLastMoveInSec;
        }

        private readonly IRb2DMover _mover;
        private readonly IUpdatesController _updatesController;
        private readonly IAnimatorBoolSetter _boolSetter;
        private readonly float _animationLengthAfterLastMoveInSec;

        private bool _countdownStarted;
        private float _timeLeft;

        public void Move(float speedMultiplier, Vector3 direction)
        {
            _timeLeft = _animationLengthAfterLastMoveInSec;
            if (!_countdownStarted)
            {
                _countdownStarted = true;
                _updatesController.AddUpdateCallback(Countdown);
            }
            
            _boolSetter.SetBoolToTrue();

            _mover.Move(speedMultiplier, direction);
        }

        private void Countdown()
        {
            if (_timeLeft > 0f)
            {
                _timeLeft -= Time.deltaTime;
                return;
            }
            
            _boolSetter.SetBoolToFalse();
            _countdownStarted = false;
            _updatesController.RemoveUpdateCallback(Countdown);
        }
    }
}