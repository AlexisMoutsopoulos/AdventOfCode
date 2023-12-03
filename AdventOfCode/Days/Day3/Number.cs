using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days.Day3
{
    internal class Number
    {
        public Point Start {  get; set; }
        public Point End { get; set; }
        public int Value { get; set; }
    }
    internal class Symbol
    {
        public Point Point { get; set; }
        public string Value { get; set; }
    }
    class Point{
        public int X { get; set; }
        public int Y { get; set; }

    }
}
