using EnhancedDIAttempt.StateMachine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine
{
    public class ActiveBehavioursStateMachine : EnhancedDIAttempt.StateMachine.StateMachine, IController
    {
        public ActiveBehavioursStateMachine(IStatesProvider statesProvider) : base(statesProvider)
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