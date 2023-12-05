using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days.Day5
{
    internal static class Day5
    {
        private static Dictionary<Element, Element> seed_to_soil_map = new Dictionary<Element, Element>();
        private static Dictionary<Element, Element> soil_to_fertilizer_map = new Dictionary<Element, Element>();
        private static Dictionary<Element, Element> fertilizer_to_water_map = new Dictionary<Element, Element>();
        private static Dictionary<Element, Element> water_to_light_map = new Dictionary<Element, Element>();
        private static Dictionary<Element, Element> light_to_temperature_map = new Dictionary<Element, Element>();
        private static Dictionary<Element, Element> temperature_to_humidity_map = new Dictionary<Element, Element>();
        private static Dictionary<Element, Element> humidity_to_location_map = new Dictionary<Element, Element>();

        public static void Day5Problem(string filename)
        {
            long partOne = PartOne(filename);
            long partTwo = PartTwo(filename);
            Console.WriteLine($"Day5 Results: Part1 = {partOne} Part2 = {partTwo}");
        }

        private static long PartOne(string filename)
        {
            long valueSoil;
            long valueFertilizer;
            long valueWater;
            long valueLight;
            long valueTemperature;
            long valueHumidity;
            List<long> valueLocation = new List<long>();
            string[] data = ReadFile.Read(filename).Split("\r\n");

            List<long> finalSeeds = data[0].Substring(data[0].IndexOf(':') + 2, data[0].Length - data[0].IndexOf(':') - 2).Split(' ').Select(long.Parse).ToList();
            SetDictionary(data);
            for (int i = 0;i< finalSeeds.Count(); i++)
            {
                valueSoil = FindNextKey(seed_to_soil_map, finalSeeds[i]);
                valueFertilizer = FindNextKey(soil_to_fertilizer_map, valueSoil);
                valueWater = FindNextKey(fertilizer_to_water_map, valueFertilizer);
                valueLight = FindNextKey(water_to_light_map, valueWater);
                valueTemperature = FindNextKey(light_to_temperature_map, valueLight);
                valueHumidity = FindNextKey(temperature_to_humidity_map, valueTemperature);
                valueLocation.Add(FindNextKey(humidity_to_location_map, valueHumidity));
            }
            return valueLocation.Min();
        }

        private static long FindNextKey(Dictionary<Element, Element> map, long key)
        {
            foreach (var value in map.Keys) 
            {
                if (key >= value.Start && key <= value.End)
                    return map[value].Start + (key - value.Start); 
            }
            return key;
                
        }

        private static void SetDictionary(string[] data)
        {
            List<string> data1= new List<string>();
            string temp = String.Empty;
            for (int k = 2; k <= data.Length; k++)
            {
                if (k < data.Length && !string.IsNullOrEmpty(data[k]))
                    temp += data[k] + '\n';
                else
                {
                    data1.Add(temp);
                    temp = String.Empty;
                }
            }
            for (int l=0; l<data1.Count; l++)
            {
                Run(data1[l].Split('\n'));
            }
        }

        private static void Run(string[] dataArray)
        {
            string map = String.Empty;
            foreach (string line in dataArray)
            {                
                if (line.Contains("map"))
                {
                    map = line.Split(' ')[0];
                    continue;
                }
                if (!string.IsNullOrEmpty(line))
                {
                    long startSource = Convert.ToInt64(line.Split(' ')[1]);
                    long startDest = Convert.ToInt64(line.Split(' ')[0]);
                    long length = Convert.ToInt64(line.Split(' ')[2]);

                    switch (map)
                    {
                        case "seed-to-soil":
                            SetDict(seed_to_soil_map, startSource, startDest, length);
                            break;
                        case "soil-to-fertilizer":
                            SetDict(soil_to_fertilizer_map, startSource, startDest, length);
                            break;
                        case "fertilizer-to-water":
                            SetDict(fertilizer_to_water_map, startSource, startDest, length);
                            break;
                        case "water-to-light":
                            SetDict(water_to_light_map, startSource, startDest, length);
                            break;
                        case "light-to-temperature":
                            SetDict(light_to_temperature_map, startSource, startDest, length);
                            break;
                        case "temperature-to-humidity":
                            SetDict(temperature_to_humidity_map, startSource, startDest, length);
                            break;
                        case "humidity-to-location":
                            SetDict(humidity_to_location_map, startSource, startDest, length);
                            break;
                        default:
                            break;

                    }


                }
            }
        }

        private static void SetDict(Dictionary<Element, Element> dict, long startSource, long startDest, long length) =>           
                dict.Add(new Element() { Start = startSource,End = startSource + length}, new Element() { Start = startDest, End = startDest + length });
            
        
        private static long PartTwo(string filename) 
        {
            long valueSoil;
            long valueFertilizer;
            long valueWater;
            long valueLight;
            long valueTemperature;
            long valueHumidity;
            List<long> valueLocation = new List<long>();
            List<long> temps = new List<long>();
            string[] data = ReadFile.Read(filename).Split("\r\n");

            List<long> seeds = data[0].Substring(data[0].IndexOf(':') + 2, data[0].Length - data[0].IndexOf(':') - 2).Split(' ').Select(long.Parse).ToList();
            SetDictionary(data);
            for (int k = 0; k < seeds.Count -1; k+=2)
            {
                int temp = k+1;
                
                for (long i = seeds[k]; i < seeds[k] + seeds[temp]; i++)
                {
                    valueSoil = FindNextKey(seed_to_soil_map, i);
                    valueFertilizer = FindNextKey(soil_to_fertilizer_map, valueSoil);
                    valueWater = FindNextKey(fertilizer_to_water_map, valueFertilizer);
                    valueLight = FindNextKey(water_to_light_map, valueWater);
                    valueTemperature = FindNextKey(light_to_temperature_map, valueLight);
                    valueHumidity = FindNextKey(temperature_to_humidity_map, valueTemperature);
                    temps.Add(FindNextKey(humidity_to_location_map, valueHumidity));
                }
                valueLocation.Add(temps.Min());
                temps.Clear();
            }
            return valueLocation.Min();
        }
    
    }
}
