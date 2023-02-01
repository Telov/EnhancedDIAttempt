using System;
using UnityEngine;

namespace Telov.Utils
{
    public class MonoUpdatesController : MonoBehaviour, IUpdatesController
    {
        private readonly UpdatesController _updatesController = new UpdatesController();
        
        public event Action UpdateCallbacks
        {
            add => _updatesController.UpdateCallbacks += value;
            remove => _updatesController.UpdateCallbacks -= value;
        }
        public event Action FixedUpdateCallbacks
        {
            add => _updatesController.FixedUpdateCallbacks += value;
            remove => _updatesController.FixedUpdateCallbacks -= value;
        }
        public event Action LateUpdateCallbacks
        {
            add => _updatesController.LateUpdateCallbacks += value;
            remove => _updatesController.LateUpdateCallbacks -= value;
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