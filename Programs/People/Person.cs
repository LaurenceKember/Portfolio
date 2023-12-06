using System;
namespace Tutorial5Ex4
{
    public class Person
    {
        private string name;
        private string surname;
        private double YearOfBirth;
        private string address;

        public Person(string namearg, string surnamearg, double yeararg)
        {
            name = namearg;
            surname = surnamearg;
            YearOfBirth = yeararg;
            address = "";
        }

        public string GetName(string name)
        {
            return name;
        }

        public string GetSurname(string surname)
        {
            return surname;
        }

        public double GetYearofbirth(double YearOfBirth)
        {
            return YearOfBirth;
        }

        public void SetAddress(string addr)
        {
            address = addr;
        }

        public string GetAddress(string address)
        {
            return address;
        }

        public void Display()
        {
            Console.WriteLine($"Name: {name} {surname}");
            Console.WriteLine($"Year of birth: {YearOfBirth}");
            Console.WriteLine($"Address: {address}");
        }
    }
}

