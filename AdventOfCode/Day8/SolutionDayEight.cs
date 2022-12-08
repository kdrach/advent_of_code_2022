using AdventOfCode.Infrastracture;

namespace AdventOfCode.Day8;

internal class SolutionDayEight : ISolution
{
    public object SolvePartOne(string filePath)
    {
        var allLines = File.ReadAllLines(filePath);

        var result = allLines.First().Length + allLines.Last().Length;
        result += (allLines.Length * 2)-4;

        for (var i = 1; i < allLines.Length - 1; i++)
        {
            var treeRow = allLines[i];
            for (var j = 1; j < treeRow.Length - 1; j++)
            {
                var currentTree = int.Parse(treeRow[j].ToString());

                var isVisibleFromLeft = !treeRow.Take(j).Any(x => int.Parse(x.ToString()) >=currentTree);
                var isVisibleFromRight = !treeRow.Skip(j+1).Any(x => int.Parse(x.ToString()) >= currentTree);

                var treeColumn = allLines.Select(x => int.Parse(x[j].ToString())).ToArray();

                var isVisibleFromBottom = !treeColumn.Take(i).Any(y => y>= currentTree); 
                var isVisibleFromTop = !treeColumn.Skip(i+1).Any(y => y >= currentTree); 

                if (isVisibleFromLeft || isVisibleFromRight || isVisibleFromBottom || isVisibleFromTop)
                {
                    result++;
                }
            }
        }

        return result;
    }

    public object SolvePartTwo(string filePath)
    {

        var allLines = File.ReadAllLines(filePath);
        var scenicScore = 0;

        for (var i = 1; i < allLines.Length - 1; i++)
        {
            var treeRow = allLines[i];
            for (var j = 1; j < treeRow.Length - 1; j++)
            {
               
                var currentTree = int.Parse(treeRow[j].ToString());

                var scenicScoreLeft = 0;
                for (var k = j-1; k >=0; k--)
                {
                    var currentValue = int.Parse(allLines[i][k].ToString());
                    if (currentTree <= currentValue)
                    {
                        scenicScoreLeft++;
                        break;
                    }
                    scenicScoreLeft++;
                }

                var scenicScoreRight = 0;
                for (var k = j+1; k < treeRow.Length; k++)
                {
                    var currentValue = int.Parse(allLines[i][k].ToString());
                    if (currentTree <= currentValue)
                    {
                        scenicScoreRight++;
                        break;
                    }

                    scenicScoreRight++;
                }

                var scenicScoreBottom = 0;
                for (var k = i+1; k< allLines.Length; k++)
                {
                    var currentValue = int.Parse(allLines[k][j].ToString());
                    if (currentTree <= currentValue)
                    {
                        scenicScoreBottom++;
                        break;
                    }

                    scenicScoreBottom++;
                }

                var scenicScoreTop = 0;
                for (var k = i-1; k >= 0; k--)
                {
                    var currentValue = int.Parse(allLines[k][j].ToString());
                    if (currentTree <= currentValue)
                    {
                        scenicScoreTop++;
                        break;
                    }

                    scenicScoreTop++;
                }

                if (i == 3 && j == 2)
                {
                    Console.WriteLine();
                }

                var tempScenicScore = scenicScoreLeft * scenicScoreRight * scenicScoreBottom * scenicScoreTop;
                if (tempScenicScore > scenicScore)
                    scenicScore = tempScenicScore;
            }
        }

        return scenicScore;
    }
}