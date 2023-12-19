using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode._2023.Days.Day16.Day16;

namespace AdventOfCode._2023.Days.Day16
{
    internal static class Day16
    { 
        public static void Day16Problem(string filename)
        {
            var partOne = PartOne(filename);
            var partTwo = PartTwo(filename);
            Console.WriteLine($"Day16 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static long PartTwo(string filename)
        {
            var data = GetData(filename);
            var result = new List<long>();
            for (int i = 0;i< data.GetLength(0); i++)
            {
                result.Add(TraverseMap(GetData(filename), i, -1, "right"));
                result.Add(TraverseMap(GetData(filename), i, data.GetLength(1), "left"));
            }
            for (int j = 0; j < data.GetLength(0); j++)
            {
                result.Add(TraverseMap(GetData(filename), -1, j, "down"));
                result.Add(TraverseMap(GetData(filename), data.GetLength(0), j, "up"));
            } 
            return result.Max();
        }


        private static long PartOne(string filename) => TraverseMap(GetData(filename), 0, -1, "right");

        private static Beam[,] GetData(string filename)
        {
            string[] data = ReadFile.Read(filename).Split("\r\n");
            Beam[,] result = new Beam[data.Length, data[0].ToCharArray().Length];
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].ToCharArray().Length; j++)
                {
                    result[i, j] = new Beam(data[i].ToCharArray()[j].ToString(),false);
                }
            }
            return result;
        }
                
        private static bool OutBound(Beam[,] map,int row,int col,string direction)
        {
            return row >= map.GetLength(0) 
                      || col >= map.GetLength(1) 
                      || row < 0 
                      || col < 0;
        }
        private static long TraverseMap(Beam[,] map,int startX,int startY,string direction)
        {
            List<Beam> list = new List<Beam>(); 
            Beam nextBeam = new Beam() { Row=startX,Column = startY,Direction =direction}; 
            list.Add(nextBeam);
            int result = 0;
            while (list.Count !=0)
            {
                nextBeam = list[0];
                list.RemoveAt(0);

                direction = nextBeam.Direction ?? direction;
                (nextBeam.Row, nextBeam.Column) = FindNext(map, direction, nextBeam.Row, nextBeam.Column);
                if (OutBound(map, nextBeam.Row, nextBeam.Column, direction))
                    continue;
                nextBeam.Element = map[nextBeam.Row, nextBeam.Column].Element;
                


                if (!map[nextBeam.Row, nextBeam.Column].Energized)
                {
                    map[nextBeam.Row, nextBeam.Column].Energized = true;
                    result++;
                }

                if (nextBeam.Element == ".")
                    list.Insert(0,nextBeam);
                else if(nextBeam.Element == "\\")
                {
                    nextBeam.Direction = FindDirection(nextBeam, direction);
                    list.Insert(0, nextBeam);
                }
                else if (nextBeam.Element == "/")
                { 
                    nextBeam.Direction = FindDirection(nextBeam, direction); ;
                    list.Insert(0, nextBeam);
                }
                else if (nextBeam.Element == "-" )
                {
                    if(direction == "up" || direction == "down")
                    {
                        if (map[nextBeam.Row, nextBeam.Column].IsVisited)
                            continue;
                        list.Insert(0, new Beam() { Row = nextBeam.Row, Column = nextBeam.Column, Direction= "left" });
                        list.Insert(0, new Beam() { Row = nextBeam.Row, Column = nextBeam.Column, Direction = "right" });
                        map[nextBeam.Row, nextBeam.Column].IsVisited = true;
                    }
                    else
                        list.Insert(0, nextBeam);
                }
                else if (nextBeam.Element == "|")
                {
                    if(direction == "right" || direction == "left")
                    {
                        if (map[nextBeam.Row, nextBeam.Column].IsVisited)
                            continue;
                        list.Insert(0, new Beam() { Row = nextBeam.Row, Column = nextBeam.Column, Direction = "up" });
                        list.Insert(0, new Beam() { Row = nextBeam.Row, Column = nextBeam.Column, Direction = "down" });
                        map[nextBeam.Row, nextBeam.Column].IsVisited = true;
                    }
                    else
                        list.Insert(0, nextBeam);
                }
                
            }
            return result;
        }
        
        private static (int row,int col) FindNext(Beam[,] map, string direction, int row, int col)
        {            
            (row, col) = direction switch
            {
                "left" => (row, col - 1),
                "right" => (row, col + 1),
                "up" => (row - 1, col),
                "down" => (row + 1, col),

            };
            return (row, col);
        }

        private static string FindDirection(Beam map, string direction)
        {
            if (map.Element == "\\")
            {
                if (direction == "right")
                    return "down";
                else if (direction == "left")
                    return "up";
                else if (direction == "up")
                    return "left";
                else if (direction == "down")
                    return "right";
            }
            else if (map.Element == "/")
            {
                if (direction == "right")
                    return "up";
                else if (direction == "left")
                    return "down";
                else if (direction == "up")
                    return "right";
                else if (direction == "down")
                    return "left";
            }
            return direction;
        }

    }
}
