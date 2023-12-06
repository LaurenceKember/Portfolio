using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPJVendingMachine2
{
    public interface iVendingMachine
    {
        //public int FindSnackIndex(string name);
        //public List<(int, int)> CalculateChange(int amountPaid, int price);
        public void PurchaseSnack(int index, CoinHandler coinhandler);
        public void adminMenu();
        public void ChangeSnackPrices();
        public void IncreaseChangePool(CoinHandler coinhandler);
    }
}



