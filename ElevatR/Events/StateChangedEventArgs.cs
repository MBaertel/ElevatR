using ElevatR.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatR.Events
{
    public class StateChangedEventArgs : EventArgs
    {
        public Guid ElevatorId { get; }
        public ElevatorState State { get; }
        public int ElevatorLocation { get; }

        public StateChangedEventArgs(Guid elevatorId, ElevatorState state,int elevatorLocation)
        {
            ElevatorId = elevatorId;
            State = state;
            ElevatorLocation = elevatorLocation;
        }
    }
}
