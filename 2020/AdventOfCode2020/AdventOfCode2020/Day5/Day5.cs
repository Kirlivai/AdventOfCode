using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day5 : IDay
    {
        private string[] _input;

        public Day5()
        {
            _input = File.ReadAllLines(@"Inputs/Day5Input.txt");
        }

        public void RunPart1()
        {
            int highest = 0;

            foreach (var line in _input)
            {
                highest = Math.Max(highest, GetSeatId(line));
            }

            Console.WriteLine($"Part 1: {highest}");
        }

        public void RunPart2()
        {
            List<int> seatsIds = new();

            foreach (var line in _input)
            {
                seatsIds.Add(GetSeatId(line));
            }

            for (int i = 1; i < 127; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    int id = i * 8 + j;

                    if (!seatsIds.Contains(id) && seatsIds.Contains(id - 1) && seatsIds.Contains(id + 1))
                    {
                        Console.WriteLine($"Part 2 : {id}");
                        return;
                    }
                }
            }
        }

        private int GetSeatId(string seat)
        {
            int row = GetRow(seat.Substring(0, 7));
            int column = GetColumn(seat.Substring(7, 3));
            return row * 8 + column;
        }

        private int GetRow(string row)
        {
            int min = 0;
            int max = 127;
            for (int i = 0; i < row.Length; i++)
            {
                if (row[i] == 'F')
                {
                    max = (min + max) / 2;
                }
                else
                {
                    min = (min + max) / 2 + 1;
                }
            }
            return min;
        }

        private int GetColumn(string column)
        {
            int min = 0;
            int max = 7;
            for (int i = 0; i < column.Length; i++)
            {
                if (column[i] == 'L')
                {
                    max = (min + max) / 2;
                }
                else
                {
                    min = (min + max) / 2 + 1;
                }
            }
            return min;
        }
    }
}
