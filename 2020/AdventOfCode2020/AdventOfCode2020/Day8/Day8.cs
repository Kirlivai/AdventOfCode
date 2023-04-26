using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day8 : IDay
    {
        private string[] _input;

        public Day8()
        {
            _input = File.ReadAllLines(@"Inputs/Day8Input.txt");
        }

        public void RunPart1()
        {
            int accumulator = 0;
            var visited = new HashSet<int>();

            for (int i = 0; i < _input.Length; i++)
            {
                if (visited.Contains(i))
                {
                    break;
                }

                visited.Add(i);

                var split = _input[i].Split(' ');
                var instruction = split[0];
                var value = int.Parse(split[1]);
                switch (instruction)
                {
                    case "acc":
                        accumulator += value;
                        break;
                    case "jmp":
                        i += value - 1;
                        break;
                }
            }

            Console.WriteLine($"Part 1: {accumulator}");
        }

        public void RunPart2()
        {
            for (int i = 0; i < _input.Length; i++)
            {
                var split = _input[i].Split(' ');
                var instruction = split[0];

                if (instruction == "jmp" || instruction == "nop")
                {
                    var res = CheckNewInstructionAccumulator(i);

                    if (res.Item1 is true)
                    {
                        Console.WriteLine($"Part 2: {res.Item2}");
                        return;
                    }
                }
            }
        }

        private (bool, int) CheckNewInstructionAccumulator(int indexChange)
        {
            int accumulator = 0;
            var visited = new HashSet<int>();

            for (int i = 0; i < _input.Length; i++)
            {
                if (visited.Contains(i))
                {
                    return (false, 0);
                }

                visited.Add(i);

                var split = _input[i].Split(' ');
                var instruction = split[0];

                if (i == indexChange)
                {
                    if (instruction == "jmp")
                        continue;
                    else
                        instruction = "jmp";
                }

                var value = int.Parse(split[1]);
                switch (instruction)
                {
                    case "acc":
                        accumulator += value;
                        break;
                    case "jmp":
                        i += value - 1;
                        break;
                }
            }

            return (true, accumulator);
        }
    }
}
