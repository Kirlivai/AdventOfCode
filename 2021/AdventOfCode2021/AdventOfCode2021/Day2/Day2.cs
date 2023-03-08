using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day2 : IDay
    {
        private string[] _input;

        public Day2()
        {
            _input = File.ReadAllLines(@"Inputs/Day2Input.txt");
        }

        public void RunPart1()
        {
            int horizontal = 0;
            int depth = 0;

            foreach (var line in _input)
            {
                var splited = line.Split(' ');

                if (splited.Length != 2)
                {
                    throw new Exception("Invalid input");
                }

                int value = int.Parse(splited[1]);

                switch (splited[0])
                {
                    case "forward":
                        horizontal += value;
                        break;
                    case "down":
                        depth += value;
                        break;
                    case "up":
                        depth -= value;
                        break;
                }
            }

            Console.WriteLine($"Part 1: {horizontal * depth}");
        }
        
        public void RunPart2()
        {
            int horizontal = 0;
            int depth = 0;
            int aim = 0;

            foreach (var line in _input)
            {
                var splited = line.Split(' ');

                if (splited.Length != 2)
                {
                    throw new Exception("Invalid input");
                }

                int value = int.Parse(splited[1]);

                switch (splited[0])
                {
                    case "forward":
                        horizontal += value;
                        depth += aim * value;
                        break;
                    case "down":
                        aim += value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                }
            }

            Console.WriteLine($"Part 1: {horizontal * depth}");
        }
    }
}
