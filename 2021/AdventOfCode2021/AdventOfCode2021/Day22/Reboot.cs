using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.D22
{
    public class Reboot
    {
        public bool IsOn { get; set; }
        public Interval Interval { get; set; }

        public Reboot()
        {
            Interval = new Interval();
        }
    }

    public class Interval
    {
        public (int, int) X { get; set; }
        public (int, int) Y { get; set; }
        public (int, int) Z { get; set; }
    }
}
