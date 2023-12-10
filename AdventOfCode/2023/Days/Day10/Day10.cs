using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCode._2023.Days.Day10
{
    internal static class Day10
    {
        public static void Day10Problem(string filename)
        {
            var partOne = PartOne(filename);
            var partTwo = PartTwo(filename);
            Console.WriteLine($"Day10 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static long PartTwo(string filename)
        {
            var map = CreateMap(filename);
            var start = FindStart(map);

            var mapElements = new MapElement[map.GetLength(0), map.GetLength(1)];
            TraverseMap(map, start.Item1, start.Item2, mapElements, 0);
            long count = 0;
            for (int i = 0; i < mapElements.GetLength(0); i++)
            {
                bool inside = false;
                char temp = '.';
                for (int j = 0; j < mapElements.GetLength(1); j++)
                {
                    string element = mapElements[i, j]?.Value ?? ".";
                    if ("|JLF7".Contains(element))
                    {
                        switch (element)
                        {
                            case "|": 
                                inside = !inside;
                                break;
                            case "F": 
                                temp = 'F';
                                break;
                            case "L": 
                                temp = 'L';
                                break;
                            case "7": 
                                if (temp == 'L') inside = !inside;
                                break;
                            case "J": 
                                if (temp == 'F') 
                                    inside = !inside; 
                                break;
                            default: break;
                        }
                    }
                    else if (element == ".")
                        if (inside) count++;                    
                }
            }
            return count;
        }

        private static long PartOne(string filename)
        {
            var map = CreateMap(filename);
            var start  = FindStart(map);
            var mapElements = new MapElement[map.GetLength(0),map.GetLength(1)];
            TraverseMap(map,start.Item1,start.Item2,mapElements,0); 
            return (FindMaxDistance(mapElements)+1)/2;
        } 

        private static long FindMaxDistance(MapElement[,] map)
        { 
                long maxElement = map[0, 0]?.StartDistance ?? 0;

                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if ((map[i, j]?.StartDistance ?? 0) > maxElement)
                        {
                            maxElement = map[i, j]?.StartDistance ?? 0;
                        }
                    }
                }

                return maxElement;
        }
        private static (int,int) FindStart(string[,] map)
        {
            for(int i=0; i<map.GetLength(0); i++)
            {
                for(int j = 0; j<map.GetLength(1); j++)
                {
                    if (map[i, j] == "S") return (i, j);
                }
            }
            return (0,0);
        }
        private static void TraverseMap(string[,] map, int row, int col, MapElement[,] mapElements, long distance)
        {
            mapElements[row,col] = new MapElement() { Value = "S",Column = col, Row = row, IsVisited = true };
            distance++;
            if (map[row + 1, col] != ".") row++; 
            else if (map[row - 1, col] != ".") row--;
            else if (map[row, col - 1] != ".") col--;
            else if(map[row, col + 1] != ".") col++; 
            while (map[row, col] != "S")
            {
                if (col < 0 || row < 0 || map[row, col] == "." || 
                        (mapElements[row, col] != null && (mapElements[row, col]?.IsVisited ?? false))) return;
                bool up = map[row, col] == "|" || map[row, col] == "L" || map[row, col] == "J";
                bool down = map[row, col] == "|" || map[row, col] == "7" || map[row, col] == "F";
                bool left = map[row, col] == "-" || map[row, col] == "7" || map[row, col] == "J";
                bool right = map[row, col] == "-" || map[row, col] == "L" || map[row, col] == "F";


                if (mapElements[row, col] == null)  mapElements[row, col] = new MapElement() { Row = row, Column = col, Value = map[row, col], StartDistance = distance, IsVisited = true };
                else return;

                distance++;
                if (up && map[row-1,col] != "." && !(mapElements[row-1,col]?.IsVisited ?? false) ){row = row - 1;continue;}

                if (down && map[row + 1, col] != "." && !(mapElements[row +1, col]?.IsVisited ?? false)) { row = row + 1; continue; }
                if (right && map[row, col + 1] != "." && !(mapElements[row , col + 1]?.IsVisited ?? false)) {col = col + 1; continue;}
                if (left && map[row, col - 1] != "." && !(mapElements[row , col - 1]?.IsVisited ?? false)) { col = col - 1; continue; }
            }

        }
    
        private static string[,] CreateMap(string filename)
        {
            string[] data = ReadFile.Read(filename).Split("\r\n");
            string[,] result = new string[data.Length,data[0].ToCharArray().Length];
            for(int i = 0; i < data.Length; i++)
            {
                for(int j = 0;j < data[i].ToCharArray().Length; j++)
                {
                    result[i,j] = data[i].ToCharArray()[j].ToString();
                }
            }
            return result;
        }
    }
}
