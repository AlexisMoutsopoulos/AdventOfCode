using AdventOfCode._2023.Days.Day16;
using AdventOfCode._2023.Days.Day22;
using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day22
{
    internal class Day22
    {
        public static void Day22Problem(string filename)
        { 
            var partOne = PartOne(filename);
            var partTwo = PartTwo(filename);
            Console.WriteLine($"Day22 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static object PartTwo(string filename) => Fall(BuildMap(GetBricks(filename)));

        private static int PartOne(string filename) => Disentigrate(BuildMap(GetBricks(filename)));
       

        private static List<Brick> GetBricks(string filename)
        {
            var data = ReadFile.Read(filename).Split("\r\n").ToList();
            List<Brick> result = new List<Brick>();
            foreach (var line in data)
            {
                var points = line.Split('~');
                var coord1 = points[0].Split(',');
                var coord2 = points[1].Split(',');
                result.Add(new Brick()
                {
                    Start = new Point() { X = int.Parse(coord1[0]), Y = int.Parse(coord1[1]), Z = int.Parse(coord1[2]) },
                    End = new Point() { X = int.Parse(coord2[0]), Y = int.Parse(coord2[1]), Z = int.Parse(coord2[2]) }
                });
            }
            return result;
        }

        private static List<Brick> BuildMap(List<Brick> bricks)
        {
            bricks = bricks.OrderBy(x => x.Start.Z).ToList();
            HashSet<Brick> highest = [];
            foreach (Brick currentBrick in bricks)
            {
                HashSet<Brick> below = [];
                foreach (var br in highest)
                {
                    if (currentBrick.Xs.Intersect(br.Xs).Any() && currentBrick.Ys.Intersect(br.Ys).Any())
                        below.Add(br);                                   

                }
                highest.Add(currentBrick);
                int newHeight = below.Count == 0 ? 1 : below.Max(b => b.End.Z) + 1;
                currentBrick.End.Z = currentBrick.End.Z - currentBrick.Start.Z + newHeight;
                currentBrick.Start.Z = newHeight;

                if (below.Count > 0)
                {
                    foreach (var brickBelow in below.Where(b => b.End.Z == currentBrick.Start.Z - 1))
                    {
                        currentBrick.Below.Add(brickBelow);
                        brickBelow.Above.Add(currentBrick);
                    }
                }
            }
            return bricks;
         
        }

        private static int Disentigrate(List<Brick> bricks)
        {
            int result = 0;

            foreach (var brick in bricks)
                if (brick.Above.All(above => above.Below.Count() > 1))
                    result++;            

            return result;
        }

        private static int Fall(List<Brick> bricks)
        {
            var result = 0;
            
            foreach(var brick in bricks)
            {
                List<Brick> removed = new();

                List<Brick> next = [];
                next.Add(brick);

                while (next.Count > 0)
                {
                    var nextBrick = next.Last();
                    next.Remove(nextBrick);
                    removed.Add(nextBrick);
                    foreach (var aboveNext in nextBrick.Above)
                    {
                        if (aboveNext.Below.All(removed.Contains))
                        {
                            next.Insert(0,aboveNext);
                            result++;
                        }
                    }
                }
            }
            return result;
        }
        
    }
}
