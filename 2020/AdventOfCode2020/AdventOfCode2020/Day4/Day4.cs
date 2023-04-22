using AdventOfCode2020.D4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day4 : IDay
    {
        private string[] _input;

        private List<Passport> _passports = new();

        public Day4()
        {
            _input = File.ReadAllLines(@"Inputs/Day4Input.txt");
            Parse();
        }

        private void Parse()
        {
            for (int i = 0; i < _input.Length; i++)
            {
                var passport = new Passport();
                while (i < _input.Length && !string.IsNullOrEmpty(_input[i]))
                {
                    passport.Lines.Add(_input[i]);
                    i++;
                }

                _passports.Add(passport);
            }
        }

        public void RunPart1()
        {
            int res = 0;

            foreach (var passport in _passports)
            {
                Dictionary<string, bool> requiredFields = new()
                {
                    { "byr", false },
                    { "iyr", false },
                    { "eyr", false },
                    { "hgt", false },
                    { "hcl", false },
                    { "ecl", false },
                    { "pid", false },
                };

                foreach (var line in passport.Lines)
                {
                    var fields = line.Split(' ');
                    foreach (var field in fields)
                    {
                        var key = field.Split(':')[0];
                        if (requiredFields.ContainsKey(key))
                        {
                            requiredFields[key] = true;
                        }
                    }
                }

                if (requiredFields.Any(f => f.Value == false)) continue;

                res++;
            }

            Console.WriteLine($"Part 1: {res}");
        }

        public void RunPart2()
        {
            int res = 0;

            foreach (var passport in _passports)
            {
                Dictionary<string, bool> requiredFields = new()
                {
                    { "byr", false },
                    { "iyr", false },
                    { "eyr", false },
                    { "hgt", false },
                    { "hcl", false },
                    { "ecl", false },
                    { "pid", false },
                };

                foreach (var line in passport.Lines)
                {
                    var fields = line.Split(' ');
                    foreach (var field in fields)
                    {
                        var split = field.Split(':');

                        if (requiredFields.ContainsKey(split[0]) && IsValidField(split[0], split[1]))
                        {
                            requiredFields[split[0]] = true;
                        }
                    }

                }
                if (requiredFields.Any(f => f.Value == false)) continue;

                res++;

            }
            Console.WriteLine($"Part 2: {res}");
        }

        private bool IsValidField(string field, string value)
        {
            switch (field)
            {
                case "byr":
                    int by = int.Parse(value);
                    return (by >= 1920 && by <= 2002);
                case "iyr":
                    int iy = int.Parse(value);
                    return (iy >= 2010 && iy <= 2020);
                case "eyr":
                    int ey = int.Parse(value);
                    return (ey >= 2020 && ey <= 2030);
                case "hgt":
                    string size = "";
                    int i = 0;
                    for (; i < value.Length && char.IsDigit(value[i]); i++)
                    {
                        size += value[i];
                    }
                    if (i >= value.Length - 1) return false;
                    int sizeValue = int.Parse(size);
                    if (value[i] == 'c' && value[i + 1] == 'm') return (sizeValue >= 150 && sizeValue <= 193);
                    else if (value[i] == 'i' && value[i + 1] == 'n') return (sizeValue >= 59 && sizeValue <= 76);
                    else return false;
                case "hcl":
                    if (value.Length != 7) return false;
                    if (value[0] != '#') return false;

                    for (int j = 1; j < value.Length; j++)
                    {
                        if (!char.IsDigit(value[j]) && (value[j] < 'a' || value[j] > 'f')) return false;
                    }

                    return true;
                case "ecl":
                    if (value == "amb" || value == "blu" || value == "brn" || value == "gry" || value == "grn" || value == "hzl" || value == "oth") return true;
                    return false;
                case "pid":
                    if (value.Length != 9) return false;

                    for (int k = 0; k < value.Length; k++)
                    {
                        if (!char.IsDigit(value[k])) return false;
                    }

                    return true;
            }

            return false;
        }
    }
}

