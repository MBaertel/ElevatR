using ElevatR.Core;
using ElevatR.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatR.Scheduling
{
    public interface IScheduler
    {
        IReadOnlyList<int> Targets { get; }

        void AddTarget(int floor);
        int? GetNextTarget();
        void CompleteTarget();
        int GetMoveCostToFloor(int floor,int currentFloor,int moveCost,int stopCost);
    }
}
