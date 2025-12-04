using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025;

public class Day2 : IDay
{

    private string[] _input = File.ReadAllLines(@"Inputs/Day2.txt");

    private List<Tuple<long, long>> Ranges = new List<Tuple<long, long>>();


    public Day2()
    {
        foreach(var line in _input)
        {
            var parts = line.Split(',');

            foreach(var part in parts)
            {
                if (!part.Contains('-'))
                    continue;
                var rangeParts = part.Split('-');
                var start = long.Parse(rangeParts[0]);
                var end = long.Parse(rangeParts[1]);
                Ranges.Add(new Tuple<long, long>(start, end));
            }
        }
    }

    public void RunPart1()
    {
        long result = 0;

        foreach (var range in Ranges)
        {
            for (long i = range.Item1; i <= range.Item2; i++)
            {
                string txt = i.ToString();
                int size = txt.Length;

                if (size % 2 != 0)
                    continue;

                string firstHald = txt.Substring(0, size / 2);
                string secondHalf = txt.Substring(size / 2, size / 2);

                if (firstHald == secondHalf)
                {
                    result += i;
                }

            }
        }

        Console.WriteLine($"The value is: {result}");
    }

    public void RunPart2()
    {
        long result = 0;


        foreach (var range in Ranges)
        {
            for (long i = range.Item1; i <= range.Item2; i++)
            {
                string txt = i.ToString();
                int size = txt.Length;

                if (size < 2) continue;


                for (int j = 1; j <= size / 2; j++)
                {
                    string part = txt.Substring(0, j);

                    string rest = txt.Substring(j);

                    bool valid = true;


                    for (int k = 0; k < rest.Length; k += j)
                    {
                        if ( k + part.Length > rest.Length)
                        {
                            valid = false;
                            break;
                        }

                        string nextPart = rest.Substring(k, part.Length);

                        if (nextPart != part)
                        {
                            valid = false;
                            break;
                        }
                    }

                    if (valid)
                    {
                        result += i;
                        break;
                    }
                }
            }
        }
        Console.WriteLine($"The value is: {result}");
    }
}
