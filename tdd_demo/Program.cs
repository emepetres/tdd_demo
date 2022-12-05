using CommandLine.Text;
using CommandLine;
using System.ComponentModel.Design;

namespace tdd_demo;
public class Program
{
    public class Options
    {
        [Option("csv", HelpText = "CSV file to be filtered.")]
        public string csv_path { get; set; }
    }

    public static int Main(string[] args)
    {
        var parse_result = Parser.Default.ParseArguments<Options>(args);

        if (parse_result.Errors.Count() > 0)
        {
            foreach (var err in parse_result.Errors)
            {
                Console.Error.WriteLine(err.ToString());
            }
            
            return 1;
        }

        // ensure csv path is correct
        var csv_path = parse_result.Value.csv_path;
        if (csv_path == null)
        {
            Console.Error.WriteLine("CSV path should not be null.");
            return 1;
        }
        if (!File.Exists(csv_path))
        {
            Console.Error.WriteLine("CSV file not found.");
            return 1;
        }
        if (!csv_path.ToLower().EndsWith("csv"))
        {
            Console.Error.WriteLine("Input file must be of csv extension.");
            return 1;
        }

        return 0;
    }

    public static int FilterCSV(Options opts)
    {
        Console.WriteLine("Hello, World!");

        return 0;
    }
}