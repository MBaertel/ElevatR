using ElevatR.Core;
using ElevatR.Enum;
using ElevatR.Events;

namespace ElevatR
{
    public class ElevatR
    {
        private List<IElevator> elevators = new();

        private int minFloor;
        private int maxFloor;

        public int MaxFloor => maxFloor;
        public int MinFloor => minFloor;

        public IReadOnlyList<IElevator> Elevators => elevators.AsReadOnly();
        public IController Controller;

        public ElevatR(IController controller = null,int minFloor = 0,int maxFloor = 10)
        {
            Controller = controller ?? new Controller();
            Controller.HallCallTriggered += OnHallCall;
            this.minFloor = minFloor;
            this.maxFloor = maxFloor;
        }

        public void AddElevator(IElevator elevator,int moveCost,int stopCost)
        {
            this.elevators.Add(elevator);
            Controller.RegisterElevator(elevator,moveCost,stopCost);
            elevator.FloorChanged += OnFloorChanged;
            elevator.FloorChanging += OnFloorChanging;
            elevator.StateChanged += OnStateChanged;
        }

        public void AddSimpleElevator()
        {
            var elevator = SimulationElevator.CreateSimpleElevator(minFloor,maxFloor);
            AddElevator(elevator,2,5);
        }

        public void RequestElevator(int floor)
        {
            try
            {
                Controller.HallCall(floor);
            } 
            catch(Exception ex)
            {
                notifyElevatorException(ex);
            }
        }

        public void AddCabinRequest(IElevator elevator, int floor)
        {
            elevator.AddTarget(floor);
        }

        public void AddCabinRequest(Guid elevatorId,int floor)
        {
            var elevator = elevators.FirstOrDefault(x => x.Id == elevatorId);
            if (elevator == null) return;
            elevator.AddTarget(floor);
        }

        public List<Guid> GetElevatorsAtFloor(int floor)
        {
            var guids = new List<Guid>();
            foreach (var elevator in elevators)
            {
                if(elevator.CurrentFloor == floor)
                {
                    guids.Add(elevator.Id);
                }
            }
            return guids;
        }

        public event EventHandler<FloorChangedEventArgs> ElevatorFloorChanged;
        public event EventHandler<FloorChangingEventArgs> ElevatorFloorChanging;
        public event EventHandler<StateChangedEventArgs> ElevatorStateChanged;
        public event EventHandler<HallCallEventArgs> ControllerHallCall;
        public event EventHandler<ElevatorExceptionEventArgs> ElevatorException;

        private async void OnFloorChanged(object sender,FloorChangedEventArgs e)
        {
            ElevatorFloorChanged?.Invoke(this, e);
        }

        private async void OnFloorChanging(object sender,FloorChangingEventArgs e)
        {
            ElevatorFloorChanging?.Invoke(this, e);
        }

        private async void OnStateChanged(object sender,StateChangedEventArgs e)
        {
            ElevatorStateChanged?.Invoke(this, e);
        }

        private async void OnHallCall(object sender, HallCallEventArgs e)
        {
            ControllerHallCall?.Invoke(this, e);
        }

        private async void notifyElevatorException(Exception ex)
        {
            ElevatorException?.Invoke(this, new ElevatorExceptionEventArgs(ex));
        }
    }
}
