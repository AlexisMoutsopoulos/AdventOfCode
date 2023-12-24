using AdventOfCode._2023.Days.Day16;
using AdventOfCode._2023.Days.Day22;
using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day23
{
    record Point(int Row, int Col);
    internal class Day23
    {

        public static void Day23Problem(string filename)
        {
            var partOne = PartOne(filename);
            var partTwo = PartTwo(filename);
            Console.WriteLine($"Day23 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static object PartTwo(string filename)
        {
            string[,] map = GetData(filename);
            List<int> distances = new List<int>();
            List<Point> visited = new List<Point>();
            Point start = FindStartPoint(map);
            Point end = FindEndPoint(map);
            TraverseMapV2(map, visited, start, end, distances);

            return distances.Max() - 1;
        }

        private static int PartOne(string filename)
        {
            string[,] map = GetData(filename);
            List<int> distances = new List<int>();
            List<Point> visited = new List<Point>();
            Point start = FindStartPoint(map);
            Point end = FindEndPoint(map); 
            TraverseMap(map,visited, start, end,distances);
            
            return distances.Max() -1;
        }

        static void Display2DArray(string[,] array, List<Point> visited)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);

            Console.WriteLine("2D Array of Strings:");

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (visited.Contains(new Point(i,j)))
                        Console.Write("O" + " ");
                    else
                        Console.Write(array[i, j] + " ");
                }
                Console.WriteLine(); // Move to the next row after printing all columns
            }
        }
        
        private static void TraverseMap(string[,] map,List<Point> visited,Point start,Point end,List<int> distances)
        {  
            visited.Add(start);
            //Display2DArray(map, visited);
            List<Point> temps = FindNextDirection(map, visited, start);
            foreach(Point temp in temps)
                TraverseMap(map, visited, temp, end, distances); 
            

            if (start == end)
            {
                distances.Add(visited.Count);
                visited.Remove(start);
            }
            else
                visited.Remove(start);
            
        }

        private static void TraverseMapV2(string[,] map, List<Point> visited, Point start, Point end, List<int> distances)
        {
            visited.Add(start);
            //Display2DArray(map, visited);
            List<Point> temps = FindNextDirectionV2(map, visited, start);
            foreach (Point temp in temps)
                TraverseMapV2(map, visited, temp, end, distances);


            if (start == end)
            {
                distances.Add(visited.Count);
                visited.Remove(start);
            }
            else 
                visited.Remove(start);
             


        }
        private static string[,] GetData(string filename)
        {
            string[] lines = ReadFile.Read(filename).Split("\r\n");
            int rows = lines.Length;
            int cols = lines[0].Length;
            string[,] array = new string[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = lines[i][j].ToString();
                }
            }
            return array;
        }

        private static List<Point> FindNextDirection(string[,] array,List<Point> visitedPoints, Point p)
        {
            List<Point> result = new List<Point>();
            if (array[p.Row, p.Col] == ">") result.Add(new Point(p.Row,p.Col+1));
            else if (array[p.Row, p.Col] == "<") result.Add(new Point(p.Row, p.Col -1));
            else if (array[p.Row, p.Col] == "v") result.Add(new Point(p.Row + 1, p.Col));
            else if (array[p.Row, p.Col] == "^") result.Add(new Point(p.Row - 1, p.Col));
            if(result.Count > 0) return result;
            if(p.Row>0)
                if (array[p.Row - 1, p.Col] != "#" && array[p.Row - 1, p.Col] != "v" && !visitedPoints.Contains(new Point(p.Row - 1, p.Col)))
                    result.Add(new Point(p.Row - 1, p.Col));
            if(p.Row + 1< array.GetLength(0))
                if (array[p.Row + 1, p.Col] != "#" && array[p.Row + 1, p.Col] != "^" && !visitedPoints.Contains(new Point(p.Row + 1, p.Col)))
                    result.Add(new Point(p.Row + 1, p.Col));
            if (array[p.Row, p.Col + 1] != "#" && array[p.Row, p.Col + 1] != "<" && !visitedPoints.Contains(new Point(p.Row, p.Col + 1)))
                result.Add(new Point(p.Row, p.Col + 1));
            if (array[p.Row, p.Col - 1] != "#" && array[p.Row, p.Col - 1] != ">" && !visitedPoints.Contains(new Point(p.Row, p.Col - 1)))
                result.Add(new Point(p.Row, p.Col - 1));
            return result;
        }

        private static List<Point> FindNextDirectionV2(string[,] array, List<Point> visitedPoints, Point p)
        {
            List<Point> result = new List<Point>(); 
            if (p.Row > 0)
                if (array[p.Row - 1, p.Col] != "#"  && !visitedPoints.Contains(new Point(p.Row - 1, p.Col)))
                    result.Add(new Point(p.Row - 1, p.Col));
            if (p.Row + 1 < array.GetLength(0))
                if (array[p.Row + 1, p.Col] != "#" && !visitedPoints.Contains(new Point(p.Row + 1, p.Col)))
                    result.Add(new Point(p.Row + 1, p.Col));
            if (array[p.Row, p.Col + 1] != "#"  && !visitedPoints.Contains(new Point(p.Row, p.Col + 1)))
                result.Add(new Point(p.Row, p.Col + 1));
            if (array[p.Row, p.Col - 1] != "#" && !visitedPoints.Contains(new Point(p.Row, p.Col - 1)))
                result.Add(new Point(p.Row, p.Col - 1));
            return result;
        }

        private static Point FindStartPoint(string[,] array)
        {
            for(int i = 0; i < array.GetLength(1); i++)
            {
                if (array[0, i] == ".") return new Point(0, i);
            }
            return new Point(0,0);
        }

        private static Point FindEndPoint(string[,] array)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                if (array[array.GetLength(0) - 1, i] == ".") return new Point(array.GetLength(0) - 1, i);
            }
            return new Point(0, 0);
        }
    }
}
