using System;
namespace DijkstraVersion3
{
    class Station
    {
        //Using a system.collections.generic LinkedList instead of hand-coded linked list. The items in the tuple store
        //the adjacent Station object, the line to reach adjacent station and distance from current station respectively.
        
        public string Name;
        public LinkedList<(Station, string, int)> Neighbours;      
        public string Line;

        public Station(string name)
        {
            Name = name;
            Neighbours = new LinkedList<(Station, string, int)>();
        }

        public Station(string name, string line)
        {
            Name = name;
            Neighbours = new LinkedList<(Station, string, int)>();
            Line = line;
        }

        public void AddToEnd(Station toStation, string lineName, int time)
        {
            Neighbours.AddLast((toStation, lineName, time));
        }
    }
}

