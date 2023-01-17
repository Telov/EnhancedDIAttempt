using System;
using UnityEngine;

namespace Telov.Utils
{
    public class MonoUpdatesController : MonoBehaviour, IUpdatesController
    {
        private readonly UpdatesController _updatesController = new UpdatesController();
        
        public void AddUpdateCallback(Action action)
        {
            _updatesController.AddUpdateCallback(action);
        }

        public void RemoveUpdateCallback(Action action)
        {
            _updatesController.RemoveUpdateCallback(action);
        }

        public void AddFixedUpdateCallback(Action action)
        {
            _updatesController.AddFixedUpdateCallback(action);
        }

        public void RemoveFixedUpdateCallback(Action action)
        {
            _updatesController.RemoveFixedUpdateCallback(action);
        }

        public void AddLateUpdateCallback(Action action)
        {
            _updatesController.AddLateUpdateCallback(action);
        }

        public void RemoveLateUpdateCallback(Action action)
        {
            _updatesController.RemoveLateUpdateCallback(action);
        }

        private void Update()
        {
            _updatesController.Tick();
        }

        private void FixedUpdate()
        {
            _updatesController.FixedTick();
        }

        private void LateUpdate()
        {
            _updatesController.LateTick();
        }
    }
}