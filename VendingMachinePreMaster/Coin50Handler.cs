using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPJVendingMachine2
{
    public class Coin50Handler : CoinHandler
    {
        
        private int numCoin;

        public Coin50Handler(int numCoin)
        {
            this.numCoin = numCoin;
            this.SetCoinValue(50);
        }

        public override void HandleCoin(int changeDue)
        {

            if (changeDue == 0)
            {
                Console.WriteLine();
                Console.WriteLine("No further change due");
                Console.WriteLine();
            }
            else if (changeDue < 50)
            {
                nextHandler.HandleCoin(changeDue);
            }
            else if (changeDue >= 50)
            {
                changeDue = changeDue - 50;
                numCoin--;
                Console.WriteLine();
                Console.WriteLine($"£{Convert.ToDouble(50) / 100} has been dispensed");
                Console.WriteLine($"Number of £0.50 coins left is {numCoin}");
                HandleCoin(changeDue);
            }



        }
    }
}
