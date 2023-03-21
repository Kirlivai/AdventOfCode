using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCode2021.D18
{
    public class PairValue
    {
        public PairValue(Pair? parent, int value)
        {
            Value = value;
            Parent = parent;
        }

        public Pair? Parent { get; set; }
        public int Value { get; set; }
    }


    public class Pair
    {
        public Pair? Parent { get; set; }
        public object Left { get; set; }
        public object Right { get; set; }

        public Pair DeepCopy()
        {
            return CopyRecurvisly(this);
        }

        private Pair CopyRecurvisly(Pair pair, Pair ?parent = null)
        {
            var newPair = new Pair();
            newPair.Parent = parent;

            if (pair.Left is Pair leftPair)
            {
                newPair.Left = CopyRecurvisly(leftPair, newPair);
            }
            else
            {
                var leftValue = ((PairValue)pair.Left).Value;
                newPair.Left = new PairValue(newPair, leftValue);
            }

            if (pair.Right is Pair rightPair)
            {
                newPair.Right = CopyRecurvisly(rightPair, newPair);
            }
            else
            {
                var rightValue = ((PairValue)pair.Right).Value;
                newPair.Right = new PairValue(newPair, rightValue);
            }

            return newPair;
        }

        public Pair() { }
    }
}
