using ElevatR.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ElevatR.Scheduling
{
    public class FIFOScheduler : IScheduler
    {
        private readonly Queue<int> targets = new Queue<int>();

        public IReadOnlyList<int> Targets => targets.ToList().AsReadOnly();
        
        public void AddTarget(int floor)
        {
            targets.Enqueue(floor);
        }

        public int? GetNextTarget()
        {
            if(!targets.Any()) return null;
            return targets.Peek();
        }

        public void CompleteTarget()
        {
            if (targets.Any()) 
                targets.Dequeue();
        }

        public int GetMoveCostToFloor(int targetFloor,int currentFloor, int moveCost = 1,int stopCost = 3)
        {
            if(currentFloor == targetFloor) return 0;
            if (!targets.Any()) 
                return Math.Abs(targetFloor - currentFloor) * moveCost;

            int total = 0;
            int pos = currentFloor;

            foreach(var next in targets)
            {
                if (IsBetween(pos,next,targetFloor))
                    return total += (Math.Abs(targetFloor - pos) * moveCost);

                total += (Math.Abs(next - pos) * moveCost);
                total += stopCost;

                pos = next;
            }

            total += (Math.Abs(targetFloor - pos) * moveCost);
            return total;
        }

        private static bool IsBetween(int a,int b,int x)
        {
            return (x >= Math.Min(a, b) && x <= Math.Max(a, b));
        }
    }
}
