using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day3 : IDay
    {
        private string[] _input;

        public Day3()
        {
            _input = File.ReadAllLines(@"Inputs/Day3Input.txt");
        }

        public void RunPart1()
        {
            string gammeRate = "";
            string epsilonRate = "";

            if (_input.Length <= 0)
            {
                throw new Exception("Invalid input");
            }

            List<int> binaryCounter = Enumerable.Repeat(0, _input[0].Length).ToList();

            foreach (var line in _input)
            {
                if (line.Length != _input[0].Length)
                {
                    throw new Exception("Invalid input");
                }

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '1')
                    {
                        binaryCounter[i]++;
                    }
                    else
                    {
                        binaryCounter[i]--;
                    }
                }
            }

            foreach (var value in binaryCounter)
            {
                if (value > 0)
                {
                    gammeRate += "1";
                    epsilonRate += "0";
                }
                else
                {
                    gammeRate += "0";
                    epsilonRate += "1";
                }
            }

            int result = BinaryToInt(gammeRate) * BinaryToInt(epsilonRate);

            Console.WriteLine($"Part 1: {result}");
        }

        public void RunPart2()
        {
            List<string> oxygenValues = new List<string>(_input);
            List<string> co2Values = new List<string>(_input);

            if (_input.Length <= 0)
            {
                throw new Exception("Invalid input");
            }

            for (int i = 0; i < _input[i].Length; i++)
            {
                ComputeBitCriteria(oxygenValues, i, true);
                ComputeBitCriteria(co2Values, i, false);
            }

            if (oxygenValues.Count != 1 || co2Values.Count != 1)
            {
                throw new Exception("Invalid input");
            }

            int result = BinaryToInt(oxygenValues[0]) * BinaryToInt(co2Values[0]);

            Console.WriteLine($"Part 2: {result}");
        }

        private void ComputeBitCriteria(List<string> values, int index, bool mostCommon)
        {
            int counter = 0;

            if (values.Count <= 1) return;

            foreach (var value in values)
            {
                if (value[index] == '1')
                {
                    counter++;
                }
                else
                {
                    counter--;
                }
            }

            if (counter >= 0)
            {
                if (mostCommon)
                    values.RemoveAll(o => o[index] == '0');
                else
                    values.RemoveAll(o => o[index] == '1');
            }
            else
            {
                if (mostCommon)
                    values.RemoveAll(o => o[index] == '1');
                else
                    values.RemoveAll(o => o[index] == '0');
            }
        }


        private int BinaryToInt(string binary)
        {
            int result = 0;

            for (int i = 0; i < binary.Length; i++)
            {
                if (binary[i] == '1')
                {
                    result += (int)Math.Pow(2, binary.Length - i - 1);
                }
            }

            return result;
        }
    }
}
