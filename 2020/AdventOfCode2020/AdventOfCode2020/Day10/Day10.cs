using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day10 : IDay
    {
        private string[] _input;

        private List<int> _values = new List<int>();

        public Day10()
        {
            _input = File.ReadAllLines(@"Inputs/Day10Input.txt");

            foreach (var line in _input)
            {
                _values.Add(int.Parse(line));
            }
        }

        public void RunPart1()
        {
            int nb1Diff = 1;
            int nb3Diff = 0;

            var values = _values.OrderBy(x => x).ToList();

            for (int i = 0; i < values.Count - 1; i++)
            {
                int diff = values[i + 1] - values[i];
                if (diff == 1)
                    nb1Diff++;
                else if (diff == 3)
                    nb3Diff++;
            }

            nb3Diff++;

            Console.WriteLine($"Part 1: {nb1Diff * nb3Diff}");

        }

        public void RunPart2()
        {
            var values = _values.OrderBy(x => x).ToList();
            values.Insert(0, 0);
            Dictionary<int,long> pathCount = new Dictionary<int, long>();
            pathCount.Add(0, 1);

            for (int i = 0; i < values.Count - 1; i++)
            {
                var possiblesValues = values.Where(x => x > values[i] && x <= values[i] + 3).ToList();

                foreach (var possibleValue in possiblesValues)
                {
                    if (pathCount.ContainsKey(possibleValue))
                        pathCount[possibleValue] += pathCount[values[i]];
                    else
                        pathCount.Add(possibleValue, pathCount[values[i]]);
                }
            }

            Console.WriteLine($"Part 2: {pathCount.Last().Value}");
        }
    }
}
