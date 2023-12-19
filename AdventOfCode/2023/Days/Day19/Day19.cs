using AdventOfCode._2023.Days.Day16;
using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode._2023.Days.Day16.Day16;

namespace AdventOfCode._2023.Days.Day19
{
    internal static class Day19
    {
        static Dictionary<string, List<Condition>> workflows = new();
        public static void Day19Problem(string filename)
        { 
            var partOne = PartOne(filename);
            var partTwo = PartTwo(filename);
            Console.WriteLine($"Day19 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static double PartTwo(string filename) 
        {
            Dictionary<string, Range> startRanges = new()
            {
                    {"x", new(1, 4000) },
                    {"m", new(1, 4000) },
                    {"a", new(1, 4000) },
                    {"s", new(1, 4000) }
            };
            return GetRangeLengths(startRanges, workflows["in"]);
        }


        private static long PartOne(string filename)  => CalculateResult(ReadData(filename));
        
        private static long CalculateResult(List<MachineParts> machineParts)
        {
            List<MachineParts> eligibleMachineParts = new List<MachineParts>();
            long result = 0L;
            foreach (var machinePart in machineParts)
            {
                if(IsMachinePartAccepted(machinePart))
                {
                    eligibleMachineParts.Add(machinePart);
                }
            }
            foreach(var part in eligibleMachineParts)
            {
                foreach (var partTwo in part.Parts)
                    result += partTwo.Value;
            }
            return result;

        }

        private static bool IsMachinePartAccepted(MachineParts machineParts)
        {
            string result = String.Empty;
            List<Condition> con = new List<Condition>();
            while(result != "A" && result != "R")
            {
                if (string.IsNullOrEmpty(result))
                    con = workflows["in"];
                else
                    con = workflows[result];

                foreach(Condition condition in con)
                {
                    if(condition.Symbol == "<")
                    {
                        if (machineParts.Parts[condition.Name] < condition.Value)
                        {
                            result=condition.Success; 
                            break;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(condition.Failure))
                            {
                                result = condition.Failure;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (machineParts.Parts[condition.Name] > condition.Value)
                        {
                            result = condition.Success;
                            break;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(condition.Failure))
                            {
                                result = condition.Failure;
                                break;
                            }
                        }
                    }
                    
                }
            }
            return result == "A";
        }
        private static List<MachineParts> ReadData(string filename)
        {
            bool machineParts = false;
            List<MachineParts> machPrts = new List<MachineParts>();
            foreach(var line in ReadFile.Read(filename).Split("\r\n"))
            {
                if(string.IsNullOrEmpty(line))
                {
                    machineParts = true;
                    continue;
                }
                    
                if (machineParts)
                {
                    string trimline = line.Trim('{').TrimEnd('}');
                    string[] parts = trimline.Split(',');
                    Dictionary<string, int> mParts = new Dictionary<string, int>();
                    foreach (var part in  parts)
                    {
                        string[] parts2 = part.Split("=");
                        mParts.Add(parts2[0], int.Parse(parts2[1])); 
                        
                    }
                    machPrts.Add(new MachineParts() { Parts = mParts });
                }
                else
                {
                    int firstIndex = line.IndexOf('{');
                    string name = line.Substring(0, firstIndex);
                    string trimline = line.Substring(firstIndex, line.Length- firstIndex - 1).Trim('{').TrimEnd('}');
                    string[] conditions = trimline.Split(",");
                    string name1 = String.Empty, symbol = String.Empty, success = String.Empty, failure=String.Empty;
                    int value = 0;
                    List<Condition> conditions1 = new List<Condition>();
                    Condition condition1 = new Condition();
                    for (int i=0; i<conditions.Length-1; i++)
                    {
                        if (conditions[i].Contains(":"))
                        {
                            name1 = conditions[i].Substring(0, 1);
                            symbol = conditions[i].Substring(1, 1);
                            value = int.Parse(conditions[i].Substring(2, conditions[i].IndexOf(":")-2));
                            success = conditions[i].Substring(conditions[i].IndexOf(":") + 1, conditions[i].Length - conditions[i].IndexOf(":") - 1);
                            condition1 = new Condition() { Value = value, Failure = failure, Success = success, Symbol = symbol, Name=name1 };
                            if (conditions[i + 1].Contains(":"))
                                conditions1.Add(condition1);
                            else
                            {
                                condition1.Failure = conditions[i+1];
                                conditions1.Add(condition1);
                            }
                        } 
                            
                         
                    }
                    workflows.Add(name,conditions1 );
                }
                
            }
            return machPrts;
        }


        

        private static long GetRangeLengths(Dictionary<string, Range> ranges, List<Condition> con)
        {
            long result = 0L;
            foreach (Condition condition in con)
            {
                Dictionary<string,Range> nR = new(ranges);

                if (condition.Symbol == ">")
                {

                    if (ranges[condition.Name].End.Value > condition.Value) 
                    {
                        nR[condition.Name] = Math.Max(nR[condition.Name].Start.Value, condition.Value + 1)..nR[condition.Name].End;
                        if (condition.Success == "A") result += nR.len();
                        else if (condition.Success != "R") result += GetRangeLengths(nR, workflows[condition.Success]);

                        ranges[condition.Name] = ranges[condition.Name].Start..condition.Value;
                    }

                }
                if (condition.Symbol == "<")
                {
                    if (ranges[condition.Name].Start.Value < condition.Value) 
                    {
                        nR[condition.Name] = nR[condition.Name].Start..Math.Min(nR[condition.Name].End.Value, condition.Value - 1);
                        if (condition.Success == "A") result += nR.len();
                        else if (condition.Success != "R") result += GetRangeLengths(nR, workflows[condition.Success]);

                        ranges[condition.Name] = condition.Value..ranges[condition.Name].End;
                    }

                }
            }

            if (con.FirstOrDefault(x => !string.IsNullOrEmpty(x.Failure))?.Failure == "A")
            {
                result += ranges.len();
            }
            else if (con.FirstOrDefault(x => !string.IsNullOrEmpty(x.Failure))?.Failure != "R")
            { 
                result += GetRangeLengths(ranges, workflows[con.FirstOrDefault(x => !string.IsNullOrEmpty(x.Failure)).Failure]);
            }

            return result;
        }

        public static long len(this Dictionary<string,Range> r) => r.Aggregate(1L, (a, b) => a *= (b.Value.End.Value - b.Value.Start.Value +1));

    }
}
