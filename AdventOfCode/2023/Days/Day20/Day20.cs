using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day20
{
    internal static class Day20
    {
        public static void Day20Problem(string filename)
        {
            var partOne = PartOne(filename);
            var partTwo = PartTwo(filename);
            Console.WriteLine($"Day20 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static long PartTwo(string filename)
        {
            throw new NotImplementedException();
        }

        private static long PartOne(string filename)
        {
           var data = GetData(filename);
             
            long countLow = 0L;
            long countHigh = 0L;
            for (var i = 0; i < 1000; i++)
            {
                var result = Process(data);
                countLow += result.Item1;
                countHigh += result.Item2;
                Console.WriteLine();
            }
           
            return countLow*countHigh;
        }

        private static (long,long) Process(List<Module> modules)
        {
            List<Module> next = new();
            long countLow = 0L;
            long countHigh = 0L;
            next.Add(modules.FirstOrDefault(x => x.ModuleType == ModuleType.Broadcast));
            while(next.Count > 0)
            {
                var currentMod = next[0];
                next.RemoveAt(0);
                foreach(var nextModStr in currentMod.Destination)
                {
                    if (currentMod.OutputPulse == Pulse.Low)
                        countLow++;
                    else
                        countHigh++;
                    //Console.WriteLine("{0} -{1}-> {2}", currentMod.Name, currentMod.OutputPulse, nextModStr);
                    var nextMod = modules.FirstOrDefault(x => x.Name == nextModStr);
                    if (nextMod == null)
                        continue;
                   

                    if(nextMod.ModuleType == ModuleType.FlipFlop)
                    {
                        if(currentMod.OutputPulse == Pulse.Low)
                        {
                            if(nextMod.State == State.On)
                            {
                                nextMod.State = State.Off;
                                nextMod.OutputPulse = Pulse.Low;
                            }
                            else
                            {
                                nextMod.State = State.On;
                                nextMod.OutputPulse = Pulse.High;
                            }
                            next.Add(nextMod);
                        }
                    }
                    else if(nextMod.ModuleType == ModuleType.Conjuction)
                    {
                        if (!nextMod.LastReceivedPulses.ContainsKey(currentMod.Name))
                            nextMod.LastReceivedPulses.Add(currentMod.Name, currentMod.OutputPulse);
                        else
                            nextMod.LastReceivedPulses[currentMod.Name] = currentMod.OutputPulse;
                        nextMod.OutputPulse = (nextMod.LastReceivedPulses.Values.All(p => p == Pulse.High)) ? Pulse.Low : Pulse.High;
                        next.Add(nextMod);
                    }
                    else
                    {
                        nextMod.OutputPulse = Pulse.Low;
                        next.Add(nextMod);
                    }
                }
            }
            // finger is low pulse
            return ((countLow+1),countHigh);
        }


        private static List<Module> GetData(string filename)
        {
            string[] data = ReadFile.Read(filename).Split("\r\n");
            List<Module> modules = new List<Module>();
            foreach (string line in data)
            {
                Module module = new Module();
                string[] mod = line.Split(" "); 
                module.Name = (mod[0].StartsWith("%") || mod[0].StartsWith("&")) ? mod[0].Substring(1) : mod[0];
                modules.Add(module);
            }
            foreach (string line in data)
            {
                string[] mod = line.Split(" ");
                string name = (mod[0].StartsWith("%") || mod[0].StartsWith("&")) ? mod[0].Substring(1) : mod[0];
                var module = modules.First(x => x.Name == name);
                    module.ModuleType = (mod[0].Substring(0, 1)) switch
                    {
                        "%" => ModuleType.FlipFlop,
                        "&" => ModuleType.Conjuction,
                        _ => ModuleType.Broadcast,
                    };
                    
                    module.State = State.Off;
                    module.Destination = new List<string>();
                    for (int i = 2; i < mod.Length; i++)
                    {
                        module.Destination.Add(mod[i].TrimEnd(','));
                        var temp = modules.FirstOrDefault(x => x.Name == mod[i]);
                        if (temp != null)
                            temp.LastReceivedPulses[module.Name] = Pulse.Low;
                    } 
                   
            }
            return modules;
        }




         

    }
}
