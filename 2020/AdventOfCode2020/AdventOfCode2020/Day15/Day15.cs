using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day15 : IDay
    {
        private string[] _input;

        public Day15()
        {
            _input = File.ReadAllLines(@"Inputs/Day15Input.txt");
        }

        public void RunPart1()
        {
            Console.WriteLine($"Part 1: {GetLastGameNumber(2020)}");
        }

        public void RunPart2()
        {
            Console.WriteLine($"Part 2: {GetLastGameNumber(30000000)}");
        }

        private int GetLastGameNumber(int nbRounds)
        {
            Dictionary<int, int> valueSpokenIndex = new Dictionary<int, int>();

            var startingNumbers = _input[0].Split(",").Select(int.Parse).ToArray();

            for (int i = 0; i < startingNumbers.Length - 1; i++)
            {
                valueSpokenIndex[startingNumbers[i]] = i + 1;
            }

            int lastNumber = startingNumbers.Last();

            for (int i = startingNumbers.Length + 1; i <= nbRounds; i++)
            {
                int number = lastNumber;
                if (valueSpokenIndex.ContainsKey(lastNumber))
                {
                    lastNumber = i - 1 - valueSpokenIndex[lastNumber];
                }
                else
                {
                    lastNumber = 0;
                }

                valueSpokenIndex[number] = i - 1;
            }

            return lastNumber;   
        }
    }
}
