using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day1 : IDay
    {
        private string[] _input;

        public Day1()
        {
            _input = File.ReadAllLines(@"Inputs/Day1Input.txt");
        }

        public void RunPart1()
        {
            for (int i = 0; i < _input.Length; i++)
            {
                for (int j = i + 1; j < _input.Length; j++)
                {
                    if (int.Parse(_input[i]) + int.Parse(_input[j]) == 2020)
                    {
                        Console.WriteLine($"Part 1 : {int.Parse(_input[i]) * int.Parse(_input[j])}");
                        return;
                    }
                }
            }
        }

        public void RunPart2()
        {
            for (int i = 0; i < _input.Length; i++)
            {
                for (int j = i + 1; j < _input.Length; j++)
                {
                    for (int k = j + 1; k < _input.Length; k++)
                    {
                        if (int.Parse(_input[i]) + int.Parse(_input[j]) + int.Parse(_input[k]) == 2020)
                        {
                            Console.WriteLine($"Part 2 : {int.Parse(_input[i]) * int.Parse(_input[j]) * int.Parse(_input[k])}");
                            return;
                        }
                    }
                }
            }
        }
    }
}
