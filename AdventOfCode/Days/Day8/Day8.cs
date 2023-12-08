using AdventOfCode.Days.Day8;
using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utilities.Numerics;

namespace AdventOfCode.Days.Day7
{
    internal static class Day8
    {

        public static void Day8Problem(string filename)
        {
            int partOne = PartOne(filename);
            var partTwo = PartTwo(filename);
            Console.WriteLine($"Day8 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static long PartTwo(string filename)
        {
            string[] data = File.ReadAllLines(filename);
            var instructions = InstructionsList(data[0]);
            var elements = ElementsList(data[2..]);

            var nextElement = elements.Where(x => x.Value.EndsWith('A')).ToList();
            var elementVal = nextElement.Select(k => k.Value).ToList();
            List<long> count = new List<long>();
            long counter = 0;
            for (int j = 0; j < nextElement.Count; j++)
            {
                for (int i = 0; i < instructions.Count; i++)
                {
                    elementVal[j] = (instructions[i] == "R") ? nextElement[j].Right : nextElement[j].Left;
                    nextElement[j] = elements.FirstOrDefault(x => x.Value == elementVal[j]);

                    if (i == instructions.Count - 1 && !nextElement[j].Value.EndsWith('Z'))
                        i = -1;
                    ++counter;
                }

                count.Add(counter);
                counter = 0;
            }
            return FindLcm(count.OrderBy(x => x).ToList());
        }

        private static int PartOne(string filename)
        {
            string[] data = File.ReadAllLines(filename);
            var instructions = InstructionsList(data[0]);
            var elements = ElementsList(data[2..]);
            var nextElement = elements.FirstOrDefault(x => x.Value == "AAA");
            string elementVal = nextElement.Value;
            int count = 0;
            for (int i = 0; i < instructions.Count; i++)
            {
                elementVal = (instructions[i] == "R") ? nextElement.Right : nextElement.Left;
                nextElement = elements.FirstOrDefault(x => x.Value == elementVal);

                if (i == instructions.Count - 1 && nextElement.Value != "ZZZ")
                    i = -1;
                count++;
            }
            return count;
        }

        private static List<string> InstructionsList(string data)
        {
            var list = new List<string>();
            foreach (char c in data)
                list.Add(c.ToString());
            return list;
        }

        private static List<Element> ElementsList(string[] data)
        {
            List<Element> list = new List<Element>();
            foreach (var element in data)
            {
                string value = element.Substring(0, element.IndexOf('=') - 1).Trim();
                string right = element.Substring(element.IndexOf("=") + 2, element.Length - element.IndexOf("=") - 3).Split(',')[1].Trim();
                string left = element.Substring(element.IndexOf("=") + 3, element.Length - element.IndexOf("=") - 3).Split(',')[0].Trim();
                list.Add(new Element() { Value = value, Left = left, Right = right });
            }
            return list;
        }

        private static long FindLcm(List<long> numbers)
        {
            return numbers.Aggregate(numbers[0], Lcm1);
        }

        private static long Lcm1(long a, long b)
        {
            return a * b / FindGCD(a, b);
        }

        private static long FindGCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }
    }
}
