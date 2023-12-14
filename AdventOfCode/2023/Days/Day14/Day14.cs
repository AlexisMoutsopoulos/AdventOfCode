using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day14
{
    internal static class Day14
    {
        
        public static void Day14Problem(string filename)
        {
            var partOne = PartOne(filename);
            var partTwo = PartTwo(filename);
            Console.WriteLine($"Day14 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static object PartTwo(string filename)
        {
            Dictionary<string, int> cache = new();
            var data = GetData(filename); string[,]
            cycle = data;
            for (int i = 1; i <= 1000000000; i++)
            {
                cycle = Cycle(cycle);
                string cycleString = ConvertArrayToString(cycle);
                if (!cache.ContainsKey(cycleString))
                    cache[cycleString] = i;
                
                else
                {
                    int firstRepeated = cache[cycleString];
                    int cycleLength = i - firstRepeated;

                    List<string> cycles = new();

                    for (int j = firstRepeated; j < i; j++)
                    {
                        cycles.Add(cache.First(x => x.Value == j).Key);
                    }

                    cycle = cycles[(1_000_000_000 - firstRepeated) % cycleLength].ConvertStringToArray();
                    break;
                }
            }

                return CalculateTotalLoad(cycle);
        }

        static string ConvertArrayToString(string[,] array)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    stringBuilder.Append(array[i, j]); 
                    stringBuilder.Append(","); 
                    if (j == columns - 1)
                        stringBuilder.AppendLine();                    
                }
            }

            return stringBuilder.ToString();

        }

        static string[,] ConvertStringToArray(this string arrayAsString)
        {
            string[] rows = arrayAsString.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            int rowCount = rows.Length;
            int colCount = rows[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Length;

            string[,] resultArray = new string[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
            {
                string[] elements = rows[i].Split(',', StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < colCount; j++)
                {
                    resultArray[i, j] = elements[j];
                }
            }

            return resultArray;
        }

        private static string[,] Cycle(string[,] data)
        {
            var result = data.MoveRockToNorth();
            var result1 = result.MoveRockToWest();
            var result2 = result1.MoveRockToSouth();
            return result2.MoveRockToEast();
        }
        private static long PartOne(string filename)
        {
            var data = GetData(filename);
            var result = data.MoveRockToNorth();
            return CalculateTotalLoad(result);
        }

        private static long CalculateTotalLoad(string[,] array)
        {
            long sum = 0L;
            var rows = array.GetRows();
            var cols = array.GetColumns();
            
            for(int i = 0; i < rows.Count; i++)
            {
                int numOfOInRow = rows[i].ToCharArray().Where(x => x.ToString() == "O").Count();
                sum += (rows.Count - i)*numOfOInRow;
            }
            
            return sum;
        }
        private static string[,] MoveRockToNorth(this string[,] data)
        {
            var col=data.GetColumns();
            List<int> nextAvailablePos = new List<int>();
            int cubeShapedRockPos = -1;

            for (int j = 0; j < col.Count; j++)
            {
                for (int i = 0; i < col[0].Length; i++)
                {
                    if (col[j][i].ToString() == ".")
                        nextAvailablePos.Add(i);
                    else if (col[j][i].ToString() == "O")
                    {
                        if (nextAvailablePos.Count == 0)
                            continue;

                        col[j] = MoveRock(col[j], nextAvailablePos[0], i);
                        nextAvailablePos.Add(i);
                        nextAvailablePos.RemoveAt(0);
                    }
                    else if (col[j][i].ToString() == "#")
                        nextAvailablePos.RemoveAll(x=> x < i);
                    
                }
                nextAvailablePos.Clear();
            }
            return col.Create2DArray();
        }

        private static string[,] MoveRockToWest(this string[,] data)
        {
            //rotate and move to north
            var rotatedData = data.Rotate();
            var result = rotatedData.MoveRockToNorth();
            //inverserotate
            return result.InverseRotate();
            
        }

        private static string[,] MoveRockToSouth(this string[,] data)
        {
            //rotate x2 and move to north
            var rotatedData = data.Rotate().Rotate();
            var result = rotatedData.MoveRockToNorth();
            //inverserotate x2
            return result.InverseRotate().InverseRotate();

        }

        private static string[,] MoveRockToEast(this string[,] data)
        {
            //inverse rotate and move to north
            var rotatedData = data.InverseRotate();
            var result = rotatedData.MoveRockToNorth();
            //rotate
            return result.Rotate();

        }

        private static string[,] Create2DArray(this List<string> columnsList)
        {
            int rows = columnsList[0].Length;
            int columns = columnsList.Count;

            string[,] array = new string[rows, columns];

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    array[j, i] = columnsList[i][j].ToString();
                }
            }

            return array;
        }

        private static string[,] Rotate(this string[,] data)
        {
            int rows = data.GetLength(0);
            int columns = data.GetLength(1);

            string[,] rotatedData = new string[columns, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    rotatedData[j, rows - 1 - i] = data[i, j];
                }
            }

            return rotatedData;
        }

        static string[,] InverseRotate(this string[,] data)
        {
            int rows = data.GetLength(0);
            int columns = data.GetLength(1);

            string[,] inverseRotatedData = new string[columns, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    inverseRotatedData[columns - 1 - j, i] = data[i, j];
                }
            }

            return inverseRotatedData;
        }

        static string MoveRock(string input, int index,int curI,char rock = 'O')
        {
            char[] charArray = input.ToCharArray();
            charArray[index] = rock;
            charArray[curI] = '.';
            return new string(charArray);
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

        private static string[,] GetData(string filename)
        {
            string[] lines = ReadFile.Read(filename).Split("\r\n");
            return CreateArray(lines.ToList());
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
