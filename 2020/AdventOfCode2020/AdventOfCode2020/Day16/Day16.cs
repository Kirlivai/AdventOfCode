using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day16 : IDay
    {
        private string[] _input;

        public Day16()
        {
            _input = File.ReadAllLines(@"Inputs/Day16Input.txt");
        }

        public void RunPart1()
        {
            List<(int, int)> intervals = new List<(int, int)>();
            int result = 0;

            int i = 0;

            for (; i < _input.Length; i++)
            {
                if (string.IsNullOrEmpty(_input[i]))
                {
                    i++;
                    break;
                }

                var interval = _input[i].Split(": ")[1].Split(" or ");

                var interval1 = interval[0].Split("-");
                var interval2 = interval[1].Split("-");

                intervals.Add((int.Parse(interval1[0]), int.Parse(interval1[1])));
                intervals.Add((int.Parse(interval2[0]), int.Parse(interval2[1])));
            }

            i += 4;

            for (; i < _input.Length; i++)
            {
                var numbers = _input[i].Split(",").Select(int.Parse).ToArray();
                foreach (var number in numbers)
                {
                    if (!intervals.Any(x => x.Item1 <= number && x.Item2 >= number))
                    {
                        result += number;
                    }
                }
            }

            Console.WriteLine($"Part 1: {result}");
        }

        public void RunPart2()
        {
            List<((int, int), (int, int))> intervals = new();

            int i = 0;
            // Parse intervals
            for (; i < _input.Length; i++)
            {
                if (string.IsNullOrEmpty(_input[i]))
                {
                    break;
                }

                var interval = _input[i].Split(": ")[1].Split(" or ");

                var interval1 = interval[0].Split("-");
                var interval2 = interval[1].Split("-");

                var values1 = (int.Parse(interval1[0]), int.Parse(interval1[1]));
                var values2 = (int.Parse(interval2[0]), int.Parse(interval2[1]));

                intervals.Add((values1, values2));
            }

            i += 2;
            // Parse your tickets
            var yourTicketsSplit = _input[i].Split(",").Select(int.Parse).ToList();

            i += 3;
            // Parse nearby tickets rows
            List<List<int>> rows = new();

            for (; i < _input.Length; i++)
            {
                var numbers = _input[i].Split(",").Select(int.Parse).ToList();

                for (int j = 0; j < numbers.Count; j++)
                {
                    if (rows.Count <= j)
                        rows.Add(new List<int>());

                    if (!intervals.Any(x => (x.Item1.Item1 <= numbers[j] && x.Item1.Item2 >= numbers[j]) || (x.Item2.Item1 <= numbers[j] && x.Item2.Item2 >= numbers[j])))
                    {
                        continue;
                    }
                    rows[j].Add(numbers[j]);
                }
            }

            // Find all possible intervals for each row
            Dictionary<int, List<int>> correspondingIntervalsIndex = new();

            for (int k = 0; k < rows.Count; k++)
            {
                for (int j = 0; j < intervals.Count; j++)
                {
                    if (rows[k].All(x => (intervals[j].Item1.Item1 <= x && intervals[j].Item1.Item2 >= x) || (intervals[j].Item2.Item1 <= x && intervals[j].Item2.Item2 >= x)))
                    {
                        if (!correspondingIntervalsIndex.ContainsKey(j))
                            correspondingIntervalsIndex.Add(j, new List<int>() { k });
                        else
                            correspondingIntervalsIndex[j].Add(k);
                    }
                }
            }

            long res = 1;

            // Find the only possible interval for each row
            while (!correspondingIntervalsIndex.All(a => a.Value.Count == 0))
            {
                var only = correspondingIntervalsIndex.First(a => a.Value.Count == 1);
                int onlyValue = only.Value.First();

                if (only.Key >= 0 && only.Key <= 5)
                {
                    res *= yourTicketsSplit[onlyValue];
                }

                foreach (var c in correspondingIntervalsIndex)
                {
                    if (c.Value.Contains(onlyValue))
                    {        
                        correspondingIntervalsIndex[c.Key].Remove(onlyValue);
                    }
                }
            }

            Console.WriteLine($"Part 2: {res}");
        }
    }
}
