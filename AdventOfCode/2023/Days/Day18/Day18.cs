using AdventOfCode._2023.Days.Day16;
using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode._2023.Days.Day16.Day16;

namespace AdventOfCode._2023.Days.Day18
{
    internal static class Day18
    {
        public static void Day18Problem(string filename)
        { 
            var partOne = PartOne(filename);
            var partTwo = PartTwo(filename);
            Console.WriteLine($"Day18 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static double PartTwo(string filename) => GetDimetionsV2(GetData(filename));


        private static double PartOne(string filename) =>  GetDimetions(GetData(filename));


        private static List<DigPlan> GetData(string filename)
        {
            var result = new List<DigPlan>();
            var data = ReadFile.Read(filename).Split("\r\n");
            foreach (var line in data)
            {
                var dataLine = line.Split(" ");
                result.Add(new DigPlan() { Orientation = dataLine[0], Meters = int.Parse(dataLine[1]), Color = dataLine[2] });
            }
            return result;
        }

        private static double GetDimetions(List<DigPlan> data)
        {

            var result = SetListWithCoordinates(data);
            long circumference = 0;
            foreach ( var row in data)
            {
                circumference += row.Meters;
            }
            return Area(result) + circumference / 2 + 1;
        }

        private static double GetDimetionsV2(List<DigPlan> data)
        {
            var newData = new List<DigPlan>();
            
            foreach ( var row in data)
            {
                string trimColor = row.Color.Trim('(').TrimEnd(')');
                newData.Add(new DigPlan() { Meters = ConvertHexToLong(trimColor), Orientation = DecryptOrientation(trimColor) });
            }
            var result = SetListWithCoordinates(newData);
            long circumference = 0;
            foreach (var row in newData)
            {
                circumference += row.Meters;
            }
            return Area(result) + circumference / 2 + 1;
        }
        static long ConvertHexToLong(string hexString) => Convert.ToInt64(hexString.Substring(1, 5), 16); 

        private static string DecryptOrientation(string hex)
        {
                if (int.Parse(hex.Substring(6,1)) == 0) return "R";
                if (int.Parse(hex.Substring(6, 1)) == 1) return "D";
                if (int.Parse(hex.Substring(6, 1)) == 2) return "L";
                if (int.Parse(hex.Substring(6, 1)) == 3) return "U";
                return String.Empty;
        }
        static double Area(List<(long Row, long Col)> polygon)
        {
            var n = polygon.Count;
            var result = 0.0;
            for (var i = 0; i < n - 1; i++)
            {
                result += polygon[i].Row * polygon[i + 1].Col - polygon[i + 1].Row * polygon[i].Col;
            }

            result = Math.Abs(result + polygon[n - 1].Row * polygon[0].Col - polygon[0].Row * polygon[n - 1].Col) / 2.0;
            return result;
        }
         
        private static List<(long, long)> SetListWithCoordinates(List<DigPlan> data)
        {
            var result = new List<(long, long)>();
            long i = 0, j = 0;
            foreach (var line in data)
            {
                if (line.Orientation == "R")
                { 
                        result.Add((i, j + line.Meters));
                        j += line.Meters;


                }
                else if (line.Orientation == "L")
                { 
                        result.Add((i, j - line.Meters));
                    j -= line.Meters;

                }
                else if (line.Orientation == "D")
                { 
                        result.Add((i + line.Meters, j));
                    i += line.Meters;

                }
                else if (line.Orientation == "U")
                { 
                        result.Add((i- line.Meters, j));
                    i -= line.Meters;
                }
            }
            return result;
        }
         
    }
}
