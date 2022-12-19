using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions
{
    public class MoveActionData
    {
        public MoveActionData(
            IUpdatesController updatesController,
            IRb2DMover rb2DMover,
            InputAction moveAction)
        {
            UpdatesController = updatesController;
            Rb2DMover = rb2DMover;
            MoveAction = moveAction;
        }
        
        public readonly IUpdatesController UpdatesController;
        public readonly InputAction MoveAction;
        public readonly IRb2DMover Rb2DMover;
    }
}
