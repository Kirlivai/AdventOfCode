using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day11 : IDay
    {
        private string[] _input;
        
        public Day11()
        {
            _input = File.ReadAllLines(@"Inputs/Day11Input.txt");
        }

        private List<List<int>> Parse()
        {
            List<List<int>> map = new();
            
            foreach (var line in _input)
            {
                List<int> mapLine = new();

                foreach (var c in line)
                {
                    mapLine.Add(c - 48);
                }

                map.Add(mapLine);
            }

            return map;
        }

        public void RunPart1()
        {
            var map = Parse();
            int steps = 100;
            int res = 0;

            for (int i = 0; i < steps; i++)
            {
                res += Navigate(map);
            }

            Console.WriteLine($"Part 1: {res}");
        }

        public void RunPart2()
        {
            var map = Parse();
            int maxSteps = int.MaxValue;
            int nbMapElements = map.Count * map[0].Count;

            for (int i = 0; i < maxSteps; i++)
            {
                int res = Navigate(map);
                
                if (res == nbMapElements)
                {
                    Console.WriteLine($"Part 2: {i + 1}");
                    return;
                }
            }
            Console.WriteLine($"Part 2: {0}");
        }

        private int Navigate(List<List<int>> map)
        {
            HashSet<(int, int)> flashedOctopus = new();

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (flashedOctopus.Contains((i, j))) continue;

                    map[i][j] += 1;

                    if (map[i][j] > 9)
                    {
                        flashedOctopus.Add((i, j));
                        FlashOctopus(map, flashedOctopus, i, j);
                    }
                }
            }

            foreach (var (i, j) in flashedOctopus)
            {
                map[i][j] = 0;
            }

            return flashedOctopus.Count;
        }

        private void FlashOctopus(List<List<int>> map, HashSet<(int, int)> flashedOctopus, int i, int j)
        {
            for (int k = 0; k < 9; k++)
            {
                int x = i + k % 3 - 1;
                int y = j + k / 3 - 1;

                if (x >= 0 && x < map.Count && y >= 0 && y < map[x].Count && !flashedOctopus.Contains((x, y)))
                {
                    map[x][y] += 1;

                    if (map[x][y] > 9)
                    {
                        flashedOctopus.Add((x, y));
                        FlashOctopus(map, flashedOctopus, x, y);
                    }
                }
            }
        }
    }
}
