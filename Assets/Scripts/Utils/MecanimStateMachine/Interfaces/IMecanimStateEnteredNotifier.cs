using System;

namespace Telov.Utils
{
    public interface IMecanimStateEnteredNotifier
    {
        public event Action OnStateEntered;
    }
}