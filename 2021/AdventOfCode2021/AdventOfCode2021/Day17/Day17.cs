using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day17 : IDay
    {
        private string[] _input;

        private (int, int) _xArea;
        private (int, int) _yArea;

        public Day17()
        {
            _input = File.ReadAllLines(@"Inputs/Day17Input.txt");
            Parse();
        }

        private void Parse()
        {
            var line = _input[0];

            Regex regex = new Regex(@"x=(-?\d+(\.\d+)?)+\.\.(-?\d+(\.\d+)?)+,\s*y=(-?\d+(\.\d+)?)+\.\.(-?\d+(\.\d+)?)+");
            Match match = regex.Match(line);

            if (match.Success)
            {
                _xArea.Item1 = int.Parse(match.Groups[1].Value);
                _xArea.Item2 = int.Parse(match.Groups[3].Value);
                _yArea.Item1 = int.Parse(match.Groups[5].Value);
                _yArea.Item2 = int.Parse(match.Groups[7].Value);                                                      
            }
            else
            {
                throw new InvalidDataException();
            }
        }

        public void RunPart1()
        {
            int maxHigh = 0;
            int maxVelocity = 200; // Lazy

            for (int i = 0; i < maxVelocity; i++)
            {
                for (int j = 0; j < maxVelocity; j++)
                {
                    IsProbeReachingTarget((i, j), ref maxHigh);
                }
            }

            Console.WriteLine($"Part 1: {maxHigh}");
        }

        public void RunPart2()
        {
            int maxHigh = 0;
            int counter = 0;
            int maxVelocity = 200; // Lazy

            for (int i = -maxVelocity; i < maxVelocity; i++)
            {
                for (int j = -maxVelocity; j < maxVelocity; j++)
                {
                    if (IsProbeReachingTarget((i, j), ref maxHigh))
                        counter++;
                }
            }

            Console.WriteLine($"Part 1: {counter}");
        }

        private bool IsProbeReachingTarget((int,int) velocity, ref int maxHigh)
        {
            (int, int) position = (0, 0);
            int currentBestHigh = 0;

            while (position.Item1 < _xArea.Item2 && position.Item2 > _yArea.Item1)
            {
                position.Item1 += velocity.Item1;
                position.Item2 += velocity.Item2;

                if (velocity.Item1 > 0)
                    velocity.Item1--;
                else if (velocity.Item1 < 0)
                    velocity.Item1++;

                velocity.Item2--;

                if (position.Item2 > currentBestHigh)
                {
                    currentBestHigh = position.Item2;
                }

                if (position.Item1 >= _xArea.Item1 && position.Item1 <= _xArea.Item2 && position.Item2 >= _yArea.Item1 && position.Item2 <= _yArea.Item2)
                {
                    maxHigh = Math.Max(maxHigh, currentBestHigh);
                    return true;
                }
            }

            return false;
        }
    }
}
