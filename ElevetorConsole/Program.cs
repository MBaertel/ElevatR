using ElevatR;
using System.Security.Cryptography.X509Certificates;

namespace ElevetarConsole
{
    internal class Program
    {
        internal static ElevatR.ElevatR ElevatR;
        public static async Task Main(string[] args)
        {
            ElevatR = new ElevatR.ElevatR();
            ElevatR.AddSimpleElevator();
            ElevatR.AddSimpleElevator();

            var visualizer = new ElevatorVisualizer();
            foreach(var elevator in ElevatR.Elevators)
            {
                visualizer.AddElevator(elevator);
            }
            ElevatR.Controller.HallCall(4);
            ElevatR.Controller.HallCall(7);
            await DrawLoop(visualizer);
        }

        public static async Task DrawLoop(ElevatorVisualizer visualizer)
        {
            bool isDrawing = true;
            while(isDrawing)
            {
                visualizer.Draw();
            }
        }
    }
}
