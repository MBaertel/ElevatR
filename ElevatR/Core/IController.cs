using ElevatR.Enum;
using ElevatR.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatR.Core
{
    public interface IController
    {
        void HallCall(int floor, Direction direction = Direction.Idle);
        void RegisterElevator(IElevator elevator,int moveCost,int stopCost);

        //Events
        public event EventHandler<HallCallEventArgs> HallCallTriggered;
    }
}
