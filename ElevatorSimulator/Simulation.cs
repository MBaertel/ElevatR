using ElevatR;
using ElevatR.Core;
using ElevatR.Scheduling;

namespace ElevatorSimulator
{
    public class Simulation
    {
        private ElevatR.ElevatR elevatorHandler = new();
        private Dictionary<Guid, IElevator> elevators = new();
        private PassengerManager passengerManager;

        public Simulation(ElevatR.ElevatR elevatorHandler)
        {
            this.elevatorHandler = elevatorHandler ?? new ElevatR.ElevatR();
            this.passengerManager = new PassengerManager(this.elevatorHandler);
        }

        public Simulation()
        {
            var elevator1 = new SimulationElevator(
                new FIFOScheduler(),
                0,
                10,
                2000,
                5000
            );

            var elevator2 = new SimulationElevator(
                new FIFOScheduler(),
                0,
                10,
                2000,
                5000
            );

            elevators.Add(elevator1.Id, elevator1);
            elevators.Add(elevator2.Id, elevator2);

            elevatorHandler.AddElevator(elevator1, 2, 5);
            elevatorHandler.AddElevator(elevator2, 2, 5);
        }

        public async Task RunSimulation()
        {
            bool isRunning = true;
            while (isRunning)
            {
                if(passengerManager.PassengerCount < 5)
                {
                    passengerManager.AddPassenger(Person.GenerateRandom(0,10));
                }
                await Task.Delay(2000);
            }
        }
    }
}
