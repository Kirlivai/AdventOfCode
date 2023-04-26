using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day9 : IDay
    {
        private string[] _input;

        private List<long> _values = new List<long>();

        public Day9()
        {
            _input = File.ReadAllLines(@"Inputs/Day9Input.txt");

            foreach (var line in _input)
            {
                _values.Add(long.Parse(line));
            }
        }

        public void RunPart1()
        {
            Console.WriteLine($"Part 1: {GetFirstWrongNumber(25)}");
        }

        public void RunPart2()
        {
            long invalidNumber = GetFirstWrongNumber(25);

            for (int i = 0; i < _values.Count; i++)
            {
                long sum = _values[i];
                var numbers = new List<long>() { _values[i]};

                for (int j = i + 1; j < _values.Count; j++)
                {
                    sum += _values[j];
                    numbers.Add(_values[j]);
                    if (sum == invalidNumber)
                    {
                        Console.WriteLine($"Part 2: {numbers.Min() + numbers.Max()}");
                        return;
                    }
                    else if (sum > invalidNumber)
                        break;
                }
            }
        }

        private long GetFirstWrongNumber(int preambleLenght)
        {
            for (int i = preambleLenght; i < _values.Count; i++)
            {
                var sums = new HashSet<long>();

                for (int j = i - preambleLenght; j < i; j++)
                {
                    for (int k = j + 1; k < i; k++)
                    {
                        sums.Add(_values[j] + _values[k]);
                    }
                }

                if (!sums.Contains(_values[i]))
                {
                    return _values[i];
                }
            }

            return -1;
        }
    }
}
