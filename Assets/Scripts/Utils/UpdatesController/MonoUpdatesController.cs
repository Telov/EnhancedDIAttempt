using System;
using UnityEngine;

namespace EnhancedDIAttempt.Utils.UpdatesController
{
    public class MonoUpdatesController : MonoBehaviour, IUpdatesController
    {
        private Action _onUpdate = () => { };
        private Action _onFixedUpdate = () => { };
        private  Action _onLateUpdate  = () => { };
        
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

        private void Update()
        {
            _onUpdate.Invoke();
        }

        public void FixedUpdate()
        {
            _onFixedUpdate.Invoke();
        }

        private void LateUpdate()
        {
            _onLateUpdate.Invoke();
        }
    }
}