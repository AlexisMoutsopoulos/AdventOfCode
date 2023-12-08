using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days.Day7
{
    internal static class Day7
    {

        public static void Day7Problem(string filename)
        {
            int partOne = PartOne(filename);
            var partTwo = PartTwo(filename); 
            Console.WriteLine($"Day7 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static int PartTwo(string filename)
        {
            var handCards = SetHandV2(filename);
            var sortedHand = handCards.OrderBy(x => x , new CardComparerV2()).ToList();
            int result = 0;
            for (int j = 0; j < sortedHand.Count; j++)
            {
                result += sortedHand[j].bidAmount * (j + 1);
            }
            return result;
        }


        private static HandType SetHandTypeV2(string value)
        {
            char[] chars = value.ToCharArray();
            Dictionary<string, int> cardAppearances = new Dictionary<string, int>();
            HandType handType = new HandType();
            foreach (char c in chars)
            {
                if (cardAppearances.ContainsKey(c.ToString()))
                    cardAppearances[c.ToString()]++;
                else cardAppearances.Add(c.ToString(), 1);
            } 
            if (cardAppearances.Count == 1 || 
                    (cardAppearances.Count == 2 && cardAppearances.ContainsKey("J") && cardAppearances["J"] == 4) ||
                        (cardAppearances.Count == 2 && cardAppearances.ContainsKey("J") && cardAppearances["J"] == 1) ||
                            (cardAppearances.Count == 2 && cardAppearances.ContainsKey("J") && cardAppearances["J"] == 3) ||
                                (cardAppearances.Count == 2 && cardAppearances.ContainsKey("J") && cardAppearances["J"] == 4) ||
                                    (cardAppearances.Count == 2 && cardAppearances.ContainsKey("J") && cardAppearances["J"] == 2 && cardAppearances.ContainsValue(3)))
                handType = HandType.FiveOfKind;
            else if ((cardAppearances.Count == 2 && cardAppearances.ContainsValue(4)) ||
                        (cardAppearances.Count == 3 && cardAppearances.ContainsValue(2) && cardAppearances.ContainsKey("J") && cardAppearances["J"] == 2) ||
                           (cardAppearances.Count == 3 && cardAppearances.ContainsKey("J") && cardAppearances["J"] == 3) ||
                            (cardAppearances.Count == 3 && cardAppearances.ContainsValue(3) && cardAppearances.ContainsKey("J") && cardAppearances["J"] == 1))
                handType = HandType.FourOfKind;
            else if ((cardAppearances.Count == 2 && cardAppearances.ContainsValue(3) && cardAppearances.ContainsValue(2)) ||
                        (cardAppearances.Count == 3 && cardAppearances.ContainsValue(2) && cardAppearances.ContainsKey("J") && cardAppearances["J"] == 1))
                handType = HandType.FullHouse;
            else if ((cardAppearances.Count == 3 && cardAppearances.ContainsValue(3)) ||
                        (cardAppearances.Count == 4 && cardAppearances.ContainsKey("J") && cardAppearances["J"] == 2) ||
                            (cardAppearances.Count == 4 && cardAppearances.ContainsKey("J") && cardAppearances["J"] == 1))
                handType = HandType.ThreeOfKind;
            else if (cardAppearances.Count == 3 && cardAppearances.ContainsValue(2))
                handType = HandType.TwoPair;
            else if ((cardAppearances.Count == 4 && cardAppearances.ContainsValue(2)) ||
                        (cardAppearances.Count == 5 && cardAppearances.ContainsKey("J")))
                handType = HandType.OnePair;
            else
                handType = HandType.HighCard;
             

            return handType;
        }

        private static List<Hand> SetHandV2(string filename)
        {
            string[] data = ReadFile.Read(filename).Split("\r\n");
            List<Hand> result = new List<Hand>();
            foreach (string line in data)
            {
                string[] nums = line.Split(' ');
                result.Add(new Hand() { bidAmount = int.Parse(nums[1]), Value = nums[0], Type = SetHandTypeV2(nums[0]) });
            }
            return result;
        }

        private static int PartOne(string filename)
        {
            var handCards = SetHand(filename);
            var sortedHand = handCards.OrderBy(x => x, new CardComparer()).ToList();
            int result = 0;
            for (int j = 0; j < sortedHand.Count; j++)
            {
                result += sortedHand[j].bidAmount * (j + 1);
            }
            return result;
        }

        private static List<Hand> SetHand(string filename)
        {
            string[] data = ReadFile.Read(filename).Split("\r\n");
            List<Hand> result = new List<Hand>();
            foreach (string line in data)
            {
                string[] nums = line.Split(' ');
                result.Add(new Hand() { bidAmount = int.Parse(nums[1]), Value = nums[0], Type = SetHandType(nums[0]) });
            }
            return result;
        }

        private static HandType SetHandType(string value)
        {
            char[] chars = value.ToCharArray();
            Dictionary<string, int> cardAppearances = new Dictionary<string, int>();
            foreach (char c in chars)
            {
                if (cardAppearances.ContainsKey(c.ToString()))
                    cardAppearances[c.ToString()]++;
                else cardAppearances.Add(c.ToString(), 1);
            }

            if (cardAppearances.Count == 1)
                return HandType.FiveOfKind;
            else if (cardAppearances.Count == 2 && cardAppearances.ContainsValue(4))
                return HandType.FourOfKind;
            else if (cardAppearances.Count == 2 && cardAppearances.ContainsValue(3) && cardAppearances.ContainsValue(2))
                return HandType.FullHouse;
            else if (cardAppearances.Count == 3 && cardAppearances.ContainsValue(3))
                return HandType.ThreeOfKind;
            else if (cardAppearances.Count == 3 && cardAppearances.ContainsValue(2))
                return HandType.TwoPair;
            else if (cardAppearances.Count == 4 && cardAppearances.ContainsValue(2))
                return HandType.OnePair;
            else if (cardAppearances.Count == 5)
                return HandType.HighCard;

            return HandType.HighCard;
        }

    }
}
