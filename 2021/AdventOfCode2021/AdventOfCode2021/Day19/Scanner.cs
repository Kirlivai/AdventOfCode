using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.D19
{
    public class Scanner
    {
        public (int, int, int) Position { get; set; }

        public List<(int, int, int)> BeaconsPositions { get; set; } = new List<(int, int, int)>();
    }

}
