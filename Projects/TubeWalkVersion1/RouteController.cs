﻿using System;

//using System.Collections.Generic;


//DSA Group Coursework Version 1 - hard coded using an Adjacency Matrix 
//Samuel Paul Jones May 2023

namespace TubeWalkVersion1
{
    public class RouteController
    {
        private TubeStation[] stations;
        private int[,] adjacencyMatrix;
        private TubeEdge[] edgeTo;

        private RouteStatus[] routeStatusArray = new RouteStatus[100];
        int routeStatusCount = 0; //keeps track of current total of delays + closures

        public RouteController()
        {
            //populating arrays - for pro version would do this via file import etc
            //Tube Stations are allocated a stationID which corresponds to their array index

            stations = new TubeStation[]
            {
                new TubeStation(0, "Elephant and Castle", "Bakerloo" , "Northern"),
                new TubeStation(1, "Lambeth North", "Bakerloo"),
                new TubeStation(2, "Waterloo", "Bakerloo", "Jubilee", "Northern", "Waterloo & City"),
                new TubeStation(3, "Embankment", "Bakerloo" , "District",  "Northern"),
                new TubeStation(4, "Charing Cross", "Bakerloo", "Northern"),
                new TubeStation(5, "Piccadilly Circus", "Bakerloo", "Piccadilly"),
                new TubeStation(6, "Oxford Circus", "Bakerloo", "Central", "Victoria"),
                new TubeStation(7, "Regents Park", "Bakerloo"),
                new TubeStation(8, "Baker Street", "Bakerloo", "Hammersmith & City", "Jubilee", "Metropolitan"),
                new TubeStation(9, "Marylebone", "Bakerloo"),
                new TubeStation(10, "Edgware Road (Bakerloo Line)", "Bakerloo"),
                new TubeStation(11, "Notting Hill Gate", "Central", "District"),
                new TubeStation(12, "Queensway", "Central"),
                new TubeStation(13, "Lancaster Gate", "Central"),
                new TubeStation(14, "Marble Arch", "Central"),
                new TubeStation(15, "Bond Street", "Central", "Jubilee"),
                new TubeStation(16, "Tottenham Court Road", "Central", "Northern"),
                new TubeStation(17, "Holborn", "Central", "Piccadilly"),
                new TubeStation(18, "Chancery Lane", "Central"),
                new TubeStation(19, "St Pauls", "Central"),
                new TubeStation(20, "Bank", "Central", "Northern", "Waterloo & City"),
                new TubeStation(21, "Tower Hill", "Circle", "District"),
                new TubeStation(22, "Edgware Road (Circle Line)","Circle", "District", "Hammersmith & City"),
                new TubeStation(23, "Paddington", "Circle", "District", "Bakerloo"),
                new TubeStation(24, "Bayswater", "Circle", "District"),
                new TubeStation(25, "High Street Kensington", "Circle", "District"),
                new TubeStation(26, "Earls Court", "Circle", "District", "Piccadilly"),
                new TubeStation(27, "Gloucester Road", "Circle", "District"),
                new TubeStation(28, "South Kensington", "Circle", "District"),
                new TubeStation(29, "Sloane Square", "Circle", "District"),
                new TubeStation(30, "Victoria", "Circle", "District", "Victoria"),
                new TubeStation(31, "St James Park", "Circle", "District"),
                new TubeStation(32, "Westminster", "Circle", "District", "Jubilee"),
                new TubeStation(33, "Temple", "Circle", "District"),
                new TubeStation(34, "Blackfriars", "Circle", "District"),
                new TubeStation(35, "Mansion House", "Circle", "District"),
                new TubeStation(36, "Cannon Street", "Circle", "District"),
                new TubeStation(37, "Monument", "Circle", "District"),
                new TubeStation(38, "Aldgate East", "District", "Hammersmith & City"),
                new TubeStation(39, "Aldgate", "Circle", "Metropolitan"),
                new TubeStation(40, "Liverpool Street", "Central", "Hammersmith & City", "Metropolitan", "Circle"),
                new TubeStation(41, "Green Park", "Jubilee", "Piccadilly", "Victoria"),
                new TubeStation(42, "Southwark", "Jubilee"),
                new TubeStation(43, "London Bridge", "Jubilee", "Northern"),
                new TubeStation(44, "Euston Square", "Metropolitan", "Circle", "Hammersmith & City"),
                new TubeStation(45, "Kings Cross St Pancras", "Metropolitan", "Northern", "Piccadilly", "Victoria", "Circle", "Hammersmith & City"),
                new TubeStation(46, "Great Portland Street", "Metropolitan", "Circle", "Hammersmith & City"),
                new TubeStation(47, "Warren Street", "Northern", "Victoria"),
                new TubeStation(48, "Goodge Street", "Northern"),
                new TubeStation(49, "Leicester Square", "Northern", "Piccadilly"),
                new TubeStation(50, "Borough", "Northern"),
                new TubeStation(51, "Moorgate", "Northern", "Central", "Metropolitan", "Hammersmith & City"),
                new TubeStation(52, "Old Street", "Northern"),
                new TubeStation(53, "Angel", "Northern"),
                new TubeStation(54, "Euston", "Northern", "Victoria"),
                new TubeStation(55, "Russell Square", "Piccadilly"),
                new TubeStation(56, "Covent Garden", "Piccadilly"),
                new TubeStation(57, "Hyde Park Corner", "Piccadilly"),
                new TubeStation(58, "Knightsbridge", "Piccadilly"),
                new TubeStation(59, "Pimlico", "Victoria"),
                new TubeStation(60, "Vauxhall", "Victoria"),
                new TubeStation(61, "Barbican", "Metropolitan", "Circle", "Hammersmith & City"),
                new TubeStation(62, "Farringdon", "Metropolitan", "Circle", "Hammersmith & City")

            };

            adjacencyMatrix = new int[stations.Length, stations.Length];

            //first initialise all adj matrix elements to -1

            for (int i = 0; i < stations.Length; i++)
            {
                for (int j = 0; j < stations.Length; j++)
                {
                    adjacencyMatrix[i, j] = -1;
                }
            }

            //then replace the -1's with edge weights where edges exist
            //note that index values correspond to the index values in the stations array - i.e. stationID

            adjacencyMatrix[0, 1] = adjacencyMatrix[1, 0] = 18;
            adjacencyMatrix[1, 2] = adjacencyMatrix[2, 1] = 9;
            adjacencyMatrix[2, 3] = adjacencyMatrix[3, 2] = 6;
            adjacencyMatrix[3, 4] = adjacencyMatrix[4, 3] = 3;
            adjacencyMatrix[4, 5] = adjacencyMatrix[5, 4] = 11;
            adjacencyMatrix[5, 6] = adjacencyMatrix[6, 5] = 12;
            adjacencyMatrix[6, 7] = adjacencyMatrix[7, 6] = 15;
            adjacencyMatrix[7, 8] = adjacencyMatrix[8, 7] = 10;
            adjacencyMatrix[8, 9] = adjacencyMatrix[9, 8] = 6;
            adjacencyMatrix[9, 10] = adjacencyMatrix[10, 9] = 7;
            adjacencyMatrix[10, 23] = adjacencyMatrix[23, 10] = 11;
            adjacencyMatrix[11, 12] = adjacencyMatrix[12, 11] = 8;
            adjacencyMatrix[12, 13] = adjacencyMatrix[13, 12] = 10;
            adjacencyMatrix[13, 14] = adjacencyMatrix[14, 13] = 15;
            adjacencyMatrix[14, 15] = adjacencyMatrix[15, 14] = 7;
            adjacencyMatrix[15, 6] = adjacencyMatrix[6, 15] = 7;
            adjacencyMatrix[6, 16] = adjacencyMatrix[16, 6] = 9;
            adjacencyMatrix[16, 17] = adjacencyMatrix[17, 16] = 10;
            adjacencyMatrix[17, 18] = adjacencyMatrix[18, 17] = 8;
            adjacencyMatrix[18, 19] = adjacencyMatrix[19, 18] = 14;
            adjacencyMatrix[19, 20] = adjacencyMatrix[20, 19] = 9;
            adjacencyMatrix[20, 40] = adjacencyMatrix[40, 20] = 10;
            adjacencyMatrix[21, 39] = adjacencyMatrix[39, 21] = 9;
            adjacencyMatrix[22, 23] = adjacencyMatrix[23, 22] = 10;
            adjacencyMatrix[23, 24] = adjacencyMatrix[24, 23] = 17;
            adjacencyMatrix[24, 11] = adjacencyMatrix[11, 24] = 10;
            adjacencyMatrix[11, 25] = adjacencyMatrix[25, 11] = 13;
            adjacencyMatrix[25, 26] = adjacencyMatrix[26, 25] = 18;
            adjacencyMatrix[26, 27] = adjacencyMatrix[27, 26] = 12;
            adjacencyMatrix[27, 28] = adjacencyMatrix[28, 27] = 8;
            adjacencyMatrix[28, 29] = adjacencyMatrix[29, 28] = 17;
            adjacencyMatrix[29, 30] = adjacencyMatrix[30, 29] = 13;
            adjacencyMatrix[30, 31] = adjacencyMatrix[31, 30] = 11;
            adjacencyMatrix[31, 32] = adjacencyMatrix[32, 31] = 11;
            adjacencyMatrix[32, 3] = adjacencyMatrix[3, 32] = 10;
            adjacencyMatrix[3, 33] = adjacencyMatrix[33, 3] = 9;
            adjacencyMatrix[33, 34] = adjacencyMatrix[34, 33] = 10;
            adjacencyMatrix[34, 35] = adjacencyMatrix[35, 34] = 10;
            adjacencyMatrix[35, 36] = adjacencyMatrix[36, 35] = 4;
            adjacencyMatrix[36, 37] = adjacencyMatrix[37, 36] = 5;
            adjacencyMatrix[37, 21] = adjacencyMatrix[21, 37] = 10;
            adjacencyMatrix[21, 38] = adjacencyMatrix[38, 21] = 10;
            adjacencyMatrix[22, 8] = adjacencyMatrix[8, 22] = 10;
            adjacencyMatrix[40, 38] = adjacencyMatrix[38, 40] = 11;
            adjacencyMatrix[8, 15] = adjacencyMatrix[15, 8] = 16;
            adjacencyMatrix[15, 41] = adjacencyMatrix[41, 15] = 14;
            adjacencyMatrix[41, 32] = adjacencyMatrix[32, 41] = 21;
            adjacencyMatrix[32, 2] = adjacencyMatrix[2, 32] = 17;
            adjacencyMatrix[2, 42] = adjacencyMatrix[42, 2] = 8;
            adjacencyMatrix[42, 43] = adjacencyMatrix[43, 42] = 19;
            adjacencyMatrix[44, 45] = adjacencyMatrix[45, 44] = 15;
            adjacencyMatrix[39, 40] = adjacencyMatrix[40, 39] = 9;
            adjacencyMatrix[40, 51] = adjacencyMatrix[51, 40] = 6;
            adjacencyMatrix[51, 61] = adjacencyMatrix[61, 51] = 10;
            adjacencyMatrix[61, 62] = adjacencyMatrix[62, 61] = 8;
            adjacencyMatrix[62, 45] = adjacencyMatrix[45, 62] = 26;
            adjacencyMatrix[45, 44] = adjacencyMatrix[44, 45] = 15;
            adjacencyMatrix[44, 46] = adjacencyMatrix[46, 44] = 10;
            adjacencyMatrix[46, 8] = adjacencyMatrix[8, 46] = 13;
            adjacencyMatrix[47, 48] = adjacencyMatrix[48, 47] = 7;
            adjacencyMatrix[48, 16] = adjacencyMatrix[16, 48] = 7;
            adjacencyMatrix[16, 49] = adjacencyMatrix[49, 16] = 8;
            adjacencyMatrix[49, 4] = adjacencyMatrix[4, 49] = 7;
            adjacencyMatrix[0, 50] = adjacencyMatrix[50, 0] = 13;
            adjacencyMatrix[50, 43] = adjacencyMatrix[43, 50] = 9;
            adjacencyMatrix[43, 20] = adjacencyMatrix[20, 43] = 6;
            adjacencyMatrix[20, 51] = adjacencyMatrix[51, 20] = 9;
            adjacencyMatrix[51, 52] = adjacencyMatrix[52, 51] = 9;
            adjacencyMatrix[52, 53] = adjacencyMatrix[53, 52] = 20;
            adjacencyMatrix[53, 45] = adjacencyMatrix[45, 53] = 16;
            adjacencyMatrix[45, 54] = adjacencyMatrix[54, 45] = 12;
            adjacencyMatrix[45, 55] = adjacencyMatrix[55, 45] = 14;
            adjacencyMatrix[55, 17] = adjacencyMatrix[17, 55] = 9;
            adjacencyMatrix[17, 56] = adjacencyMatrix[56, 17] = 8;
            adjacencyMatrix[56, 49] = adjacencyMatrix[49, 56] = 4;
            adjacencyMatrix[49, 5] = adjacencyMatrix[5, 49] = 6;
            adjacencyMatrix[5, 41] = adjacencyMatrix[41, 5] = 8;
            adjacencyMatrix[41, 57] = adjacencyMatrix[57, 41] = 12;
            adjacencyMatrix[57, 58] = adjacencyMatrix[58, 57] = 7;
            adjacencyMatrix[58, 28] = adjacencyMatrix[28, 58] = 17;
            adjacencyMatrix[54, 47] = adjacencyMatrix[47, 54] = 9;
            adjacencyMatrix[47, 6] = adjacencyMatrix[6, 47] = 18;
            adjacencyMatrix[6, 41] = adjacencyMatrix[41, 6] = 15;
            adjacencyMatrix[41, 30] = adjacencyMatrix[30, 41] = 19;
            adjacencyMatrix[30, 59] = adjacencyMatrix[59, 30] = 12;
            adjacencyMatrix[59, 60] = adjacencyMatrix[60, 59] = 18;
            adjacencyMatrix[2, 20] = adjacencyMatrix[20, 2] = 33;

            

            

        }
        public int DijkstraSP(string sourceName, string destinationName)
        {
            //array of the edges to the stations is initially all zero
            edgeTo = new TubeEdge[stations.Length];
            for (int i = 0; i < stations.Length; i++)
            {
                edgeTo[i] = null;
            }

            //getting source station via its stationID
            int sourceID = TubeStation.GetStationIDByName(stations, sourceName);
            TubeStation sourcestation = stations[sourceID];

            //getting destination station via its stationID
            int destinationID = TubeStation.GetStationIDByName(stations, destinationName);
            TubeStation destinationstation = stations[destinationID];

            //exception handling - not expecting any of these to occur in this version
            if (sourceID < 0 || sourceID >= stations.Length || destinationID < 0 || destinationID >= stations.Length)
            {
                throw new ArgumentException("Invalid source or destination station name.");
            }

            //array of shortest distances to vertices - initialising all elements to int.MaxValue as infinity substitute
            int[] distances = new int[stations.Length];
            for (int i = 0; i < stations.Length; i++)
            {
                distances[i] = int.MaxValue;
            }

            //set distance to the source station as 0, create new priority queue, add distance-station pair to PQ
            distances[sourceID] = 0;
            MinHeap priqueue = new MinHeap();
            HeapItem sourceStationPair = new HeapItem(0, sourcestation);
            priqueue.Insert(sourceStationPair);

            //main loops section of algorithm
            while (priqueue.Empty() == false)
            {
                //find current station which is at head of queue
                TubeStation nearestStation = priqueue.GetMinimum().GetValue();
                //dequeue it from priority queue
                priqueue.ExtractMinimum();


                for (int i = 0; i < stations.Length; i++)
                    if (nearestStation.GetStationID() < stations.Length && i < stations.Length) 
                    {
                    //loop through the relevant row of the adj matrix and check for edges
                    int walktime = adjacencyMatrix[nearestStation.GetStationID(), i];

                    if (walktime != -1)
                    {

                        //Edge Relaxation

                        TubeStation dv = stations[i];
                        TubeStation sv = nearestStation;
                        TubeEdge tubeedge = new TubeEdge(sv, dv, walktime);

                        //check to see if route via this new edge is shorter and update distances if so
                        //plus add new pair to priority queue
                        if (distances[dv.GetStationID()] > (distances[sv.GetStationID()] + walktime))
                        {
                            distances[dv.GetStationID()] = distances[sv.GetStationID()] + walktime;
                            edgeTo[dv.GetStationID()] = tubeedge;

                            HeapItem newPair = new HeapItem(distances[dv.GetStationID()], dv);
                            priqueue.Insert(newPair);
                        }
                    }
                }
            }

            //Shortest path
            //First trace the path back to the source vertex and store the edges involved
            TubeEdge[] pathArray = new TubeEdge[100];
            int pathIndex = 0;
            int totalJourneyTime = 0;

            for (TubeEdge edge = edgeTo[destinationID]; edge != null; edge = edgeTo[edge.GetSource().GetStationID()])
            {
                pathArray[pathIndex++] = edge;
                totalJourneyTime += edge.GetWalkingTime();
            }

            Console.WriteLine();
            Console.WriteLine($"Shortest path from {sourceName} to {destinationName}:");
            Console.WriteLine();

            //displaying change of tube line where one occurs
            string previousLine = null;
            for (int i = pathIndex - 1; i >= 0; i--)
            {
                TubeEdge currentEdge = pathArray[i];
                string currentLine = currentEdge.GetTubeLine();

                if (previousLine != null && currentLine != previousLine)
                {
                    Console.WriteLine($"Change from {previousLine} Line to {currentLine} Line");
                }
                //Display each step of path
                Console.WriteLine($"{currentEdge.GetSource().GetStationName()} to {currentEdge.GetDestination().GetStationName()} ({currentEdge.GetWalkingTime()} min) on {currentEdge.GetTubeLine()} Line");

                previousLine = currentLine;
            }

            Console.WriteLine();
            Console.WriteLine($"Total journey time: {totalJourneyTime} minutes");

            return distances[destinationID];
        }

        //END OF DIJKSTRA 



        public TubeStation GetTubeStationByName(string stationname)
        {
            //Get the TubeStation Object from knowing the station name
            //includes dealing with incorrect station names

            int sourceID = TubeStation.GetStationIDByName(stations, stationname);

            if (sourceID == -1)
            {
                Console.WriteLine("Error: Station not found. Please check your spelling and try again.");
                Console.WriteLine("Please note that all station names begin with capital letters, and apostrophes should not be included. ");
                Console.WriteLine("Saint may be abbreviated to St, but Street should be typed in full - e.g. St Pauls, Baker Street, etc.");
                return null;
            }

            TubeStation station = stations[sourceID];
            return station;
        }

        //Manager Menu delay/closure functions, using RouteStatus class
        public void AddDelay(int sourceID, int destinationID, int delay, string reason)
        {
            adjacencyMatrix[sourceID, destinationID] += delay;
            adjacencyMatrix[destinationID, sourceID] += delay;
            routeStatusArray[routeStatusCount++] = new RouteStatus(sourceID, destinationID, "Delay", delay, reason);
        }

        //RemoveDelay method slightly fragile in that it requires Manager to remember exact delay length
        public void RemoveDelay(int sourceID, int destinationID, int delay)
        {
            adjacencyMatrix[sourceID, destinationID] -= delay;
            adjacencyMatrix[destinationID, sourceID] -= delay;
            for (int i = 0; i < routeStatusCount; i++)
            {
                RouteStatus routestatus = routeStatusArray[i];
                if (routestatus != null && routestatus.GetSourceID() == sourceID && routestatus.GetDestinationID() == destinationID && routestatus.GetStatus() == "Delay" && routestatus.GetDelay() == delay)
                {
                    routeStatusArray[i] = null;
                }
            }
        }
        public void AddClosure(int sourceID, int destinationID, string reason)
        {
            adjacencyMatrix[sourceID, destinationID] = -1;
            adjacencyMatrix[destinationID, sourceID] = -1;
            routeStatusArray[routeStatusCount++] = new RouteStatus(sourceID, destinationID, "Closure", -1, reason);
        }

        //RemoveClosure requires Manager to remember original walktime value
        public void RemoveClosure(int sourceID, int destinationID, int walkTime)
        {
            adjacencyMatrix[sourceID, destinationID] = walkTime;
            adjacencyMatrix[destinationID, sourceID] = walkTime;
            for (int i = 0; i < routeStatusCount; i++)
            {
                RouteStatus routestatus = routeStatusArray[i];
                if (routestatus != null && routestatus.GetSourceID() == sourceID && routestatus.GetDestinationID() == destinationID && routestatus.GetStatus() == "Closure")
                {
                    routeStatusArray[i] = null;
                }
            }
        }

        public void DisplayDelays()
        {
            Console.WriteLine("Current delays:");
            bool hasDelays = false;
            for (int i = 0; i < routeStatusCount; i++)
            {
                RouteStatus routestatus = routeStatusArray[i];
                if (routestatus != null && routestatus.GetStatus() == "Delay")
                {
                    TubeStation sourceStation = stations[routestatus.GetSourceID()];
                    TubeStation destinationStation = stations[routestatus.GetDestinationID()];
                    Console.WriteLine($"{sourceStation.GetStationName()} to {destinationStation.GetStationName()}: {routestatus.GetDelay()} min delay due to {routestatus.GetReason()}");
                    hasDelays = true;
                }
            }

            if (!hasDelays)
            {
                Console.WriteLine("No delays currently reported. A good service is operating on all lines.");
            }
        }

        public void DisplayClosures()
        {
            Console.WriteLine("Current closures:");
            bool hasClosures = false;
            for (int i = 0; i < routeStatusCount; i++)
            {
                RouteStatus routestatus = routeStatusArray[i];
                if (routestatus != null && routestatus.GetStatus() == "Closure")
                {
                    TubeStation sourceStation = stations[routestatus.GetSourceID()];
                    TubeStation destinationStation = stations[routestatus.GetDestinationID()];
                    Console.WriteLine($"{sourceStation.GetStationName()} to {destinationStation.GetStationName()}: Closed as a result of {routestatus.GetReason()}");
                    hasClosures = true;
                }
            }

            if (!hasClosures)
            {
                Console.WriteLine("No closures currently reported.");
            }
        }

    }
}
