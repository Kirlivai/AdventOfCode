using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.D12
{
    public class CaveNode
    {
        public string Name { get; set; } = "";

        public List<CaveNode> Neighbors { get; set; } = new();
    }
}
