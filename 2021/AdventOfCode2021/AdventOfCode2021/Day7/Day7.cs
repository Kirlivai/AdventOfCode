using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day7 : IDay
    {
        private string[] _input;
        private List<int> _horizontalCrabsPosition = new();

        public Day7()
        {
            _input = File.ReadAllLines(@"Inputs/Day7Input.txt");
            Parse();
        }

        private void Parse()
        {
            foreach (var line in _input)
            {
                line.Split(",").ToList().ForEach(x => _horizontalCrabsPosition.Add(int.Parse(x)));
            }
        }

        public void RunPart1()
        {
            int minValue = _horizontalCrabsPosition.Min();
            int maxValue = _horizontalCrabsPosition.Max();
            int minCost = int.MaxValue;

            for (int i = minValue; i <= maxValue; i++)
            {
                int cost = 0;
                
                foreach (var crab in _horizontalCrabsPosition)
                {
                    cost += Math.Abs(i - crab);
                }

                if (cost < minCost)
                {
                    minCost = cost;
                }
            }

            Console.WriteLine($"Part 1: {minCost}");
        }

        public void RunPart2()
        {
            int minValue = _horizontalCrabsPosition.Min();
            int maxValue = _horizontalCrabsPosition.Max();
            int minCost = int.MaxValue;

            for (int i = minValue; i <= maxValue; i++)
            {
                int cost = 0;

                foreach (var crab in _horizontalCrabsPosition)
                {
                    int diff = Math.Abs(i - crab);

                    for (int j = 1; j <= diff; j++)
                    {
                        cost += j;
                    }
                }

                if (cost < minCost)
                {
                    minCost = cost;
                }
            }

            Console.WriteLine($"Part 1: {minCost}");
        }
    }
}
