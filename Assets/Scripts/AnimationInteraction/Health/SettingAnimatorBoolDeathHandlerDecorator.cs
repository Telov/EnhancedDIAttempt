using EnhancedDIAttempt.Health;
using Telov.Utils;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public class SettingAnimatorBoolDeathHandlerDecorator : IDeathHandler
    {
        public SettingAnimatorBoolDeathHandlerDecorator(IDeathHandler deathHandler, IAnimatorBoolSetter boolSetter)
        {
            _deathHandler = deathHandler;
            _boolSetter = boolSetter;
        }

        private readonly IDeathHandler _deathHandler;
        private readonly IAnimatorBoolSetter _boolSetter;

        public void Trigger()
        {
            _boolSetter.SetBoolToTrue();
            _deathHandler.Trigger();
        }
    }
}