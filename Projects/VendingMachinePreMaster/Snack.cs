using System;
namespace SPJVendingMachine2
{
    public class Snack
    {
        private int snackNumber;
        private string Name { get; set; }
        private int Price { get; set; }
        private int Quantity { get; set; }
        public Snack(int index, string name, int price, int quantity)
        {
            snackNumber = index;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public int GetSnackNumber()
        {
            return snackNumber;
        }

        public void SetSnackNumber(int snackNumber)
        {
            this.snackNumber = snackNumber;
        }

        public string getName()
        {
            return Name;
        }

        public int getPrice()
        {
            return Price;
        }

        public void setPrice(int price)
        {
            this.Price = price;
        }
        public void ReduceQuantity()
        {
            Quantity--;
        }
        public bool IsOutOfStock()
        {
            return Quantity == 0;
        }

        public void Display()
        {
            if (IsOutOfStock())
            {
                Console.WriteLine("No " + Name + " today sorry!");
                Console.WriteLine();
            }
            else
            {
                Console.Write(snackNumber.ToString() + ". ");
                Console.Write(Name);
                double displayPrice = Convert.ToDouble(Price) / 100;
                Console.WriteLine(" - Price: £" + displayPrice.ToString());
                Console.WriteLine("(Number available: " + Quantity.ToString() + ")");
                Console.WriteLine();
            }
        }

        public void DisplayPrice()
        {
            Console.Write(snackNumber.ToString() + ". ");
            Console.Write(Name);
            double displayPrice = Convert.ToDouble(Price) / 100;
            Console.WriteLine(" - Price: " + displayPrice.ToString());
            Console.WriteLine();
        }
    }
}


