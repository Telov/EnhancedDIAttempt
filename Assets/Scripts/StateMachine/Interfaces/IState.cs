using EnhancedDIAttempt.StateMachine;

public interface IState
{
    public bool IsSuitedToBeAppliedNow();
    public void Activate(StateMachine.CallbackContext callbackContext);
    public void Deactivate();
}