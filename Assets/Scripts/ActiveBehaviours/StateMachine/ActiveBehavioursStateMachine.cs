using EnhancedDIAttempt.StateMachine;

namespace EnhancedDIAttempt.ActiveBehaviours
{
    public class ActiveBehavioursStateMachine : EnhancedDIAttempt.StateMachine.StateMachine, IController
    {
        public ActiveBehavioursStateMachine(ISpareStatesProvider spareStatesProvider) : base(spareStatesProvider)
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