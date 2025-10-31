using ElevatR.Enum;
using ElevatR.Events;
using ElevatR.Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ElevatR.Core
{
    public class SimulationElevator : IElevator
    {
        private readonly int moveDuration = 2000;
        private readonly int loadingTime = 5000;
        private readonly IScheduler scheduler;
        private bool isRunning;

        public Guid Id { get; } = Guid.NewGuid();

        public int CurrentFloor { get; private set; }
        public int MaxFloor { get; }

        public int MinFloor { get; }

        public Direction Direction { get; private set; }

        public ElevatorState State { get; private set; } = ElevatorState.Idle;

        public IReadOnlyList<int> Targets => scheduler.Targets;


        public event EventHandler<FloorChangedEventArgs>? FloorChanged;
        public event EventHandler<FloorChangingEventArgs>? FloorChanging;
        public event EventHandler<StateChangedEventArgs>? StateChanged;

        public SimulationElevator(IScheduler? scheduler,int minFloor = 0,int maxFloor = 10,int moveDuration = 2000,int loadingTime = 5000)
        {
            this.scheduler = scheduler ?? new FIFOScheduler();
            this.MinFloor = minFloor;
            this.MaxFloor = maxFloor;
        }

        public static SimulationElevator CreateSimpleElevator(int minFloor = 0,int maxFloor = 10)
        {
            var scheduler = new FIFOScheduler();
            return new SimulationElevator(scheduler);
        }

        public void AddTarget(int floor)
        {
            if (floor > MaxFloor || floor < MinFloor) 
                throw new ArgumentOutOfRangeException("Floor out of bounds");
            scheduler.AddTarget(floor);
            if (!isRunning) Start();
        }

        public int? GetMoveCost(int targetFloor,int moveCost = 1,int stopCost = 3)
        {
            if (State == ElevatorState.OutOfOrder || State == ElevatorState.OutOfOrder) return null;
            return scheduler.GetMoveCostToFloor(targetFloor,CurrentFloor,moveCost,stopCost);
        }

        public async void Start()
        {
            if (isRunning) return;
            isRunning = true;
            MoveLoop();
        }

        public async void Stop()
        {
            isRunning = false;
        }

        public async void MoveLoop()
        {
            while (isRunning)
            {
                var next = scheduler.GetNextTarget();
                if (!next.HasValue)
                {
                    setState(ElevatorState.Idle);
                    setDirection(Direction.Idle);
                    isRunning = false;
                    continue;
                }

                if(CurrentFloor != next.Value)
                {
                    if (next.Value > CurrentFloor)
                        setDirection(Direction.Up);
                    else if (next.Value < CurrentFloor)
                        setDirection(Direction.Down);
                    
                    setState(ElevatorState.Moving);
                    var newFloor = CurrentFloor + (Direction == Direction.Up ? 1 : -1);
                    
                    floorChanging(CurrentFloor,newFloor);
                    await Task.Delay(moveDuration);
                    CurrentFloor += (Direction == Direction.Up ? 1 : -1);
                    notifyFloorChanged(CurrentFloor,CurrentFloor == next.Value);
                }
                else
                {
                    scheduler.CompleteTarget();
                    setState(ElevatorState.Loading);
                    await Task.Delay(loadingTime);
                }
            }
        }

        private void setState(ElevatorState newState)
        {
            if (State != newState)
            {
                State = newState;
                StateChanged?.Invoke(this,new StateChangedEventArgs(Id, newState,CurrentFloor));
            }
        }

        private void setDirection(Direction newDirection)
        {
            if (Direction != newDirection)
                Direction = newDirection;
        }

        private void floorChanging(int previous,int next)
        {
            FloorChanging?.Invoke(this,new FloorChangingEventArgs(Id, previous, next));
        }

        private void notifyFloorChanged(int newFLoor,bool isStopped)
        {
            FloorChanged?.Invoke(this,new FloorChangedEventArgs(Id, newFLoor,isStopped));
        }
    }
}
