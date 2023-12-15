using AdventOfCode.Helpers;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day12
{
    internal static class Day12
    {
        static Dictionary<string, long> cache = new Dictionary<string, long>();
        public static void Day12Problem(string filename)
        {
            var partOne = PartOne(filename);
            var partTwo = PartTwo(filename);
            Console.WriteLine($"Day12 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static long PartTwo(string filename)
        {
            long result = 0;

            foreach (var line in ReadFile.Read(filename).Split("\r\n"))
            {
                var data = line.Split(" ");
                var continuousGroup = ContinguousGroups(data[1]);

                data[0] = string.Join('?', Enumerable.Repeat(data[0], 5));
                continuousGroup = Enumerable.Repeat(continuousGroup, 5).SelectMany(g => g).ToList();

                result += CachCheck(data[0], continuousGroup);
            }



            return result;
        }

        private static long CachCheck(string value,List<int> groups)
        {
            var key = $"{value},{string.Join(',', groups)}"; 

            if (cache.TryGetValue(key, out var value1))
            {
                return value1;
            }

            value1 = CalculateV2(value, groups);
            cache[key] = value1;

            return value1;
        }

        private static int PartOne(string filename)
        {
            int result = 0;
            foreach (var line in ReadFile.Read(filename).Split("\r\n"))
            {
                var data = line.Split(" ");
                var continuousGroup = ContinguousGroups(data[1]);
                result += Calculate(data[0], continuousGroup, result);
            }



            return result;
        }

        private static List<int> ContinguousGroups(string data)
        {
            return data.Split(',').Select(int.Parse).ToList();
        }

        private static int Calculate(string value, List<int> groups, dynamic counter)
        {
            char[] charArray = value.ToCharArray();
            char[] charArray1 = value.ToCharArray();
            if (!value.Contains("?"))
                return (IsValueValid(value, groups) ? 1 : 0);
            else
            {

                charArray[value.IndexOf('?')] = '.';
                charArray1 = value.ToCharArray();
                charArray1[value.IndexOf('?')] = '#';
                return Calculate(new string(charArray), groups, counter) + Calculate(new string(charArray1), groups, counter);


            }

        }

        private static bool IsValueValid(string value, List<int> groups)
        {
            List<int> consecutiveHashCountList = GroupValueBasedOnHash(value);

            if (consecutiveHashCountList.Count != groups.Count)
                return false;
            else
                return consecutiveHashCountList.SequenceEqual(groups);

        }

        private static List<int> GroupValueBasedOnHash(string value)
        {
            List<int> consecutiveHashCountList = new List<int>();
            int consecutiveCount = 0;
            foreach (char c in value)
            {
                if (c == '#')
                {
                    consecutiveCount++;
                }
                else if (consecutiveCount > 0)
                {
                    consecutiveHashCountList.Add(consecutiveCount);
                    consecutiveCount = 0;
                }
            }

            if (consecutiveCount > 0)
            {
                consecutiveHashCountList.Add(consecutiveCount);
            }

            return consecutiveHashCountList;
        }




        private static long CalculateV2(string value, List<int> groups)
        {
            while (true)
            {
                //No group Left
                if (groups.Count == 0)
                    if (!value.Contains('#')) // the value is empty or have dots or ?
                        return 1;
                    else
                        return 0; // value has # with no groups left
                //Not enough characters
                if (value.Length < groups[0])
                    return 0;
                if (value.Length == 0)
                    return 0;
                if (value.StartsWith("."))
                {
                    value = value.Trim('.');
                    continue;
                }
                if (value.StartsWith("?"))
                {
                    char[] charArray = value.ToCharArray();
                    char[] charArray1 = value.ToCharArray();
                    charArray[value.IndexOf('?')] = '.';
                    charArray1 = value.ToCharArray();
                    charArray1[value.IndexOf('?')] = '#';
                    return CachCheck(new string(charArray), groups) + CachCheck(new string(charArray1), groups);
                }
                if (value.StartsWith("#"))
                {                    
                    if (value.Length < groups[0])
                        return 0;
                    string subValue = value.Substring(0, groups[0]);
                    if (subValue.Contains("."))
                        return 0;
                    if (groups.Count > 1)
                    {
                        // Group must be followed by . or ?
                        if (value.Length < groups[0] + 1 || value[groups[0]].ToString() == "#")
                            return 0;
                        value = value.Substring(groups[0] + 1, value.Length - groups[0] - 1);
                        groups = groups.GetRange(1, groups.Count - 1);
                        continue;
                    }

                }
                value = value.Substring(groups[0], value.Length - groups[0]);
                groups = groups.GetRange(1, groups.Count - 1);
                continue;
            }

        }








    }
}

