using _1_Halstead_Metrics.Analysis;

namespace _1_Halstead_Metrics.AnalysisIO;

public static class HalsteadIO
{
    public static (int, int) InputParameters()
    {
        Console.WriteLine("Enter dictionary size (for example, 16):");
        var dictionarySize = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter sample size (for example, 100):");
        var sampleSize = int.Parse(Console.ReadLine()!);

        return (dictionarySize, sampleSize);
    }

    public static void PrintResultValues(HalsteadResult result)
    {
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Statistical Values:");
        Console.WriteLine($"Average (M):\t\t{Math.Round(result.StatisticalValues.ExpectedValue, 2)}");
        Console.WriteLine($"Disperse (D):\t\t{Math.Round(result.StatisticalValues.Dispersion, 2)}");
        Console.WriteLine($"Measurement error (d):\t{result.StatisticalValues.MeasurementError}");
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Theoretical Values:");
        Console.WriteLine($"Average (M):\t\t{Math.Round(result.TheoreticalValues.ExpectedValue, 2)}");
        Console.WriteLine($"Disperse (D):\t\t{Math.Round(result.TheoreticalValues.Dispersion, 2)}");
        Console.WriteLine($"Measurement error (d):\t{result.TheoreticalValues.MeasurementError}");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"Stable Sample Size:\t{(int)result.NewExpectedSampleSize:### ### ##0}");
        Console.ResetColor();

        Console.WriteLine();
    }
}