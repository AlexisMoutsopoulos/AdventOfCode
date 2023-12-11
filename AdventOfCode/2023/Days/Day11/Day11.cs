using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day11
{
    internal class Day11
    {
        public static void Day11Problem(string filename)
        {
            var partOne = PartOne(filename);
            var partTwo = PartTwo(filename);
            Console.WriteLine($"Day11 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static long PartTwo(string filename)
        {
            var universe = SetUniverse(filename);
            var expandedUniverse = ExpandeUniverseV2(universe,1000000);
            List<long> distances = new List<long>();

            for (int i = 0; i < expandedUniverse.Count(); i++)
            {
                for (int j = i + 1; j < expandedUniverse.Count(); j++)
                {
                    distances.Add(Math.Abs(expandedUniverse[j].X - expandedUniverse[i].X) + Math.Abs(expandedUniverse[j].Y - expandedUniverse[i].Y));
                }
            }
            return distances.Sum();
        }

        private static long PartOne(string filename)
        {
            var universe = SetUniverse(filename);
            var expandedUniverse = ExpandeUniverse(universe);
            List<int> distances = new List<int>();

            for(int i = 0;i<expandedUniverse.Count();i++)
            {
                for(int j = i + 1; j < expandedUniverse.Count(); j++)
                {
                    distances.Add(Math.Abs(expandedUniverse[j].X - expandedUniverse[i].X) + Math.Abs(expandedUniverse[j].Y - expandedUniverse[i].Y));
                }
            }
            return distances.Sum();

        }

        private static string[,] SetUniverse(string filename)
        {
            string[] data = ReadFile.Read(filename).Split("\r\n");
            string[,] universe = new string[data.Length, data[0].ToCharArray().Length];
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[0].ToCharArray().Length; j++)
                {
                    universe[i, j] = data[i].ToCharArray()[j].ToString();
                }
            }
            return universe;
        }

        private static List<Galaxy> ExpandeUniverse(string[,] universe)
        {
            List<int> rowsNoGalaxies = new List<int>();
            List<int> columnsNoGalaxies = new List<int>();
            List<Galaxy> galaxies = new List<Galaxy>();
            var rows = GetRows(universe);
            for (int i = 0; i < rows.Count; i++)
            {
                if (!rows[i].Contains("#"))
                {
                    rowsNoGalaxies.Add(i);
                }
            }
            var columns = GetColumns(universe);
            for (int i = 0; i < columns.Count; i++)
            {
                if (!columns[i].Contains("#"))
                {
                    columnsNoGalaxies.Add(i);
                }
            }

            string[,] expandedUniverse = new string[universe.GetLength(0)+ rowsNoGalaxies.Count()*2,universe.GetLength(1)+ columnsNoGalaxies.Count * 2];
            for (int i = 0; i < universe.GetLength(0); i++)
            {
                for(int j = 0; j < universe.GetLength(1); j++)
                {
                    if (universe[i,j] == "#") galaxies.Add(new Galaxy() { X=i, Y=j });
                }
            }

            List<Galaxy> newGalaxies = new List<Galaxy>();
            foreach (Galaxy g in galaxies)
            {
                int newX = rowsNoGalaxies.Where(k => k < g.X).Count();
                int newY = columnsNoGalaxies.Where(k => k < g.Y).Count();
                newGalaxies.Add(new Galaxy() { X=g.X+newX, Y=g.Y + newY});
            }
            
            return newGalaxies;
        }

        private static List<Galaxy> ExpandeUniverseV2(string[,] universe,int oldest)
        {
            List<int> rowsNoGalaxies = new List<int>();
            List<int> columnsNoGalaxies = new List<int>();
            List<Galaxy> galaxies = new List<Galaxy>();
            var rows = GetRows(universe);
            for (int i = 0; i < rows.Count; i++)
            {
                if (!rows[i].Contains("#"))
                {
                    rowsNoGalaxies.Add(i);
                }
            }
            var columns = GetColumns(universe);
            for (int i = 0; i < columns.Count; i++)
            {
                if (!columns[i].Contains("#"))
                {
                    columnsNoGalaxies.Add(i);
                }
            }

            string[,] expandedUniverse = new string[universe.GetLength(0) + rowsNoGalaxies.Count() * 2, universe.GetLength(1) + columnsNoGalaxies.Count * 2];
            for (int i = 0; i < universe.GetLength(0); i++)
            {
                for (int j = 0; j < universe.GetLength(1); j++)
                {
                    if (universe[i, j] == "#") galaxies.Add(new Galaxy() { X = i, Y = j });
                }
            }

            List<Galaxy> newGalaxies = new List<Galaxy>();
            foreach (Galaxy g in galaxies)
            {
                int newX = rowsNoGalaxies.Where(k => k < g.X).Count() * (oldest - 1) ;
                int newY = columnsNoGalaxies.Where(k => k < g.Y).Count() * (oldest - 1);
                newGalaxies.Add(new Galaxy() { X = g.X + newX, Y = g.Y + newY });
            }

            return newGalaxies;
        }
        private static List<List<string>> GetColumns(string[,] array)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);

            List<List<string>> result = new List<List<string>>();

            for (int j = 0; j < columns; j++)
            {
                List<string> column = new List<string>();
                for (int i = 0; i < rows; i++)
                {
                    column.Add(array[i, j]);
                }
                result.Add(column);
            }

            return result;
        }

        static List<List<string>> GetRows(string[,] array)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);

            List<List<string>> result = new List<List<string>>();

            for (int i = 0; i < rows; i++)
            {
                List<string> row = new List<string>();
                for (int j = 0; j < columns; j++)
                {
                    row.Add(array[i, j]);
                }
                result.Add(row);
            }

            return result;
        }
    }
}
