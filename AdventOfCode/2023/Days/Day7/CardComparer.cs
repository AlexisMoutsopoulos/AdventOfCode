using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days.Day7
{
    internal class CardComparer : IComparer<Hand>
    {
        private char[] cardValues = { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };
        public int Compare(Hand card1, Hand card2)
        {
            var comp = card1.Type.CompareTo(card2.Type);
            if (comp != 0) return comp;
            for (int i = 0; i < card1.Value.ToCharArray().Length; i++)
            {
              comp = Array.IndexOf(cardValues, card1.Value[i]).CompareTo(Array.IndexOf(cardValues, card2.Value[i]));
              if (comp != 0) return comp;
              else continue;
            }
            return -1;
        }
    }
}
