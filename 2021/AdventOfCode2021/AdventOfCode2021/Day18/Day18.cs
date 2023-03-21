using AdventOfCode2021.D18;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day18 : IDay
    {
        private string[] _input;
        

        public Day18()
        {
            _input = File.ReadAllLines(@"Inputs/Day18Input.txt");
        }

        private List<Pair> Parse()
        {
            List<Pair> pairs = new();

            foreach (var line in _input)
            {
                pairs.Add(ParsePair(line, null));
            }

            return pairs;
        }

        private Pair ParsePair(string line, Pair? parent)
        {
            line = line.Remove(0, 1);
            line = line.Remove(line.Length - 1, 1);

            int parenthesisCounter = 0;
            int i = 0;

            for (; i < line.Length; i++)
            {
                if (line[i] == '[')
                {
                    parenthesisCounter++;
                }
                else if (line[i] == ']')
                {
                    parenthesisCounter--;
                }
                else if (line[i] == ',' && parenthesisCounter == 0)
                {
                    break;
                }
            }

            return ParseLeftRight(line, i, parent);
        }

        private Pair ParseLeftRight(string line, int i, Pair? parent)
        {
            string left = line.Substring(0, i);
            string right = line.Substring(i + 1);
            object leftPair;
            object rightPair;

            var pair = new Pair();
            pair.Parent = parent;

            if (left[0] == '[')
            {
                leftPair = ParsePair(left, pair);
            }
            else
            {
                leftPair = new PairValue(pair, int.Parse(left));
            }

            if (right[0] == '[')
            {
                rightPair = ParsePair(right, pair);
            }
            else
            {
                rightPair = new PairValue(pair, int.Parse(right));
            }

            pair.Right = rightPair;
            pair.Left = leftPair;

            return pair;
        }

        public void RunPart1()
        {
            var pairs = Parse();
            
            Pair pair = pairs[0];

            for (int i = 1; i < pairs.Count; i++)
            {
                pair = AddPair(pair, pairs[i]);
            }

            Console.WriteLine($"Part 1: {GetMagnitude(pair)}");
        }

        public void RunPart2()
        {
            var pairs = Parse();

            int maxMagnitude = 0;
            
            foreach (var pair1 in pairs)
            {
                foreach (var pair2 in pairs)
                {
                    
                    if (pair1 == pair2) continue;

                    int magnitude = GetMagnitude(AddPair(pair1.DeepCopy(), pair2.DeepCopy()));

                    if (magnitude > maxMagnitude)
                    {
                        maxMagnitude = magnitude;                        
                    }
                }
            }

            Console.WriteLine($"Part 2: {maxMagnitude}");
        }

        private Pair AddPair(Pair left, Pair right)
        {
            var newPair = new Pair();
            newPair.Left = left;
            newPair.Right = right;
            newPair.Parent = null;
            left.Parent = newPair;
            right.Parent = newPair;

            
            int counterExplosion = 0;
            bool isSplit = false;

            while (true)
            {
                FindExplosion(newPair, 1, ref counterExplosion);

                if (counterExplosion == 0 && !isSplit)
                {
                    break;
                }
                
                counterExplosion = 0;

                isSplit = CheckSplit(newPair);
            }

            return newPair;
        }

        private void FindExplosion(Pair pair, int depth, ref int counterExplosions)
        {
            if (depth >= 5 && pair.Left is PairValue leftPairValue && pair.Right is PairValue rightPairValue)
            {
                counterExplosions++;

                if (pair.Parent is not null)
                    FindLeftValue(pair, pair.Parent, leftPairValue.Value);

                if (pair.Parent is not null)
                    FindRightValue(pair, pair.Parent, rightPairValue.Value);

                if (pair.Parent is not null)
                {
                    if (pair.Parent.Left == pair)
                    {
                        pair.Parent.Left = new PairValue(pair.Parent, 0);
                    }
                    else
                    {
                        pair.Parent.Right = new PairValue(pair.Parent, 0);
                    }
                }

                return;
            }

            if (pair.Left is Pair leftPair)
            {
                FindExplosion(leftPair, depth + 1, ref counterExplosions);
            }

            if (pair.Right is Pair rightPair)
            {
                FindExplosion(rightPair, depth + 1, ref counterExplosions);
            }
        }

        private void FindRightValue(Pair? lastPair, Pair pair, int addValue)
        {
            if (lastPair is null)
            {
                if (pair.Left is PairValue leftPairValue)
                {
                    leftPairValue.Value += addValue;
                }
                else if (pair.Left is Pair leftPair)
                {
                    FindRightValue(null, leftPair, addValue);
                }
                return;
            }

            if (pair.Right is PairValue rightPairValue)
            {
                rightPairValue.Value += addValue;

            }
            else if (pair.Right is Pair rightPair && rightPair != lastPair)
            {
                FindRightValue(null, rightPair, addValue);
            }
            else
            {
                if (pair.Parent != null)
                    FindRightValue(pair, pair.Parent, addValue);
            }
        }

        private void FindLeftValue(Pair? lastPair, Pair pair, int addValue)
        {
            if (lastPair is null)
            {
                if (pair.Right is PairValue rightPairValue)
                {
                    rightPairValue.Value += addValue;

                }
                else if (pair.Right is Pair rightPair)
                {
                    FindLeftValue(null, rightPair, addValue);

                }
                return;
            }

            if (pair.Left is PairValue leftPairValue)
            {
                leftPairValue.Value += addValue;
            }
            else if (pair.Left is Pair leftPair && leftPair != lastPair)
            {
                FindLeftValue(null, leftPair, addValue);
            }
            else
            {
                if (pair.Parent != null)
                    FindLeftValue(pair, pair.Parent, addValue);
            }
        }

        private bool CheckSplit(Pair pair)
        {

            if (pair.Left is PairValue leftValue && leftValue.Value >= 10)
            {
                var newPair = new Pair();
                newPair.Parent = pair;
                newPair.Left = new PairValue(newPair, leftValue.Value / 2);
                newPair.Right = new PairValue(newPair, (int)Math.Ceiling(leftValue.Value / 2.0f));
                pair.Left = newPair;

                return true;
            }
            else if (pair.Left is Pair leftPair)
            {
                if (CheckSplit(leftPair))
                {
                    return true;
                }
            }
            
            if (pair.Right is PairValue rightValue && rightValue.Value >= 10)
            {
                var newPair = new Pair();
                newPair.Parent = pair;
                newPair.Left = new PairValue(newPair, rightValue.Value / 2);
                newPair.Right = new PairValue(newPair, (int)Math.Ceiling(rightValue.Value / 2.0f));
                pair.Right = newPair;
                return true;
            }
            else if (pair.Right is Pair rightPair)
            {
                if (CheckSplit(rightPair))
                {
                    return true;
                }
            }

            return false;
        }

        private int GetMagnitude(object pair)
        {
            if (pair is PairValue pairValue)
            {
                return pairValue.Value;
            }
            else if (pair is Pair pairPair)
            {
                return 3 * GetMagnitude(pairPair.Left) +  2 * GetMagnitude(pairPair.Right);
            }

            return -1;
        }

        private void Print(Pair pair, bool isFirst)
        {
            var tmpPair = pair;

            if (isFirst)
            {
                while (tmpPair.Parent is not null)
                {
                    tmpPair = tmpPair.Parent;
                }
            }
            Console.Write("[");
            if (tmpPair.Left is Pair leftPair)
            {
                Print(leftPair, false);
            }
            else
            {
                Console.Write(((PairValue)pair.Left).Value);
            }

            Console.Write(",");

            if (tmpPair.Right is Pair rightPair)
            {
                Print(rightPair, false);
            }
            else
            {
                Console.Write(((PairValue)pair.Right).Value);
            }

            Console.Write("]");

            if (isFirst)
            {
                Console.WriteLine();
            }
        }
    }
}
