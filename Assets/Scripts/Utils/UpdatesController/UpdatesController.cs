using System;
using Zenject;

namespace Telov.Utils
{
    public class UpdatesController : IUpdatesController, ITickable, IFixedTickable, ILateTickable
    {
        private Action _onUpdate = () => { };
        private Action _onFixedUpdate = () => { };
        private Action _onLateUpdate = () => { };

        public event Action UpdateCallbacks
        {
            add => _onUpdate += value;
            remove => _onUpdate -= value;
        }
        public event Action FixedUpdateCallbacks
        {
            add => _onFixedUpdate += value;
            remove => _onFixedUpdate -= value;
        }
        public event Action LateUpdateCallbacks
        {
            add => _onLateUpdate += value;
            remove => _onLateUpdate -= value;
        }
        

        public void Tick()
        {
            _onUpdate.Invoke();
        }

        public void FixedTick()
        {
            _onFixedUpdate.Invoke();
        }

        public void LateTick()
        {
            _onLateUpdate.Invoke();
        }

    }
}