using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day5 : IDay
    {
        private string[] _input;
        private List<(int, int, int, int)> _segments = new();


        public Day5()
        {
            _input = File.ReadAllLines(@"Inputs/Day5Input.txt");
            Parse();   
        }

        private void Parse()
        {
            foreach (var line in _input)
            {
                string[] parts = line.Split(" -> ");
                string[] fromCoords = parts[0].Split(",");
                string[] toCoords = parts[1].Split(",");

                int fromX = int.Parse(fromCoords[0]);
                int fromY = int.Parse(fromCoords[1]);
                int toX = int.Parse(toCoords[0]);
                int toY = int.Parse(toCoords[1]);

                _segments.Add((fromX, fromY, toX, toY));
            }
        }

        public void RunPart1()
        {
            Dictionary<(int, int), int> crossedPoints = new();

            foreach (var segment in _segments)
            {
                if (segment.Item2 == segment.Item4)
                {
                    int minX = Math.Min(segment.Item1, segment.Item3);
                    int maxX = Math.Max(segment.Item1, segment.Item3);

                    for (int i = minX; i <= maxX; i++)
                    {
                        if (crossedPoints.ContainsKey((i, segment.Item2)))
                        {
                            crossedPoints[(i, segment.Item2)]++;
                        }
                        else
                        {
                            crossedPoints.Add((i, segment.Item2), 1);
                        }
                    }
                }
                else if (segment.Item1 == segment.Item3)
                {
                    int minY = Math.Min(segment.Item2, segment.Item4);
                    int maxY = Math.Max(segment.Item2, segment.Item4);

                    for (int i = minY; i <= maxY; i++)
                    {
                        if (crossedPoints.ContainsKey((segment.Item1, i)))
                        {
                            crossedPoints[(segment.Item1, i)]++;
                        }
                        else
                        {
                            crossedPoints.Add((segment.Item1, i), 1);
                        }
                    }
                }
            }

            int result = crossedPoints.Count(x => x.Value > 1);

            Console.WriteLine($"Part 1: {result}");
        }

        public void RunPart2()
        {
            Dictionary<(int, int), int> crossedPoints = new();

            foreach (var segment in _segments)
            {
                if (segment.Item2 == segment.Item4)
                {
                    int minX = Math.Min(segment.Item1, segment.Item3);
                    int maxX = Math.Max(segment.Item1, segment.Item3);

                    for (int i = minX; i <= maxX; i++)
                    {
                        if (crossedPoints.ContainsKey((i, segment.Item2)))
                        {
                            crossedPoints[(i, segment.Item2)]++;
                        }
                        else
                        {
                            crossedPoints.Add((i, segment.Item2), 1);
                        }
                    }
                }
                else if (segment.Item1 == segment.Item3)
                {
                    int minY = Math.Min(segment.Item2, segment.Item4);
                    int maxY = Math.Max(segment.Item2, segment.Item4);

                    for (int i = minY; i <= maxY; i++)
                    {
                        if (crossedPoints.ContainsKey((segment.Item1, i)))
                        {
                            crossedPoints[(segment.Item1, i)]++;
                        }
                        else
                        {
                            crossedPoints.Add((segment.Item1, i), 1);
                        }
                    }
                }
                else
                {
                    double slope = (double)(segment.Item4 - segment.Item2) / (segment.Item3 - segment.Item1);

                    double yIntercept = segment.Item2 - slope * segment.Item1;

                    for (int x = Math.Min(segment.Item1, segment.Item3); x <= Math.Max(segment.Item1, segment.Item3); x++)
                    {
                        int y = (int)Math.Round(slope * x + yIntercept);

                        if (crossedPoints.ContainsKey((x, y)))
                        {
                            crossedPoints[(x, y)]++;
                        }
                        else
                        {
                            crossedPoints.Add((x, y), 1);
                        }
                    }
                }
            }

            int result = crossedPoints.Count(x => x.Value > 1);

            Console.WriteLine($"Part 2: {result}");
        }
    }
}
