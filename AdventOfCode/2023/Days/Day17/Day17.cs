using AdventOfCode._2023.Days.Day16;
using AdventOfCode.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day17
{
    
    internal class Day17
    {
        public static void Day17Problem(string filename)
        {
            var partOne = PartOne(filename);
            var partTwo = PartTwo(filename);
            Console.WriteLine($"Day17 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static int PartTwo(string filename) => 0;


        private static long PartOne(string filename)
        {
            var intArray = GetMap(filename);
             

            List<int> distances = new List<int>();
            Point start = new Point(0, 0, 0, "right");
            Point end = new Point(intArray.GetLength(0) - 1, intArray.GetLength(1) - 1, 1, ""); ;
            distances = TraverseMap(intArray, start, end, distances, 0);

            return 0; 
        }

        private static Point TurnLeft(Point p)
        {            
            return p.Direction switch
            {
                "right" => new Point(p.Row - 1, p.Col,1, "up"),
                "left" => new Point(p.Row + 1, p.Col, 1, "down"),
                "up" => new Point(p.Row , p.Col - 1, 1, "left"),
                "down" => new Point(p.Row , p.Col + 1, 1, "right")
            };
        }

        private static Point TurnRight(Point p)
        { 
            return p.Direction switch
            {
                "right" => new Point(p.Row + 1, p.Col, 1, "down"),
                "left" => new Point(p.Row - 1, p.Col, 1, "up"),
                "up" => new Point(p.Row, p.Col + 1, 1, "right"),
                "down" => new Point(p.Row, p.Col - 1, 1, "left")
            };
        }

        private static Point FindNextDirection(int[,] array, HashSet<Point> visitedPoints, Point p)
        {
            int path = p.Consecutive;
            if (p.Direction== "right")
                return new Point(p.Row, p.Col + 1,++path, "right");
            else if (p.Direction== "left")
                return new Point(p.Row, p.Col - 1, ++path, "left"); 
            else if (p.Direction== "up")
                return new Point(p.Row - 1, p.Col , ++path, "up"); 
            else if (p.Direction== "down") 
                return new Point(p.Row + 1, p.Col , ++path, "down");
            return default;
        }

        private static List<int> TraverseMap(int[,] map,  Point start, Point end, List<int> distances,int heat)
        {
            HashSet<Point> visited = new();


            PriorityQueue<Point,int> path = new();
            path.Enqueue(start, 0);
            while(path.Count > 0)
            {
                start = path.Dequeue();  
                
                Display2DArray(map, visited);
                
                if (start.Col == end.Col && start.Row == end.Row)
                {
                    distances.Add(start.Heat);
                    break;
                }

                if (start.Consecutive < 3)
                {
                    Point tmp = FindNextDirection(map,visited, start);
                    if(tmp != null && !OutBound(map, tmp.Row, tmp.Col))
                        if (!visited.Any(x => x.Row == tmp.Row && x.Col == tmp.Col))
                        {
                            tmp.Heat = start.Heat + map[tmp.Row, tmp.Col];
                            path.Enqueue(tmp, tmp.Heat);
                            visited.Add(tmp);
                        }
                            
                }
                Point tmp1 = TurnLeft(start);
                if (tmp1 != null && !OutBound(map, tmp1.Row, tmp1.Col))
                    if (!visited.Any(x => x.Row == tmp1.Row && x.Col == tmp1.Col))
                    {
                        visited.Add(tmp1);
                        tmp1.Heat = start.Heat + map[tmp1.Row, tmp1.Col];
                        path.Enqueue(tmp1, tmp1.Heat);
                    }
                        
                Point tmp2 = TurnRight(start);
                if (tmp2 != null && !OutBound(map, tmp2.Row, tmp2.Col))
                    if (!visited.Any(x => x.Row == tmp2.Row && x.Col == tmp2.Col))
                    {
                        visited.Add(tmp2);
                        tmp2.Heat = start.Heat + map[tmp2.Row, tmp2.Col];
                        path.Enqueue(tmp2, tmp2.Heat);
                    }
                        

            }


            return distances;

        }
        static void Display2DArray(int[,] array, HashSet<Point> visited)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);

            Console.WriteLine("2D Array of Strings:");

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (visited.Any(x=> x.Row ==i && x.Col ==j))
                        Console.Write("O" + " ");
                    else
                        Console.Write(array[i, j] + " ");
                }
                Console.WriteLine(); // Move to the next row after printing all columns
            }
        }

        private static bool OutBound(int[,] map, int row, int col)
        {
            return row >= map.GetLength(0)
                      || col >= map.GetLength(1)
                      || row < 0
                      || col < 0;
        }

        private static int[,] GetMap(string filename)
        {
            var data = ReadFile.Read(filename).Split("\r\n");
            int[,] intArray = new int[data[0].ToCharArray().Length, data.Length];
            for (int i = 0; i < intArray.GetLength(0); i++)
            {
                for (int j = 0; j < data[i].ToCharArray().Length; j++)
                {
                    intArray[i, j] = int.Parse(data[i].ToCharArray()[j].ToString());
                }
            }
            return intArray;
        }
         
    }
}
