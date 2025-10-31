using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatR.Events
{
    public class ElevatorExceptionEventArgs : EventArgs
    {
        public Exception Exception { get; }

        public ElevatorExceptionEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}
