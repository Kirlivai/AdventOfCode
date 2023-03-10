using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.D4
{
    public class BingoGrid
    {
        public List<List<int>> Lines = new List<List<int>>();

        public HashSet<(int, int)> MarkedPositions = new HashSet<(int, int)>();

        public bool IsWin = false;
    }
}
