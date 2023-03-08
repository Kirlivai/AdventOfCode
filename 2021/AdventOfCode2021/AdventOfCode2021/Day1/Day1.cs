using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day1 : IDay
    {
        private string[] _input;

        private List<int> _depths = new List<int>();

        public Day1()
        {
            _input = File.ReadAllLines(@"Inputs/Day1Input.txt");

            foreach (var line in _input)
            {
                _depths.Add(int.Parse(line));
            }
        }
            
        
        public void RunPart1()
        {
            int increments = 0;

            for (int i = 1; i < _depths.Count; i++)
            {
                if (_depths[i] > _depths[i - 1])
                {
                    increments++;
                }
            }

            Console.WriteLine($"Part 1: {increments}");
        }

        public void RunPart2()
        {
            int increments = 0;
            int previousIncrement = int.MaxValue;

            for (int i = 2; i < _depths.Count; i++)
            {
                int sum = _depths[i] + _depths[i - 1] + _depths[i - 2];

                if (sum > previousIncrement)
                {
                    increments++;
                }

                previousIncrement = sum;
            }

            Console.WriteLine($"Part 2: {increments}");
        }
    }
}
