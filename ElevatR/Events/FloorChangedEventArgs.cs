using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatR.Events
{
    public class FloorChangedEventArgs : EventArgs
    {
        public Guid ElevatorId { get; }
        public int Floor { get; }
        public bool IsStopped { get; }

        public FloorChangedEventArgs(Guid elevatorId, int floor, bool isStopped)
        {
            ElevatorId = elevatorId;
            Floor = floor;
            IsStopped = isStopped;
        }
    }
}
