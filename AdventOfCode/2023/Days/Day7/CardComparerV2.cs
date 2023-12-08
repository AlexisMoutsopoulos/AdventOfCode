using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode.Days.Day7.Day7;

namespace AdventOfCode.Days.Day7
{
    internal class CardComparerV2 : IComparer<Hand>
    {

        private char[] cardValuesV2 = { 'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' };
        public int Compare(Hand card1, Hand card2)
        { 
            var comp = card1.Type.CompareTo(card2.Type);
            if (comp != 0) return comp;
            for (int i = 0; i < card1.Value.ToCharArray().Length; i++)
            {
                if (Array.IndexOf(cardValuesV2, card1.Value[i]) > Array.IndexOf(cardValuesV2, card2.Value[i]))
                     return 1;
                else if (Array.IndexOf(cardValuesV2, card1.Value[i]) < Array.IndexOf(cardValuesV2, card2.Value[i]))
                     return -1;
            }
            return 0;
        }
    }
}

