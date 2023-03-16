using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day15 : IDay
    {
        private string[] _input;

        private List<List<int>> _map = new();
        private List<(int, int)> _neighbors = new()
        {
            (0, 1),
            (0, -1),
            (1, 0),
            (-1, 0)
        };

        public Day15()
        {
            _input = File.ReadAllLines(@"Inputs/Day15Input.txt");
            Parse();
        }

        private void Parse()
        {
            foreach (var line in _input)
            {
                List<int> row = new();

                foreach (var c in line)
                {
                    row.Add(c - 48);
                }
                _map.Add(row);
            }
        }

        public void RunPart1()
        {
            Console.WriteLine($"Part 1: {FindLowerRiskPath(_map, (0, 0), (_map[0].Count - 1, _map.Count - 1))}");
        }

        public void RunPart2()
        {
            List<List<int>> mapCopy = new();

            for (int i = 0; i < 5; i++)
            {
                foreach (var line in _map)
                {
                    List<int> newLine = new();

                    for (int k = 0; k < 5; k++)
                    {
                        foreach (var value in line)
                        {
                            int newValue = value + i + k;

                            if (newValue > 9)
                            {
                                newValue %= 9;
                            }
                            newLine.Add(newValue);
                        }
                    }
                    mapCopy.Add(newLine);
                }
            }
            
            Console.WriteLine($"Part 2: {FindLowerRiskPath(mapCopy, (0, 0), (mapCopy[0].Count - 1, mapCopy.Count - 1))}");
        }

        private int FindLowerRiskPath(List<List<int>> map, (int, int) start, (int, int) end)
        {
            int width = map[0].Count;
            int height = map.Count;
            PriorityQueue<(int, int), int> queue = new();
            int[,] cost = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if ((x, y) != start)
                    {
                        cost[x, y] = int.MaxValue;
                    }
                    queue.Enqueue((x, y), cost[x, y]);
                }
            }
            cost[start.Item1, start.Item2] = 0;

            while (queue.Count > 0)
            {
                (int, int) current = queue.Dequeue();

                if (current == end)
                {
                    return cost[current.Item1, current.Item2] + map[current.Item2][current.Item1] - map[start.Item2][start.Item1];
                }

                foreach (var neighbor in _neighbors)
                {
                    int x = current.Item1 + neighbor.Item1;
                    int y = current.Item2 + neighbor.Item2;


                    if (x < 0 || x >= width || y < 0 || y >= height) continue;

                    int newCost = cost[current.Item1, current.Item2] + map[current.Item2][current.Item1];

                    if (newCost < cost[x, y])
                    {
                        cost[x, y] = newCost;
                        queue.Enqueue((x, y), newCost);
                    }
                }
            }
            return -1;
        }
    }
}