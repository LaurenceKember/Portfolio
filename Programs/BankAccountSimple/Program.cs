using System;

namespace BankAccountSimple
{
    class Program
    {
        static void Main(string[] args)
        {
            BankAccount account1 = new BankAccount("A1234", 1000);
            BankAccount account2 = new BankAccount("B1234", 2000);

            account1.Deposit(1500);
            account2.Deposit(1000);

            account1.Withdrawal(200);
            account2.Withdrawal(1300);

            if (account1.GetBalance() < account2.GetBalance())
            {
                Console.WriteLine("Account 2 has a larger balance than Account 1");
            }
            else
            {
                Console.WriteLine("Account 1 has a larger balance than Account 2");
            }
        }
    }
}

