using AdventOfCode2021.D19;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day19 : IDay
    {
        private string[] _input;
        private List<Scanner> _scanners = new();

        private static readonly Func<(int, int, int), (int, int, int)>[] _transformations = new Func<(int, int, int), (int, int, int)>[]{
        v => v,
        v => new(v.Item1, -v.Item3, v.Item2),
        v => new(v.Item1, -v.Item2, -v.Item3),
        v => new(v.Item1, v.Item3, -v.Item2),

        v => new(-v.Item2, v.Item1, v.Item3),
        v => new(v.Item3, v.Item1, v.Item2),
        v => new(v.Item2, v.Item1, -v.Item3),
        v => new(-v.Item3, v.Item1, -v.Item2),

        v => new(-v.Item1, -v.Item2, v.Item3),
        v => new(-v.Item1, -v.Item3, -v.Item2),
        v => new(-v.Item1, v.Item2, -v.Item3),
        v => new(-v.Item1, v.Item3, v.Item2),

        v => new(v.Item2, -v.Item1, v.Item3),
        v => new(v.Item3, -v.Item1, -v.Item2),
        v => new(-v.Item2, -v.Item1, -v.Item3),
        v => new(-v.Item3, -v.Item1, v.Item2),

        v => new(-v.Item3, v.Item2, v.Item1),
        v => new(v.Item2, v.Item3, v.Item1),
        v => new(v.Item3, -v.Item2, v.Item1),
        v => new(-v.Item2, -v.Item3, v.Item1),

        v => new(-v.Item3, -v.Item2, -v.Item1),
        v => new(-v.Item2, v.Item3, -v.Item1),
        v => new(v.Item3, v.Item2, -v.Item1),
        v => new(v.Item2, -v.Item3, -v.Item1)
    };

        public Day19()
        {
            _input = File.ReadAllLines(@"Inputs/Day19Input.txt");
            Parse();
            Scan();
        }

        private void Parse()
        {
            for (int i = 0; i < _input.Length; i++)
            {
                var scanner = new Scanner();

                for (i++; i < _input.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(_input[i]))
                    {
                        break;
                    }

                    var parts = _input[i].Split(',');
                    scanner.BeaconsPositions.Add((int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2])));
                }

                _scanners.Add(scanner);
            }
        }

        public void RunPart1()
        {
            Console.WriteLine($"Part 1: {_scanners[0].BeaconsPositions.Count}");
        }

        public void RunPart2()
        {
            int max = int.MinValue;

            for (int i = 0; i < _scanners.Count; i++)
            {
                for (int j = i + 1; j <_scanners.Count; j++)
                {
                    int manhattantDistance = Math.Abs(_scanners[i].Position.Item1 - _scanners[j].Position.Item1) +
                        Math.Abs(_scanners[i].Position.Item2 - _scanners[j].Position.Item2) +
                        Math.Abs(_scanners[i].Position.Item3 - _scanners[j].Position.Item3);

                    max = Math.Max(max, manhattantDistance);
                }
            }

            Console.WriteLine($"Part 2: {max}");
        }

        private void Scan()
        {
            HashSet<int> _overlapedScanner = new();
            Scanner scanner0 = _scanners[0];

            while (_overlapedScanner.Count < _scanners.Count - 1)
            {
                for (int i = 1; i < _scanners.Count; i++)
                {
                    if (_overlapedScanner.Contains(i))
                    {
                        continue;
                    }

                    if (OverLap(scanner0, _scanners[i]))
                    {
                        _overlapedScanner.Add(i);
                    }
                }
            }
        }

        private bool OverLap(Scanner scannerZero, Scanner relativeScanner)
        {
            Dictionary<(int, int, int), int> positions = new();

            foreach (var relativeBeacon in relativeScanner.BeaconsPositions)
            {
                foreach (var beaconZero in scannerZero.BeaconsPositions)
                {
                    foreach (var transformation in _transformations)
                    {
                        var newPos = transformation(relativeBeacon);

                        var offset = (beaconZero.Item1 - newPos.Item1, beaconZero.Item2 - newPos.Item2, beaconZero.Item3 - newPos.Item3);

                        if (positions.ContainsKey(offset))
                        {
                            positions[offset]++;
                        }
                        else
                        {
                            positions.Add(offset, 1);
                        }

                        if (positions[offset] >= 12)
                        {
                            AddScannerPositions(scannerZero, relativeScanner, transformation, offset);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void AddScannerPositions(Scanner scannerZero, Scanner relativeScanner, Func<(int, int, int), (int, int, int)> transformation, (int, int, int) offset)
        {
            relativeScanner.Position = offset;
            
            foreach (var relativeBeacon in relativeScanner.BeaconsPositions)
            {
                var newPos = transformation(relativeBeacon);

                var newBeacon = (newPos.Item1 + offset.Item1, newPos.Item2 + offset.Item2, newPos.Item3 + offset.Item3);

                if (!scannerZero.BeaconsPositions.Contains(newBeacon))
                {
                    scannerZero.BeaconsPositions.Add(newBeacon);
                }
            }
        }
    }
}
