using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day22
{
    internal class Brick
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public List<Brick> Above = new();
        public List<Brick> Below = new();

        public int[] Xs => Enumerable.Range(Start.X, End.X - Start.X + 1).ToArray();
        public int[] Ys => Enumerable.Range(Start.Y, End.Y - Start.Y + 1).ToArray();
    }

    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}
