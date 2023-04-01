using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day21 : IDay
    {
        private string[] _input;
        private int _player1StartPos;
        private int _player2StartPos;

        private long _player1Wins = 0;
        private long _player2Wins = 0;


        public Day21()
        {
            _input = File.ReadAllLines(@"Inputs/Day21Input.txt");
            Parse();
        }

        private void Parse()
        {
            _player1StartPos = int.Parse(_input[0].Split(':')[1]);
            _player2StartPos = int.Parse(_input[1].Split(':')[1]);
        }

        public void RunPart1()
        {
            int diceValue = 1;
            int player1Score = 0;
            int player1Position = _player1StartPos;
            int player2Score = 0;
            int player2Position = _player2StartPos;
            int totalRolls = 0;
            bool player1Turn = true;

            while (true)
            {
                int rollsSums = 0;

                for (int i = 0; i < 3; i++)
                {
                    rollsSums += diceValue;
                    diceValue++;
                    if (diceValue > 100) diceValue = 1;
                }
                totalRolls += 3;

                if (player1Turn)
                {
                    player1Position += rollsSums;

                    while (player1Position > 10) player1Position -= 10;

                    player1Score += player1Position;

                    if (player1Score >= 1000)
                    {
                        Console.WriteLine($"Part1: {player2Score * totalRolls}");
                        return;
                    }

                }
                else
                {
                    player2Position += rollsSums;

                    while (player2Position > 10) player2Position -= 10;

                    player2Score += player2Position;

                    if (player2Score >= 1000)
                    {
                        Console.WriteLine($"Part1: {player1Score * totalRolls}");
                        return;
                    }
                }

                player1Turn = !player1Turn;
            }
        }

        public void RunPart2()
        {
            Dictionary<int, int> rollsPosibilities = new Dictionary<int, int>();

            for (int i = 1; i <= 3; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    for (int k = 1; k <= 3; k++)
                    {
                        int sum = i + j + k;
                        if (rollsPosibilities.ContainsKey(sum))
                        {
                            rollsPosibilities[sum]++;
                        }
                        else
                        {
                            rollsPosibilities.Add(sum, 1);
                        }
                    }
                }
            }

            SimulateGame(_player1StartPos, _player2StartPos, 0, 0, true, rollsPosibilities, 1);

            long max = Math.Max(_player1Wins, _player2Wins);

            Console.WriteLine($"Part2: {max}");
        }

        private void SimulateGame(int player1Position, int player2Position, int player1Score, int player2Score, bool player1Turn, Dictionary<int, int> rollsPosibilities, long possibilities)
        {
            if (player1Turn)
            {
                foreach (var roll in rollsPosibilities)
                {
                    int newPosition = player1Position + roll.Key;
                    while (newPosition > 10) newPosition -= 10;

                    int newScore = player1Score + newPosition;
                    
                    long newPossibilities = possibilities * roll.Value;

                    if (newScore >= 21)
                    {
                        _player1Wins += newPossibilities;
                        continue;
                    }

                    SimulateGame(newPosition, player2Position, newScore, player2Score, false, rollsPosibilities, newPossibilities);
                }
            }
            else
            {
                foreach (var roll in rollsPosibilities)
                {
                    int newPosition = player2Position + roll.Key;
                    while (newPosition > 10) newPosition -= 10;

                    int newScore = player2Score + newPosition;

                    long newPossibilities = possibilities * roll.Value;

                    if (newScore >= 21)
                    {
                        _player2Wins += newPossibilities;
                        continue;
                    }

                    SimulateGame(player1Position, newPosition, player1Score, newScore, true, rollsPosibilities, newPossibilities);
                }
            }
        }
    }
}
