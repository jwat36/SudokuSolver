using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodokuSolver.Strategies
{
    internal interface ISudokuStrategy
    {
        int[,] Solve(int[,] sudokuBoard);
    }
}
