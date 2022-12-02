using AdventOfCode.Infrastracture;

namespace AdventOfCode.Day1
{
    internal sealed class SolutionDayOne : ISolution
    {
        public int SolvePartOne(string filePath)
        {
            int elvesCount = 1;
            return GetSumOfCaloriesForTopElfs(filePath, elvesCount);
        }

        public int SolvePartTwo(string filePath)
        {
            int elvesCount = 3;
            return GetSumOfCaloriesForTopElfs(filePath, elvesCount);
        }
    
        private int GetSumOfCaloriesForTopElfs(string filePath, int elvesCount)
        {
            var fullText = File.ReadAllText(filePath);
            return fullText.Split("\n\n").Select(ge => ge.Split("\n")).Select(x => x.Sum(y =>
            {
                int.TryParse(y, out int n);
                return n;
            })).ToArray().OrderByDescending(p => p).Take(elvesCount).Sum();
        }
    }
}
