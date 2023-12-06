namespace DijkstraVersion3;
class Program
{
    static void Main(string[] args)
    {
        //Dictionary to hold info about delays. Key = station1, station2 - Value = time, reason and line
        Dictionary<(string, string), (int, string, string)> delays = new Dictionary<(string, string), (int, string, string)>();

        // Dictionary to hold headers, as part of system.collections.generic instead of an array. This allows for
        //ease of searching for a station as the key is the station name.
        Dictionary<string, Station> stations = new Dictionary<string, Station>();

        // reading .csv file and instantiating objects to pass into dictionary
        using (var reader = new StreamReader("stations3.csv"))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                string lineName = values[0];
                string fromName = values[1];
                string toName = values[2];
                int time = int.Parse(values[3]);

                Station fromStation, toStation;

                // Checking to make sure we don't get separate stations for (eg. bakerloo oxford st and central oxford st)
                if (!stations.ContainsKey(fromName))
                {
                    fromStation = new Station(fromName);
                    stations[fromName] = fromStation;
                }
                else
                {
                    fromStation = stations[fromName];
                }

                if (!stations.ContainsKey(toName))
                {
                    toStation = new Station(toName);
                    stations[toName] = toStation;
                }
                else
                {
                    toStation = stations[toName];
                }

                // Adding the connection between the two stations
                fromStation.AddToEnd(toStation, lineName, time);
                toStation.AddToEnd(fromStation, lineName, time);
            }
        }
        //Simple main interface
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Welcome to the TfL Zone 1 Walking Times Main Menu");
            Console.WriteLine("*************************************************");
            Console.WriteLine("1. Find a walking route");
            Console.WriteLine("2. Display information");
            Console.WriteLine("3. Access Admin Menu");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice (1-4): ");

            int selection;
            int.TryParse(Console.ReadLine(), out selection);

            switch (selection)
            {
                case 1:
                    Dijkstra(stations, delays);     //Call to Dijkstra's Algorithm function
                    break;
                case 2:
                    DisplayStation(stations);       //Call to display chosen station function
                    break;
                case 3:
                    AdminMenu(stations, delays);    //Call to Admin menu function
                    break;
                case 4:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter 1,2,3 or 4 only. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }

    }







    //Function employing Dijkstra's Algorithm
    public static void Dijkstra(Dictionary<string, Station> stations, Dictionary<(string, string), (int, string, string)> delays)
    {
        Console.Write("Enter Start Station: ");
        string startStationName = Console.ReadLine();

        Console.Write("Enter End Station: ");       
        string endStationName = Console.ReadLine();
        //Important to note that all apostrophes have been removed from the .csv file (so kings cross, not king's cross)

        if (!stations.ContainsKey(startStationName) || !stations.ContainsKey(endStationName))
        {
            Console.WriteLine("Invalid Input. Station Not Found.");
            return;
        }

        Station startStation = stations[startStationName];
        Station endStation = stations[endStationName];


        //Declaring distanceTo, final edgeTo and priority queue data structures.
        var distanceTo = new Dictionary<Station, int>();
        var edgeTo = new Dictionary<Station, Station>();
        var queue = new List<Station>();

        // Setting distance to start station to 0 and every other station to maxvalue.
        foreach (var station in stations.Values)
        {
            if (station == startStation)
            {
                distanceTo[station] = 0;    
            }
            else
            {
                distanceTo[station] = int.MaxValue;
            }
            //Adding station to priority queue
            queue.Add(station);
        }

        //Ordering queue by distance priority
        while (queue.Count > 0)
        {
            var currentStation = queue.OrderBy(station => distanceTo[station]).First();

            if (currentStation == endStation)
            {
                break;
            }

            queue.Remove(currentStation);

            //Performing edge relaxation
            foreach (var neighbour in currentStation.Neighbours)
            {
                var alternativeDistance = distanceTo[currentStation] + neighbour.Item3;

                if (alternativeDistance < distanceTo[neighbour.Item1])
                {
                    distanceTo[neighbour.Item1] = alternativeDistance;

                    edgeTo[neighbour.Item1] = currentStation;
                }
            }
        }

        // outputting shortest path
        var path = new LinkedList<Station>();
        var current = endStation;

        while (current != null)
        {
            path.AddFirst(current);
            current = edgeTo.ContainsKey(current) ? edgeTo[current] : null;
        }
        if (path.First.Value != startStation || path.Last.Value != endStation)
        {
            Console.WriteLine("No Path Found.");
            return;
        }
        Console.WriteLine();
        Console.WriteLine("Shortest Path:");
        Console.WriteLine();

        //Cannot assign null value to implicit (var) type for some reason
        Station previousStation = null;
        var currentLine = "";
        var totalTime = 0;
        string delayReason = "";
        int delay = 0;
        string delayLine = "";

        foreach (var station in path)
        {
            if (previousStation != null)
            {
                var neighbour = station.Neighbours.First(n => n.Item1 == previousStation);

                //Check for delays
                if (delays.ContainsKey((previousStation.Name, station.Name)))
                {
                    (delay, delayReason, delayLine) = delays[(previousStation.Name, station.Name)];
                    delayReason = "due to " + delayReason;      
                }
                if (delay != int.MaxValue)
                {
                    //comparing lines to output "Change!" message at interchanges
                    if(neighbour.Item2 != currentLine && previousStation != startStation)
                    {
                        Console.Write("Change! ");
                    }
                    Console.WriteLine($"{neighbour.Item2}: {previousStation.Name} -> {station.Name} ({neighbour.Item3 + delay} mins)");
                    Console.WriteLine();
                    totalTime += neighbour.Item3 + delay;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Route is impossible due to {delayReason} between {delayLine} Line: {previousStation.Name} and {station.Name}");
                }
                currentLine = neighbour.Item2;
            }
            
            previousStation = station;
        }
        if (delay != int.MaxValue)
        {
            Console.WriteLine();
            Console.WriteLine($"Total Walk Time: {totalTime} mins {delayReason}");
        }
    }




    //Function to display information about user input station
    public static void DisplayStation(Dictionary<string, Station> stations)
    {
        Console.WriteLine();
        Console.Write("Enter Station Name: ");
        string chosenStation = Console.ReadLine();
        Console.WriteLine();
        //try and catch statements to output error message instead of throwing exception when invalid station entered
        try
        {
            Console.WriteLine($"Station Name: {stations[chosenStation].Name}");

            Console.WriteLine();
            Console.Write("Station Line(s): ");

            //Not outputting .Neighbours lines directly to avoid duplicate lines
            List<string> Lines = new List<string>();
            foreach (var neighbour in stations[chosenStation].Neighbours)
            {
                //check to make sure each line is only added once and then outputting list
                if (!Lines.Contains(neighbour.Item2))
                {
                    Lines.Add(neighbour.Item2);
                }
            }
            foreach (var line in Lines)
            {
                Console.Write(line + ", ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Adjacent Station(s): ");
            foreach (var neighbour in stations[chosenStation].Neighbours)
            {
                Console.Write(neighbour.Item1.Name + ", ");
            }
        }
        catch
        {
            Console.WriteLine("Invalid Option Entered! Please Try Again.");
        }
    }

    //Admin menu function
    public static void AdminMenu(Dictionary<string, Station> stations, Dictionary<(string, string), (int, string, string)> delays)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Manager Menu");
            Console.WriteLine("************");
            Console.WriteLine("1. Add delay");
            Console.WriteLine("2. Remove delay");
            Console.WriteLine("3. Add closure");
            Console.WriteLine("4. Remove closure");
            Console.WriteLine("5. Print closed routes");
            Console.WriteLine("6. Print delayed routes");
            Console.WriteLine("7. Return to Main Menu");
            Console.Write("Enter your choice (1-7): ");
            Console.WriteLine();
            int selection;
            int.TryParse(Console.ReadLine(), out selection);

            switch (selection)
            {
                //Case 1 adds delays to the delays dictionary which is checked when outputting shortest path
                case 1:
                    Console.Write("Enter Starting Station: ");
                    string firstStation = Console.ReadLine();

                    Console.Write("Enter Adjacent Station: ");
                    string secondStation = Console.ReadLine();

                    Console.Write("Enter Line of Occurence: ");
                    string delayLine = Console.ReadLine();

                    Console.Write("Enter Delay Amount: ");
                    int delay = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter Reason for Delay: ");
                    string delayReason = Console.ReadLine();

                    delays[(firstStation, secondStation)] = (delay, delayReason, delayLine);
                    delays[(secondStation, firstStation)] = (delay, delayReason, delayLine);

                    Console.WriteLine("Delay Added Successfully!");



                    break;
                    //removing delay from dictionary
                case 2:
                    Console.WriteLine("Enter Station: ");
                    string firstRemove = Console.ReadLine();

                    Console.WriteLine("Enter Adjacent Station: ");
                    string secondRemove = Console.ReadLine();

                    if (delays.ContainsKey((firstRemove, secondRemove)))
                    {
                        delays.Remove((firstRemove, secondRemove));
                        delays.Remove((secondRemove, firstRemove));

                        Console.WriteLine("Delay Removed Successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Delay could not be found.");
                    }

                    break;
                    //Using delays dictionary and setting delay time (value.Item1) as MaxValue to emulate a closure
                    //while using the same data structure for efficiency
                case 3:
                    Console.Write("Enter Starting Station: ");
                    string firstImpossible = Console.ReadLine();

                    Console.Write("Enter Adjacent Station: ");
                    string secondImpossible = Console.ReadLine();

                    Console.Write("Enter Close Line: ");
                    string closedLine = Console.ReadLine();

                    Console.Write("Enter Reason for Route Closure: ");
                    string impossibleReason = Console.ReadLine();

                    delays[(firstImpossible, secondImpossible)] = (int.MaxValue, impossibleReason, closedLine);
                    delays[(secondImpossible, firstImpossible)] = (int.MaxValue, impossibleReason, closedLine);

                    Console.WriteLine("Closure Added Successfully!");

                    break;
                    //Removing closure 
                case 4:
                    Console.WriteLine("Enter Station: ");
                    string removeFirstClosure = Console.ReadLine();

                    Console.WriteLine("Enter Adjacent Station: ");
                    string removeSecondClosure = Console.ReadLine();

                    if (delays.ContainsKey((removeFirstClosure, removeSecondClosure)))
                    {
                        delays.Remove((removeFirstClosure, removeSecondClosure));
                        delays.Remove((removeSecondClosure, removeFirstClosure));

                        Console.WriteLine("Closure Removed Successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Closure could not be found.");
                    }

                    break;
                    //Displaying closed routes
                case 5:
                    Console.WriteLine("Closed Routes: ");
                    foreach (var key in delays)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"{key.Value.Item3} Line: {key.Key.Item1} - {key.Key.Item2} due to {key.Value.Item2}");
                        Console.WriteLine();
                    }

                    break;
                    //Displaying delayed routes. Unfortunately the undelayed time could not be displayed
                    //due to difficulties traversing linked list for adjacent stations
                case 6:
                    Console.WriteLine("Delayed Routes: ");
                    foreach(var keys in delays)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"{keys.Value.Item3} Line: {keys.Key.Item1} - {keys.Key.Item2} delayed by {keys.Value.Item1} minutes");
                        Console.WriteLine();

                    }
             
                    break;
                case 7:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
        return;
    }

}




