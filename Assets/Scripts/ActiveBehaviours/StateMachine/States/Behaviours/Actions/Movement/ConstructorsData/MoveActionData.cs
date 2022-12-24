using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.Actions
{
    public class MoveActionData
    {
        public MoveActionData(
            IUpdatesController updatesController,
            IRb2DMover rb2DMover,
            IMoveAllower moveAllower,
            InputAction moveAction)
        {
            UpdatesController = updatesController;
            Rb2DMover = rb2DMover;
            MoveAllower = moveAllower;
            MoveAction = moveAction;
        }
        
        public readonly IUpdatesController UpdatesController;
        public readonly IRb2DMover Rb2DMover;
        public readonly IMoveAllower MoveAllower;
        public readonly InputAction MoveAction;
    }
}
