using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPJVendingMachine2
{
    public class Coin100Handler : CoinHandler
    {
        
        private int numCoin;

        public Coin100Handler(int numCoin)
        {
            this.numCoin = numCoin;
            this.SetCoinValue(100);
        }



        public override void HandleCoin(int changeDue)
        {

            if (changeDue == 0)
            {
                Console.WriteLine();
                Console.WriteLine("No further change due");
                Console.WriteLine();
            }
            else if (changeDue < 100)
            {
                nextHandler.HandleCoin(changeDue);
            }
            else if (changeDue >= 100)
            {
                changeDue = changeDue - 100;
                numCoin--;
                Console.WriteLine();
                Console.WriteLine($"£{Convert.ToDouble(100) / 100} has been dispensed");
                Console.WriteLine($"Number of £1 coins left is {numCoin}");
                HandleCoin(changeDue);
            }



        }
    }
}