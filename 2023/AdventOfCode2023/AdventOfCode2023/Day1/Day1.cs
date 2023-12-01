using AdventOfCode2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
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
            int sum = 0;

            foreach (var line in _input)
            {
                int firstDigit = 0;
                int lastDigit = 0;

                for (int i = 0; i < line.Length; i++)
                {
                    if (char.IsDigit(line[i]))
                    {
                        if (firstDigit == 0)
                            firstDigit = line[i] - 48;
                        else
                            lastDigit = line[i] - 48;
                    }
                }

                if (lastDigit == 0)
                    lastDigit = firstDigit;
                sum += int.Parse(firstDigit.ToString() + lastDigit.ToString());

            }
            Console.WriteLine($"Part 1: {sum}");
        }

        public void RunPart2()
        {
            int sum = 0;

            var numberDictionary = new Dictionary<string, int>
            {
                { "One", 1 },
                { "Two", 2 },
                { "Three", 3 },
                { "Four", 4 },
                { "Five", 5 },
                { "Six", 6 },
                { "Seven", 7 },
                { "Eight", 8 },
                { "Nine", 9 }
            };

            foreach (var line in _input)
            {
                int firstDigit = 0;
                int lastDigit = 0;

                for (int i = 0; i < line.Length; i++)
                {
                    if (char.IsDigit(line[i]))
                    {
                        if (firstDigit == 0)
                            firstDigit = line[i] - 48;
                        else
                            lastDigit = line[i] - 48;
                        continue;
                    }

                    var strNumber = CheckDigitLetters(numberDictionary, line.Substring(i));

                    if (strNumber != null)
                    {
                        int digit = numberDictionary[strNumber];
                        if (firstDigit == 0)
                            firstDigit = digit;
                        else
                            lastDigit = digit;

                        i += strNumber.Length - 2;
                    }
                }
                if (lastDigit == 0)
                    lastDigit = firstDigit;
                sum += int.Parse(firstDigit.ToString() + lastDigit.ToString());
            }

            Console.WriteLine($"Part 2: {sum}");
        }

        private string? CheckDigitLetters(Dictionary<string, int> dictionary, string subString)
        {
            if (dictionary == null) return null;

            foreach (var key in  dictionary.Keys)
            {
                if (subString.StartsWith(key, StringComparison.OrdinalIgnoreCase))
                    return key;
            }

            return null;
        }

    }
}
