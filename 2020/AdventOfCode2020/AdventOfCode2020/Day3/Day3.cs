using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day3 : IDay
    {
        private string[] _input;

        private int _mapHeight;
        private int _mapWidth;

        public Day3()
        {
            _input = File.ReadAllLines(@"Inputs/Day3Input.txt");
            _mapHeight = _input.Length;
            _mapWidth = _input[0].Length;
        }

        public void RunPart1()
        {
            Console.WriteLine($"Part 1 : {GetSlopeEncounteredTrees(3,1)}");
        }


        public void RunPart2()
        {
            long res = GetSlopeEncounteredTrees(1, 1) * GetSlopeEncounteredTrees(3, 1) * GetSlopeEncounteredTrees(5, 1) * GetSlopeEncounteredTrees(7, 1) * GetSlopeEncounteredTrees(1, 2);

            Console.WriteLine($"Part 2 : {res}");
        }

        private long GetSlopeEncounteredTrees(int right, int down)
        {
            long trees = 0;
            int j = 0;

            for (int i = down; i < _mapHeight; i += down)
            {
                j += right;

                if (_input[i][j % _mapWidth] == '#')
                {
                    trees++;
                }
            }

            return trees;
        }
    }
}
