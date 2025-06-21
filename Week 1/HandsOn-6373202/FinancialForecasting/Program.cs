using System;
public class Program
{
    public static void Main(string[] args)
    {
        double initialValue = 1000.0;  
        double growthRate = 0.10;    
        int years = 5;               

        double futureValue = PredictFuture(initialValue, growthRate, years);
        Console.WriteLine("Predicted value after " + years + " years: " + futureValue);
    }
    public static double PredictFuture(double currentValue, double rate, int years)
    {
        if (years == 0)
        {
            return currentValue;
        }

        double nextValue = currentValue * (1 + rate);
        return PredictFuture(nextValue, rate, years - 1);
    }
}