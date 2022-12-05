using AdventOfCode.Infrastracture;

namespace AdventOfCode.Day2;

internal sealed class SolutionDayTwo : ISolution
{
    private const int LossScore = 0;
    private const int DrawScore = 3;
    private const int WinScore = 6;

    private const int RockScore = 1;
    private const int PaperScore = 2;
    private const int ScissorsScore = 3;

    public Dictionary<char, int> ChoiceDictionary = new()
    {
        { 'X', RockScore },
        { 'Y', PaperScore },
        { 'Z', ScissorsScore }
    };

    public Dictionary<char, Dictionary<char, int>> ResultDictionary = new()
    {
        {
            'A', new Dictionary<char, int>
            {
                { 'X', DrawScore },
                { 'Y', WinScore },
                { 'Z', LossScore }
            }
        },
        {
            'B', new Dictionary<char, int>
            {
                { 'X', LossScore },
                { 'Y', DrawScore },
                { 'Z', WinScore }
            }
        },
        {
            'C', new Dictionary<char, int>
            {
                { 'X', WinScore },
                { 'Y', LossScore },
                { 'Z', DrawScore }
            }
        }
    };

    public Dictionary<char, int> ChoiceToPointDictionary = new()
    {
        { 'X', LossScore },
        { 'Y', DrawScore },
        { 'Z', WinScore }
    };

    public object SolvePartOne(string filePath)
    {
        var allLines = File.ReadAllLines(filePath);
        var points = allLines.Select(line => ResultDictionary[line[0]][line[2]] + ChoiceDictionary[line[2]]).Sum();
        return points;
    }

    public object SolvePartTwo(string filePath)
    {
        var allLines = File.ReadAllLines(filePath);
        var points = allLines.Select(line => CalculatePoints(line, ChoiceToPointDictionary[line[2]])).Sum();
        return points;
    }

    private int CalculatePoints(string line, int resultValue)
    {
        var myChoice = ResultDictionary[line[0]].Single(t => t.Value == resultValue).Key;
        var myChoicePoints = ChoiceDictionary[myChoice];
        return resultValue + myChoicePoints;
    }
}