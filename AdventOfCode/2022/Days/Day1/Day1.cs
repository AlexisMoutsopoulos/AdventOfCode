using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Days.Day1
{
    internal static class Day1
    {
        public static void Day1Problem(string filename)
        {
            var part1 = PartOne(ReadFile.Read(filename).Split("\r\n"));
            var part2 = PartTwo(ReadFile.Read(filename).Split("\r\n"));
            Console.WriteLine($"Day1 results: Part1= {part1}  Part2 = {part2}");
        }

        private static int PartOne(string[] data)
        {
            List<int> list = new List<int>();
            int sumCalories = 0;
            foreach (string s in data)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    sumCalories += int.Parse(s);
                }
                else
                {
                    list.Add(sumCalories);
                    sumCalories = 0;
                }
            }
            return list.Max();
            
        }

        private static int PartTwo(string[] data)
        {
            List<int> list = new List<int>();
            int sumCalories = 0;
            foreach (string s in data)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    sumCalories += int.Parse(s);
                }
                else
                {
                    list.Add(sumCalories);
                    sumCalories = 0;
                }
            }
            return list.OrderBy(x => x).ToList().TakeLast(3).Sum();

        }
    }
}
