using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day25
{
    internal class Day25
    {
        private const string Wire1 = "thx";
        private const string Wire2 = "frl";
        private const string Wire3 = "lhg";
        private const string Wire4 = "llm";
        private const string Wire5 = "fvm";
        private const string Wire6 = "ccp";
        public static void Day25Problem(string filename)
        {
            var partOne = PartOne(filename);
            var partTwo = PartTwo(filename);
            Console.WriteLine($"Day25 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static object PartTwo(string filename)
        {
            throw new NotImplementedException();
        }

        private static object PartOne(string filename) => CalculateResult(BuildGraph(filename));

        private static long CalculateResult(Dictionary<string, HashSet<string>> graph)
        {
            graph[Wire1].Remove(Wire2);
            graph[Wire2].Remove(Wire1);
            graph[Wire3].Remove(Wire4);
            graph[Wire4].Remove(Wire3);
            graph[Wire5].Remove(Wire6);
            graph[Wire6].Remove(Wire5);

            var visited = new List<string>();
            var list = new List<string>();
            list.Add(Wire1);

            while (list.Count > 0)
            {
                var current = list[0];
                list.RemoveAt(0);

                if (visited.Contains(current)) 
                    continue; 
                else
                    visited.Add(current);

                foreach (var connection in graph[current])
                {
                    list.Insert(0,connection);
                }
            }

            return visited.Count * (graph.Count - visited.Count);
        }
        private static Dictionary<string, HashSet<string>> BuildGraph(string filename)
        {
            Dictionary<string, HashSet<string>> graph = new();
            string[] data = ReadFile.Read(filename).Split("\r\n");
            var graphViz = new StringBuilder();
            graphViz.AppendLine("graph {");
            foreach (string line in data)
            {
                var parts = line.Split(": ");
                var wire = parts[0].Trim();
                var connections = parts[1].Trim().Split(" ").ToList();

                if (!graph.ContainsKey(wire))
                    graph[wire] = [];


                foreach (var connection in connections)
                {
                    graphViz.AppendLine($"    {wire} -- {connection} [color=blue]");
                    if (!graph.ContainsKey(connection))
                        graph[connection] = [];

                    graph[wire].Add(connection);
                    graph[connection].Add(wire);
                }
            }

            graphViz.AppendLine("}");
            File.WriteAllText("graph.dot", graphViz.ToString());
            return graph;
        }
    }
}
