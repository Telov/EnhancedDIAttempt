using Telov.Utils;
using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States
{
    public class InAirStateDecorator : IState
    {
        public InAirStateDecorator(
            IState state,
            IGroundChecker groundChecker,
            IUpdatesController updatesController
            )
        {
            _state = state;
            _groundChecker = groundChecker;
            _updatesController = updatesController;
        }

        private readonly IState _state;
        private readonly IGroundChecker _groundChecker;
        private readonly IUpdatesController _updatesController;

        private EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext _callbackContext;

        public bool IsSuitedToBeAppliedNow()
        {
            return !_groundChecker.Grounded && _state.IsSuitedToBeAppliedNow();
        }

        public void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            Debug.Log("Air entered");
            _callbackContext = callbackContext;
            StartUpdating();
            _state.Activate(callbackContext);
        }

        public void Deactivate()
        {
            StopUpdating();
            _state.Deactivate();
        }
        
        private void StartUpdating()
        {
            _updatesController.AddUpdateCallback(OnUpdate);
        }
    
        private void StopUpdating()
        {
            _updatesController.RemoveUpdateCallback(OnUpdate);
        }
    
        private void OnUpdate()
        {
            if (_groundChecker.Grounded) _callbackContext.ExitCurrentState();
        }
    }
}