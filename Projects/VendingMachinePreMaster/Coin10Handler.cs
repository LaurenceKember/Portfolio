using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPJVendingMachine2
{
    public class Coin10Handler : CoinHandler
    {
        
        private int numCoin;

        public Coin10Handler(int numCoin)
        {
            this.numCoin = numCoin;
            this.SetCoinValue(10);
        }

        public override void HandleCoin(int changeDue)
        {

            if (changeDue == 0)
            {
                Console.WriteLine();
                Console.WriteLine("No further change due");
                Console.WriteLine();
            }
            else if (changeDue < GetCoinValue())
            {
                nextHandler.HandleCoin(changeDue);
            }
            else if (changeDue >= GetCoinValue())
            {
                changeDue = changeDue - GetCoinValue();
                numCoin--;
                Console.WriteLine();
                Console.WriteLine($"£{Convert.ToDouble(GetCoinValue()) / 100} has been dispensed");
                Console.WriteLine($"Number of £0.10 coins left is {numCoin}");
                HandleCoin(changeDue);
            }



        }
    }
}

