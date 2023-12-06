using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPJVendingMachine2
{

    public abstract class CoinHandler
    {
        private int coinValue;
        private int numCoin;
        protected CoinHandler nextHandler;
        protected int changeDue;



        public int GetCoinValue()
        {
            return coinValue;
        }
        public void SetCoinValue(int coinValue)
        {
            this.coinValue = coinValue;
        }
        public int GetNumCoin()
        {
            return numCoin;
        }
        public void SetNumCoin(int numCoin)
        {
            this.numCoin = numCoin;
        }
        public void SetNextHandler(CoinHandler handler)
        {
            nextHandler = handler;
        }

        public abstract void HandleCoin(int changeDue);
    }
}