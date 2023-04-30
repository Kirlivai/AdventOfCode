using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day13 : IDay
    {
        private string[] _input;

        public Day13()
        {
            _input = File.ReadAllLines(@"Inputs/Day13Input.txt");
        }

        public void RunPart1()
        {
            int departTime = int.Parse(_input[0]);
            List<int> ids = new List<int>();

            var split = _input[1].Split(',');

            foreach (var item in split)
            {
                if (item != "x")
                {
                    ids.Add(int.Parse(item));
                }
            }

            int min = int.MaxValue;
            int minId = 0;

            foreach (var id in ids)
            {
                int time = id - (departTime % id);
                if (time < min)
                {
                    min = time;
                    minId = id;
                }
            }

            Console.WriteLine($"Part 1: {min * minId}");
        }

        public void RunPart2()
        {
            List<long> buses = new List<long>();
            List<long> ids = new List<long>();

            var split = _input[1].Split(',');

            for (int i = 0; i < split.Length; i++)
            {
                if (split[i] != "x")
                {
                    buses.Add(long.Parse(split[i]));
                    ids.Add(i);
                }
            }

            // Chinese remainder algo

            List<long> xi = new List<long>();

            for (int i = 0; i < buses.Count; i++)
            {
                long x = (buses[i] - ids[i]) % buses[i];
                xi.Add(x);
            }

            
            long m = 1;

            foreach (var item in buses)
            {
                m *= item;
            }

            List<long> mi = new List<long>();

            foreach (var item in buses)
            {
                mi.Add(m / item);
            }

            List<long> bi = new List<long>();

            for (int i = 0; i < ids.Count; i++)
            {
                bi.Add(BezoutCoefficient(mi[i], buses[i]));
            }

            long x0 = 0;

            for (int i = 0; i < buses.Count; i++)
            {
                x0 += xi[i] * mi[i] * bi[i];
            }

            long solution = x0 % m;

            while (solution < 0)
            {
                solution += m;
            }

            Console.WriteLine($"Part 2: {solution}");
        }

        private long BezoutCoefficient(long a, long b)
        {
            long r, r1 = a, r2 = b, u, u1 = 1, u2 = 0, v, v1 = 0, v2 = 1;

            while (r2 > 0)
            {
                long q = r1 / r2;

                r = r1 - q * r2;
                r1 = r2;
                r2 = r;

                u = u1 - q * u2;
                u1 = u2;
                u2 = u;

                v = v1 - q * v2;
                v1 = v2;
                v2 = v;
            }

            return u1;
        }
    }
}
