using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day6 : IDay
    {
        private string[] _input;

        public Day6()
        {
            _input = File.ReadAllLines(@"Inputs/Day6Input.txt");
        }

        public void RunPart1()
        {
            int total = 0;

            for (int i = 0; i < _input.Length; i++)
            {
                Dictionary<char, int> answers = new Dictionary<char, int>();

                for (int j = i; i < _input.Length && !string.IsNullOrWhiteSpace(_input[j]); j++, i++)
                {
                    string line = _input[j];

                    foreach (char c in line)
                    {
                        if (answers.ContainsKey(c))
                        {
                            answers[c]++;
                        }
                        else
                        {
                            answers.Add(c, 1);
                        }
                    }
                }

                total += answers.Count;
            }

            Console.WriteLine($"Part 1: {total}");
        }

        public void RunPart2()
        {
            int total = 0;

            for (int i = 0; i < _input.Length; i++)
            {
                Dictionary<char, int> answers = new Dictionary<char, int>();
                int nbPeople = 0;

                for (int j = i; i < _input.Length && !string.IsNullOrWhiteSpace(_input[j]); j++, i++, nbPeople++)
                {
                    string line = _input[j];

                    foreach (char c in line)
                    {
                        if (answers.ContainsKey(c))
                        {
                            answers[c]++;
                        }
                        else
                        {
                            answers.Add(c, 1);
                        }
                    }
                }

                total += answers.Count(a => a.Value == nbPeople);
            }

            Console.WriteLine($"Part 2: {total}");
        }
    }
}
