using Telov.Utils;

namespace EnhancedDIAttempt.Health
{
    public class DisablingInputControllerDeathHandlerDecorator : IDeathHandler
    {
        public DisablingInputControllerDeathHandlerDecorator(IDeathHandler deathHandler, IInputController inputController)
        {
            _deathHandler = deathHandler;
            _inputController = inputController;
        }

        private readonly IDeathHandler _deathHandler;
        private readonly IInputController _inputController;
        
        public void Trigger()
        {
            _inputController.DisableInputActionCollection();
            _deathHandler.Trigger();
        }
    }
}