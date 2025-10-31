using ElevatR.Enum;
using ElevatR.Events;
using ElevatR.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ElevatR.Core
{
    internal class Controller : IController
    {
        private List<IElevator> elevators = new List<IElevator>();
        private Dictionary<Guid,IElevator> elevatorDict = new Dictionary<Guid,IElevator>();
        private Dictionary<Guid, (int move, int stop)> moveCostDict = new();

        public event EventHandler<HallCallEventArgs> HallCallTriggered;

        public void HallCall(int floor, Direction direction = Direction.Idle)
        {
            int minValue = int.MaxValue;
            IElevator selectedElevator = null;
            foreach(var elevator in elevators)
            {
                var moveCosts = moveCostDict[elevator.Id];
                var cost = elevator.GetMoveCost(floor,moveCosts.move,moveCosts.stop);
                if(cost.HasValue && cost.Value < minValue)
                {
                    minValue = cost.Value;
                    selectedElevator = elevator;
                }
            }
            if (selectedElevator == null) throw new ElevatorUnavailableException();

            notifyHallCall(floor, selectedElevator.Id);
            selectedElevator.AddTarget(floor);
        }

        public void RegisterElevator(IElevator elevator,int moveCost = 1,int stopCost = 3)
        {
            elevators.Add(elevator);
            elevatorDict.Add(elevator.Id, elevator);
            moveCostDict.Add(elevator.Id, (moveCost, stopCost));
        }

        private void notifyHallCall(int floor,Guid elevatorId)
        {
            HallCallTriggered?.Invoke(this,new HallCallEventArgs(floor,elevatorId));
        }
    }
}
