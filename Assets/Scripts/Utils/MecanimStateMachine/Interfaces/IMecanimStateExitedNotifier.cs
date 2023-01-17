using System;

namespace Telov.Utils
{
    public interface IMecanimStateExitedNotifier
    {
        public event Action OnStateExited;
    }
}