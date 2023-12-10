using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day9
{
    internal class Day9
    {

        public static void Day9Problem(string filename)
        {
            var partOne = PartOne(filename);
            var partTwo = PartTwo(filename);
            Console.WriteLine($"Day9 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static object PartTwo(string filename)
        {
            return ReturnSumOfPrediction(filename, 2);
        }

        private static long PartOne(string filename)
        {
            return ReturnSumOfPrediction(filename,1);
            
        }

        private static long ReturnSumOfPrediction(string filename,int part)
        {
            string[] data = ReadFile.Read(filename).Split("\r\n");
            List<long> initialList = new List<long>();
            List<long> nextlList = new List<long>();
            List<List<long>> allLists = new List<List<long>>();
            long result = 0;
            foreach(string line in data)
            {
                initialList = line.Split(' ').Select(long.Parse).ToList();
                allLists.Add(initialList);

                do
                {
                    nextlList = CreateNextList(initialList);
                    allLists.Add(nextlList);
                    initialList = nextlList;
                }while(initialList.Any(x => x != 0));


                result += (part ==1) ? FindPrediction(allLists) : FindPredictionV2(allLists);
                allLists.Clear();
            }
            return result;
        }

        private static long FindPrediction(List<List<long>> list)
        {
            int k = 0;
            for (int i = list.Count - 1; i >= 0 ; i--)
            {
                if(i == list.Count - 1) list[i].Add(0);
                else
                {
                    k = i + 1;
                    list[i].Add(list[i].Last() + list[k].Last());
                }
            }
            return list[0].Last();
        }

        private static long FindPredictionV2(List<List<long>> list)
        {
            int k = 0;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (i == list.Count - 1) list[i].Insert(0,0);
                else
                {
                    k = i + 1;
                    list[i].Insert(0,list[i].First() - list[k].First());
                }
            }
            return list[0].First();
        }

        private static List<long> CreateNextList(List<long> previousList)
        {
            List<long> list = new List<long>();
            for(int i = 0; i < previousList.Count - 1; i++)
            {
                list.Add(previousList[i+1] - previousList[i]);
            }
            return list;
        }
    }
}
