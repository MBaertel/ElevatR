using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatR.Events
{
    public class FloorChangingEventArgs : EventArgs
    {
        public Guid ElevatorId { get; }
        public int PreviousFloor { get; }

        public int NextFloor { get; }

        public FloorChangingEventArgs(Guid ElevatorId,int previousFloor, int nextFloor)
        {
            PreviousFloor = previousFloor;
            NextFloor = nextFloor;
        }
    }
}
