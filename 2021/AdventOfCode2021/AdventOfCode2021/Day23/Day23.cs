using AdventOfCode2021.D23;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day23 : IDay
    {
        private string[] _input;

        private Amphipods _startState = new();

        public Day23()
        {
            _input = File.ReadAllLines(@"Inputs/Day23Input.txt");
            Parse();
        }

        private void Parse()
        {
            var firstLine = _input[2];
            var secondLine = _input[3];

            for (int i = 0; i < firstLine.Length && i < secondLine.Length; i++)
            {
                AddPosition(i, 1, firstLine[i]);
                AddPosition(i, 2, secondLine[i]);
            }

            _startState.Energy = 0;
        }

        private void AddPosition(int x, int y, char c)
        {
            switch (c)
            {
                case 'A':
                    _startState.Positions.Add(((x, y), AmphipodsType.Amber));
                    break;
                case 'B':
                    _startState.Positions.Add(((x, y), AmphipodsType.Bronze));
                    break;
                case 'C':
                    _startState.Positions.Add(((x, y), AmphipodsType.Copper));
                    break;
                case 'D':
                    _startState.Positions.Add(((x, y), AmphipodsType.Desert));
                    break;
                default:
                    break;
            }
        }

        public void RunPart1()
        {

            var res = GetLeastEnergyPath(_startState, 2);

            Console.WriteLine($"Part 1: {res}");
        }

        public void RunPart2()
        {
            var startStateCopy = new Amphipods();
            startStateCopy.Energy = _startState.Energy;

            startStateCopy.Positions = new List<((int, int), AmphipodsType)>
            {
                {((3,2), AmphipodsType.Desert) },
                {((3,3), AmphipodsType.Desert) },

                {((5,2), AmphipodsType.Copper) },
                {((5,3), AmphipodsType.Bronze) },
                
                {((7,2), AmphipodsType.Bronze) },
                {((7,3), AmphipodsType.Amber) },
                
                {((9,2), AmphipodsType.Amber) },
                {((9,3), AmphipodsType.Copper) },

            };

            foreach (var pos in _startState.Positions)
            {
                if (pos.Item1.Item2 == 2)
                {
                    startStateCopy.Positions.Add(((pos.Item1.Item1, 4), pos.Item2));
                }
                else
                {
                    startStateCopy.Positions.Add(pos);
                }
            }

            var res = GetLeastEnergyPath(startStateCopy, 4);

            Console.WriteLine($"Part 2: {res}");
        }

        private int GetLeastEnergyPath(Amphipods start, int columnSize)
        {
            var leastEnergy = int.MaxValue;
 
            var queue = new PriorityQueue<Amphipods, int>();
            var visited = new HashSet<string>();

            queue.Enqueue(start, 0);
            visited.Add(start.ToString());

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current.Energy >= leastEnergy)
                {
                    continue;
                }

                if (IsOrganized(current, columnSize))
                {
                    leastEnergy = Math.Min(leastEnergy, current.Energy);

                    if (leastEnergy == 43289)
                    {
                        
                    }
                    continue;
                }

                var moves = GetPossibleMoves(current, columnSize);

                foreach (var move in moves)
                {
                    string moveString = move.ToString();
                    
                    if (!visited.Contains(moveString))
                    {
                        queue.Enqueue(move, move.Energy);
                        visited.Add(moveString);
                    }
                }

            }
            
            return leastEnergy;
        }

        private bool IsOrganized(Amphipods state, int columnSize)
        {
            for (int i = 1; i <= columnSize; i++)
            {
                if (!state.Positions.Contains(((3, i), AmphipodsType.Amber)))
                {
                    return false;
                }

                if (!state.Positions.Contains(((5, i), AmphipodsType.Bronze)))
                {
                    return false;
                }

                if (!state.Positions.Contains(((7, i), AmphipodsType.Copper)))
                {
                    return false;
                }

                if (!state.Positions.Contains(((9, i), AmphipodsType.Desert)))
                {
                    return false;
                }
            }
            return true;
        }

        private List<Amphipods> GetPossibleMoves(Amphipods currentState, int columnSize)
        {
            var possibleMoves = new List<Amphipods>();

            var ambers = currentState.Positions.Where(a => a.Item2 == AmphipodsType.Amber).Select(a => a.Item1).ToList();
            var bronzes = currentState.Positions.Where(a => a.Item2 == AmphipodsType.Bronze).Select(a => a.Item1).ToList();
            var coppers = currentState.Positions.Where(a => a.Item2 == AmphipodsType.Copper).Select(a => a.Item1).ToList();
            var deserts = currentState.Positions.Where(a => a.Item2 == AmphipodsType.Desert).Select(a => a.Item1).ToList();

            var ambersMoves = GetAmphiTypeMoves(currentState,  ambers, AmphipodsType.Amber, 3, columnSize);
            if (ambersMoves.Item1) return ambersMoves.Item2;

            var bronzesMoves = GetAmphiTypeMoves(currentState, bronzes, AmphipodsType.Bronze, 5, columnSize);
            if (bronzesMoves.Item1) return bronzesMoves.Item2;
            
            var coppersMoves = GetAmphiTypeMoves(currentState, coppers, AmphipodsType.Copper, 7, columnSize);
            if (ambersMoves.Item1) return ambersMoves.Item2;
            
            var desertsMoves = GetAmphiTypeMoves(currentState, deserts, AmphipodsType.Desert, 9, columnSize);
            if (desertsMoves.Item1) return desertsMoves.Item2;

            possibleMoves.AddRange(ambersMoves.Item2);
            possibleMoves.AddRange(bronzesMoves.Item2);
            possibleMoves.AddRange(coppersMoves.Item2);
            possibleMoves.AddRange(desertsMoves.Item2);

            return possibleMoves;
        }

        private (bool,List<Amphipods>) GetAmphiTypeMoves(Amphipods currentState, List<(int, int)> amphis, AmphipodsType type, int xSpot, int columnSize)
        {
            var possibleMoves = new List<Amphipods>();

            foreach (var amphi in amphis)
            {
                if (amphi.Item1 == xSpot && amphi.Item2 > 0 && IsAmphipodRightPlaced(currentState, amphi, type, xSpot, columnSize))
                    continue;

                if (amphi.Item2 == 0 && !IsColumnValid(currentState, amphi, type, xSpot, columnSize))
                {        
                    continue;
                }

                for (int i = columnSize; i >= 0; i--)
                {
                    if (currentState.Positions.Any(a => a.Item1 == (xSpot, i)))
                    {
                        continue;
                    }

                    bool isValid = true;

                    for (int j = i + 1; j <= columnSize; j++)
                    {
                        if (currentState.Positions.Any(a => a.Item1 == (xSpot, j) && a.Item2 != type))
                        {
                            isValid = false;
                            break;
                        }
                    }

                    if (isValid)
                    {
                        var state = GetNewState(currentState, (amphi, type), (xSpot, i));
                        if (state is not null)
                        {
                            return (true, new List<Amphipods>() { state });
                        }
                    }
                }

                for (int i = 1; i < 12; i++)
                {
                    if (i == 3 || i == 5 || i == 7 || i == 9)
                    {
                        continue;
                    }

                    if (!currentState.Positions.Any(a => a.Item1 == (i,0)))
                    {
                        var state = GetNewState(currentState,(amphi, type), (i, 0));
                        if (state is not null) possibleMoves.Add(state);
                    }
                }
            }

            return (false,possibleMoves);
        }

        private bool IsAmphipodRightPlaced(Amphipods currentState, (int,int) amphi, AmphipodsType type, int xSpot, int columnSize)
        {  
            if (amphi == (xSpot, columnSize)) return true;

            for (int i = amphi.Item2 + 1; i <= columnSize; i++)
            {
                if (!currentState.Positions.Contains(((xSpot, i), type)))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsColumnValid(Amphipods currentState, (int, int) amphi, AmphipodsType type, int xSpot, int columnSize)
        {
            for (int i = 1; i <= columnSize; i++)
            {
                if (currentState.Positions.Any(a => a.Item2 != type && (a.Item1 == (xSpot, i))))
                {
                    return false;
                }
            }

            return true;
        }

        private Amphipods? GetNewState(Amphipods currentState, ((int, int),AmphipodsType) oldPos, (int,int) newPos)
        {
            int energy = IsReachable(currentState, oldPos.Item2, oldPos.Item1, newPos);

            if (energy == -1)
            {
                return null;
            }

            var amphi = new Amphipods();
            amphi.Positions = new(currentState.Positions);

            amphi.Positions.Remove(oldPos);
            amphi.Positions.Add((newPos, oldPos.Item2));

            amphi.Energy = energy;

            return amphi;
        }

        // TODO CHANGE TO A*
        private int IsReachable(Amphipods currentState, AmphipodsType type, (int, int) oldPos, (int, int) newPos)
        {
            var ini = oldPos;
            int distance = 0;

            while (true)
            {
                if (oldPos == newPos)
                {
                    return currentState.Energy + GetEnergyCost(type, distance);
                }

                if (oldPos.Item1 == newPos.Item1)
                {
                    if (oldPos.Item2 < newPos.Item2)
                    {
                        oldPos.Item2++;
                    }
                    else
                    {
                        oldPos.Item2--;
                    }
                }
                else
                {
                    if (oldPos.Item2 == 0)
                    {

                        if (oldPos.Item1 < newPos.Item1)
                        {
                            oldPos.Item1++;
                        }
                        else
                        {
                            oldPos.Item1--;
                        }
                    }
                    else
                    {
                        oldPos.Item2--;
                    }
                }

                if (currentState.Positions.Any(a => a.Item1 == oldPos))
                {
                    return -1;
                }

                distance++;
            }
        }

        private int GetEnergyCost(AmphipodsType type, int energy)
        {
            switch (type)
            {
                case AmphipodsType.Bronze:
                    return energy * 10;
                case AmphipodsType.Copper:
                    return energy * 100;
                case AmphipodsType.Desert:
                    return energy * 1000;
                default:
                    return energy;
            }
        }
    }
}
