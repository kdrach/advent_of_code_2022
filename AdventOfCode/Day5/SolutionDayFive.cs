using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Infrastracture;

namespace AdventOfCode.Day5;

internal class SolutionDayFive : ISolution
{
    private readonly Regex _moveValueRegex = new(@"(?<=move\s).*(?=from)");
    private readonly Regex _sourceListRegex = new(@"(?<=from\s).*(?=to)");

    public object SolvePartOne(string filePath)
    {
        return SolvePuzzle(filePath, false);
    }

    public object SolvePartTwo(string filePath)
    {
        return SolvePuzzle(filePath, true);
    }

    private object SolvePuzzle(string filePath, bool isCrateMover9001)
    {
        var allLines = File.ReadAllLines(filePath);
        var line = GetLabelLine(allLines, out var labelLineNumber);
        var indexes = GenerateIndexesOfLabels(line);
        var cranes = ParseInputToDictionary(indexes, line, labelLineNumber, allLines);

        for (var i = labelLineNumber + 2; i < allLines.Length; i++)
        {
            var moveValue = int.Parse(_moveValueRegex.Match(allLines[i]).Value);
            var sourceListLabel = int.Parse(_sourceListRegex.Match(allLines[i]).Value);
            var destinationList = int.Parse(allLines[i].Last().ToString());
            if (isCrateMover9001)
                MoveCrateMover9001(cranes, sourceListLabel, moveValue, destinationList);
            else
                MoveCrateMover9000(moveValue, cranes, sourceListLabel, destinationList);
        }

        return GetTopElementsOfStacks(cranes);
    }

    private static void MoveCrateMover9001(Dictionary<int, LinkedList<string>> cranes, int sourceListLabel,
        int moveValue, int destinationList)
    {
        var valuesToMove = cranes[sourceListLabel].Take(moveValue).Reverse();

        foreach (var value in valuesToMove)
        {
            cranes[sourceListLabel].RemoveFirst();
            cranes[destinationList].AddFirst(value);
        }
    }

    private static void MoveCrateMover9000(int moveValue, Dictionary<int, LinkedList<string>> cranes,
        int sourceListLabel, int destinationList)
    {
        for (var j = 0; j < moveValue; j++)
        {
            var value = cranes[sourceListLabel].First.Value;
            cranes[sourceListLabel].RemoveFirst();
            cranes[destinationList].AddFirst(value);
        }
    }

    private static string GetLabelLine(string[] allLines, out int labelLineNumber)
    {
        var line = allLines[0];
        labelLineNumber = 0;
        while (!int.TryParse(line[1].ToString(), out var _))
        {
            labelLineNumber++;
            line = allLines[labelLineNumber];
        }

        return line;
    }

    private static Dictionary<int, LinkedList<string>> ParseInputToDictionary(IEnumerable<int> indexes, string line,
        int labelLineNumber, string[] allLines)
    {
        var cranes = new Dictionary<int, LinkedList<string>>();
        foreach (var index in indexes)
        {
            var label = int.Parse(line[index].ToString());
            var list = new LinkedList<string>();
            for (var j = 0; j < labelLineNumber; j++)
            {
                var stringRepresentation = allLines[j][index].ToString();
                if (!string.IsNullOrWhiteSpace(stringRepresentation))
                    list.AddLast(stringRepresentation);
            }

            cranes.Add(label, list);
        }

        return cranes;
    }

    private static IEnumerable<int> GenerateIndexesOfLabels(string line)
    {
        var lastLabel = line.Last();
        var lastLabelIndex = line.LastIndexOf(lastLabel);
        var indexes = Enumerable.Range(1, lastLabelIndex).Where(i => (i - 1) % 4 == 0 || i == 1);
        return indexes;
    }

    private static object GetTopElementsOfStacks(Dictionary<int, LinkedList<string>> stacks)
    {
        var stringBuilder = new StringBuilder();
        foreach (var stack in stacks) stringBuilder.Append(stack.Value.First.Value);
        return stringBuilder.ToString();
    }
}