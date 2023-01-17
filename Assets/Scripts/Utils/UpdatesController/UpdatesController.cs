using System;
using Zenject;

namespace Telov.Utils
{
    public class UpdatesController : IUpdatesController, ITickable, IFixedTickable, ILateTickable
    {
        private Action _onUpdate = () => { };
        private Action _onFixedUpdate = () => { };
        private Action _onLateUpdate = () => { };

        public void AddUpdateCallback(Action action)
        {
            _onUpdate += action;
        }

        public void RemoveUpdateCallback(Action action)
        {
            _onUpdate -= action;
        }

        public void AddFixedUpdateCallback(Action action)
        {
            _onFixedUpdate += action;
        }

        public void RemoveFixedUpdateCallback(Action action)
        {
            _onFixedUpdate -= action;
        }

        public void AddLateUpdateCallback(Action action)
        {
            _onLateUpdate += action;
        }

        public void RemoveLateUpdateCallback(Action action)
        {
            _onLateUpdate -= action;
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