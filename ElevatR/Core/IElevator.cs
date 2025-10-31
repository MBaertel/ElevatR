using ElevatR.Enum;
using ElevatR.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatR.Core
{
    public interface IElevator
    {
        public Guid Id { get; }
        public int CurrentFloor { get; }
        public int MaxFloor { get; }
        public int MinFloor { get; }
        public Direction Direction { get; }
        public ElevatorState State { get; }
        IReadOnlyList<int> Targets { get; }
        void AddTarget(int floor);
        int? GetMoveCost(int targetFloor,int moveCost = 1,int stopCost = 3);

        // Events
        public event EventHandler<FloorChangedEventArgs>? FloorChanged;
        public event EventHandler<FloorChangingEventArgs>? FloorChanging;
        public event EventHandler<StateChangedEventArgs>? StateChanged;
    }
}
