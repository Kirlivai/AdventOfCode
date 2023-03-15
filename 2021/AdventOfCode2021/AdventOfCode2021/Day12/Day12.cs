using AdventOfCode2021.D12;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{

    public class Day12 : IDay
    {
        private string[] _input;
        public List<CaveNode> _nodes = new();

        public Day12()
        {
            _input = File.ReadAllLines(@"Inputs/Day12Input.txt");
            Parse();
        }

        private void Parse()
        {
            foreach (var line in _input)
            {
                var splited = line.Split("-");

                CaveNode? leftNode = _nodes.FirstOrDefault(n => n.Name == splited[0]);
                if (leftNode == null)
                {
                    leftNode = new CaveNode() { Name = splited[0] };
                    _nodes.Add(leftNode);
                }

                CaveNode? rightNode = _nodes.FirstOrDefault(n => n.Name == splited[1]);
                if (rightNode == null)
                {
                    rightNode = new CaveNode() { Name = splited[1] };
                    _nodes.Add(rightNode);
                }

                leftNode.Neighbors.Add(rightNode);
                rightNode.Neighbors.Add(leftNode);
            }
        }

        public void RunPart1()
        {
            CaveNode start = _nodes.First(n => n.Name == "start");

            int numberPath = 0;

            GetFirstNumberPath(start, new List<CaveNode>(), ref numberPath);

            Console.WriteLine($"Part 1: {numberPath}");
        }

        public void RunPart2()
        {
            CaveNode start = _nodes.First(n => n.Name == "start");

            int numberPath = 0;

            GetSecondNumberPath(start, new Dictionary<CaveNode, int>(), ref numberPath);

            Console.WriteLine($"Part 1: {numberPath}");
        }

        private void GetFirstNumberPath(CaveNode node, List<CaveNode> visitedNodes, ref int numberPath)
        {
            if (node.Name == "end")
            {
                numberPath++;
                return;
            }

            if (node.Name.Any(char.IsLower))
                visitedNodes.Add(node);

            foreach (var neighbor in node.Neighbors)
            {
                if (!visitedNodes.Contains(neighbor))
                {
                    GetFirstNumberPath(neighbor, visitedNodes.ToList(), ref numberPath);
                }
            }
        }

        private void GetSecondNumberPath(CaveNode node, Dictionary<CaveNode, int> visitedNodes, ref int numberPath)
        {
            if (node.Name == "end")
            {
                numberPath++;
                return;
            }

            if (node.Name.Any(char.IsLower))
            {
                if (visitedNodes.ContainsKey(node))
                {
                    visitedNodes[node]++;
                }
                else
                {
                    visitedNodes.Add(node, 1);
                }
            }

            foreach (var neighbor in node.Neighbors)
            {
                if ((neighbor.Name == "start" || neighbor.Name == "end") && visitedNodes.ContainsKey(neighbor) && visitedNodes[neighbor] == 1)
                    continue;

                if (visitedNodes.ContainsKey(neighbor) && visitedNodes.Any(n => n.Value > 1))
                     continue;

                GetSecondNumberPath(neighbor, new Dictionary<CaveNode, int>(visitedNodes), ref numberPath);
            }
        }
    }
}
