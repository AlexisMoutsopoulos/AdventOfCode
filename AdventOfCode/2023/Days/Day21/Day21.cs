using AdventOfCode._2023.Days.Day16;
using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day21
{
    internal class Day21
    {
        public static void Day21Problem(string filename)
        { 
            var partOne = PartOne(filename);
            var partTwo = PartTwo(filename);
            Console.WriteLine($"Day21 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static object PartTwo(string filename)
        {
            return 0;
        }

        private static object PartOne(string filename)
        {
            var data = ReadFile.Read(filename).Split("\r\n").ToList();
            (var array, int x, int y) = CreateArray(data);
            return NumberOfPlots(array,100,x,y);
        }
        private static bool OutBound(string[,] map, int row, int col)
        {
            return row >= map.GetLength(0)
                      || col >= map.GetLength(1)
                      || row < 0
                      || col < 0;
        }
        private static int NumberOfPlots(string[,] array,int iterations,int startX,int startY)
        {
            
            HashSet<(int, int)> next = new HashSet<(int, int)>();
            for (int i = 0; i < iterations; i++)
            {
                HashSet<(int, int)> current = new HashSet<(int, int)>();
                if (next.Count > 0)
                {
                    current = next;
                    next = new HashSet<(int, int)>();
                }
                else
                {
                    current.Add((startX, startY));
                }
                foreach (var value in current)
                {
                    (int x, int y) = value;
                    if(!OutBound(array, x+1, y))
                    {
                        if(array[x+1,y] != "#")
                            next.Add((x+1, y));
                    }
                    if (!OutBound(array, x - 1, y))
                    {
                        if (array[x - 1, y] != "#")
                            next.Add((x - 1, y));
                    }
                    if (!OutBound(array, x, y+1))
                    {
                        if (array[x, y+1] != "#")
                            next.Add((x, y + 1));
                    }
                    if (!OutBound(array, x , y - 1))
                    {
                        if (array[x, y -1] != "#")
                            next.Add((x, y - 1));
                    }

                }

            }
            return next.Count();
        }

        //static void Display2DArray(string[,] array,List<(int,int)> next)
        //{
        //    int rows = array.GetLength(0);
        //    int columns = array.GetLength(1);

        //    Console.WriteLine("2D Array of Strings:");

        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < columns; j++)
        //        {
        //            if(next.Any(x=> x.Item1 == i && x.Item2 == j))
        //                Console.Write("O" + " ");
        //            else
        //                Console.Write(array[i, j] + " ");
        //        }
        //        Console.WriteLine(); // Move to the next row after printing all columns
        //    }
        //}

        private static (string[,],int,int) CreateArray(List<string> lines)
        {
            int rows = lines.Count;
            int cols = lines[0].Length;
            int startX = 0;
            int startY = 0;
            string[,] array = new string[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = lines[i][j].ToString();
                    if (array[i,j] == "S")
                    {
                        startX = i; startY = j; 
                    }
                }
            }
            return (array,startX,startY);
        }
    }
}
