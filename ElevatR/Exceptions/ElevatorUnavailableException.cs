using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatR.Exceptions
{
    public class ElevatorUnavailableException : Exception
    {
        public ElevatorUnavailableException(string message = "No Elevator is Available") : base(message) 
        {
        }
    }
}
