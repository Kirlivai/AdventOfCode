using AdventOfCode2020.D12;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day12 : IDay
    {
        private string[] _input;

        public Day12()
        {
            _input = File.ReadAllLines(@"Inputs/Day12Input.txt");
        }

        public void RunPart1()
        {
            int northDirection = 0;
            int eastDirection = 0;
            ShipDirection shipDirection = ShipDirection.East;

            foreach (var line in _input)
            {
                var direction = line[0];
                var amount = int.Parse(line[1..]);

                switch (direction)
                {
                    case 'N':
                        northDirection += amount;
                        break;
                    case 'S':
                        northDirection -= amount;
                        break;
                    case 'E':
                        eastDirection += amount;
                        break;
                    case 'W':
                        eastDirection -= amount;
                        break;
                    case 'L':
                        shipDirection = (ShipDirection)(((int)shipDirection - amount) % 360);
                        while (shipDirection < 0)
                            shipDirection += 360;
                        break;
                    case 'R':
                        shipDirection = (ShipDirection)(((int)shipDirection + amount) % 360);
                        break;
                    case 'F':
                        switch (shipDirection)
                        {
                            case ShipDirection.North:
                                northDirection += amount;
                                break;
                            case ShipDirection.East:
                                eastDirection += amount;
                                break;
                            case ShipDirection.South:
                                northDirection -= amount;
                                break;
                            case ShipDirection.West:
                                eastDirection -= amount;
                                break;
                        }
                        break;
                }
            }

            Console.WriteLine($"Part 1: {Math.Abs(northDirection) + Math.Abs(eastDirection)}");
        }

        public void RunPart2()
        {
            int northDirection = 0;
            int eastDirection = 0;
            int northWayPoint = 1;
            int eastWayPoint = 10;

            foreach (var line in _input)
            {
                var direction = line[0];
                var amount = int.Parse(line[1..]);

                switch (direction)
                {
                    case 'N':
                        northWayPoint += amount;
                        break;
                    case 'S':
                        northWayPoint -= amount;
                        break;
                    case 'E':
                        eastWayPoint += amount;
                        break;
                    case 'W':
                        eastWayPoint -= amount;
                        break;
                    case 'L':
                        for (int i = 0; i < amount; i += 90)
                        {
                            (northWayPoint, eastWayPoint) = TurnWayPoint(false, northWayPoint, eastWayPoint);
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < amount; i += 90)
                        {
                            (northWayPoint, eastWayPoint) = TurnWayPoint(true, northWayPoint, eastWayPoint);
                        }
                        break;
                    case 'F':
                        northDirection += northWayPoint * amount;
                        eastDirection += eastWayPoint * amount;
                        break;
                }
            }

            Console.WriteLine($"Part 2: {Math.Abs(northDirection) + Math.Abs(eastDirection)}");
        }

        private (int,int) TurnWayPoint(bool turnRight, int north, int east)
        {
            if (turnRight)
            {
                return (-east, north);
            }
            else
            {
                return (east, -north);
            }
        }
    }
}
