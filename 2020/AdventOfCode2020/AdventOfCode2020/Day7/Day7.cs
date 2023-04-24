using AdventOfCode2020.D7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day7 : IDay
    {
        private string[] _input;

        private List<Bag> _bags = new(); 

        public Day7()
        {
            _input = File.ReadAllLines(@"Inputs/Day7Input.txt");
            Parse();
        }

        private void Parse()
        {
            foreach (var line in _input)
            {
                var bag = new Bag();
                var split = line.Split(" bags contain ");
                bag.Name = split[0];
                var containedBags = split[1].Split(", ");
                foreach (var containedBag in containedBags)
                {
                    if (containedBag == "no other bags.")
                    {
                        continue;
                    }
                    var bagSplit = containedBag.Split(" ");
                    var containedBagName = bagSplit[1] + " " + bagSplit[2];
                    var containedBagCount = int.Parse(bagSplit[0]);
                    bag.ContainedBags[containedBagName] = containedBagCount;
                }
                _bags.Add(bag);
            }
        }

        public void RunPart1()
        {
            int result = 0;

            foreach (var bag in _bags)
            {
                if (ContainsShinyGoldBag(bag, 0))
                {
                    result++;
                }
            }

            Console.WriteLine($"Part 1: {result}");
        }

        public void RunPart2()
        {
            var shinyBag = _bags.Find(b => b.Name == "shiny gold");

            if (shinyBag is null) return;
            
            Console.WriteLine($"Part 2: {CountInsideShinyGolgBag(shinyBag)}");
        }

        private bool ContainsShinyGoldBag(Bag bag, int depth)
        {
            if (bag.Name == "shiny gold" && depth > 0)
            {
                return true;
            }
            foreach (var containedBag in bag.ContainedBags.Keys)
            {
                var contained = _bags.Find(b => b.Name == containedBag);

                if (contained is null) continue;

                if (ContainsShinyGoldBag(contained, depth + 1))
                {
                    return true;
                }
            }
            return false;
        }

        private int CountInsideShinyGolgBag(Bag bag)
        {
            int sum = 0;

            foreach (var containedBag in bag.ContainedBags)
            {
                var contained = _bags.Find(b => b.Name == containedBag.Key);

                if (contained is null) continue;

                sum += containedBag.Value;

                sum += containedBag.Value * CountInsideShinyGolgBag(contained);
            }

            return sum;
        }
    }
}
