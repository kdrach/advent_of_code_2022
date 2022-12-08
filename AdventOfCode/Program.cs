using System.Reflection;
using AdventOfCode.Infrastracture;

namespace AdventOfCode;

internal class Program
{
    private static void Main(string[] args)
    {
        var day = 8;
        var programType = typeof(Program);
        var rootNamespace = programType.Namespace;

        if (args.Length == 1) day = int.Parse(args[0]);
        var inputPath = $"Day{day}/Input/input.txt";
        var solutionNamespace = $"{rootNamespace}.Day{day}";

        var type = Assembly
            .GetExecutingAssembly()
            .GetTypes().Single(x => x.Namespace == solutionNamespace &&
                                    x.IsAssignableTo(typeof(ISolution)));

        var solution = (ISolution)Activator.CreateInstance(type)!;

        var result1 = solution.SolvePartOne(inputPath);
        var result2 = solution.SolvePartTwo(inputPath);

        Console.WriteLine($"Solutions for Day {day}");
        Console.WriteLine($"First solution: {result1}");
        Console.WriteLine($"Second solution: {result2}");
    }
}