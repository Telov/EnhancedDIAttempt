using System;

namespace Telov.Utils
{
    public interface IUpdatesController
    {
        public void AddUpdateCallback(Action action);
        public void RemoveUpdateCallback(Action action);
        public void AddFixedUpdateCallback(Action action);
        public void RemoveFixedUpdateCallback(Action action);
        public void AddLateUpdateCallback(Action action);
        public void RemoveLateUpdateCallback(Action action);
    }
}