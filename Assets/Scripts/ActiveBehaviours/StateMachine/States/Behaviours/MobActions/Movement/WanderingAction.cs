using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBasedActions;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.MobActions
{
    public class WanderingAction : IBehaviour
    {
        public WanderingAction(IUpdatesController updatesController, IRb2DMover mover, float wanderTimeInOneDirection, float wanderSpeed)
        {
            _updatesController = updatesController;
            _mover = mover;
            _wanderTimeInOneDirection = wanderTimeInOneDirection;
            _wanderSpeed = wanderSpeed;
        }

        private readonly IUpdatesController _updatesController;
        private readonly IRb2DMover _mover;
        private readonly float _wanderTimeInOneDirection;
        private readonly float _wanderSpeed;

        private float _timeWalkedInOneDirection;
        private bool _goingToTheRight = true;
        
        public void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            _updatesController.AddFixedUpdateCallback(Wander);
        }

        public void Deactivate()
        {
            _updatesController.RemoveFixedUpdateCallback(Wander);
        }

        private void Wander()
        {
            _mover.Move(_wanderSpeed,_goingToTheRight? Vector2.right : Vector2.left);
            _timeWalkedInOneDirection += Time.fixedDeltaTime;
            if (_timeWalkedInOneDirection > _wanderTimeInOneDirection)
            {
                _goingToTheRight = !_goingToTheRight;
                _timeWalkedInOneDirection = 0f;
            }
        }
    }
}