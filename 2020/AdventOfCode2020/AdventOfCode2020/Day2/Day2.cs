using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
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
            int res = 0;

            foreach (var line in _input)
            {
                var split = line.Split(' ');
                var minMax = split[0].Split('-');
                var min = int.Parse(minMax[0]);
                var max = int.Parse(minMax[1]);
                var letter = split[1][0];
                var password = split[2];
                var count = password.Count(x => x == letter);

                if (count >= min && count <= max)
                {
                    res++;
                }
            }

            Console.WriteLine($"Part 1 : {res}");
        }

        public void RunPart2()
        {
            int res = 0;

            foreach (var line in _input)
            {
                var split = line.Split(' ');
                var minMax = split[0].Split('-');
                var index1 = int.Parse(minMax[0]);
                var index2 = int.Parse(minMax[1]);
                var letter = split[1][0];
                var password = split[2];
                

                if (password[index1 - 1] == letter ^ password[index2 - 1] == letter)
                {
                    res++;
                }
            }

            Console.WriteLine($"Part 2 : {res}");
        }
    }
}
