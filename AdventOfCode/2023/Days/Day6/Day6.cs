using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days.Day6
{
    internal static class Day6
    {
        public static void Day6Problem(string filename)
        {
            int partOne = PartOne(filename);
            long partTwo = PartTwo(filename);
            Console.WriteLine($"Day6 Results: Part1 = {partOne} Part2 = {partTwo}");
        }
        

        private static (long,long) TakeTimeDistance(List<int> time, List<int> distance)
        {
            string timeString= String.Empty, distanceString = String.Empty;
            for (int i = 0; i < time.Count; i++)
            {
                timeString += time[i];
                distanceString += distance[i];
            }
            return (long.Parse(timeString),long.Parse(distanceString));
        }
        private static long PartTwo(string filename)
        {
            string data = ReadFile.Read(filename);
            List<int> times = new List<int>();
            List<int> distances = new List<int>();
            string time = data.Split("\r\n")[0];
            times = time.Substring(time.IndexOf(':') + 1, time.Length - time.IndexOf(':') - 1).Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();
            string distance = data.Split("\r\n")[1];
            distances = distance.Substring(distance.IndexOf(':') + 1, distance.Length - distance.IndexOf(':') - 1).Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();

            long timeRun = TakeTimeDistance(times, distances).Item1;
            long distanceRun = TakeTimeDistance(times, distances).Item2;

            return WaysToWin(timeRun, distanceRun);
        }

        private static long WaysToWin(long time, long distance)
        {
            long pureTravelTime = 0;
            long velocity = 0;
            List<long> result = new List<long>();
            for (int i = 0; i < time; i++)
            {
                pureTravelTime = time - i;
                velocity = i;
                result.Add(pureTravelTime * velocity);
            }
            return result.Where(x => x > distance).Count();
        }

        private static int WaysToWin(int time, int distance)
        {
            int pureTravelTime = 0;
            int velocity = 0;
            List<int> result = new List<int>();
            for(int i = 0; i < time; i++)
            {
                pureTravelTime = time - i;
                velocity = i;
                result.Add(CalculateDistance(pureTravelTime,velocity));
            }
            return result.Where(x => x > distance).Count();
        }
        private static int CalculateDistance(int v,int t) =>
             v * t;
        

        private static int PartOne(string filename)
        {
            string data = ReadFile.Read(filename);
            int result = 1;
            List<int> times = new List<int>();
            List<int> distances = new List<int>();
            string time = data.Split("\r\n")[0];
            times = time.Substring(time.IndexOf(':') +1 , time.Length - time.IndexOf(':')-1).Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();
            string distance = data.Split("\r\n")[1];
            distances = distance.Substring(distance.IndexOf(':') + 1, distance.Length - distance.IndexOf(':') - 1).Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();
            for(int j = 0; j < times.Count;j++)
            {
                result *= WaysToWin(times[j], distances[j]);
            }
            
            
            return result;
        }
    }
}
