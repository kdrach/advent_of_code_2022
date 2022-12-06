using AdventOfCode.Infrastracture;

namespace AdventOfCode.Day6;

internal class SolutionDaySix : ISolution
{
    public object SolvePartOne(string filePath)
    {
        var signal = File.ReadAllText(filePath);
        var startOfPacketUniqueChars = 4;
        return GetObjectStartMarket(signal, startOfPacketUniqueChars);
    }

    public object SolvePartTwo(string filePath)
    {
        var signal = File.ReadAllText(filePath);
        var startOfMessageUniqueChars = 14;
        return GetObjectStartMarket(signal, startOfMessageUniqueChars);
    }

    private static object GetObjectStartMarket(string signal, int startOfObjectUniqueChars)
    {
        var result = 0;
        for (var i = 0; i < signal.Length; i++)
        {
            var match = signal.Skip(i).Take(startOfObjectUniqueChars).Distinct().Count() ==
                        startOfObjectUniqueChars;
            if (match)
            {
                result = i + startOfObjectUniqueChars;
                break;
            }
        }

        return result;
    }
}