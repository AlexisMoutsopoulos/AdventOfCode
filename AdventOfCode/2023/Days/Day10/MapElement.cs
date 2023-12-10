using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day10
{
    internal class MapElement
    {
        public string Value {  get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsEnd {  get; set; }
        public bool IsVisited { get; set; }
        public string RightValue { get; set; }
        public string LeftValue { get; set; }
        public long StartDistance { get; set; }
    }
}
