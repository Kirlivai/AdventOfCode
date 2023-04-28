using AdventOfCode2020.D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day11 : IDay
    {
        private string[] _input;

        private List<List<SeatState>> _seats = new();

        public Day11()
        {
            _input = File.ReadAllLines(@"Inputs/Day11Input.txt");

            foreach (var line in _input)
            {
                List<SeatState> seats = new();

                foreach (var c in line)
                {
                    if (c == 'L')
                    {
                        seats.Add(SeatState.Empty);
                    }
                    else if (c == '#')
                    {
                        seats.Add(SeatState.Occupied);
                    }
                    else
                    {
                        seats.Add(SeatState.Floor);
                    }
                }

                _seats.Add(seats);
            }

        }

        public void RunPart1()
        {
            var seats = new List<List<SeatState>>(_seats);

            while (true)
            {
                var newSeats = ApplySeatsRules(seats, true);

                if (newSeats.SelectMany(s => s).SequenceEqual(seats.SelectMany(s => s)))
                {
                    int occupied = seats.SelectMany(s => s).Count(s => s == SeatState.Occupied);

                    Console.WriteLine($"Part 1: {occupied}");
                    break;
                }

                seats = newSeats;
            }
        }

        public void RunPart2()
        {
            var seats = new List<List<SeatState>>(_seats);

            while (true)
            {
                var newSeats = ApplySeatsRules(seats, false);

                if (newSeats.SelectMany(s => s).SequenceEqual(seats.SelectMany(s => s)))
                {
                    int occupied = seats.SelectMany(s => s).Count(s => s == SeatState.Occupied);

                    Console.WriteLine($"Part 2: {occupied}");
                    break;
                }

                seats = newSeats;
            }
        }

        private List<List<SeatState>> ApplySeatsRules(List<List<SeatState>> seats, bool isFirstRules)
        {
            var newSeats = new List<List<SeatState>>();

            for (int i = 0; i < seats.Count; i++)
            {
                List<SeatState> row = new();

                for (int j = 0; j < seats[i].Count; j++)
                {
                    var seat = seats[i][j];
                    if (seat == SeatState.Floor)
                    {
                        row.Add(SeatState.Floor);
                        continue;
                    }

                    List<SeatState> adjacentSeats = (isFirstRules) ? GetAdjacentSeats(seats, i, j) : GetVisibleSeats(seats, i, j);

                    if (seat == SeatState.Empty)
                    {
                        if (adjacentSeats.All(s => s != SeatState.Occupied))
                            row.Add(SeatState.Occupied);
                        else
                            row.Add(SeatState.Empty);
                    }
                    else
                    {
                        if (adjacentSeats.Count(s => s == SeatState.Occupied) >= (isFirstRules ? 4 : 5))
                            row.Add(SeatState.Empty);
                        else
                            row.Add(SeatState.Occupied);
                    }
                }
                newSeats.Add(row);
            }
            return newSeats;
        }

        private List<SeatState> GetAdjacentSeats(List<List<SeatState>> seats, int i, int j)
        {
            var adjacentSeats = new List<SeatState>();
            if (i > 0)
            {
                adjacentSeats.Add(seats[i - 1][j]);
                if (j > 0)
                {
                    adjacentSeats.Add(seats[i - 1][j - 1]);
                }
                if (j < seats[i].Count - 1)
                {
                    adjacentSeats.Add(seats[i - 1][j + 1]);
                }
            }
            if (i < seats.Count - 1)
            {
                adjacentSeats.Add(seats[i + 1][j]);
                if (j > 0)
                {
                    adjacentSeats.Add(seats[i + 1][j - 1]);
                }
                if (j < seats[i].Count - 1)
                {
                    adjacentSeats.Add(seats[i + 1][j + 1]);
                }
            }
            if (j > 0)
            {
                adjacentSeats.Add(seats[i][j - 1]);
            }
            if (j < seats[i].Count - 1)
            {
                adjacentSeats.Add(seats[i][j + 1]);
            }
            return adjacentSeats;
        }

        private List<SeatState> GetVisibleSeats(List<List<SeatState>> seats, int i, int j)
        {
            var adjacentSeats = new List<SeatState>();
            List<(int,int)> directions = new() { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1,  0), (1, 1) };

            foreach (var dir in directions)
            {
                int x = i + dir.Item1;
                int y = j + dir.Item2;

                while (x >= 0 && x < seats.Count && y >= 0 && y < seats[x].Count)
                {
                    if (seats[x][y] != SeatState.Floor)
                    {
                        adjacentSeats.Add(seats[x][y]);
                        break;
                    }
                    x += dir.Item1;
                    y += dir.Item2;
                }
                if (x < 0 || x >= seats.Count || y < 0 || y >= seats[x].Count)
                {
                    adjacentSeats.Add(SeatState.Floor);
                }
            }
            return adjacentSeats;
        }
    }
}
