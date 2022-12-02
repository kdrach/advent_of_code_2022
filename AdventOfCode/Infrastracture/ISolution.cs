using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Infrastracture
{
    internal interface ISolution
    {
        int SolvePartOne(string filePath);
        int SolvePartTwo(string filePath);
    }
}
