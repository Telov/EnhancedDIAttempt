using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions
{
    public class MoveActionData
    {
        public MoveActionData(
            IPlayerRbProvider playerRbProvider,
            IGroundChecker groundChecker,
            IUpdatesController updatesController,
            InputAction moveAction)
        {
            PlayerRbProvider = playerRbProvider;
            GroundChecker = groundChecker;
            UpdatesController = updatesController;
            MoveAction = moveAction;
        }
        
        public readonly IPlayerRbProvider PlayerRbProvider;
        public readonly IGroundChecker GroundChecker;
        public readonly IUpdatesController UpdatesController;
        public readonly InputAction MoveAction;
    }
}
