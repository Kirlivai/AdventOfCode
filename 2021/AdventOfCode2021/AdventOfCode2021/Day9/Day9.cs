using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day9 : IDay
    {
        private string[] _input;

        private List<List<int>> _map = new();

        public Day9()
        {
            _input = File.ReadAllLines(@"Inputs/Day9Input.txt");
            Parse();
        }

        private void Parse()
        {
            foreach (var line in _input)
            {
                List<int> values = new();

                foreach (var c in line)
                {
                    values.Add(c - 48);
                }

                _map.Add(values);
            }
        }


        public void RunPart1()
        {
            int res = 0;

            for (int i = 0; i < _map.Count; i++)
            {
                for (int j = 0; j < _map[i].Count; j++)
                {
                    if (IsLowPoint(i,j))
                        res += _map[i][j] + 1;
                }
            }

            Console.WriteLine($"Part 1: {res}");
        }

        public void RunPart2()
        {
            int[] sizes = { 0, 0, 0 };

            for (int i = 0; i < _map.Count; i++)
            {
                for (int j = 0; j < _map[i].Count; j++)
                {
                    if (!IsLowPoint(i, j))
                        continue;

                    int size = GetBasinSize(new HashSet<(int, int)>(), i, j);

                    int min = sizes.Min();

                    if (size < min) continue;

                    for (int k = 0; k < sizes.Length; k++)
                    {
                        if (sizes[k] == min)
                        {
                            sizes[k] = size;
                            break;
                        }
                    }
                }
            }

            int res = 1;

            foreach (var size in sizes)
            {
                res *= size;
            }

            Console.WriteLine($"Part 2: {res}");
        }

        private int GetBasinSize(HashSet<(int, int)> visitedNodes, int i, int j)
        {
            if (visitedNodes.Contains((i, j)))
                return 0;

            int size = 1;

            visitedNodes.Add((i, j));
            
            if (j != 0 && _map[i][j - 1] != 9 && _map[i][j] < _map[i][j - 1])
            {
                size += GetBasinSize(visitedNodes, i, j - 1);
            }
            if (j != _map[i].Count - 1 && _map[i][j + 1] != 9 && _map[i][j] < _map[i][j + 1])
            {
                size += GetBasinSize(visitedNodes, i, j + 1);
            }
            if (i != 0 && _map[i - 1][j] != 9 && _map[i][j] < _map[i - 1][j])
            {
                size += GetBasinSize(visitedNodes, i - 1, j);
            }
            if (i != _map.Count - 1 && _map[i + 1][j] != 9 && _map[i][j] < _map[i + 1][j])
            {
                size += GetBasinSize(visitedNodes, i + 1, j);
            }

            return size;

        }

        private bool IsLowPoint(int i, int j)
        {
            if (j != 0 && _map[i][j] >= _map[i][j - 1])
            {
                return false;
            }
            if (j != _map[i].Count - 1 && _map[i][j] >= _map[i][j + 1])
            {
                return false;
            }
            if (i != 0 && _map[i][j] >= _map[i - 1][j])
            {
                return false;
            }
            if (i != _map.Count - 1 && _map[i][j] >= _map[i + 1][j])
            {
                return false;
            }

            return true;
        }
    }
}
