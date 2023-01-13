using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBasedActions
{
    public class MoveActionData
    {
        public MoveActionData(
            IUpdatesController updatesController,
            IRb2DMover rb2DMover,
            IMoveAllower moveAllower,
            InputAction inputAction)
        {
            UpdatesController = updatesController;
            Rb2DMover = rb2DMover;
            MoveAllower = moveAllower;
            InputAction = inputAction;
        }
        
        public readonly IUpdatesController UpdatesController;
        public readonly IRb2DMover Rb2DMover;
        public readonly IMoveAllower MoveAllower;
        public readonly InputAction InputAction;
    }
}
