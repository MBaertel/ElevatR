using ElevatR.Core;
using ElevatR.Enum;
using ElevatR.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator
{
    public class PassengerManager
    {
        private List<Person> people = new();
        private List<Person> waiting = new();
        private Dictionary<Guid, Person> elevatorPassengers = new();
        private ElevatR.ElevatR elevatorHandler;

        public int PassengerCount => people.Count;

        public PassengerManager(ElevatR.ElevatR elevatorHandler)
        {
            this.elevatorHandler = elevatorHandler;
            this.elevatorHandler.ElevatorStateChanged += OnElevatorStateChanged;
        }

        public void AddPassenger(Person person)
        {
            people.Add(person);
            var elevatorsAtFloor = elevatorHandler.GetElevatorsAtFloor(person.Current);
            if (elevatorsAtFloor.Any())
            {
                elevatorPassengers.Add(elevatorsAtFloor[0],person);
                elevatorHandler.AddCabinRequest(elevatorsAtFloor[0],person.Target);
            }
            else
            {
                waiting.Add(person);
                elevatorHandler.RequestElevator(person.Current);
            }
        }

        private void OnElevatorStateChanged(object sender, StateChangedEventArgs e)
        {
            if(e.State == ElevatorState.Loading)
            {
                foreach (var kvp in elevatorPassengers)
                {
                    if(e.ElevatorId == kvp.Key && e.ElevatorLocation == kvp.Value.Target)
                    {
                        elevatorPassengers.Remove(kvp.Key);
                        people.Remove(kvp.Value);
                    }
                }
                foreach(var person in waiting)
                {
                    if(e.ElevatorLocation == person.Current)
                    { 
                        waiting.Remove(person);
                        elevatorPassengers.Add(e.ElevatorId,person);
                        elevatorHandler.AddCabinRequest(e.ElevatorId,person.Target);
                    }
                }
            }
        }
    }
}
