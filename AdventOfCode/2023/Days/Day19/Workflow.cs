using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day19
{
    internal class Workflow
    {
        public string Name { get; set; }
        public List<Condition> Condition { get; set; }
    }

    public class Condition
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int Value { get; set; }
        public string Success { get; set; }
        public string Failure { get; set; }
    }
      
}
