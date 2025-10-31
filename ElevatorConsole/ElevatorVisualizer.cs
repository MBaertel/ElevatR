using ElevatR.Core;
using ElevatR.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace ElevetarConsole
{
    internal class ElevatorVisualizer
    {
        public int Height { get; set; } = 10;
        public Dictionary<Guid, IElevator> ElevatorPositions { get; set; } = new();

        public void AddElevator(IElevator elevator)
        {
            ElevatorPositions.Add(elevator.Id, elevator);
        }


        private static void DrawElevator(int x, int height, IElevator elevator)
        {
            for (int y = 0; y < height; y++)
            {
                // translate floor index (0 = bottom) to console Y coordinate
                int consoleY = (height - 1) - y;

                Console.SetCursorPosition(x, consoleY);
                if (y == elevator.CurrentFloor)
                    Console.Write("[O]");
                else
                    Console.Write("[ ]");
                
            }
            Console.SetCursorPosition(x, height + 1);
            Console.Write(' ');
            switch(elevator.State)
            {
                case ElevatorState.Idle:
                    Console.Write("I");
                    break;
                case ElevatorState.Moving:
                    Console.Write("M"); 
                    break;
                case ElevatorState.Loading:
                    Console.Write("L");
                    break;
            }

        }

        private static void DrawArrow(int x,int height, int y)
        {
            int consoleY = (height - 1) - y;
            Console.SetCursorPosition(x, consoleY);
            Console.Write("<");
            Console.SetCursorPosition(x + 1, consoleY);
            Console.Write("-");
            Console.SetCursorPosition(x + 2, consoleY);
            Console.Write("-");
        }

        public void Draw()
        {
            Console.CursorVisible = false;
            int xpos = 0;

            foreach(var car in ElevatorPositions.Values)
            {
                DrawElevator(xpos, Height, car);
                xpos += 4;
            }
        }

    }

    internal class VisualizerCursor
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}
