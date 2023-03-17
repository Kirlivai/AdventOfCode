using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.D16
{
    public class Packet
    {
        public long Version { get; set; }
        public long Id { get; set; }
        public long LiteralValue { get; set; }

        public List<Packet> Subs { get; set; } = new();
    }
}
