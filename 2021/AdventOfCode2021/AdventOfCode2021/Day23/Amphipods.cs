using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.D23
{
    public enum AmphipodsType
    {
        Amber,
        Bronze,
        Copper,
        Desert
    }

    public class Amphipods
    {
        public List<((int, int), AmphipodsType)> Positions { get; set; } = new();

        public int Energy { get; set; }

        public new string ToString()
        {
            var sb = new StringBuilder();
            foreach (var pos in Positions)
            {
                sb.Append(pos.Item1.ToString());
                sb.Append(pos.Item2.ToString());
            }

            return sb.ToString();
        }
    }
}
