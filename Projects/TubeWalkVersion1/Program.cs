using System;

//DSA Group Coursework Version 1 - hard coded using an Adjacency Matrix 
//Samuel Paul Jones May 2023

namespace TubeWalkVersion1
{
    class Program
    {
        static void Main(string[] args)
        {
            //ultimately will load data from external file via...
            //controller.LoadDataFromCSV(@"C:\Users\hunza\Downloads\DSACWK2\LUWT.csv");          

            //Customer Menu:

            bool exit = false;

            while (!exit)
            {
                //Console.Clear();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Welcome to the TfL Zone 1 Walking Times Main Menu");
                Console.WriteLine("*************************************************");
                Console.WriteLine("1. Find a walking route");
                Console.WriteLine("2. Display information");
                Console.WriteLine("3. Access Manager Menu");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice (1-4): ");

                int selection;
                int.TryParse(Console.ReadLine(), out selection);

                switch (selection)
                {
                    case 1:
                        FindRoute();
                        break;
                    case 2:
                        DisplayInfo();
                        break;
                    case 3:
                        AccessManagerMenu();
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

        private static RouteController controller = new RouteController();

        //Method for Finding Route via Dijkstra
        private static void FindRoute()
        {
            Console.WriteLine("Please enter the source station name:");
            string sourceName = Console.ReadLine();

            TubeStation sourceStation = controller.GetTubeStationByName(sourceName);
            if (sourceStation == null)
            {
                Console.WriteLine("...");
                Console.WriteLine("Press any key to return to the main menu.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Please enter the destination station name:");
            string destinationName = Console.ReadLine();

            TubeStation destinationStation = controller.GetTubeStationByName(destinationName);
            if (destinationStation == null)
            {
                Console.WriteLine("...");
                Console.WriteLine("Press any key to return to the main menu.");
                Console.ReadKey();
                return;
            }

            int result = controller.DijkstraSP(sourceName, destinationName);

        }

        //Method for displaying station info
        private static void DisplayInfo()
        {
            Console.Clear();

            Console.WriteLine("Please enter the station name:");
            string stationname = Console.ReadLine();

            TubeStation station = controller.GetTubeStationByName(stationname);

            if (station != null)
            {
                station.DisplayInfo();
            }
            else
            {
                Console.WriteLine("...");
            }

            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
        }

        //Accessing Manager Menu - password protected (password = "manager")
        private static void AccessManagerMenu()
        {
            Console.Clear();
            Console.WriteLine("Access Manager Menu");
            Console.WriteLine("*******************");
            Console.Write("Please enter the password (hint - it's \"manager\": ");
            string password = Console.ReadLine();

            if (password == "manager")
            {
                Console.WriteLine("Access granted.");
                bool exit = false;

                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("Manager Menu");
                    Console.WriteLine("************");
                    Console.WriteLine("1. Add delay");
                    Console.WriteLine("2. Remove delay");
                    Console.WriteLine("3. Add closure");
                    Console.WriteLine("4. Remove closure");
                    Console.WriteLine("5. Print delayed routes");
                    Console.WriteLine("6. Print closed routes");
                    Console.WriteLine("7. Return to Main Menu");
                    Console.Write("Enter your choice (1-7): ");

                    int selection;
                    int.TryParse(Console.ReadLine(), out selection);

                    switch (selection)
                    {
                        //Method for adding delay
                        case 1:
                            Console.WriteLine("Add Delay");

                            Console.WriteLine("Enter the name of the source station:");
                            string sourcename1 = Console.ReadLine();
                            TubeStation sourcestation1 = controller.GetTubeStationByName(sourcename1);
                            if (sourcestation1 == null)
                            {
                                Console.WriteLine("...");
                                Console.WriteLine("Press any key to return to the main menu.");
                                Console.ReadKey();
                                return;
                            }
                            int sourceid1 = sourcestation1.GetStationID();


                            Console.WriteLine("Enter the name of the destination station:");
                            string destinationname1 = Console.ReadLine();
                            TubeStation destinationstation1 = controller.GetTubeStationByName(destinationname1);
                            if (destinationstation1 == null)
                            {
                                Console.WriteLine("...");
                                Console.WriteLine("Press any key to return to the main menu.");
                                Console.ReadKey();
                                return;
                            }
                            int destinationid1 = destinationstation1.GetStationID();

                            Console.WriteLine("Enter the length of delay in minutes:");
                            int delay1 = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Enter the reason for the delay:");
                            string reason1 = Console.ReadLine();

                            controller.AddDelay(sourceid1, destinationid1, delay1, reason1);

                            Console.WriteLine("Delay added. Press any key to return to Manager Menu");
                            Console.ReadKey();
                            break;

                        //Method for removing delay
                        case 2:
                            Console.WriteLine("Remove Delay");
                            Console.WriteLine("Enter the name of the source station:");
                            string sourcename2 = Console.ReadLine();
                            TubeStation sourcestation2 = controller.GetTubeStationByName(sourcename2);
                            if (sourcestation2 == null)
                            {
                                Console.WriteLine("...");
                                Console.WriteLine("Press any key to return to the main menu.");
                                Console.ReadKey();
                                return;
                            }
                            int sourceid2 = sourcestation2.GetStationID();

                            Console.WriteLine("Enter the name of the destination station:");
                            string destinationname2 = Console.ReadLine();
                            TubeStation destinationstation2 = controller.GetTubeStationByName(destinationname2);
                            if (destinationstation2 == null)
                            {
                                Console.WriteLine("...");
                                Console.WriteLine("Press any key to return to the main menu.");
                                Console.ReadKey();
                                return;
                            }
                            int destinationid2 = destinationstation2.GetStationID();

                            Console.WriteLine("Enter the length of delay to be removed in minutes:");
                            int delay2 = Convert.ToInt32(Console.ReadLine());

                            controller.RemoveDelay(sourceid2, destinationid2, delay2);

                            Console.WriteLine("Delay removed. Press any key to return to Manager Menu");

                            Console.ReadKey();
                            break;

                        //Method for adding closure
                        case 3:
                            Console.WriteLine("Add Closure");
                            Console.WriteLine("Enter the name of the source station:");
                            string sourcename3 = Console.ReadLine();
                            TubeStation sourcestation3 = controller.GetTubeStationByName(sourcename3);
                            if (sourcestation3 == null)
                            {
                                Console.WriteLine("...");
                                Console.WriteLine("Press any key to return to the main menu.");
                                Console.ReadKey();
                                return;
                            }
                            int sourceid3 = sourcestation3.GetStationID();

                            Console.WriteLine("Enter the name of the destination station:");
                            string destinationname3 = Console.ReadLine();
                            TubeStation destinationstation3 = controller.GetTubeStationByName(destinationname3);
                            if (destinationstation3 == null)
                            {
                                Console.WriteLine("...");
                                Console.WriteLine("Press any key to return to the main menu.");
                                Console.ReadKey();
                                return;
                            }
                            int destinationid3 = destinationstation3.GetStationID();

                            Console.WriteLine("Enter the reason for the closure:");
                            string reason3 = Console.ReadLine();

                            controller.AddClosure(sourceid3, destinationid3, reason3);

                            Console.WriteLine("Closure added. Press any key to return to Manager Menu");
                            Console.ReadKey();
                            break;

                        //Method for removing closure
                        case 4:
                            Console.WriteLine("Remove Closure");
                            Console.WriteLine("Enter the name of the source station:");
                            string sourcename4 = Console.ReadLine();
                            TubeStation sourcestation4 = controller.GetTubeStationByName(sourcename4);
                            if (sourcestation4 == null)
                            {
                                Console.WriteLine("...");
                                Console.WriteLine("Press any key to return to the main menu.");
                                Console.ReadKey();
                                return;
                            }
                            int sourceid4 = sourcestation4.GetStationID();

                            Console.WriteLine("Enter the name of the destination station:");
                            string destinationname4 = Console.ReadLine();
                            TubeStation destinationstation4 = controller.GetTubeStationByName(destinationname4);
                            if (destinationstation4 == null)
                            {
                                Console.WriteLine("...");
                                Console.WriteLine("Press any key to return to the main menu.");
                                Console.ReadKey();
                                return;
                            }
                            int destinationid4 = destinationstation4.GetStationID();

                            Console.WriteLine("Enter the walking distance in minutes for the reopened route:");
                            int walktime4 = Convert.ToInt32(Console.ReadLine());

                            controller.RemoveClosure(sourceid4, destinationid4, walktime4);

                            Console.WriteLine("Closure removed. Press any key to return to Manager Menu");
                            Console.ReadKey();
                            break;

                        //Method for displaying info on current delayed routes
                        case 5:
                            Console.WriteLine("Printing delayed routes...");
                            controller.DisplayDelays();
                            Console.WriteLine();
                            Console.WriteLine("Press any key to return to Manager Menu");
                            Console.ReadKey();
                            break;

                        //Method for displaying info on current closed routes
                        case 6:
                            Console.WriteLine("Printing closed routes...");
                            controller.DisplayClosures();
                            Console.WriteLine();
                            Console.WriteLine("Press any key to return to Manager Menu");
                            Console.ReadKey();
                            break;

                        //Return to Main Menu
                        case 7:
                            exit = true;
                            break;

                        //Choices 1-7 only
                        default:
                            Console.WriteLine("Invalid choice. Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Incorrect password. Access denied.");
                Console.WriteLine("Press any key to return to the main menu.");
                Console.ReadKey();
            }


        }

    }
}

