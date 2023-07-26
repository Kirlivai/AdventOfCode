using AdventOfCode2020.D19;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020;

public class Day19 : IDay
{
    private string[] _input;

    private List<Rule> _rules = new();

    private List<string> _messages = new();

    public Day19()
    {
        _input = File.ReadAllLines(@"Inputs/Day19Input.txt");
        Parse();
    }

    private void Parse()
    {
        int i = 0;

        for (; i < _input.Length; i++)
        {
            if (string.IsNullOrEmpty(_input[i]))
            {
                break;
            }
            var rule = new Rule();
            var parts = _input[i].Split(':');
            rule.RuleId = int.Parse(parts[0]);
            if (parts[1].Contains('"'))
            {
                rule.IsValueRule = true;
                rule.RuleValue = parts[1].Trim().Replace("\"", "").ToCharArray()[0];
            }
            else
            {
                rule.IsValueRule = false;
                var ruleIds = parts[1].Trim().Split('|');
                foreach (var ruleId in ruleIds)
                {
                    var ruleIdParts = ruleId.Trim().Split(' ');
                    var ruleIdsList = new List<int>();
                    foreach (var ruleIdPart in ruleIdParts)
                    {
                        ruleIdsList.Add(int.Parse(ruleIdPart));
                    }
                    rule.RuleIds.Add(ruleIdsList);
                }
            }
            _rules.Add(rule);
        }

        for (i++; i < _input.Length; i++)
        {
            _messages.Add(_input[i]);
        }
    }


    public void RunPart1()
    {
        int result = 0;
        var rule0 = _rules.First(r => r.RuleId == 0);

        foreach (var message in _messages)
        {
            if (IsValidMessage(rule0, message))
            {
                result++;
            }
        }

        Console.WriteLine($"Part 1: {result}");
    }

    public void RunPart2()
    {
        var newRule8 = new Rule();
        newRule8.RuleId = 8;
        newRule8.IsValueRule = false;
        newRule8.RuleIds.Add(new List<int>() { 42 });
        newRule8.RuleIds.Add(new List<int>() { 42, 8 });

        var newRule11 = new Rule();
        newRule11.RuleId = 11;
        newRule11.IsValueRule = false;
        newRule11.RuleIds.Add(new List<int>() { 42, 31 });
        newRule11.RuleIds.Add(new List<int>() { 42, 11, 31 });

        _rules.RemoveAll(r => r.RuleId == 8 || r.RuleId == 11);

        _rules.Add(newRule8);
        _rules.Add(newRule11);

        int result = 0;
        var rule0 = _rules.First(r => r.RuleId == 0);

        foreach (var message in _messages)
        {
            if (IsValidMessage(rule0, message))
            {
                result++;
            }
        }

        Console.WriteLine($"Part 2: {result}");
    }

    private bool IsValidMessage(Rule rule0, string message)
    {
        int messageIndex = 0;

        foreach (var id in rule0.RuleIds[0])
        {
            int result = IsRuleMatching(_rules.First(r => r.RuleId == id), message, messageIndex, id == rule0.RuleIds[0].Last());

            if (result < 0)
            {
                return false;
            }
            else
            {
                messageIndex = result;
            }
        }

        return messageIndex == message.Length;
    }

    private int IsRuleMatching(Rule rule, string message, int messageIndex, bool isLast)
    {
        if (messageIndex >= message.Length)
        {
            return messageIndex;
        }

        if (rule.IsValueRule)
        {
            if (message[messageIndex] == rule.RuleValue)
            {
                return messageIndex + 1;
            }
            else
            {
                return -1;
            }
        }


        foreach (var child in rule.RuleIds)
        {
            int tmpIndex = messageIndex;
            bool isMatching = true;

            foreach (var childRule in child)
            {
                bool isLastChild = (childRule == child.Last()) && isLast;

                if (childRule == 11)
                {
                    isLastChild = true;
                }

                int result = IsRuleMatching(_rules.First(r => r.RuleId == childRule), message, tmpIndex, isLastChild);


                if (result == message.Length && !isLastChild)
                {
                    isMatching = false;
                    break;
                }

                if (result == -1)
                {
                    isMatching = false;
                    break;
                }
                else
                {
                    tmpIndex = result;
                }
            }

            if (isMatching)
            {

                return tmpIndex;
            }
        }

        return -1;
    }


}
