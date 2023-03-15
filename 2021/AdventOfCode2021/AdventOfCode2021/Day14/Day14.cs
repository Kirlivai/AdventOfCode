using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day14 : IDay
    {
        private string[] _input;

        private string _template = "";
        private List<(char, char, char)> _rules = new();

        public Day14()
        {
            _input = File.ReadAllLines(@"Inputs/Day14Input.txt");
            Parse();
        }

        private void Parse()
        {
            _template = _input[0];

            for (int i = 2; i < _input.Length; i++)
            {
                var splited = _input[i].Split(" -> ");
                _rules.Add((splited[0][0], splited[0][1], splited[1][0]));
            }
        }


        public void RunPart1()
        {
            Console.WriteLine($"Part 1: {Run(10)}");
        }

        public void RunPart2()
        {
            Console.WriteLine($"Part 2: {Run(40)}");
        }

        private long Run(int steps)
        {
            Dictionary<(char, char), long> pairsCounter = new();

            for (int i = 0; i < _template.Length - 1; i++)
            {
                var pair = (_template[i], _template[i + 1]);
                if (pairsCounter.ContainsKey(pair))
                    pairsCounter[pair]++;
                else
                    pairsCounter.Add(pair, 1);
            }

            for (int i = 0; i < steps; i++)
            {
                ApplyRules(pairsCounter);
            }

            return CountCharacters(pairsCounter);
        }

        private long CountCharacters(Dictionary<(char, char), long> pairsCounter)
        {
            Dictionary<char, long> charCounter = new();

            foreach (var pair in pairsCounter)
            {
                if (charCounter.ContainsKey(pair.Key.Item1))
                    charCounter[pair.Key.Item1] += pair.Value;
                else
                    charCounter.Add(pair.Key.Item1, pair.Value);

                if (charCounter.ContainsKey(pair.Key.Item2))
                    charCounter[pair.Key.Item2] += pair.Value;
                else
                    charCounter.Add(pair.Key.Item2, pair.Value);
            }

            long min = long.MaxValue;
            long max = long.MinValue;

            foreach (var pair in charCounter)
            {
                if (pair.Value / 2 < min)
                    min = (long)Math.Ceiling((double)pair.Value / 2);
                   
                if (pair.Value / 2 > max)
                    max = (long)Math.Ceiling((double)pair.Value / 2);
            }

            return max - min;
        }

        private void ApplyRules(Dictionary<(char, char), long> pairsCounter)
        {
            var newPairsCounter = new Dictionary<(char, char), long>();

            foreach (var pair in pairsCounter)
            {
                if (pair.Value == 0)
                    continue;

                var key = pair.Key;
                var rule = _rules.FirstOrDefault(r => r.Item1 == key.Item1 && r.Item2 == key.Item2);

                var newPair1 = (key.Item1, rule.Item3);
                var newPair2 = (rule.Item3, key.Item2);

                if (newPairsCounter.ContainsKey(newPair1))
                    newPairsCounter[newPair1] += pair.Value;
                else
                    newPairsCounter.Add(newPair1, pair.Value);

                if (newPairsCounter.ContainsKey(newPair2))
                    newPairsCounter[newPair2] += pair.Value;
                else
                    newPairsCounter.Add(newPair2, pair.Value);

                pairsCounter[pair.Key] -= pair.Value;
            }

            foreach (var pair in newPairsCounter)
            {
                if (pairsCounter.ContainsKey(pair.Key))
                    pairsCounter[pair.Key] += pair.Value;
                else
                    pairsCounter.Add(pair.Key, pair.Value);
            }
        }
    }
}
