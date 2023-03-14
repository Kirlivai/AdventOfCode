using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day10 : IDay
    {
        private string[] _input;

        private Dictionary<char, int> _incorrectCharacterValues = new Dictionary<char, int> {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 },
        };

        private Dictionary<char, int> _incompleteCharacterValues = new Dictionary<char, int> {
            { ')', 1 },
            { ']', 2 },
            { '}', 3 },
            { '>', 4 },
        };

        public Day10()
        {
            _input = File.ReadAllLines(@"Inputs/Day10Input.txt");
        }

        public void RunPart1()
        {
            int res = 0;

            foreach (var line in _input)
            {
                res += GetCorruptedCharacterValue(line, new Stack<char>());
            }

            Console.WriteLine($"Part 1: {res}");
        }

        

        public void RunPart2()
        {
            List<long> scores = new();
            
            foreach (var line in _input)
            {
                Stack<char> characters = new();
                long score = 0;

                int res = GetCorruptedCharacterValue(line, characters);

                if (res != 0 || characters.Count == 0) continue;

                score = GetIncompleteSequenceScore(characters, score);

                scores.Add(score);
            }

            scores.Sort((a, b) => b.CompareTo(a));

            int middleIndex = scores.Count / 2;

            Console.WriteLine($"Part 2: {scores[middleIndex]}");
        }
        
        private int GetCorruptedCharacterValue(string line, Stack<char> characters)
        {
            foreach (var c in line)
            {
                if (c == '(' || c == '[' || c == '{' || c == '<')
                {
                    characters.Push(c);
                }
                else if (c == ')' || c == ']' || c == '}' || c == '>')
                {
                    if (characters.Count == 0)
                        return 0;

                    var last = characters.Pop();

                    if (last == '(' && c != ')')
                        return _incorrectCharacterValues[c];
                    else if (last == '[' && c != ']')
                        return _incorrectCharacterValues[c];
                    else if (last == '{' && c != '}')
                        return _incorrectCharacterValues[c];
                    else if (last == '<' && c != '>')
                        return _incorrectCharacterValues[c];
                }
            }
            return 0;
        }

        private long GetIncompleteSequenceScore(Stack<char> characters, long score)
        {
            while (characters.Count > 0)
            {
                var c = characters.Pop();

                score *= 5;

                switch (c)
                {
                    case '(':
                        score += _incompleteCharacterValues[')'];
                        break;
                    case '[':
                        score += _incompleteCharacterValues[']'];
                        break;
                    case '{':
                        score += _incompleteCharacterValues['}'];
                        break;
                    case '<':
                        score += _incompleteCharacterValues['>'];
                        break;
                }
            }
            return score;
        }
    }
}
