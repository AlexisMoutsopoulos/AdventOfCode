using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Days.Day20
{
    internal class Module
    {
        public State State { get; set; }
        public List<string> Destination { get; set; }

        public string Name { get; set; }

        public Pulse OutputPulse { get; set; }

        public ModuleType ModuleType { get; set; }

        public Dictionary<string, Pulse> LastReceivedPulses { get; set; } = new();

    }
    enum Pulse
    {
        Low,
        High
    }
    enum ModuleType
    {
        FlipFlop,
        Conjuction,
        Broadcast
    }
    enum State
    {
        Off = 0,
        On = 1
    }
}
