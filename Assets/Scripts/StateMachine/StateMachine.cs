using System.Collections.Generic;

namespace EnhancedDIAttempt.StateMachine
{
    public class StateMachine
    {
        public StateMachine(IStatesProvider statesProvider)
        {
            SpareStates = statesProvider.GetStates();
        }

        protected readonly List<IState> SpareStates; //states which you try to revert to when null state is specified 

        protected IState CurrentState;
        protected bool MachineActive;
        protected bool CurrentStateActive;

        public void ActivateMachine()
        {
            if (!MachineActive)
            {
                MachineActive = true;

                if (CurrentState == null) SetOneOfSpareStates();
                if (!CurrentStateActive) ActivateCurrentState();
            }
        }

        public void DeactivateMachine()
        {
            if (MachineActive)
            {
                DeactivateCurrentState();

                MachineActive = false;
            }
        }

        public virtual void SetState(IState newState)
        {
            if (newState == null)
            {
                ExitCurrentState();
                return;
            }

            if (newState == CurrentState) return;

            DeactivateCurrentState();
            CurrentState = newState;
            if (MachineActive) ActivateCurrentState();
        }

        public void ExitCurrentState()
        {
            SetOneOfSpareStates();
        }

        protected virtual void SetOneOfSpareStates()
        {
            SetState(ChooseSpareState());
        }

        protected IState ChooseSpareState()
        {
            foreach (IState spareState in SpareStates)
            {
                if (spareState.IsSuitedToBeAppliedNow())
                {
                    return spareState;
                }
            }

            return SpareStates[^1];
        }

        protected virtual void ActivateCurrentState()
        {
            if (!CurrentStateActive)
            {
                CurrentState.Activate(new CallbackContext(this));

                CurrentStateActive = true;
            }
        }

        protected virtual void DeactivateCurrentState()
        {
            if (CurrentStateActive)
            {
                CurrentState.Deactivate();

                CurrentStateActive = false;
            }
        }

        public class CallbackContext
        {
            public CallbackContext
            (
                StateMachine stateMachine
            )
            {
                _stateMachine = stateMachine;
            }

            private readonly StateMachine _stateMachine;

            public void SetState(IState state)
            {
                _stateMachine.SetState(state);
            }

            public void ExitCurrentState()
            {
                _stateMachine.ExitCurrentState();
            }
        }
    }
}