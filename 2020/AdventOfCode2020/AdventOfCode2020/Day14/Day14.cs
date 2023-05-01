using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day14 : IDay
    {
        private string[] _input;

        public Day14()
        {
            _input = File.ReadAllLines(@"Inputs/Day14Input.txt");
        }

        public void RunPart1()
        {
            Dictionary<long, long> values = new();
            string currentMask = "";

            foreach (var line in _input)
            {
                if (line.StartsWith("mask"))
                {
                    currentMask = line.Split(" = ")[1];
                }
                else
                {
                    var split = line.Split(" = ");
                    var address = int.Parse(split[0].Substring(4, split[0].Length - 5));
                    var value = int.Parse(split[1]);
                    var binary = Convert.ToString(value, 2).PadLeft(36, '0').ToCharArray();

                    for (int i = 0; i < currentMask.Length; i++)
                    {
                        if (currentMask[i] != 'X')
                        {
                            binary[i] = currentMask[i];
                        }
                    }

                    values[address] = Convert.ToInt64(new string(binary), 2);

                }
            }

            Console.WriteLine($"Part 1: {values.Sum(a => a.Value)}");
        }

        public void RunPart2()
        {
            Dictionary<long, long> values = new();
            string currentMask = "";

            foreach (var line in _input)
            {
                if (line.StartsWith("mask"))
                {
                    currentMask = line.Split(" = ")[1];
                }
                else
                {
                    var split = line.Split(" = ");
                    var address = int.Parse(split[0].Substring(4, split[0].Length - 5));

                    var binary = Convert.ToString(address, 2).PadLeft(36, '0').ToCharArray();
                    var value = int.Parse(split[1]);

                    List<int> floatings = new();

                    for (int i = 0; i < currentMask.Length; i++)
                    {
                        if (currentMask[i] == '1')
                        {
                            binary[i] = currentMask[i];
                        }
                        else if (currentMask[i] == 'X')
                        {
                            floatings.Add(i);
                        }
                    }

                    int possibilites = (int)Math.Pow(2, floatings.Count);

                    for (int i = 0; i < possibilites; i++)
                    {
                        var binaryCopy = binary.ToArray();

                        for (int j = 0; j < floatings.Count; j++)
                        {
                            int bit = (i >> j) & 1;
                            binaryCopy[floatings[j]] = bit == 1 ? '1' : '0';
                            long newAdress = Convert.ToInt64(new string(binaryCopy), 2);

                            values[newAdress] = value;
                        }
                    }
                }
            }

            Console.WriteLine($"Part 2: {values.Sum(a => a.Value)}");
        }
    }
}
