using System;
//using System.Collections.Generic;


//DSA Group Coursework Version 1 - hard coded using an Adjacency Matrix 
//Samuel Paul Jones May 2023

//this is a Route Status class to store information on route delays and closures

namespace TubeWalkVersion1
{
    public class RouteStatus
    {
        private int SourceID;
        private int DestinationID;
        private string Status; // "Delay" or "Closure"
        private int Delay; // Delay time or -1 for closures
        private string Reason; // Reason for delay

        public RouteStatus(int sourceID, int destinationID, string status, int delay, string reason)
        {
            SourceID = sourceID;
            DestinationID = destinationID;
            Status = status;
            Delay = delay;
            Reason = reason;
        }

        public int GetSourceID()
        {
            return SourceID;
        }

        public int GetDestinationID()
        {
            return DestinationID;
        }
        public string GetStatus()
        {
            return Status;
        }
        public int GetDelay()
        {
            return Delay;
        }
        public string GetReason()
        {
            return Reason;
        }
    }

}
