using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.D19;

public class Rule
{
    public int RuleId { get; set; }

    public bool IsValueRule;

    public char RuleValue;

    public List<List<int>> RuleIds = new();

}
