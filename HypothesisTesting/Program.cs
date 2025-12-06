// stats hypothesis testing
using System;
using System.Collections.Generic;

namespace HypothesisTesting
{
    internal class Program
    {
        static List<double> binomialCD = new List<double>() {};

        // input for closest or rounded

        // input for significance level

        // input for number of tails

        // input for number of trials

        // input for probability of success

        // input for number of successes

        // combination function
        static double Combination(int trials, int currentTrialNumber)
        {
            return (Factorial(trials)) / (Factorial(trials - currentTrialNumber) * Factorial(currentTrialNumber));
        }

        // factorial function
        static int Factorial(int number)
        {
            if (number == 0)
            {
                return 1;
            }
            else
            {
                return number * Factorial(number - 1);
            }
        }

        // generate binomial cumulative distribution list
        static void GenerateBinomialCDList(int trials, double successProbablity)
        {
            double PDForTrial;

            PDForTrial = Math.Pow((1 - successProbablity), trials);
            binomialCD.Add(PDForTrial);

            for (int i = 1; i < trials + 1; i++)
            {
                PDForTrial = Combination(trials, i) * Math.Pow(successProbablity, i) * Math.Pow((1 - successProbablity), (trials - i));    
                
                PDForTrial = PDForTrial + binomialCD[i - 1];

                binomialCD.Add(PDForTrial);            
            }
        }

        // prints the binomial cumulative distribution list
        static void PrintBinomialCDList()
        {
            for(int i = 0; i < binomialCD.Count; i++)
            {
                Console.WriteLine("P(X ≤ " + i + ") = " + binomialCD[i]);
            }
        }

        // main method
        static void Main(string[] args)
        {
            GenerateBinomialCDList(10, 0.25);
            PrintBinomialCDList();
        }
    }
}

// smol change