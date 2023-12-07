using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode.Days.Day7.Day7;

namespace AdventOfCode.Days.Day7
{
    internal class Hand  
    {
        public string Value {  get; set; }
        public int bidAmount { get; set; }
        public HandType Type { get; set; } 
         
    }

    enum HandType
    {
        FiveOfKind = 6,
        FourOfKind = 5,
        FullHouse = 4,
        ThreeOfKind = 3,
        TwoPair = 2,
        OnePair = 1,
        HighCard = 0
    }
}
