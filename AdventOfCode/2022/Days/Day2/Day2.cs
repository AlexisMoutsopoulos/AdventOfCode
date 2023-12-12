using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Days.Day2
{
    internal class Day2
    {
        private static Dictionary<string, int> MySelection = new Dictionary<string, int>()
        {
            { "X", 1},
            { "Y" , 2},
            { "Z" , 3}
        };
        private static Dictionary<string, int> RoundResult = new Dictionary<string, int>()
        {
            { "X", 0},
            { "Y" , 3},
            { "Z" , 6}
        };

        public static void Day2Problem(string filename)
        {
            var part1 = PartOne(ReadFile.Read(filename).Split("\r\n"));
            var part2 = PartTwo(ReadFile.Read(filename).Split("\r\n"));
            Console.WriteLine($"Day2 results: Part1= {part1}  Part2 = {part2}");
        }

        private static int PartTwo(string[] strings)
        {
            int result = 0;
            foreach (string s in strings)
            {
                var game = s.Split(" ");
                result += RoundResult[game[1]] + Select(game);

            }
            return result;
        }

        private static int Select(string[] game)
        {
            if (game[0] == "A" && game[1] == "X") return MySelection["Z"];
            else if (game[0] == "B" && game[1] == "Y") return MySelection["Y"];
            else if (game[0] == "C" && game[1] == "Z") return MySelection["X"];
            else if (game[0] == "A" && game[1] == "Y") return MySelection["X"];
            else if (game[0] == "A" && game[1] == "Z") return MySelection["Y"];
            else if (game[0] == "B" && game[1] == "X") return MySelection["X"];
            else if (game[0] == "B" && game[1] == "Z") return MySelection["Z"];
            else if (game[0] == "C" && game[1] == "X") return MySelection["Y"];
            else if (game[0] == "C" && game[1] == "Y") return MySelection["Z"];
            return 1000;
        }

        private static int Result(string[] game)
        {
            if (game[0] == "A" && game[1] == "X") return 3;
            else if (game[0] == "B" && game[1] == "Y") return 3;
            else if (game[0] == "C" && game[1] == "Z") return 3;
            else if (game[0] == "A" && game[1] == "Y") return 6;
            else if (game[0] == "A" && game[1] == "Z") return 0;
            else if (game[0] == "B" && game[1] == "X") return 0;
            else if (game[0] == "B" && game[1] == "Z") return 6;
            else if (game[0] == "C" && game[1] == "X") return 6;
            else if (game[0] == "C" && game[1] == "Y") return 0;
            return 1000;
        }
        private static int PartOne(string[] strings)
        {
            int result = 0;
            foreach (string s in strings)
            {
                var game = s.Split(" ");
                result += MySelection[game[1]] + Result(game);

            }
            return result;
        }


        
}
}
