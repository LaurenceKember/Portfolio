using System;
namespace BankAccountSimple
{
    public class BankAccount
    {
        private string accnumber;
        private double balance;

        public BankAccount(string number, double bal)
        {
            accnumber = number;
            balance = bal;
        }

        public void Deposit(double dep)
        {
            balance = balance + dep;
        }

        public bool Withdrawal(double with)
        {
            if (with < balance)
            {
                balance -= with;
                return true;
            }
            else
            {
                return false;
            }
        }

        public double GetBalance()
        {
            return balance;
        }

        public string GetNumber()
        {
            return accnumber;
        }

        public void close()
        {
            balance = 0;
        }

    }
}

