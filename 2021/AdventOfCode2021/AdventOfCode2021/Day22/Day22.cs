using AdventOfCode2021.D22;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day22 : IDay
    {
        private string[] _input;
        private List<Reboot> _reboots = new();

        public Day22()
        {
            _input = File.ReadAllLines(@"Inputs/Day22Input.txt");
            Parse();
        }

        private void Parse()
        {
            foreach (var line in _input)
            {
                var regex = new Regex(@"(on|off)\s+x=(-?\d+)\.\.(-?\d+),y=(-?\d+)\.\.(-?\d+),z=(-?\d+)\.\.(-?\d+)");
                var match = regex.Match(line);

                if (!match.Success)
                {
                    throw new ArgumentException("Invalid input format.");
                }

                var reboot = new Reboot
                {
                    IsOn = match.Groups[1].Value == "on",
                    Interval = new Interval
                    {
                        X = (int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value)),
                        Y = (int.Parse(match.Groups[4].Value), int.Parse(match.Groups[5].Value)),
                        Z = (int.Parse(match.Groups[6].Value), int.Parse(match.Groups[7].Value))
                    }
                };

                _reboots.Add(reboot);
            }
        }

        public void RunPart1()
        {
            Console.WriteLine($"Part 1: {CalculateOnCubes(-50,50)}");
        }

        public void RunPart2()
        {
            Console.WriteLine($"Part 2: {CalculateOnCubes()}");
        }

        private long CalculateOnCubes(long minCoord = long.MinValue, long maxCoord = long.MaxValue)
        {
            var intervals = new List<Reboot>();

            foreach (var reboot in _reboots)
            {
                if (reboot.Interval.X.Item1 < minCoord || reboot.Interval.X.Item2 > maxCoord || reboot.Interval.Y.Item1 < minCoord || reboot.Interval.Y.Item2 > maxCoord || reboot.Interval.Z.Item1 < minCoord || reboot.Interval.Z.Item2 > maxCoord)
                {
                    continue;
                }

                var newIntervals = new List<Reboot>();

                if (reboot.IsOn) newIntervals.Add(reboot);

                foreach (var cube in intervals)
                {
                    var interval = Overlap(cube.Interval, reboot.Interval);

                    if (interval is not null)
                    {
                        var newReboot = new Reboot
                        {
                            IsOn = !cube.IsOn,
                            Interval = interval
                        };

                        newIntervals.Add(newReboot);
                    }
                }

                intervals.AddRange(newIntervals);
            }

            long count = 0;

            foreach (var reboot in intervals)
            {
                long xRange = (reboot.Interval.X.Item2 - reboot.Interval.X.Item1 + 1);
                long yRange = (reboot.Interval.Y.Item2 - reboot.Interval.Y.Item1 + 1);
                long zRange = (reboot.Interval.Z.Item2 - reboot.Interval.Z.Item1 + 1);
                long value = xRange * yRange * zRange;

                if (!reboot.IsOn) value *= -1;

                count += value;
            }

            return count;
        }

        private Interval? Overlap(Interval interval1, Interval interval2)
        {

            if (interval1.X.Item2 < interval2.X.Item1 || interval2.X.Item2 < interval1.X.Item1)
                return null;
            if (interval1.Y.Item2 < interval2.Y.Item1 || interval2.Y.Item2 < interval1.Y.Item1)
                return null;
            if (interval1.Z.Item2 < interval2.Z.Item1 || interval2.Z.Item2 < interval1.Z.Item1)
                return null;

            var x = (Math.Max(interval1.X.Item1, interval2.X.Item1), Math.Min(interval1.X.Item2, interval2.X.Item2));
            var y = (Math.Max(interval1.Y.Item1, interval2.Y.Item1), Math.Min(interval1.Y.Item2, interval2.Y.Item2));
            var z = (Math.Max(interval1.Z.Item1, interval2.Z.Item1), Math.Min(interval1.Z.Item2, interval2.Z.Item2));

            return new Interval { X = x, Y = y, Z = z };
        }
    }
}
