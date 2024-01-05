using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day17
{
    internal class Point
    {
        public Point(int Row, int Col, int Consecutive, string Direction)
        {
            this.Row = Row;
            this.Col = Col;
            this.Consecutive = Consecutive;
            this.Direction = Direction;
        }
        public int Row { get; set; }
        public int Col { get; set; }
        public int Consecutive { get; set; }
        public string Direction { get; set; }
        public int Heat { get; set; }

    }
}
