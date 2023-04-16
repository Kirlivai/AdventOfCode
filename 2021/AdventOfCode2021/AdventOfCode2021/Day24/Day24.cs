using AdventOfCode2021.D24;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day24 : IDay
    {
        private string[] _input;
        private List<AluOperation> _operations = new();

        private List<HashSet<int>> _possibleZValues = new();

        public Day24()
        {
            _input = File.ReadAllLines(@"Inputs/Day24Input.txt");
            Parse();
            FindPossibleZValues();
        }

        private void Parse()
        {
            for (int i = 0; i < _input.Length; i++)
            {
                AluOperation aluOperation = new AluOperation();

                aluOperation.DivZValue = int.Parse(_input[i + 4].Split(' ')[2]);
                aluOperation.AddXValue = int.Parse(_input[i + 5].Split(' ')[2]);
                aluOperation.AddYValue = int.Parse(_input[i + 15].Split(' ')[2]);

                _operations.Add(aluOperation);
                i += 17;
            }
        }

        public void RunPart1()
        {
            List<int> number = GetModelNumber(true);

            Console.Write("Part 1: ");

            foreach (var item in number)
            {
                Console.Write(item);
            }

            Console.WriteLine();
        }

        public void RunPart2()
        {
            List<int> number = GetModelNumber(false);

            Console.Write("Part 2: ");

            foreach (var item in number)
            {
                Console.Write(item);
            }

            Console.WriteLine();
        }

        private void FindPossibleZValues()
        {
            HashSet<int> nextPossibleZValues = new HashSet<int>();
            nextPossibleZValues.Add(0);
            _possibleZValues.Add(nextPossibleZValues);

            for (int i = _operations.Count - 1; i > 0; i--)
            {
                var possibleZValues = FindOperationPossibleZvalues(_operations[i], nextPossibleZValues);
                _possibleZValues.Insert(0, possibleZValues);
                nextPossibleZValues = possibleZValues;
            }
        }

        private HashSet<int> FindOperationPossibleZvalues(AluOperation operations, HashSet<int> toReachZValues)
        {
            HashSet<int> possibleZValues = new HashSet<int>();

            for (int w = 1; w <= 9; w++)
            {
                for (int z = 0; z < 10000000; z++)
                {
                    if (IsValidValues(operations, toReachZValues, w, z))
                    {
                        if (!possibleZValues.Contains(z))
                            possibleZValues.Add(z);
                    }
                }
            }

            return possibleZValues;
        }

        private bool IsValidValues(AluOperation operations, HashSet<int> toReachZvalues, int w, int z)
        {
            int x = z % 26;

            z /= operations.DivZValue;
            x += operations.AddXValue;

            x = (x == w) ? 1 : 0;
            x = (x == 0) ? 1 : 0;

            z *= (25 * x + 1);

            z += (w + operations.AddYValue) * x;

            return toReachZvalues.Contains(z);
        }

        private List<int> GetModelNumber(bool highestNumber)
        {
            List<int> number = new List<int>();
            int z = 0;

            for (int i = 0; i < _operations.Count; i++)
            {
                if (highestNumber)
                {
                    for (int w = 9; w >= 1; w--)
                    {
                        if (IsValidWValue(_operations[i], _possibleZValues[i], w, ref z))
                        {
                            number.Add(w);
                            break;
                        }
                    }
                }
                else
                {
                    for (int w = 1; w <= 9; w++)
                    {
                        if (IsValidWValue(_operations[i], _possibleZValues[i], w, ref z))
                        {
                            number.Add(w);
                            break;
                        }
                    }
                }
            }

            return number;
        }

        private bool IsValidWValue(AluOperation operations, HashSet<int> possibleZValues, int w, ref int z)
        {
            int tmpZ = z;
            int x = tmpZ % 26;

            tmpZ /= operations.DivZValue;
            x += operations.AddXValue;

            x = (x == w) ? 1 : 0;
            x = (x == 0) ? 1 : 0;

            tmpZ *= (25 * x + 1);

            tmpZ += ((w + operations.AddYValue) * x);

            if (possibleZValues.Contains(tmpZ))
            {
                z = tmpZ;
                return true;
            }

            return false;
        }
    }
}
