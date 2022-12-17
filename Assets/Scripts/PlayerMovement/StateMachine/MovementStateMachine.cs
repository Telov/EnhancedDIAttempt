using EnhancedDIAttempt.StateMachine;

namespace EnhancedDIAttempt.PlayerActions
{
    public class MovementStateMachine : EnhancedDIAttempt.StateMachine.StateMachine, IController
    {
        public MovementStateMachine(ISpareStatesProvider spareStatesProvider) : base(spareStatesProvider)
        {
        }

        public void Enable()
        {
            ActivateMachine();
        }

        public void Disable()
        {
            DeactivateMachine();
        }
    }
}