using UnityEngine;
using UnityEngine.InputSystem;

namespace Telov.Utils
{
    public abstract class InputController<T> : MonoBehaviour, IInputController where T : IInputActionCollection2, new()
    {
        private T _inputActionsCollection;
        
        public void DisableInputActionCollection()
        {
            _inputActionsCollection.Disable();
        }
        
        public T GetInputClassInstance()
        {
            if(_inputActionsCollection == null) _inputActionsCollection = new T();
            return _inputActionsCollection;
        }

        private void OnEnable()
        {
            _inputActionsCollection.Enable();
        }

        private void OnDisable()
        {
            DisableInputActionCollection();
        }
    }
}