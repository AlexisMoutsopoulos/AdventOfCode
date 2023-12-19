using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day16
{
    internal class Beam
    {
        public Beam(string element, bool isVisited) { this.Element = element; this.IsVisited = isVisited; }   
        public Beam() {  }   
        public string Element {  get; set; }
        public bool IsVisited { get; set; }
        public bool Energized { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public string Direction { get; set; }
    }
}
