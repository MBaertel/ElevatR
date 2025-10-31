using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator
{
    public class Person
    {
        public Guid Id = Guid.NewGuid();

        public string Name { get; set; }

        public int Target;

        public int Current;

        public Person(int start,int target, string name = null)
        {
            this.Current = start;
            this.Target = target;
            Name = name ?? NameGenerator.GetName();
        }

        public static Person GenerateRandom(int min,int max)
        {
            var random = new Random();
            var current = random.Next(min,max);
            var target = random.Next(min,max);

            return new Person(min,max);
        }
    }
}
