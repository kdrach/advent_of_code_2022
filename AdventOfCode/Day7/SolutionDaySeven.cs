using AdventOfCode.Infrastracture;

namespace AdventOfCode.Day7;

internal class SolutionDaySeven : ISolution
{
    public object SolvePartOne(string filePath)
    {
        var allLines = File.ReadAllLines(filePath);
        var rootDirectoryIndex = allLines[0].IndexOf("cd") + 3;
        var rootDirectory = allLines[0][rootDirectoryIndex];
        var treeRoot = CreateTree(rootDirectory, allLines);
        var magicNumber = 100000;

        var outList = new List<double>();
        treeRoot.FindNodesWithValueLessThanN(ref outList, magicNumber);
        return outList.Sum();
    }

    public object SolvePartTwo(string filePath)
    {
        var allLines = File.ReadAllLines(filePath);

        var rootDirectoryIndex = allLines[0].IndexOf("cd") + 3;
        var rootDirectory = allLines[0][rootDirectoryIndex];
        var treeRoot = CreateTree(rootDirectory, allLines);
        var outList = new List<double>();
      
        double diskSpace = 70000000;
        double neededSpace = 30000000;
        var currentlyUsedSpace = treeRoot.Calculate();
        var currentlyFreeSpace = diskSpace - currentlyUsedSpace;
        var diff = neededSpace - currentlyFreeSpace;

        treeRoot.FindNodesWithValueMoreThanN(ref outList, diff);
        outList.Sort();
        return outList.First();
    }

    private static Node<string> CreateTree(char rootDirectory, string[] allLines)
    {
        var treeRoot = new Node<string> { NodeList = new List<Node<string>>(), Directory = rootDirectory.ToString() };
        var currentNode = treeRoot;

        for (var i = 1; i < allLines.Length; i++)
        {
            var currentLine = allLines[i];
            if (currentLine.Contains("$ ls")) continue;

            if (currentLine.Contains("dir"))
            {
                var splittedLine = allLines[i].Split(" ");
                var node = new Node<string>
                {
                    Directory = splittedLine[1],
                    Parrent = currentNode,
                    NodeList = new List<Node<string>>(),
                    Value = 0
                };
                currentNode.NodeList.Add(node);
                continue;
            }

            if (int.TryParse(currentLine[0].ToString(), out var _))
            {
                var splittedLine = allLines[i].Split(" ");
                var leaf = new Node<string>
                    { Parrent = currentNode, Value = int.Parse(splittedLine[0]), FileName = splittedLine[1] };
                currentNode.NodeList.Add(leaf);
                continue;
            }

            if (currentLine.Contains("cd"))
            {
                var splittedLine = allLines[i].Split(" ");
                if (splittedLine.Contains(".."))
                {
                    currentNode = currentNode.Parrent;
                }
                else
                {
                    var dirName = splittedLine[2];
                    currentNode = currentNode.NodeList.Single(c => c.Directory == dirName);
                }
            }
        }
        return treeRoot;
    }
}