using AdventOfCode2021.D8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day8 : IDay
    {
        private string[] _input;

        private List<SignalEntry> _signalsEntries = new();

        public Day8()
        {
            _input = File.ReadAllLines(@"Inputs/Day8Input.txt");
            Parse();
        }

        private void Parse()
        {
            
            foreach (var line in _input)
            {
                SignalEntry signalEntry = new();
                var split = line.Split("|");
                var patterns = split[0].Split(" ");
                var digits = split[1].Split(" ");

                signalEntry.Patterns = patterns.ToList();
                signalEntry.Digits = digits.ToList();

                signalEntry.Digits.RemoveAll(d => string.IsNullOrEmpty(d));

                _signalsEntries.Add(signalEntry);
            }
        }
        
        public void RunPart1()
        {
            int uniqueDigits = 0;

            foreach (var signalEntry in _signalsEntries)
            {
                uniqueDigits += signalEntry.Digits.Where(d => d.Length == 2 || d.Length == 3 || d.Length == 4 || d.Length == 7).Count();
            }

            Console.WriteLine($"Part 1: {uniqueDigits}");
        }

        public void RunPart2()
        {
            long sum = 0;

            foreach (var signalentry in _signalsEntries)
            {
                sum += GetDigitsValue(signalentry);
            }

            Console.WriteLine($"Part 2: {sum}");

        }

        private int GetDigitsValue(SignalEntry entry)
        {
            int digitValue = 0;
            
            string rightSegments = entry.Patterns.Single(p => p.Length == 2);
            string midLeftSegments = entry.Patterns.Single(p => p.Length == 4);
            string topSegment = entry.Patterns.Single(p => p.Length == 3);
            string backLeftSegments = entry.Patterns.Single(p => p.Length == 7);

            foreach (var c in rightSegments)
            {
                midLeftSegments = midLeftSegments.Replace(c.ToString(), "");
                topSegment = topSegment.Replace(c.ToString(), "");
                backLeftSegments = backLeftSegments.Replace(c.ToString(), "");
            }

            foreach (var c in midLeftSegments)
            {
                backLeftSegments = backLeftSegments.Replace(c.ToString(), "");
            }

            foreach (var c in topSegment)
            {
                backLeftSegments = backLeftSegments.Replace(c.ToString(), "");
            }

            int multiplier = 1000;
            
            foreach (var digit in entry.Digits)
            {
                digitValue += FindDigitValue(digit.ToList(), rightSegments.ToList(), midLeftSegments.ToList(), backLeftSegments.ToList()) * multiplier;
                multiplier /= 10;
            }

            return digitValue;
        }

        private int FindDigitValue(List<char> digit, List<char> rightSegments, List<char> midLeftSegments, List<char> backLeftSegments)
        {
            if (digit.Count == 2) return 1;
            else if (digit.Count == 3) return 7;
            else if (digit.Count == 4) return 4;
            else if (digit.Count == 7) return 8;

            int rightSegmentsCount = digit.Intersect(rightSegments).Count();
            int midLeftSegmentsCount = digit.Intersect(midLeftSegments).Count();
            int backLeftSegmentsCount = digit.Intersect(backLeftSegments).Count();

            if (backLeftSegmentsCount == 2 && rightSegmentsCount == 2) return 0;
            else if (backLeftSegmentsCount == 2 && midLeftSegmentsCount == 1) return 2;
            else if (backLeftSegmentsCount == 1 && rightSegmentsCount == 2 && midLeftSegmentsCount == 1) return 3;
            else if (rightSegmentsCount == 1 && backLeftSegmentsCount == 1) return 5;
            else if (rightSegmentsCount == 1 && midLeftSegmentsCount == 2) return 6;
            else return 9;

        }
    }
}
