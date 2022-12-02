namespace AdventOfCode;
using AdventOfCode.Infrastracture;
using System.Linq;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        var day = 1;
        var programType = typeof(Program);
        var rootNamespace = programType.Namespace;

        if (args.Length == 1)
        {
            day = int.Parse(args[0]);
        }
        var inputPath = $"Day{day}/Input/input.txt";
        var solutionNamespace = $"{rootNamespace}.Day{day}";

        var type = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.Namespace==solutionNamespace &&
            x.IsAssignableTo(typeof(ISolution))).
            Single();
 
        var solution = (ISolution) Activator.CreateInstance(type)!;

        var result1 = solution.SolvePartOne(inputPath);
        var result2 = solution.SolvePartTwo(inputPath);

        Console.WriteLine($"Resolutions for Day {day}");
        Console.WriteLine($"First soluion: {result1}");
        Console.WriteLine($"First soluion: {result2}");
    }
}