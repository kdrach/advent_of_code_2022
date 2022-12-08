namespace AdventOfCode.Day7;

internal class Node<T>
{
    public Node<T>? Parrent;
    public List<Node<T>> NodeList { get; set; }
    public T? Directory { get; set; }
    public T? FileName { get; set; }
    public int Value { get; set; }

    public bool IsLeaf()
    {
        return NodeList.Count == 0;
    }

    public Node()
    {
        NodeList = new List<Node<T>>();
        Value = 0;
    }

    public double FindNodesWithValueLessThanN(ref List<double> outList, double n)
    {
        return FindValueNodeImpl(ref outList, n);
    }

    public double FindNodesWithValueMoreThanN(ref List<double> outList, double n)
    {
        return FindValueNodeImpl(ref outList, n, true);
    }

    private double FindValueNodeImpl(ref List<double> outList, double n, bool more = false)
    {
        double result = 0;
        if (IsLeaf())
            return result;
        foreach (var node in NodeList)
            if (node.IsLeaf())
            {
                result += node.Value;
            }
            else
            {
                var currentDir = node.FindValueNodeImpl(ref outList, n, more);
                result += currentDir;
                if (!more && currentDir <= n)
                {
                    outList.Add(currentDir);
                }
                else if (more && currentDir >= n)
                {
                    outList.Add(currentDir);
                }
            }

        return result;
    }

    public double Calculate()
    {
        double result = 0;
        if (IsLeaf())
            return Value;
        foreach (var node in NodeList)
            if (node.IsLeaf())
            {
                result += node.Calculate();
            }
            else
            {
                var currentDir = node.NodeList.Sum(x => x.Calculate());
                result += currentDir;
            }

        return result;
    }
}