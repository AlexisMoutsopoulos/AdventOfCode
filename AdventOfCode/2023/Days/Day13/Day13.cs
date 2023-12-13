using AdventOfCode.Helpers;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day13
{
    internal static class Day13
    {
        public static void Day13Problem(string filename)
        {
            var partOne = PartOne(filename);
            var partTwo = PartTwo(filename);
            Console.WriteLine($"Day13 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static long PartTwo(string filename)
        { 
            var data = GetData(filename);
            List<Mirrors> list = new List<Mirrors>();
            Mirrors mirror = new Mirrors();
            long result = 0;
            for (int i=0;i< data.Count;i++)
            {
                mirror.Rows = data[i].GetRows();
                mirror.Columns = data[i].GetColumns();
                result += FindNumberOfReflectionsV2(mirror);
            }
            return result;
        }

        private static long PartOne(string filename)
        {
            var data = GetData(filename);
            List<Mirrors> list = new List<Mirrors>();
            Mirrors mirror = new Mirrors();
            long result = 0;
            foreach (var item in data)
            {
                mirror.Rows = item.GetRows();
                mirror.Columns = item.GetColumns();
                result += FindNumberOfReflections(mirror);
            }
            return result;
        }
        private static int FindRowIndexV2(int index, Mirrors mirror)
        {
            int start1 = index;
            int start2 = index + 1;
            int lenght1 = mirror.Rows.Count - (start1 + 1);
            int minLen = Math.Min(start1 + 1, lenght1);
            bool allSame = true;
            int smudge = 0;
            for (int k = 0; k < minLen; k++)
            {
                if (StringsDiffer(mirror.Rows[start1 - k], mirror.Rows[start2 + k]) > 1)
                {
                    allSame = false;
                    break;
                }
                else if (StringsDiffer(mirror.Rows[start1 - k], mirror.Rows[start2 + k]) == 1)
                    ++smudge;
                

                if (smudge == 2)
                {
                    allSame = false;
                    break;
                }
            }
            if (allSame && smudge == 1)
                return (start1 + 1) * 100;
            else 
                return -1;
        }
        private static int FindRowIndex(int index, Mirrors mirror)
        {
            int start1 = index;
            int start2 = index + 1;
            int lenght1 = mirror.Rows.Count - (start1 + 1);
            int minLen = Math.Min(start1 + 1, lenght1);
            bool allSame = true;
            for (int k = 0; k < minLen; k++)
            {
                if (mirror.Rows[start1 - k] != mirror.Rows[start2 + k]) //diferences>1
                {
                    allSame = false;
                    break;
                }
            }
            if (allSame)
                return (start1 + 1) * 100;
            else return -1;
        }

        private static int StringsDiffer(string str1, string str2)
        {
            int differences = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] != str2[i])
                    differences++;
            }
            return differences;
        }
        private static int FindColumnIndexV2(int index, Mirrors mirror)
        {
            int start1 = index;
            int start2 = index + 1;
            int lenght1 = mirror.Columns.Count - (start1 + 1);
            int minLen = Math.Min(start1 + 1, lenght1);
            bool allSame = true;
            int smudge = 0;
            for (int k = 0; k < minLen; k++)
            {
                if (StringsDiffer(mirror.Columns[start1 - k], mirror.Columns[start2 + k]) > 1)
                {
                    allSame = false;
                    break;
                }
                else if (StringsDiffer(mirror.Columns[start1 - k], mirror.Columns[start2 + k]) == 1)
                    ++smudge;               

                if (smudge == 2)
                {
                    allSame = false;
                    break;
                }
            }
            if (allSame && smudge == 1)
                return start1 + 1;
            else 
                return -1;
        }
        private static int FindColumnIndex(int index, Mirrors mirror)
        {
            int start1 = index;
            int start2 = index + 1;
            int lenght1 = mirror.Columns.Count - (start1 + 1);
            int minLen = Math.Min(start1 + 1, lenght1);
            bool allSame = true;
            for (int k = 0; k < minLen; k++)
            {
                if (mirror.Columns[start1 - k] != mirror.Columns[start2 + k])
                {
                    allSame = false;
                    break;
                }
            }
            if (allSame)
                return start1 + 1;
            else return -1;
        }
        private static int FindNumberOfReflections(Mirrors mirror)
        {
            int index = -1;
            for (int i = 0; i < mirror.Rows.Count - 1; i++)
            {
                if (mirror.Rows[i] == mirror.Rows[i + 1])
                {
                    index = FindRowIndex(i, mirror);
                    if (index == -1)
                        continue;
                    else
                        return index;
                }
            }
            for (int j = 0; j < mirror.Columns.Count - 1; j++)
            {
                if (mirror.Columns[j] == mirror.Columns[j + 1])
                {
                    index = FindColumnIndex(j, mirror);
                    if (index == -1)
                        continue;
                    else
                        return index;
                }
            }


            return index;
        }

        private static int FindNumberOfReflectionsV2(Mirrors mirror)
        {
            int index = -1;
            for (int i = 0; i < mirror.Rows.Count - 1; i++)
            {
                if (mirror.Rows[i] == mirror.Rows[i + 1] || StringsDiffer(mirror.Rows[i], mirror.Rows[i + 1]) == 1)
                {
                    index = FindRowIndexV2(i, mirror);
                    if (index == -1)
                        continue;
                    else
                        return index;
                }
            }
            for (int j = 0; j < mirror.Columns.Count - 1; j++)
            {
                if (mirror.Columns[j] == mirror.Columns[j + 1] || StringsDiffer(mirror.Columns[j], mirror.Columns[j + 1]) == 1)
                {
                    index = FindColumnIndexV2(j, mirror);
                    if (index == -1)
                        continue;
                    else
                        return index;
                }
            }
            

            return index;
        }
        private static List<string> GetColumns(this string[,] array)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);

            List<string> result = new List<string>();

            for (int j = 0; j < columns; j++)
            {
                StringBuilder column = new StringBuilder();
                for (int i = 0; i < rows; i++)
                {
                    column.Append(array[i, j]);
                }
                result.Add(column.ToString());
            }

            return result;
        }

        static List<string> GetRows(this string[,] array)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);

            List<string> result = new List<string>();

            for (int i = 0; i < rows; i++)
            {
                StringBuilder row = new StringBuilder();
                for (int j = 0; j < columns; j++)
                {
                    row.Append(array[i, j]);
                }
                result.Add(row.ToString());
            }

            return result;
        }

        private static List<string[,]> GetData(string filename)
        {
            List<string[,]> result = new List<string[,]>();
            string[] lines = ReadFile.Read(filename).Split("\r\n");
            List<string> currentArray = new List<string>();

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    if (currentArray.Count > 0)
                    {
                        result.Add(CreateArray(currentArray));
                        currentArray.Clear();
                    }
                }
                else
                    currentArray.Add(line);

            }

            if (currentArray.Count > 0)
                result.Add(CreateArray(currentArray));


            return result;
        }

        private static string[,] CreateArray(List<string> lines)
        {
            int rows = lines.Count;
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


    }
}

