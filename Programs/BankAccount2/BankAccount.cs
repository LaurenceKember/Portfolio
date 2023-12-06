using System;
namespace BankAccount2
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

        public void MoveAccount(BankAccount otherAccount)
        {
            if (IsOpen() && otherAccount != null && otherAccount.IsOpen())
            {
                otherAccount.Deposit(balance);
                close(); // Close the current account after transferring the balance
                Console.WriteLine($"Account {GetNumber()} moved to Account {otherAccount.GetNumber()}");
            }
            else
            {
                Console.WriteLine("MoveAccount failed: Either source or destination account is closed or invalid.");
            }
        }

        public bool IsOpen()
        {
            if (balance > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

