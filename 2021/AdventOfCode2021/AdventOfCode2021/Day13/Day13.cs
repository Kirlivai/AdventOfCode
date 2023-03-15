using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day13 : IDay
    {
        private string[] _input;

        private List<(int, int)> _dots = new();
        private List<(bool, int)> _folds = new();

        public Day13()
        {
            _input = File.ReadAllLines(@"Inputs/Day13Input.txt");
            Parse();
        }

        private void Parse()
        {
            int i = 0;

            for (; i < _input.Length; i++)
            {
                if (_input[i] == "")
                    break;

                var splited = _input[i].Split(",");
                _dots.Add((int.Parse(splited[0]), int.Parse(splited[1])));
            }

            for (i++; i < _input.Length; i++)
            {
                var splited = _input[i].Split(" ");
                var coordonate = splited[2].Split("=");
                _folds.Add((coordonate[0] == "x", int.Parse(coordonate[1])));
            }
        }

        public void RunPart1()
        {
            var dots = _dots.ToList();

            ApplyFold(dots, _folds[0]);

            Console.WriteLine($"Part 1: {dots.Count()}");
        }

        public void RunPart2()
        {
            var dots = _dots.ToList();

            foreach (var fold in _folds)
            {
                ApplyFold(dots, fold);
            }

            int minX = dots.Min(d => d.Item1);
            int maxX = dots.Max(d => d.Item1);
            int minY = dots.Min(d => d.Item2);
            int maxY = dots.Max(d => d.Item2);

            Console.WriteLine($"Part 2:");

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (dots.Any(d => d.Item1 == x && d.Item2 == y))
                        Console.Write("#");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        private void ApplyFold(List<(int, int)> dots, (bool, int) fold)
        {
            bool isXFold = fold.Item1;
            int foldCoordonate = fold.Item2;

            for (int i = dots.Count - 1; i >= 0; i--)
            {
                var dot = dots[i];

                if (isXFold)
                {
                    if (dot.Item1 <= foldCoordonate)
                        continue;

                    var newCoordonates = (foldCoordonate - (dot.Item1 - foldCoordonate), dot.Item2);

                    if (dots.Any(d => d == newCoordonates))
                        dots.RemoveAt(i);
                    else
                        dots[i] = newCoordonates;
                }
                else
                {
                    if (dot.Item2 <= foldCoordonate)
                        continue;

                    var newCoordonates = (dot.Item1, foldCoordonate - (dot.Item2 - foldCoordonate));

                    if (dots.Any(d => d == newCoordonates))
                        dots.RemoveAt(i);
                    else
                        dots[i] = newCoordonates;
                }
            }
        }  
        
    }
}
