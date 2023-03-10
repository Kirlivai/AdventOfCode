using AdventOfCode2021.D4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day4 : IDay
    {
        private string[] _input;
        private List<int> _generatedBingoNumber = new();
        private List<BingoGrid> _grids = new();

        public Day4()
        {
            _input = File.ReadAllLines(@"Inputs/Day4Input.txt");
            Parse();
        }

        private void Parse()
        {
            if (_input.Length <= 0) return;

            _generatedBingoNumber = _input[0].Split(',').Select(int.Parse).ToList();

            for (int i = 1; i < _input.Length; i++)
            {
                if (string.IsNullOrEmpty(_input[i])) continue;

                var grid = new BingoGrid();
                for (int j = 0; j < 5; j++)
                {
                    var split = _input[i].Split(' ');
                    List<int> number = new();

                    foreach (var n in split)
                    {
                        if (int.TryParse(n, out int result))
                        {
                            number.Add(result);
                        }
                    }

                    grid.Lines.Add(number);
                    i++;
                }        
                _grids.Add(grid);
            }
        }

        public void RunPart1()
        {
            foreach (var number in _generatedBingoNumber)
            {
                foreach (var grid in _grids)
                {
                    for (int i = 0; i < grid.Lines.Count; i++)
                    {
                        for (int j = 0; j < grid.Lines[i].Count; j++)
                        {
                            if (grid.Lines[i][j] == number)
                            {
                                grid.MarkedPositions.Add((i, j));
                            }
                        }
                    }

                    if (CheckAlignment(grid.MarkedPositions, 5))
                    {
                        int score = CalculateScore(grid, number);
                        Console.WriteLine($"Part 1: {score}");
                        return;
                    }
                }       
            }
        }

        

        public void RunPart2()
        {
            foreach (var number in _generatedBingoNumber)
            {
                foreach (var grid in _grids)
                {
                    if (grid.IsWin is true) continue;

                    for (int i = 0; i < grid.Lines.Count; i++)
                    {
                        for (int j = 0; j < grid.Lines[i].Count; j++)
                        {
                            if (grid.Lines[i][j] == number)
                            {
                                grid.MarkedPositions.Add((i, j));
                            }
                        }
                    }

                    if (CheckAlignment(grid.MarkedPositions, 5))
                    {
                        grid.IsWin = true;
                        if (_grids.All(x => x.IsWin))
                        {
                            int score = CalculateScore(grid, number);
                            Console.WriteLine($"Part 2: {score}");
                            return;
                        }
                    }
                }
            }
        }

        private int CalculateScore(BingoGrid grid, int number)
        {
            int score = 0;

            for (int i = 0; i < grid.Lines.Count; i++)
            {
                for (int j = 0; j < grid.Lines[i].Count; j++)
                {
                    if (!grid.MarkedPositions.Contains((i, j)))
                    {
                        score += grid.Lines[i][j];
                    }
                }
            }

            score *= number;

            return score;
        }

        public static bool CheckAlignment(HashSet<(int, int)> markedPositions, int gridSize)
        {
            foreach (var pos in markedPositions)
            {
                int row = pos.Item1;
                int col = pos.Item2;

                // Check for horizontal alignment
                int count = 1;
                for (int i = col + 1; i < gridSize && markedPositions.Contains((row, i)); i++)
                {
                    count++;
                    if (count == 5) return true;
                }

                // Check for vertical alignment
                count = 1;
                for (int i = row + 1; i < gridSize && markedPositions.Contains((i, col)); i++)
                {
                    count++;
                    if (count == 5) return true;
                }
            }

            return false;
        }
    }
}
