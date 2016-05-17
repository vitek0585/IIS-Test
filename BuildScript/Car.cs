using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildScript
{
    public class Car
    {
        public string Model { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Description car = {Model} - {Name}";
        }
    }
}
