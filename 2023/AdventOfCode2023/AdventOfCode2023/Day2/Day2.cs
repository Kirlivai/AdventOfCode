using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023
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
            Dictionary<string, int> maxValues = new Dictionary<string, int>()
            {
                {"red", 12},
                {"green",13 },
                {"blue", 14 }
            };

            int lineIndex = 1;
            int result = 0;

            foreach (string line in _input)
            {
                bool isValid = true;
                var subsets = ParseSubsest(line);

                foreach (var subset in subsets)
                {
                    var colors = subset.Split(',');

                    foreach (var color in colors)
                    {
                        var regex = new Regex(@"(\d+)\s+(\w+)");
                        var match = regex.Match(color);

                        if (match.Success)
                        {
                            int value = int.Parse(match.Groups[1].Value);
                            string colorName = match.Groups[2].Value;

                            if (value > maxValues[colorName])
                            {
                                isValid = false;
                            }
                        }
                    }

                }
                if (isValid)
                    result += lineIndex;
                lineIndex++;
            }

            Console.WriteLine($"Part 1 : {result}");
        }

        public void RunPart2()
        {
            int result = 0;

            foreach (string line in _input)
            {
                var subsets = ParseSubsest(line);

                Dictionary<string, int> maxValues = new Dictionary<string, int>()
                {
                    {"red", int.MinValue},
                    {"green", int.MinValue },
                    {"blue", int.MinValue }
                };

                foreach (var subset in subsets)
                {
                    var colors = subset.Split(',');

                    foreach (var color in colors)
                    {
                        var regex = new Regex(@"(\d+)\s+(\w+)");
                        var match = regex.Match(color);

                        if (match.Success)
                        {
                            int value = int.Parse(match.Groups[1].Value);
                            string colorName = match.Groups[2].Value;

                            if (maxValues[colorName] < value)
                            {
                                maxValues[colorName] = value;
                            }
                        }
                    }
                }

                int multiplyResult = 1;

                foreach (var value in maxValues.Values)
                {
                    multiplyResult *= value;
                }

                result += multiplyResult;
            }
            Console.WriteLine($"Part 2 : {result}");
        }

        private List<string> ParseSubsest(string line)
        {
            var sets = line.Substring(line.IndexOf(':') + 1);

            return sets.Split(';').ToList();
        }
    }
}
