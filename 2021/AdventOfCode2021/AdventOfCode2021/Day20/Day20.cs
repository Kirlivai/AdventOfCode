using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day20 : IDay
    {
        private string[] _input;

        private string _enhancementKey = "";

        private List<List<bool>> _map = new();

        public Day20()
        {
            _input = File.ReadAllLines(@"Inputs/Day20Input.txt");
            Parse();
        }

        private void Parse()
        {
            _enhancementKey = _input[0];

            for (int i = 2; i < _input.Length; i++)
            {
                var row = new List<bool>();
                for (int j = 0; j < _input[i].Length; j++)
                {
                    row.Add(_input[i][j] == '#');
                }
                _map.Add(row);
            }
        }

        public void RunPart1()
        {
            var mapCopy = new List<List<bool>>(_map);

            mapCopy = Enhance(mapCopy, 2);

            Console.WriteLine($"Part 1: {CountPixels(mapCopy)}");
        }

        public void RunPart2()
        {
            var mapCopy = new List<List<bool>>(_map);

            mapCopy = Enhance(mapCopy, 50);

            Console.WriteLine($"Part 2: {CountPixels(mapCopy)}");

        }

        private List<List<bool>> Enhance(List<List<bool>> map, int numberTimes)
        {
            bool isVoidEmpty = _enhancementKey[0] == '#';

            for (int i = 0; i < numberTimes; i++)
            {
                map = ApplyEnhancement(map, isVoidEmpty);
                isVoidEmpty = !isVoidEmpty;
            }

            return map;
        }

        private List<List<bool>> ApplyEnhancement(List<List<bool>> map, bool isVoidEmpty)
        {
            List<List<bool>> newMap = new();
            int mapSize = map[0].Count;

            for (int i = -1; i < map.Count + 1; i++)
            {
                List<bool> line = new();

                for (int j = -1; j < mapSize + 1; j++)
                {
                    List<bool> surronundingPixels = new();

                    for (int k = -1; k < 2; k++)
                    {
                        for (int h = -1; h < 2; h++)
                        {
                            if (i + k >= 0 && i + k < map.Count && j + h >= 0 && j + h < mapSize)
                            {
                                surronundingPixels.Add(map[i + k][j + h]);
                            }
                            else
                            {
                                if (isVoidEmpty)
                                {
                                    surronundingPixels.Add(false);
                                }
                                else
                                {
                                    surronundingPixels.Add(true);
                                }
                            }
                        }
                    }
                    line.Add(GetOutputPixel(surronundingPixels));
                }
                newMap.Add(line);
            }

            return newMap;
        }
        
        private bool GetOutputPixel(List<bool> inputPixels)
        {
            string binaryString = string.Join("", inputPixels.Select(b => b ? "1" : "0"));

            int decimalNumber = Convert.ToInt32(binaryString, 2);

            return _enhancementKey[decimalNumber] == '#';
        }

        private int CountPixels(List<List<bool>> map)
        {
            var count = 0;
            foreach (var line in map)
            {
                foreach (var c in line)
                {
                    if (c)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
