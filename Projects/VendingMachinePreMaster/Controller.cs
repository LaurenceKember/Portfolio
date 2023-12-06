using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization.Metadata;

namespace SPJVendingMachine2
{
    public class VendingMachineA : iVendingMachine
    {
        protected CoinHandler coin200Handler;
        protected CoinHandler coin100Handler;
        protected CoinHandler coin50Handler;
        protected CoinHandler coin20Handler;
        protected CoinHandler coin10Handler;
        protected CoinHandler coin5Handler;
        //int numCoin;

        private Dictionary<int, Snack> snacks = new Dictionary<int, Snack>();
        private Dictionary<int, CoinHandler> coinhandlers = new Dictionary<int, CoinHandler>();

        public int totalMoney = 1200;

        // Constructor 
        public VendingMachineA()
        {

        }
        public bool AddSnack(int index, Snack snack)
        {
            snacks.Add(index, snack);
            return true;
        }

        public bool AddCoinHandler(int coinValue, CoinHandler coinhandler)
        {
            coinhandlers.Add(coinValue, coinhandler);
            return true;
        }

        //Method to emulate a database with various transaction scenarios to be printed to named file
        public void GenerateReport(Snack snack, int amountPaid, int changeDue, int possibility)
        {
            StreamWriter streamWriter = new StreamWriter("dataBase");
            DateTime now = DateTime.Now;

            if (possibility == 1)
            {
                streamWriter.WriteLine();
                streamWriter.WriteLine($"Transaction Date and Time: {now}");
                streamWriter.WriteLine();
                streamWriter.WriteLine("Transaction Unsuccessful - Invalid Coins Entered");
                streamWriter.WriteLine();
            }
            else if(possibility == 2)
            {
                streamWriter.WriteLine();
                streamWriter.WriteLine($"Transaction Date and Time: {now}");
                streamWriter.WriteLine();
                streamWriter.WriteLine("Transaction Unsuccessful - Unable to Dispense Change");
                streamWriter.WriteLine();
            }
            else if(possibility == 3)
            {
                streamWriter.WriteLine();
                streamWriter.WriteLine($"Transaction Date and Time: {now}");
                streamWriter.WriteLine($"Snack Bought: {snack.getName()}");
                streamWriter.WriteLine($"Amount Paid: £{Convert.ToDouble(amountPaid) / 100} - Change Given: £{Convert.ToDouble(changeDue) / 100}");
                streamWriter.WriteLine($"Total Money After Transaction: £{Convert.ToDouble(totalMoney) / 100}");
                streamWriter.WriteLine();
            }
            streamWriter.Close();
        }



        public void ListSnacks()
        {
            Console.WriteLine();
            Console.WriteLine("Available Snacks:");
            Console.WriteLine();
            foreach (KeyValuePair<int, Snack> nextSnackPair in snacks)
            {
                //int nextSnackNumber = nextSnackPair.Key;
                Snack nextSnack = nextSnackPair.Value;
                nextSnack.Display();
            }
        }

        public Snack GetSnack(int snackNumber)
        {
            Snack snack = null;
            foreach (KeyValuePair<int, Snack> nextSnackPair in snacks)
            {
                int nextSnackNumber = nextSnackPair.Key;
                Snack nextSnack = nextSnackPair.Value;
                if (nextSnack.GetSnackNumber() == snackNumber)
                {
                    snack = nextSnack;
                    break;
                }
            }
            return snack;
        }

        public CoinHandler GetCoinHandler(int coinValue)
        {
            CoinHandler coinHandler = null;
            foreach (KeyValuePair<int, CoinHandler> nextCoinHandlerPair in coinhandlers)
            {
                int nextCoinValue = nextCoinHandlerPair.Key;
                CoinHandler nextCoinHandler = nextCoinHandlerPair.Value;
                //Console.WriteLine(coinValue + " " + nextCoinHandler.GetCoinValue());
                if (nextCoinHandler.GetCoinValue() == coinValue)
                {
                    coinHandler = nextCoinHandler;
                    break;
                }
            }
            return coinHandler;
        }

        public void PurchaseSnack(int snackNumber, CoinHandler coinhandler)
        {          
            Snack snack = GetSnack(snackNumber);

            double coin = 0;
            int amountPaid = 0;
            while (amountPaid < snack.getPrice())
            {
                Console.Write("Insert coins: ");
                coin = Convert.ToDouble(Console.ReadLine());
                if (coin != 2 && coin != 1 && coin != 0.5 && coin != 0.2 && coin != 0.1 && coin != 0.05)
                {
                    GenerateReport(snack, amountPaid, 0, 1);
                    throw new ArgumentException("Invalid coin entered! Please try again");
                }
                coin = coin * 100.0;

                amountPaid = amountPaid + Convert.ToInt32(coin);
            }

            int changeDue = amountPaid - snack.getPrice();
            totalMoney = totalMoney + (amountPaid - changeDue);

            if (changeDue <= totalMoney && changeDue % 5 == 0)          //simplified version of change checking process
            {
                coinhandler.HandleCoin(changeDue);

            }
            else
            {
                Console.WriteLine("Sorry - not enough change. Coins being returned.");
                GenerateReport(snack, amountPaid, changeDue, 2);
                //Coin return method
            }

            Console.WriteLine();
            snack.ReduceQuantity();
            //snacks[index] = snack;
            Console.WriteLine("Enjoy your " + snack.getName() + "!");
            GenerateReport(snack, amountPaid, changeDue, 3);
        }

        public void adminMenu()
        {
            string password;
            Console.Write("Enter password: ");
            password = Console.ReadLine();
            if (password != "A5144l")
            {
                Console.WriteLine("Incorrect password!");
                return;
            }
            Console.WriteLine();
            Console.WriteLine("1. Change snack prices");
            Console.WriteLine();
            Console.WriteLine("2. Increase change pool");
            Console.WriteLine();
            Console.WriteLine("3. Total money in machine");
            Console.WriteLine();
            int choice;
            Console.Write("Enter choice: ");
            choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    ChangeSnackPrices();
                    break;
                case 2:
                    Console.WriteLine("Which type of coin would you like to add?");
                    Console.WriteLine("(NB Please enter 2, 1, 0.50, 0.20, 0.10 or 0.05)");
                    double inputCoinValue = Convert.ToDouble(Console.ReadLine()) * 100;
                    int coinValue = Convert.ToInt32(inputCoinValue);
                    CoinHandler coinhandler = GetCoinHandler(coinValue);
                    IncreaseChangePool(coinhandler);
                    break;
                case 3:
                    double displayTotal = Convert.ToDouble(totalMoney) / 100;       //dividing by 100 was easier than sifting through many stackoverflow pages. Apologies for lazy solution lol.
                    Console.WriteLine("Total money in machine: £" + displayTotal.ToString());
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }

        // Method to change snack prices
        public void ChangeSnackPrices()
        {
            Console.WriteLine();
            Console.WriteLine("Current Snack Prices:");
            Console.WriteLine();
            foreach (KeyValuePair<int, Snack> nextSnackPair in snacks)
            {
                //int nextSnackNumber = nextSnackPair.Key;
                Snack nextSnack = nextSnackPair.Value;
                nextSnack.DisplayPrice();
            }
            Console.WriteLine("Select Snack number to change price: ");
            int snackNumber = Convert.ToInt32(Console.ReadLine());
            if (snackNumber != 1 && snackNumber != 2 && snackNumber != 3)
            {
                throw new ArgumentException("Invalid Snack Choice");
            }
            Console.WriteLine("Enter new price: ");
            double inputPrice = Convert.ToDouble(Console.ReadLine()) * 100;
            int newPrice = Convert.ToInt32(inputPrice);
            Snack snack = GetSnack(snackNumber);
            snack.setPrice(newPrice);
        }

        public void IncreaseChangePool(CoinHandler coinhandler)
        {
            int count;
            if (coinhandler.GetCoinValue() >= 100)
            {
                Console.Write("Enter number of £" + (coinhandler.GetCoinValue() / 100) + " coins to add: ");
                count = Convert.ToInt32(Console.ReadLine());
                int oldNumCoin = coinhandler.GetNumCoin();
                coinhandler.SetNumCoin(oldNumCoin + count);
                Console.WriteLine();
                Console.WriteLine(count + " £" + Convert.ToDouble(coinhandler.GetCoinValue()) / 100 + " coins added.");
            }
            else
            {
                Console.Write("Enter number of " + coinhandler.GetCoinValue() + "p coins to add: ");
                count = Convert.ToInt32(Console.ReadLine());
                int oldNumCoin = coinhandler.GetNumCoin();
                coinhandler.SetNumCoin(oldNumCoin + count);
                Console.WriteLine();
                Console.WriteLine(count + " " + Convert.ToDouble(coinhandler.GetCoinValue()) / 100 + "coins added.");
            }
            totalMoney = totalMoney + coinhandler.GetCoinValue() * count;

        }

        public int ChangePoolTotal()
        {
            int coin200Total = (coin200Handler.GetNumCoin()) * 200;
            int coin100Total = (coin100Handler.GetNumCoin()) * 100;
            int coin50Total = (coin50Handler.GetNumCoin()) * 50;
            int coin20Total = (coin20Handler.GetNumCoin()) * 20;            //This is not called anywhere and it's task is done by Controller.cs line 115. Can we delete it?
            int coin10Total = (coin10Handler.GetNumCoin()) * 10;
            int coin5Total = (coin5Handler.GetNumCoin()) * 5;
            int coinTotal = coin200Total + coin100Total + coin50Total + coin20Total + coin10Total + coin5Total;
            return coinTotal;
        }
        static void Main(string[] args)
        {

            VendingMachineA vendingMachineA = new VendingMachineA();


            Snack newSnack1 = new Snack(1, "Skittles", 150, 5);
            Snack newSnack2 = new Snack(2, "Doritos", 175, 8);
            Snack newSnack3 = new Snack(3, "Bovril", 50, 15);

            vendingMachineA.AddSnack(1, newSnack1);
            vendingMachineA.AddSnack(2, newSnack2);
            vendingMachineA.AddSnack(3, newSnack3);



            CoinHandler coin5Handler = new Coin5Handler(20);
            CoinHandler coin10Handler = new Coin10Handler(10);
            CoinHandler coin20Handler = new Coin20Handler(5);
            CoinHandler coin50Handler = new Coin50Handler(4);
            CoinHandler coin100Handler = new Coin100Handler(3);
            CoinHandler coin200Handler = new Coin200Handler(2);

            vendingMachineA.AddCoinHandler(5, coin5Handler);
            vendingMachineA.AddCoinHandler(10, coin10Handler);
            vendingMachineA.AddCoinHandler(20, coin20Handler);
            vendingMachineA.AddCoinHandler(50, coin50Handler);
            vendingMachineA.AddCoinHandler(100, coin100Handler);
            vendingMachineA.AddCoinHandler(200, coin200Handler);




            coin10Handler.SetNextHandler(coin5Handler);
            coin20Handler.SetNextHandler(coin10Handler);
            coin50Handler.SetNextHandler(coin20Handler);
            coin100Handler.SetNextHandler(coin50Handler);
            coin200Handler.SetNextHandler(coin100Handler);


            while (true)
            {
                Console.WriteLine("    ******************************************************");
                Console.WriteLine("       AlpLaurSam Vending Machines LLc. Plc. Ltd. Corp.");
                Console.WriteLine("    ******************************************************");
                Console.WriteLine("");
                Console.WriteLine("1. Purchase snack");
                Console.WriteLine();
                Console.WriteLine("2. Exit");
                Console.WriteLine("");
                Console.WriteLine("Only enter £2.0, £1.0, £0.5, £0.2, £0.1 or £0.05 coins");

                int choice;
                Console.Write("Enter choice: ");
                choice = int.Parse(Console.ReadLine());



                switch (choice)
                {
                    case 1:
                        {

                            vendingMachineA.ListSnacks();

                            Console.Write("Enter snack number: ");
                            int number = Convert.ToInt32(Console.ReadLine());
                            if (number != 1 && number != 2 && number != 3)
                            {
                                throw new ArgumentException("Invalid Snack Choice! Please try again.");
                            }
                            vendingMachineA.PurchaseSnack(number, coin200Handler);
                            break;
                        }

                    case 1011:
                        {
                            vendingMachineA.adminMenu();
                            break;
                        }

                    case 2:
                        {
                            return;
                        }

                    default:
                        {
                            Console.WriteLine("Invalid choice!");
                            break;
                        }
                }

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("AlpLaurSam is a registered charity in Mordor and the Cayman Islands");
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }
    }

}

