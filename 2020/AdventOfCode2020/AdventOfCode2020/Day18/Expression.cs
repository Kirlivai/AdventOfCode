using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.D18
{
    public class Expression
    {
        public string Value { get; set; } = "";
        public int Index { get; set; }
    }

    public enum Operation
    {
        Add,
        Multiply
    }
}
