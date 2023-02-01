using System;

namespace Telov.Utils
{
    public interface IUpdatesController
    {
        public event Action UpdateCallbacks;
        public event Action FixedUpdateCallbacks;
        public event Action LateUpdateCallbacks;
    }
}