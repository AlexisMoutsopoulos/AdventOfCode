using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day15
{
    internal static class Day15
    {
        public static void Day15Problem(string filename)
        {
            var partOne = PartOne(filename);
            var partTwo = PartTwo(filename);
            Console.WriteLine($"Day15 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static List<Item> GetItems(List<string> data)
        {
            List<Item> items = new List<Item>();
            foreach(var item in data)
            {
                if (item.Contains("="))
                    items.Add(new Item() { Text = item.Substring(0,item.IndexOf("=")),
                                            Symbol = item.Substring(item.IndexOf("="), item.Length - item.IndexOf("=")-1),
                                                Number = int.Parse(item.Substring(item.IndexOf("=")+1, item.Length - item.IndexOf("=") - 1))});
                else if(item.Contains("-"))
                    items.Add(new Item()
                    {
                        Text = item.Substring(0, item.IndexOf("-")),
                        Symbol = item.Substring(item.IndexOf("-"), item.Length - item.IndexOf("-"))});
            }
            return items;
        }
        
        private static List<Item>[] FillBoxes(List<Item> items)
        {
            List<Item>[] boxes = new List<Item>[256];
            for (int i = 0; i < boxes.Length; i++)
                boxes[i] = new List<Item>();
            foreach (var item in items)
            {
                int boxNumber = HashWord(item.Text);
                if (!boxes[boxNumber].Any(x => x.Text == item.Text) && item.Symbol == "=")
                    boxes[boxNumber].Add(item);
                else if(item.Symbol == "-")
                    boxes[boxNumber].RemoveAll(y => y.Text == item.Text);
                else if(item.Symbol == "=" && boxes[boxNumber].Any(x => x.Text == item.Text))
                    boxes[boxNumber][boxes[boxNumber].FindIndex(k => k.Text == item.Text)] = item;                    
            }
            return boxes;
        }

        private static int CalculateResult(List<Item>[] boxes)
        {
            int result = 0;
            for (int i = 0;i < boxes.Length;i++)
                for(int j=0;j< boxes[i].Count; j++)
                    result += (i + 1) * (j + 1) * boxes[i][j].Number;
            return result;
        }
        private static int PartTwo(string filename) =>
             CalculateResult(FillBoxes(GetItems(GetData(filename))));
        

        private static int PartOne(string filename)
        {
            var data = GetData(filename);
            int result = 0;
            foreach (var item in data)
            {
                result += HashWord(item);
            }
            return result;
        }

        private static List<string> GetData(string filename) =>
            ReadFile.Read(filename).Split(',').ToList();
        

        private static int Hash(char letter,int cur)
        {
            int asciiValue = (int)letter;
            int result = cur + asciiValue;
            result *= 17;
            result %=256;
            return result;
        }

        private static int HashWord(string word)
        {
            int result = 0;
            foreach(char c in word)
            {
                result = Hash(c,result);
            }
            return result;
        }
    }
}
