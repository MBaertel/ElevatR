using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator
{
    internal static class NameGenerator
    {
        private static Random random = new Random();
        private static List<string> names = new List<string>()
        {
            "Tom",
            "Bob",
            "Lisa",
            "Sarah",
            "Günther",
            "Herbert",
            "Rosa",
            "Laurah",
            "Fritz",
            "Marcel",
            "Eduard",
            "Alice",
            "Anna",
            "Brigitte",
            "Willi",
            "Christa"
        };

        public static string GetName()
        {
            var index = random.Next(0, names.Count -1);
            return names[index];
        }
    }
}
