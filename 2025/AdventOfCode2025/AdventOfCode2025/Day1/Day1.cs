using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025;

public class Day1 : IDay
{
    private string[] _input;

    public Day1()
    {
        _input = File.ReadAllLines(@"Inputs/Day1.txt");
    }

    public void RunPart1()
    {
        var dial = 50;
        int counter = 0;

        foreach (var line in _input)
        {
            var direction = line[0];
            var distance = int.Parse(line[1..]);

            if (direction == 'R')
            {
                dial += distance;
            }
            else if (direction == 'L')
            {
                dial -= distance;
            }

            dial = (dial + 100) % 100;

            if (dial == 0) counter++;
        }

        Console.WriteLine($"The value is: {counter}");
    }

    public void RunPart2()
    {
        var dial = 50;
        int counter = 0;

        int loop = 0;

        foreach (var line in _input)
        {
            loop++;


            var direction = line[0];
            var distance = int.Parse(line[1..]);

            if (direction == 'R')
            {
                dial += distance;

                if (dial >= 100)
                {
                    counter += dial / 100;
                }
            }
            else if (direction == 'L')
            {
                if (dial == 0) counter--;

                dial -= distance;
                if (dial <= 0)
                {
                    counter += (Math.Abs(dial) + 100) / 100;
                }

            }

            dial = MyModBecauseTheCSharpOneIsShitOneNegativeNumbersILost1HourBeacauseOfThat(dial, 100);
        }

        Console.WriteLine($"The value is: {counter}");
    }

    int MyModBecauseTheCSharpOneIsShitOneNegativeNumbersILost1HourBeacauseOfThat(int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }
}
