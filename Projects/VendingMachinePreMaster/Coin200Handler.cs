using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SPJVendingMachine2
{
    public class Coin200Handler : CoinHandler
    {

     
        private int numCoin;

        public Coin200Handler(int numCoin)
        {
            this.numCoin = numCoin;
            this.SetCoinValue(200);
        }


        public override void HandleCoin(int changeDue)
        {
            if (changeDue == 0)
            {
                Console.WriteLine();
                Console.WriteLine("No further change due");
                Console.WriteLine();
            }
            else if (changeDue < 200)
            {
                nextHandler.HandleCoin(changeDue);
            }
            else if (changeDue >= 200)
            {
                changeDue = changeDue - 200;
                numCoin--;
                Console.WriteLine();
                Console.WriteLine($"£{Convert.ToDouble(200) / 100} has been dispensed");
                Console.WriteLine($"Number of £2 coins left is {numCoin}");
                HandleCoin(changeDue);
            }



        }
    }
}
