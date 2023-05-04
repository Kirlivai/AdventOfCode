using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Vector3I
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }

    public class Vector4I
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int W { get; set; }
    }

    public class Day17 : IDay
    {
        private string[] _input;

        public Day17()
        {
            _input = File.ReadAllLines(@"Inputs/Day17Input.txt");
        }

        public void RunPart1()
        {
            var cubes = new List<Vector3I>();

            for (int y = 0; y < _input.Length; y++)
            {
                for (int x = 0; x < _input[y].Length; x++)
                {
                    if (_input[y][x] == '#')
                    {
                        cubes.Add(new Vector3I { X = x, Y = y, Z = 0 });
                    }
                }
            }

            for (int i = 0; i < 6; i++)
            {
                List<Vector3I> newCubes = new List<Vector3I>();
                int minX = cubes.Min(c => c.X);
                int maxX = cubes.Max(c => c.X);
                int minY = cubes.Min(c => c.Y);
                int maxY = cubes.Max(c => c.Y);
                int minZ = cubes.Min(c => c.Z);
                int maxZ = cubes.Max(c => c.Z);

                for (int x = minX - 1; x <= maxX + 1; x++)
                {
                    for (int y = minY - 1; y <= maxY + 1; y++)
                    {
                        for (int z = minZ - 1; z <= maxZ + 1; z++)
                        {
                            var neighbours = Get3DCubeNeighbours(x, y, z, cubes);
                            bool isActive = cubes.Any(c => c.X == x && c.Y == y && c.Z == z);

                            if (isActive && (neighbours == 2 || neighbours == 3))
                            {

                                newCubes.Add(new Vector3I { X = x, Y = y, Z = z });
                            }
                            else if (!isActive && neighbours == 3)
                            {
                                newCubes.Add(new Vector3I { X = x, Y = y, Z = z });
                            }
                        }
                    }
                }

                cubes = newCubes;
            }

            Console.WriteLine($"Part 1: {cubes.Count}");
        }

        private int Get3DCubeNeighbours(int x, int y, int z, List<Vector3I> cubes)
        {
            int neighbours = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    for (int k = -1; k <= 1; k++)
                    {
                        if (i == 0 && j == 0 && k == 0)
                        {
                            continue;
                        }
                        if (cubes.Any(c => c.X == x + i && c.Y == y + j && c.Z == z + k))
                            neighbours++;
                    }
                }
            }

            return neighbours;
        }

        public void RunPart2()
        {
            var cubes = new List<Vector4I>();

            for (int y = 0; y < _input.Length; y++)
            {
                for (int x = 0; x < _input[y].Length; x++)
                {
                    if (_input[y][x] == '#')
                    {
                        cubes.Add(new Vector4I { X = x, Y = y, Z = 0, W = 0 });
                    }
                }
            }

            for (int i = 0; i < 6; i++)
            {
                List<Vector4I> newCubes = new List<Vector4I>();
                int minX = cubes.Min(c => c.X);
                int maxX = cubes.Max(c => c.X);
                int minY = cubes.Min(c => c.Y);
                int maxY = cubes.Max(c => c.Y);
                int minZ = cubes.Min(c => c.Z);
                int maxZ = cubes.Max(c => c.Z);
                int minW = cubes.Min(c => c.W);
                int maxW = cubes.Max(c => c.W);

                for (int x = minX - 1; x <= maxX + 1; x++)
                {
                    for (int y = minY - 1; y <= maxY + 1; y++)
                    {
                        for (int z = minZ - 1; z <= maxZ + 1; z++)
                        {
                            for (int w = minW - 1; w <= maxW + 1; w++)
                            {
                                var neighbours = Get4DCubeNeighbours(x, y, z, w, cubes);
                                bool isActive = cubes.Any(c => c.X == x && c.Y == y && c.Z == z && c.W == w);

                                if (isActive && (neighbours == 2 || neighbours == 3))
                                {

                                    newCubes.Add(new Vector4I { X = x, Y = y, Z = z, W = w });
                                }
                                else if (!isActive && neighbours == 3)
                                {
                                    newCubes.Add(new Vector4I { X = x, Y = y, Z = z, W = w });
                                }
                            }
                        }
                    }
                }

                cubes = newCubes;
            }

            Console.WriteLine($"Part 2: {cubes.Count}");
        }

        private int Get4DCubeNeighbours(int x, int y, int z, int w, List<Vector4I> cubes)
        {
            int neighbours = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            if (i == 0 && j == 0 && k == 0 && l == 0)
                            {
                                continue;
                            }
                            if (cubes.Any(c => c.X == x + i && c.Y == y + j && c.Z == z + k && c.W == w + l))
                                neighbours++;
                        }
                    }
                }
            }

            return neighbours;
        }
    }
}
