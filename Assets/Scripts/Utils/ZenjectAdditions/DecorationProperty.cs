using System;
using System.Collections.Generic;
using Zenject;

namespace EnhancedDIAttempt.Utils.ZenjectAdditions
{
    public class DecorationProperty<T> where T : class
    {
        // public DecorationProperty(Context context, MonoInstaller installer)
        // {
        //     _context = context;
        //     _installer = installer;
        // }
        //
        // private readonly MonoInstaller _installer;
        // private readonly Context _context;

        private readonly List<Func<T, T>> _decorators = new();
        private Action _preSetRoutines;
        private Func<T> _value;
        private T _finalValue;

        public void Decorate(Func<T, T> decorator)
        {
            _decorators.Add(decorator);
            Decorated = false;
        }

        public void AddPreSetRoutine(Action routine)
        {
            _preSetRoutines += routine;
            Decorated = false;
        }

        public Func<T> PrimaryValue
        {
            set
            {
                if (_value != null) throw GetException("PrimaryValue of DecorationProperty was set more than once");
                _value = value;
                Decorated = false;
            }
        }

        public T FinalValue
        {
            get
            {
                if (_value == null) throw GetException("No value to DecorationProperty");

                if (!Decorated)
                {
                    _preSetRoutines?.Invoke();

                    _finalValue = _value();

                    foreach (var funcDecorator in _decorators)
                    {
                        _finalValue = funcDecorator(_finalValue);
                    }

                    Decorated = true;
                }

                return _finalValue;
            }
        }

        private Exception GetException(string text)
        {
            return new Exception
            (
                text
                //+ "\n GameObject with Context: " + _context.name
                //+ "\n Installer Type:" + _installer.GetType()
            );
        }

        private bool _decorated;

        private bool Decorated
        {
            get => _decorated;
            set
            {
                if (Decorated && !value)
                {
                    throw GetException("DecorationProperty changed after someone already used the final value on");
                }

                _decorated = value;
            }
        }
    }
}