using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.D7
{
    public class Bag
    {
        public string Name { get; set; } = "";

        public Dictionary<string, int> ContainedBags { get; set; } = new();
    }
}
