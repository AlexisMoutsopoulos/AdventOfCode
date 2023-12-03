using AdventOfCode.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days.Day3
{
    public static  class Day3 
    {
        public static void Day3Problem(string filename)
        {
            //var temp1 = FindSumPart1("Days\\Day3\\Day3Input.txt").Sum(x=>x);
            // PartOne("Days\\Day3\\Day3Input.txt");
            Day3LoadData("Days\\Day3\\Day3Input.txt"); 

             
        } 
        public static void Day3LoadData(string filename)
        {
            string file = ReadFile.Read(filename);
            string[] lines = file.Split("\r\n");

            List<int> sum = new List<int>();
            List<Number> numbers = new List<Number>();
            List<Symbol> symbols = new List<Symbol>();
            int start = 0;
            int end = 0;
            StringBuilder numberValue= new StringBuilder();
            for (int line= 0; line<lines.Length;line++)
            {
                for(int c = 0; c < lines[line].Length;c++)
                {
                    start = 0;
                    while (c< lines[line].Length && char.IsDigit(lines[line][c]))
                    {
                        if(numberValue.Length == 0)
                            start = c;
                        numberValue.Append(lines[line][c]);
                        end = c;
                        c++;
                    } 
                     if(numberValue.Length != 0)
                     {
                        numbers.Add(new Number()
                        {
                            Value = Convert.ToInt32(numberValue.ToString()),
                            Start = new Point() { X = line, Y = start },
                            End = new Point() { X = line, Y = end }
                        }) ;
                        numberValue = new StringBuilder();
                     }
                    if (c < lines[line].Length && lines[line][c].ToString() != "." && !char.IsDigit(lines[line][c]))
                    {
                        symbols.Add(new Symbol() { Point = new Point() { X = line, Y = c }, Value = lines[line][c].ToString() });
                    }
                     
                }                
            }
            int part1 = Part1(numbers, symbols);
            int part2 = Part2(numbers, symbols);

            Console.WriteLine($"Day3: Results Part1 = {part1} Part2 = {part2}");
        }

        private static int Part1(List<Number> numbers,List<Symbol>symbols)
        {
            return numbers.Where(number => symbols.Any(symbol => Math.Abs(symbol.Point.X - number.Start.X )<= 1 
                                                && symbol.Point.Y >= number.Start.Y -1
                                                && symbol.Point.Y <= number.End.Y + 1)).Sum(num=> num.Value);
        }

        private static int Part2(List<Number> numbers, List<Symbol> symbols)
        {
            return  symbols.Where(symbol => symbol.Value == "*")
                        .Select(symbol => numbers.Where(num=>Math.Abs(symbol.Point.X - num.Start.X) <= 1
                                                && symbol.Point.Y >= num.Start.Y - 1
                                                && symbol.Point.Y <= num.End.Y + 1).ToList())
                         .Where(list=> list.Count == 2).Sum(num => num[0].Value * num[1].Value);
        }
            public static List<int> FindSumPart2(string filename)
        {
            string file = ReadFile.Read(filename);
            string[] lines = file.Split("\r\n");

            List<int> sum = new List<int>();
            for (int line = 0; line < lines.Length; line++)
            {
                for (int c = 0; c < lines[line].Length; c++)
                {
                    if (lines[line][c].ToString() == "*")
                    {
                        CheckNumberV2(lines[line].ToCharArray(), line, lines, c);
                    }   
                }
            }
            return sum;
        }


        private static int CheckNumber(char[] line, int lineIndex,string[] lines,int start,int end)
        {
            string result = String.Empty;
            if (start > 0 && line[start-1].ToString() != "." && !char.IsDigit(line[start - 1]))
            {
                result = BuildNumber(line, start, end);

            }
            else if(end < line.Length -1 && line[end + 1].ToString() != "." && !char.IsDigit(line[end + 1]))
            {
                result = BuildNumber(line, start, end);
            }
            if(lineIndex > 0 && string.IsNullOrEmpty(result))
            {
                var prevLine = lines[lineIndex - 1].ToCharArray();
                bool find = false;
                if (start > 0 && end< line.Length-1)
                {
                    
                    for(int k= start-1; k<= end+1; k++)
                    {
                        if (prevLine[k].ToString() != "." && !char.IsDigit(prevLine[k]))
                        {
                            find = true;
                            break;
                        }
                    }
                    result = (find) ? BuildNumber(line, start, end) : String.Empty;
                }else if(start ==0)
                {
                    for (int k = start; k <= end + 1; k++)
                    {
                        if (prevLine[k].ToString() != "." && !char.IsDigit(prevLine[k]))
                        {
                            find = true;
                            break;
                        }
                    }
                    result = (find) ? BuildNumber(line, start, end) : String.Empty;
                }else if(end == line.Length -1)
                {
                    for (int k = start-1; k < end; k++)
                    {
                        if (prevLine[k].ToString() != "." && !char.IsDigit(prevLine[k]))
                        {
                            find = true;
                            break;
                        }
                    }
                    result = (find) ? BuildNumber(line, start, end) : String.Empty;
                }
            }
            if(lineIndex < lines.Length-1 && string.IsNullOrEmpty(result))
            {
                bool find = false;
                var nextLine = lines[lineIndex + 1].ToCharArray();
                if (start > 0 && end < line.Length - 1)
                {                    
                    for (int k = start - 1; k <= end + 1; k++)
                    {
                        if (nextLine[k].ToString() != "." && !char.IsDigit(nextLine[k]))
                        {
                            find = true;
                            break;
                        }
                    }
                    result = (find) ? BuildNumber(line, start, end) : String.Empty;
                }
                else if (start == 0)
                {
                    for (int k = start; k <= end + 1; k++)
                    {
                        if (nextLine[k].ToString() != "." && !char.IsDigit(nextLine[k]))
                        {
                            find = true;
                            break;
                        }
                    }
                    result = (find) ? BuildNumber(line, start, end) : String.Empty;
                }
                else if (end == line.Length - 1)
                {
                    for (int k = start - 1; k <= end; k++)
                    {
                        if (nextLine[k].ToString() != "." && !char.IsDigit(nextLine[k]))
                        {
                            find = true;
                            break;
                        }
                    }
                    result = (find) ? BuildNumber(line, start, end) : String.Empty;
                }
            }

            return (string.IsNullOrEmpty(result)) ? 0: Convert.ToInt32(result);
        }



        private static void CheckNumberV2(char[] line, int lineIndex, string[] lines, int asteriskIndex)
        {
            string resultPrev = String.Empty;
            //previous number
            if (asteriskIndex > 0 && char.IsDigit(line[asteriskIndex - 1]))
            {
                int start = asteriskIndex;
                for(int i= asteriskIndex-1; i >= 0; i--)
                {
                    if (!char.IsDigit(line[i]) || i==0)
                    {
                        start = (char.IsDigit(line[i])) ? i : ++i;
                        break;
                    }
                }
                resultPrev = BuildNumber(line, start, asteriskIndex -1);

            }
            if(lineIndex > 0 && string.IsNullOrEmpty(resultPrev))
            {
                var prevLine = lines[lineIndex - 1].ToCharArray();
                if (char.IsDigit(prevLine[asteriskIndex]) || char.IsDigit(prevLine[asteriskIndex-1]) || char.IsDigit(prevLine[asteriskIndex+1]))
                {
                    int start = 0;
                    
                    if (char.IsDigit(prevLine[asteriskIndex + 1]) && !char.IsDigit(prevLine[asteriskIndex]))
                        start = asteriskIndex + 1;
                    else if (char.IsDigit(prevLine[asteriskIndex]) && !char.IsDigit(prevLine[asteriskIndex - 1]))
                        start = asteriskIndex;
                    else
                    {
                        for (int i = asteriskIndex - 1; i >= 0; i--)
                        {
                            if (!char.IsDigit(prevLine[i]) || i == 0)
                            {
                                start = (char.IsDigit(prevLine[i])) ? i : ++i;
                                break;
                            }
                        }
                    }
                    
                    int end = 0;
                    if (char.IsDigit(prevLine[asteriskIndex - 1]) && !char.IsDigit(prevLine[asteriskIndex]))
                        end = asteriskIndex - 1;
                    else if (char.IsDigit(prevLine[asteriskIndex]) && !char.IsDigit(prevLine[asteriskIndex + 1]))
                        end = asteriskIndex;
                    else
                    {
                        for (int i = asteriskIndex + 1; i < line.Length; i++)
                        {
                            if (i == line.Length  || !char.IsDigit(prevLine[i]))
                            {
                                end = --i ;
                                break;
                            }
                        }
                        if (end == 0) end = line.Length-1;
                    }

                    resultPrev = BuildNumber(prevLine, start, end);
                }
                
            }
            if (lineIndex < lines.Length - 1 && string.IsNullOrEmpty(resultPrev))
            {
                var nextLine = lines[lineIndex + 1].ToCharArray();
                if (char.IsDigit(nextLine[asteriskIndex]) || char.IsDigit(nextLine[asteriskIndex - 1]) || char.IsDigit(nextLine[asteriskIndex + 1]))
                {
                    int start = 0;

                    if (char.IsDigit(nextLine[asteriskIndex + 1]) && !char.IsDigit(nextLine[asteriskIndex]))
                        start = asteriskIndex + 1;
                    else if (char.IsDigit(nextLine[asteriskIndex]) && !char.IsDigit(nextLine[asteriskIndex - 1]))
                        start = asteriskIndex;
                    else
                    {
                        for (int i = asteriskIndex - 1; i >= 0; i--)
                        {
                            if (!char.IsDigit(nextLine[i]) || i == 0)
                            {
                                start = (char.IsDigit(nextLine[i])) ? i : ++i;
                                break;
                            }
                        }
                    }
                    int end = 0;
                    if (char.IsDigit(nextLine[asteriskIndex - 1]) && !char.IsDigit(nextLine[asteriskIndex]))
                        end = asteriskIndex - 1;
                    else if (char.IsDigit(nextLine[asteriskIndex]) && !char.IsDigit(nextLine[asteriskIndex + 1]))
                        end = asteriskIndex;
                    else
                    {
                        for (int i = asteriskIndex + 1; i < line.Length; i++)
                        {
                            if (i == line.Length || !char.IsDigit(nextLine[i]))
                            {
                                end = --i;
                                break;
                            }
                        }
                        if (end == 0) end = line.Length - 1;
                    }
                    resultPrev = BuildNumber(nextLine, start, end);
                }
            }


            //next number
            string resultNext = String.Empty;
            if (asteriskIndex < line.Length && char.IsDigit(line[asteriskIndex + 1]))
            {
                int end = 0;
                for (int i = asteriskIndex + 1; i < line.Length; i++)
                {
                    if (i == line.Length || !char.IsDigit(line[i])  )
                    {
                        end = --i;
                        break;
                    }
                }
                resultNext = BuildNumber(line, asteriskIndex + 1, end);
            }
            if (lineIndex < lines.Length - 1 && string.IsNullOrEmpty(resultNext))
            {
                var nextLine = lines[lineIndex + 1].ToCharArray();
                if (char.IsDigit(nextLine[asteriskIndex]) || char.IsDigit(nextLine[asteriskIndex - 1]) || char.IsDigit(nextLine[asteriskIndex + 1]))
                {
                    int start = 0;

                    if (char.IsDigit(nextLine[asteriskIndex + 1]) && !char.IsDigit(nextLine[asteriskIndex]))
                        start = asteriskIndex + 1;
                    else if (char.IsDigit(nextLine[asteriskIndex]) && !char.IsDigit(nextLine[asteriskIndex - 1]))
                        start = asteriskIndex;
                    else
                    {
                        for (int i = asteriskIndex - 1; i >= 0; i--)
                        {
                            if (!char.IsDigit(nextLine[i]) || i == 0)
                            {
                                start = (char.IsDigit(nextLine[i])) ? i : ++i;
                                break;
                            }
                        }
                    }
                    int end = 0;
                    if (char.IsDigit(nextLine[asteriskIndex - 1]) && !char.IsDigit(nextLine[asteriskIndex]))
                        end = asteriskIndex - 1;
                    else if (char.IsDigit(nextLine[asteriskIndex]) && !char.IsDigit(nextLine[asteriskIndex + 1]))
                        end = asteriskIndex;
                    else
                    {
                        for (int i = asteriskIndex + 1; i < line.Length; i++)
                        {
                            if (i == line.Length  || !char.IsDigit(nextLine[i]))
                            {
                                end = --i ;
                                break;
                            }
                        }
                        if (end == 0) end = line.Length-1;
                    }
                    resultNext = BuildNumber(nextLine, start, end);
                }
            }
             if (lineIndex > 0 && string.IsNullOrEmpty(resultNext))
            {
                var prevLine = lines[lineIndex - 1].ToCharArray();
                if (char.IsDigit(prevLine[asteriskIndex]) || char.IsDigit(prevLine[asteriskIndex - 1]) || char.IsDigit(prevLine[asteriskIndex + 1]))
                {
                    int start = 0;

                    if (char.IsDigit(prevLine[asteriskIndex + 1]) && !char.IsDigit(prevLine[asteriskIndex]))
                        start = asteriskIndex + 1;
                    else if (char.IsDigit(prevLine[asteriskIndex]) && !char.IsDigit(prevLine[asteriskIndex - 1]))
                        start = asteriskIndex;
                    else
                    {
                        for (int i = asteriskIndex - 1; i >= 0; i--)
                        {
                            if (!char.IsDigit(prevLine[i]) || i == 0)
                            {
                                start = (char.IsDigit(prevLine[i])) ? i : ++i;
                                break;
                            }
                        }
                    }

                    int end = 0;
                    if (char.IsDigit(prevLine[asteriskIndex - 1]) && !char.IsDigit(prevLine[asteriskIndex]))
                        end = asteriskIndex - 1;
                    else if (char.IsDigit(prevLine[asteriskIndex]) && !char.IsDigit(prevLine[asteriskIndex + 1]))
                        end = asteriskIndex;
                    else
                    {
                        for (int i = asteriskIndex + 1; i < line.Length; i++)
                        {
                            if (i == line.Length || !char.IsDigit(prevLine[i]))
                            {
                                end = --i;
                                break;
                            }
                        }
                        if (end == 0) end = line.Length - 1;
                    }

                    resultNext = BuildNumber(prevLine, start, end);
                }

            }

            if (!string.IsNullOrEmpty(resultNext) && !string.IsNullOrEmpty(resultPrev)) {

                Console.WriteLine("\n\n\n" + Convert.ToInt32(resultPrev) + "   " + Convert.ToInt32(resultNext));
            }
        }




        private static string BuildNumber(char[] line, int start, int end)
        {
            string result = String.Empty;
            for (int i = start; i < end+1; i++)
            {
                result = result + line[i].ToString();
            }

            return result;
        }

        
    }
}
