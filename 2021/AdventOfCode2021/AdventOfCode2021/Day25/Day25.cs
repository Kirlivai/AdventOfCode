using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public enum Direction
    {
        Down,
        Right,
    }

    public class Day25 : IDay
    {
        private string[] _input;

        private HashSet<(int, int)> _rights = new HashSet<(int, int)>();
        private HashSet<(int, int)> _downs = new HashSet<(int, int)>();

        private int _width;
        private int _height;

        public Day25()
        {
            _input = File.ReadAllLines(@"Inputs/Day25Input.txt");
            Parse();
        }

        private void Parse()
        {
            _height = _input.Length;
            _width = _input[0].Length;

            for (int i = 0; i < _input.Length; i++)
            {
                var line = _input[i];
                for (int j = 0; j < line.Length; j++)
                {
                    var c = line[j];
                    if (c == 'v')
                    {
                        _downs.Add((j, i));
                    }
                    else if (c == '>')
                    {
                        _rights.Add((j, i));
                    }
                }
            }
        }

        public void RunPart1()
        {
            int counter = 1;

            while (true)
            {
                HashSet<(int, int)> newRights = new HashSet<(int, int)>();

                foreach (var c in _rights)
                {
                    var (x, y) = c;
                    x++;

                    if (x >= _width)
                    {
                        x = 0;
                    }

                    if (_rights.Contains((x, y)) || _downs.Contains((x,y)))
                    {
                        newRights.Add(c);
                    }
                    else
                    {
                        newRights.Add((x, y));
                    }
                }

                HashSet<(int, int)> newDowns = new HashSet<(int, int)>();

                foreach (var c in _downs)
                {
                    var (x, y) = c;
                    y++;

                    if (y >= _height)
                    {
                        y = 0;
                    }

                    if (_downs.Contains((x, y)) || newRights.Contains((x, y)))
                    {
                        newDowns.Add(c);
                    }
                    else
                    {
                        newDowns.Add((x, y));
                    }
                }

                if (counter == 58)
                {

                }

                if (newDowns.SequenceEqual(_downs) && newRights.SequenceEqual(_rights))
                {
                    break;
                }
                else
                {
                    _downs = newDowns;
                    _rights = newRights;
                }

                counter++;
            }

            Console.WriteLine($"Part 1 : {counter}");
        }

        public void RunPart2()
        {
            Console.WriteLine($"Part 2 : Ez");
        }
    }
}
