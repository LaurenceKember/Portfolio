using System;

namespace BankAccount2
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

            Console.WriteLine("Account " + account1.GetNumber() + " open? " + account1.IsOpen());
            Console.WriteLine("Account " + account1.GetNumber() + " balance: " + account1.GetBalance());
            Console.WriteLine("Account " + account2.GetNumber() + " open? " + account2.IsOpen());
            Console.WriteLine("Account " + account2.GetNumber() + " balance: " + account2.GetBalance());
            Console.WriteLine();

            if (account1.IsOpen() && account2.IsOpen())
            {
                account1.Withdrawal(50.0);
                account1.MoveAccount(account2);
            }

            Console.WriteLine("Account " + account1.GetNumber() + " open? " + account1.IsOpen());
            Console.WriteLine("Account " + account1.GetNumber() + " balance: " + account1.GetBalance());
            Console.WriteLine("Account " + account2.GetNumber() + " open? " + account2.IsOpen());
            Console.WriteLine("Account " + account2.GetNumber() + " balance: " + account2.GetBalance());
        }
    }
}

