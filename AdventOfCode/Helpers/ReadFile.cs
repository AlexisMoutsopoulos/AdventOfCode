using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Helpers
{
    public class ReadFile
    {
        public static string Read(string filename)
        {
            return File.ReadAllText(filename);
        }
    }
}
