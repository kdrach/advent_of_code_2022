using AdventOfCode.Infrastracture;

namespace AdventOfCode.Day4;

internal class SolutionDayFour : ISolution
{
    public int SolvePartOne(string filePath)
    {
        var allLines = File.ReadAllLines(filePath);

        return allLines.Select(line =>
        {
            var (firstElfRange, secondElfRange) = GetElvesRanges(line);
            return RangeFullyOverlaps(firstElfRange, secondElfRange);
        }).Count(x => x);
    }

    public int SolvePartTwo(string filePath)
    {
        var allLines = File.ReadAllLines(filePath);

        return allLines.Select(line =>
        {
            var (firstElfRange, secondElfRange) = GetElvesRanges(line);
            return RangeOverlaps(firstElfRange, secondElfRange);
        }).Count(x => x);
    }

    private static (int[], int[]) GetElvesRanges(string line)
    {
        var splittedParts = line.Split(',').SelectMany(l => l.Split('-')).Select(int.Parse).ToArray();
        var firstElfRange = Enumerable.Range(splittedParts[0], splittedParts[1] - splittedParts[0] + 1)
            .ToArray();
        var secondElfRange = Enumerable.Range(splittedParts[2], splittedParts[3] - splittedParts[2] + 1)
            .ToArray();
        return (firstElfRange, secondElfRange);
    }

    private bool RangeFullyOverlaps(int[] firstElfRange, int[] secondElfRange)
    {
        return firstElfRange
                   .Intersect(secondElfRange)
                   .Count() == firstElfRange.Length ||
               secondElfRange.Intersect(firstElfRange).Count() == secondElfRange.Length;
    }

    private bool RangeOverlaps(int[] firstElfRange, int[] secondElfRange)
    {
        return firstElfRange
            .Intersect(secondElfRange)
            .Any();
    }
}