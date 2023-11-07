﻿using System;
//using System.Collections.Generic;

//DSA Group Coursework Version 1 - hard coded using an Adjacency Matrix 
//Samuel Paul Jones May 2023

namespace TubeWalkVersion1
{
    public class TubeStation
    {
        private int stationID;
        private string stationName;
        private string[] tubeLines;

        //constructors to deal with a station being on up to 6 different lines
        public TubeStation(int stationID, string stationName, string tubeline)
        {
            this.stationID = stationID;
            this.stationName = stationName;
            this.tubeLines = new string[] { tubeline };
        }

        public TubeStation(int stationID, string stationName, string tubeline1, string tubeline2)
        {
            this.stationID = stationID;
            this.stationName = stationName;
            this.tubeLines = new string[] { tubeline1, tubeline2 };
        }
        public TubeStation(int stationID, string stationName, string tubeline1, string tubeline2, string tubeline3)
        {
            this.stationID = stationID;
            this.stationName = stationName;
            this.tubeLines = new string[] { tubeline1, tubeline2, tubeline3 };
        }
        public TubeStation(int stationID, string stationName, string tubeline1, string tubeline2, string tubeline3, string tubeline4)
        {
            this.stationID = stationID;
            this.stationName = stationName;
            this.tubeLines = new string[] { tubeline1, tubeline2, tubeline3, tubeline4 };
        }
        public TubeStation(int stationID, string stationName, string tubeline1, string tubeline2, string tubeline3, string tubeline4, string tubeline5, string tubeline6)
        {
            this.stationID = stationID;
            this.stationName = stationName;
            this.tubeLines = new string[] { tubeline1, tubeline2, tubeline3, tubeline4, tubeline5, tubeline6 };
        }
        public int GetStationID()
        {
            return stationID;
        }
        public void SetStationID(int id)
        {
            stationID = id;
        }
        public string GetStationName()
        {
            return stationName;
        }
        public void SetStationName(string name)
        {
            stationName = name;
        }
        public string[] GetTubeLines()
        {
            return tubeLines;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Station ID: {stationID}");
            Console.WriteLine($"Station Name: {stationName}");
            Console.Write("Tube Lines: ");

            for (int i = 0; i < tubeLines.Length; i++)
            {
                Console.Write(tubeLines[i]);
                if (i < tubeLines.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine();
        }

        public static int GetStationIDByName(TubeStation[] stations, string stationName)
        {
            foreach (var station in stations)
            {
                if (station.GetStationName() == stationName)
                {
                    return station.GetStationID();
                }
            }

            // Return -1 if no station with the given name is found
            return -1;
        }
    }
}
