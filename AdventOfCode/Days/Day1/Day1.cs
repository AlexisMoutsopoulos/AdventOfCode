using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days.Day1
{
    internal static class Day1
    {
        private static Dictionary<string, int> numbers = new Dictionary<string, int>
        {
            {"one",1 },
            {"two",2},
            {"three",3},
            {"four",4},
            {"five",5},
            {"six",6},
            {"seven",7},
            {"eight",8},
            {"nine",9},
         };
        private static int LineNumber(string line)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in line)
            {
                if (char.IsDigit(c))
                {
                    sb.Append(c);
                }
            }
            if (sb.Length == 1) return int.Parse(sb.ToString());
            if (sb.Length == 0) return 0;
            else return int.Parse(sb[0].ToString() + sb[sb.Length - 1].ToString());
        }

        private static int LineNumberV2(string line)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            for (int c = 0; c < line.Length; c++)
            {
                if (char.IsDigit(line[c]))
                {
                    result.Add(c, int.Parse(line[c].ToString()));
                }
            }
            foreach (KeyValuePair<string, int> num in numbers)
            {
                if (line.Contains(num.Key))
                {
                    result.Add(line.IndexOf(num.Key), num.Value);
                    if (line.IndexOf(num.Key) != line.LastIndexOf(num.Key))
                    {
                        result.Add(line.LastIndexOf(num.Key), num.Value);
                    }
                }
            }

            int first = result.Keys.Min();
            int last = result.Keys.Max();
            return Convert.ToInt32(result[first].ToString() + result[last].ToString());
        }

        public static void Day1Problem(string filename)
        {
            string file = ReadFile.Read(filename);
            string[] lines = file.Split("\r\n");
            int part1 = 0;
            int part2 = 0;
            foreach (string line in lines)
            {
                part1 += LineNumber(line);
                part2 += LineNumberV2(line);
            }
            Console.WriteLine($"Day1 results: Part1= {part1}  Part2 = {part2}");
        }



    }

}
