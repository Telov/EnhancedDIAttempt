using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBased;
using EnhancedDIAttempt.Utils.MecanimStateMachine;

namespace EnhancedDIAttempt.AnimationInteraction.MoveAction
{
    public class AnimationDependantMoveAllowerDecorator : IMoveAllower
    {
        public AnimationDependantMoveAllowerDecorator(IMoveAllower moveAllower, IMecanimStateInfoProvider stateInfoProvider)
        {
            _stateInfoProvider = stateInfoProvider;
            _moveAllower = moveAllower;
        }

        private readonly IMecanimStateInfoProvider _stateInfoProvider;
        private readonly IMoveAllower _moveAllower;

        public bool Allows => !_stateInfoProvider.IsActive && _moveAllower.Allows;
    }
}