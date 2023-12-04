using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days.Day4
{
    internal static class Day4
    {
        public static void Day4Problem(string filename)
        {
            double partOne = Day4Part1(filename);
            int numberOfScratches =  Day4Part2(filename);
            Console.WriteLine($"Day4 Results: Part1 = {partOne} Part2 = {numberOfScratches}");
        }
        private static double Day4Part1(string filename)
        {
            string data = File.ReadAllText(filename);
            double sum = 0;
            int n = 0;
            List<int> myNumbers = new List<int>();
            List<int> winningsNumbers = new List<int>();
            foreach (string line in data.Split("\r\n"))
            {
                myNumbers = CardNumbers(line);
                winningsNumbers = WinningNumbers(line);
                n = CalculateScore(winningsNumbers, myNumbers);
                sum +=  n == -1 ? 0 : Math.Pow(2, n);
            }
            return sum;
        }

        private static int Day4Part2(string filename)
        {
            Dictionary<int,int> cardWinningTimes = CalculateTimeOfCardsWinning(filename);
            int sum = 0;
            foreach(int i in  cardWinningTimes.Keys)
            {
                sum += cardWinningTimes[i];
            }
            return sum;
        }

        private static Dictionary<int,int> CalculateTimeOfCardsWinning(string filename)
        {
            string[] data = File.ReadAllText(filename).Split("\r\n"); 
            Dictionary<int,int> result = new Dictionary<int,int>();
            int n = 0;
            List<int> myNumbers = new List<int>();
            List<int> winningsNumbers = new List<int>();
            for(int i=0;i<data.Length;i++)
            {
                myNumbers = CardNumbers(data[i]);
                winningsNumbers = WinningNumbers(data[i]);
                n = CalculateScore(winningsNumbers, myNumbers);
                CheckTheGameWin(result, n, i);
                for (int k = 0; k < result[i]; k++)
                {
                    for (int j = i + 1; j <= i + n + 1; j++)
                    {
                        CheckTheGameWin(result, n, j);
                    }
                }
                
            }
            return result;
        }

        private static void CheckTheGameWin(Dictionary<int, int> result, int n, int i)
        {
            if (n > -1 && !result.ContainsKey(i))
                result.Add(i, 1);
            else if(n == -1 && !result.ContainsKey(i))
                result.Add(i, 1);
            else if (result.ContainsKey(i))
                result[i]++;
        }

        private static int CalculateScore(List<int> winningsNmbers, List<int> myNumbers)
        {
            int counter = -1;
            foreach (int n in myNumbers)
            {
                if(winningsNmbers.Contains(n))
                    counter++;                
            }
            return counter;
        }
        private static List<int> CardNumbers(string line)
        {
            string number = line.Substring(line.IndexOf('|') + 1);
            string[] numberStr = number.Split(' ').Where(y => !string.IsNullOrEmpty(y)).ToArray();
            return numberStr.Select(x => Convert.ToInt32(x)).ToList();
        }
        private static List<int> WinningNumbers(string line)
        {
            string number = line.Substring(line.IndexOf(':') + 1, line.IndexOf('|') - line.IndexOf(':') - 1).Trim();
            string[] numberStr = number.Split(' ').Where(y => !string.IsNullOrEmpty(y)).ToArray();
            return numberStr.Select(x => Convert.ToInt32(x)).ToList();
        }
    }
}
