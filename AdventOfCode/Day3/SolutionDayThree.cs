using AdventOfCode.Infrastracture;

namespace AdventOfCode.Day3;

internal class SolutionDayThree : ISolution
{
    public int SolvePartOne(string filePath)
    {
        var allLines = File.ReadAllLines(filePath);
        return allLines.Select(line =>
        {
            var first = line.Substring(0, line.Length / 2);
            var last = line.Substring(line.Length / 2, line.Length / 2);
            return CommonChars(first, last).Select(ConvertCharToPriority).Sum();
        }).Sum();
    }

    public int SolvePartTwo(string filePath)
    {
        var allLines = File.ReadAllLines(filePath);
        var chunkedLines = ChunkBy(allLines.ToList(),3);
        return chunkedLines.Select(cl =>
        {
            var commonCharsOfFirstTwo = CommonChars(cl[0], cl[1]);
            var commonChars = CommonChars(new string(commonCharsOfFirstTwo), cl[2]);
            return commonChars.Select(ConvertCharToPriority).Sum();
        }).Sum();
    }
    private char[] CommonChars(string left, string right)
    {
        return left.GroupBy(c => c).Join(
            right.GroupBy(c => c),
            k => k.Key,
            k => k.Key,
            (fg, lg) => 
                fg.Zip(lg, (x1, x2) => x2)).SelectMany(c=>c).Distinct().ToArray();
    }

    private int ConvertCharToPriority(char c)
    {
        var asciiValue = (int)c;
        if (asciiValue is >= 65 and <= 90) return asciiValue - 38;
        return asciiValue - 96;
    }

    public static List<List<T>> ChunkBy<T>(List<T> source, int chunkSize)
    {
        return source
            .Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index / chunkSize)
            .Select(x => x.Select(v => v.Value).ToList())
            .ToList();
    }
}