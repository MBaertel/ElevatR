using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatR.Events
{
    public class HallCallEventArgs : EventArgs
    {
        public int Floor { get; }
        public Guid ElevatorId { get; }

        public HallCallEventArgs(int floor,Guid elevatorId)
        {
            Floor = floor;
            ElevatorId = elevatorId;
        }
    }
}
