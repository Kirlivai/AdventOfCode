using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
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
            var fishs = Parse();

            Console.WriteLine($"Part 1: {SimulateFishesLive(fishs, 80)}");
        }


        public void RunPart2()
        {
            var fishs = Parse();

            Console.WriteLine($"Part 2: {SimulateFishesLive(fishs, 256)}");
        }

        private List<int> Parse()
        {
            List<int> fishs = new();

            foreach (var line in _input)
            {
                line.Split(",").ToList().ForEach(x => fishs.Add(int.Parse(x)));
            }

            return fishs;
        }


        private long SimulateFishesLive(List<int> fishs, int days)
        {
            Dictionary<int, long> newFishsCount = new();
            Dictionary<int, long> oldFishCount = new();

            for (int i = 0; i < fishs.Count; i++)
            {
                if (oldFishCount.ContainsKey(fishs[i] % 7))
                {
                    oldFishCount[fishs[i] % 7]++;
                }
                else
                {
                    oldFishCount.Add(fishs[i] % 7, 1);
                }
            }

            for (int i = 0; i < days; i++)
            {

                if (newFishsCount.ContainsKey(i % 9))
                {
                    if (oldFishCount.ContainsKey(i % 7))
                    {
                        oldFishCount[i % 7] += newFishsCount[i % 9];
                    }
                    else
                    {
                        oldFishCount.Add(i % 7, newFishsCount[i % 9]);
                    }

                    newFishsCount[i % 9] = 0;
                }

                if (oldFishCount.ContainsKey(i % 7))
                {
                    if (newFishsCount.ContainsKey(i % 9))
                    {
                        newFishsCount[i % 9] += oldFishCount[i % 7];
                    }
                    else
                    {
                        newFishsCount.Add(i % 9, oldFishCount[i % 7]);
                    }
                }
            }

            return newFishsCount.Values.Sum() + oldFishCount.Values.Sum();
        }
    }
}
