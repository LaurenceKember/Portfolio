using System;

namespace AverageScores
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Insert the number of scores: ");
            int size = Convert.ToInt32(Console.ReadLine());

            int[] scores = new int[size];  //creates an array to hold all the scores
            int i = 0;
            int sum = 0;
            double average = 0;
            for (i=0; i<scores.Length; i = i+1)  //loop through the array
            {
                int value;   //integer created for each score which will be entered

                Console.WriteLine($"Please enter score {i + 1}, scoring is between 1 and 5"); //enter value for score
                value = Convert.ToInt32(Console.ReadLine());  //convert value of score to int
                if (value > 0 && value <= 5) //tesing if score is valid, i.e. between 1 & 5
                {
                    scores[i] = value; //if valid score is added to the array
                }
                else
                {
                    Console.WriteLine("The score entered is not valid!"); //if not valid they are informed
                    i--;  //i is reset to disregard the previous entry and still allow all scores to be entered
                }

            }
            for (i=0;i<scores.Length; i++)  //loops through all the values in the array
            {
                sum += scores[i];  //totals each individual value in the array
            }
            average = sum / (double)size;  //calculates the average, size needs to be changed to double so that average remains a double
            Console.WriteLine($"The avererage score is {average}"); //print average
        }
    }
}

