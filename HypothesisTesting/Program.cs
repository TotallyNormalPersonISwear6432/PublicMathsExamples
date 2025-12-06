// stats hypothesis testing
using System;
using System.Collections.Generic;
using ScottPlot;

namespace HypothesisTesting
{
    internal class Program
    {
        static List<double> binomialCD = new List<double>();
        static List<double> binomialPD = new List<double>();

        // combination function (numerically stable, avoids large factorials)
        static double Combination(int n, int k)
        {
            if (k < 0 || k > n) return 0.0;
            if (k > n - k) k = n - k;

            double result = 1.0;
            for (int i = 1; i <= k; i++)
            {
                result *= (n - (k - i));
                result /= i;
            }

            return result;
        }

        // generate binomial cumulative distribution list
        static void GenerateBinomialCDList(int trials, double successProbability)
        {
            if (trials < 0) throw new ArgumentOutOfRangeException(nameof(trials));
            if (successProbability < 0.0 || successProbability > 1.0) throw new ArgumentOutOfRangeException(nameof(successProbability));

            binomialCD.Clear();

            // P(X = 0)
            double pXeq0 = Math.Pow(1.0 - successProbability, trials);
            double cumulative = pXeq0;
            binomialCD.Add(cumulative);

            for (int i = 1; i <= trials; i++)
            {
                double pXeqi = Combination(trials, i) * Math.Pow(successProbability, i) * Math.Pow(1.0 - successProbability, trials - i);
                cumulative += pXeqi;
                binomialCD.Add(cumulative);
            }
        }

        // gnerate binomial probability distribution list
        static void GenerateBinomialPDList(int trials, double successProbability)
        {
            if (trials < 0) throw new ArgumentOutOfRangeException(nameof(trials));
            if (successProbability < 0.0 || successProbability > 1.0) throw new ArgumentOutOfRangeException(nameof(successProbability));
            binomialPD.Clear();
            for (int i = 0; i <= trials; i++)
            {
                double pXeqi = Combination(trials, i) * Math.Pow(successProbability, i) * Math.Pow(1.0 - successProbability, trials - i);
                binomialPD.Add(pXeqi);
            }
        }


        // prints the binomial cumulative distribution list
        static void PrintBinomialCDList()
        {
            for (int i = 0; i < binomialCD.Count; i++)
            {
                Console.WriteLine($"P(X ≤ {i}) = {binomialCD[i]:F6}");
            }
        }

        // prints the binomial probability distribution list
        static void PrintBinomialPDList()
        {
            for (int i = 0; i < binomialPD.Count; i++)
            {
                Console.WriteLine($"P(X = {i}) = {binomialPD[i]:F6}");
            }
        }

        // main method
        static void Main(string[] args)
        {
            int trials = 250;
            double p = 0.5;

            GenerateBinomialPDList(trials, p);
            PrintBinomialPDList();

            // prepare arrays for plotting
            double[] xs = new double[binomialPD.Count];
            for (int i = 0; i < xs.Length; i++) xs[i] = i;
            double[] ys = binomialPD.ToArray();

            // create plot and save to PNG
            var plt = new ScottPlot.Plot();
            int plotWidth = 700;
            int plotHeight = 400;
            plt.Title("Binomial Probability Distribution");
            plt.XLabel("k");
            plt.YLabel("P(X ≤ k)");
            plt.Add.Scatter(xs, ys);
            plt.Axes.SetLimitsX(-0.5, trials + 0.5);
            //plt.Axes.SetLimitsY(0, 1.05);

            string outFile = "binomial_pdf.png";
            plt.SavePng(outFile, plotWidth, plotHeight);

            Console.WriteLine("Saved plot to " + outFile);
        }
    }
}
