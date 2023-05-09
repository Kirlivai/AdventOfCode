using AdventOfCode2020.D18;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day18 : IDay
    {
        private string[] _input;

        public Day18()
        {
            _input = File.ReadAllLines(@"Inputs/Day18Input.txt");
        }

        public void RunPart1()
        {
            long sum = 0;

            foreach (var line in _input)
            {
                sum += ComputeExpressionNoPriority(new Expression() { Value = line, Index = 0 });
            }

            Console.WriteLine($"Part 1: {sum}");
        }

        public void RunPart2()
        {
            long sum = 0;

            foreach (var line in _input)
            {
                sum += ComputeExpressionAddPriority(new Expression() { Value = line, Index = 0 });
            }

            Console.WriteLine($"Part 2: {sum}");
        }

        private long ComputeExpressionNoPriority(Expression expression)
        {
            long value = 0;
            Operation lastOperation = Operation.Add;

            for (; expression.Index < expression.Value.Length; expression.Index++)
            {
                char c = expression.Value[expression.Index];

                switch (c)
                {
                    case ')':
                        expression.Index++;
                        return value;
                    case '(':
                        expression.Index++;
                        long result = ComputeExpressionNoPriority(expression);
                        expression.Index--;
                        if (lastOperation == Operation.Add)
                        {
                            value += result;
                        }
                        else
                        {
                            value *= result;
                        }
                        break;
                    case '+':
                        lastOperation = Operation.Add;
                        break;
                    case '*':
                        lastOperation = Operation.Multiply;
                        break;
                    case ' ':
                        break;
                    default:
                        int nb = int.Parse(c.ToString());
                        if (lastOperation == Operation.Add)
                        {
                            value += nb;
                        }
                        else
                        {
                            value *= nb;
                        }
                        break;
                }
            }

            return value;
        }

        private long ComputeExpressionAddPriority(Expression expression, bool openParenthesis = false)
        {
            long value = 0;

            for (; expression.Index < expression.Value.Length;)
            {
                char c = expression.Value[expression.Index];

                switch (c)
                {
                    case ')':
                        if (openParenthesis)
                        {
                            expression.Index++;
                            openParenthesis = false;
                        }
                        return value;
                    case '(':
                        expression.Index++;
                        value += ComputeExpressionAddPriority(expression, true);
                        break;
                    case '+':
                        expression.Index++;
                        break;
                    case '*':
                        expression.Index++;
                        value *= ComputeExpressionAddPriority(expression, false);
                        break;
                    case ' ':
                        expression.Index++;
                        break;
                    default:
                        value += int.Parse(c.ToString());
                        expression.Index++;
                        break;
                }
            }

            return value;
        }
    }
}
