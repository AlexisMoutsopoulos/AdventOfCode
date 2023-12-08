using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days.Day2
{
    internal class Day2
    {
        private const int BLUE = 14;
        private const int RED = 12;
        private const int GREEN = 13;
        public static void Day2Problem(string filename)
        {
            string file = ReadFile.Read(filename);
            string[] lines = file.Split("\r\n");
            List<Game> result1 = new List<Game>(); 
            int result2 = 0;
            foreach (string line in lines)
            {
               result1.Add(CreateGamePart1(line));
                result2 += CreateGamePart2(line);
            }
            result1.RemoveAll(item => item == null);
            Console.WriteLine($"Day2 results: Part1 = {result1.Sum(x=>x.ID)} Part2 = {result2}");
        }

        private static Game CreateGamePart1(string line)
        {
             
            string gameInfo = line.Substring(0,line.IndexOf(':'));
            string[] parts = gameInfo.Split(' ');
            int id = Convert.ToInt32(parts[1]); 
            string cubeInfo = line.Substring(line.IndexOf(':')+1, line.Length - gameInfo.Length -1);
            string[] setInfo = cubeInfo.Split(';');
            
            foreach (string set in setInfo)
            {
                int sumOfBlue = 0;
                int sumOfGreen = 0;
                int sumOfRed = 0;
                string[] colorInfo = set.Split(",");
                foreach (string color in colorInfo)
                {
                    string[] numberOfColorsInfo = color.Split(" ");
                    switch (numberOfColorsInfo[2])
                    {
                        case "red":
                            sumOfRed = Convert.ToInt32(numberOfColorsInfo[1]);
                            break;
                        case "green":
                            sumOfGreen = Convert.ToInt32(numberOfColorsInfo[1]);
                            break;
                        case "blue":
                            sumOfBlue = Convert.ToInt32(numberOfColorsInfo[1]);
                            break;
                        default:
                            break;
                    }
                }

                if (sumOfBlue > BLUE || sumOfGreen > GREEN || sumOfRed > RED)
                    return null;
            }
            return new Game() { ID = id }; ;
            
        }


        private static int CreateGamePart2(string line)
        {

            string gameInfo = line.Substring(0, line.IndexOf(':'));
            string cubeInfo = line.Substring(line.IndexOf(':') + 1, line.Length - gameInfo.Length - 1);
            string[] setInfo = cubeInfo.Split(';');
            int maxOfBlue = 0;
            int maxOfGreen = 0;
            int maxOfRed = 0;
            foreach (string set in setInfo)
            {
                
                string[] colorInfo = set.Split(",");
                foreach (string color in colorInfo)
                {
                    string[] numberOfColorsInfo = color.Split(" ");
                    switch (numberOfColorsInfo[2])
                    {
                        case "red":
                            maxOfRed = Math.Max(Convert.ToInt32(numberOfColorsInfo[1]), maxOfRed);
                            break;
                        case "green":
                            maxOfGreen = Math.Max(Convert.ToInt32(numberOfColorsInfo[1]), maxOfGreen);
                            break;
                        case "blue":
                            maxOfBlue = Math.Max(Convert.ToInt32(numberOfColorsInfo[1]), maxOfBlue);
                            break;
                        default:
                            break;
                    }
                }

                 
            }
            return maxOfBlue * maxOfGreen * maxOfRed ;

        }

    }
}
