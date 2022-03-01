using SudokuSolver.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodokuSolver.Strategies
{
    internal class SimpleMarkupStrategy : ISudokuStrategy
    {
        private readonly SudokuMapper sudokuMapper;
        private SudokuMapper _sudokuMapper;

        public SimpleMarkupStrategy(SudokuMapper sudokuMapper)
        {
            _sudokuMapper = sudokuMapper;
        }

        public int[,] Solve(int[,] sudokuBoard)
        {
            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetLength(1); col++)
                {
                    var possibilitiesRowCol = GetPossibilitiesRowCol(sudokuBoard, row, col);
                    var possibilitiesBlock = GetPossiblitiesBlock(sudokuBoard, row, col);
                    sudokuBoard[row, col] = GetPossibilitiesIntersect(possibilitiesRowCol, possibilitiesBlock);
                }
            }
            return sudokuBoard;
        }

        private int GetPossibilitiesRowCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            int[] possibilities = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int col = 0; col < 9; col++) if (IsValidSingle(sudokuBoard[givenRow, col])) possibilities[sudokuBoard[givenRow, col] - 1] = 0;
            for (int row = 0; row < 9; row++) if (IsValidSingle(sudokuBoard[row, givenCol])) possibilities[sudokuBoard[row, givenCol] - 1] = 0;
            return Convert.ToInt32(String.Join(string.Empty, possibilities.Select(p => p).Where(p => p != 0)));
        }

        private int GetPossiblitiesBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            int[] possibilities = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var sudokuMap = _sudokuMapper.Find(givenRow, givenCol);

            for (int row = sudokuMap.StartRow; row <= sudokuMap.StartRow + 2; row++)
            {
                for (int col = sudokuMap.StartCol; col < sudokuMap.StartCol + 2; col++)
                {
                    if (IsValidSingle(sudokuBoard[row, col])) possibilities[sudokuBoard[row, col] - 1] = 0;
                }
            }

            return Convert.ToInt32(String.Join(string.Empty, possibilities.Select(p => p).Where(p => p != 0)));
        }
        private bool IsValidSingle(int digit)
        {
             return digit != 0 && digit.ToString().Length < 2;
        }

        private int GetPossibilitiesIntersect(object possibilitiesRowCol, object possibilitiesBlock)
        {
            var possibilitiesRowColCharArray = possibilitiesRowCol.ToString().ToCharArray();
            var possibilitiesBlockArray = possibilitiesBlock.ToString().ToCharArray();
            var possibilitiesSubset = possibilitiesRowColCharArray.Intersect(possibilitiesBlockArray);
            return Convert.ToInt32(string.Join(string.Empty, possibilitiesSubset));
        }

        
    }
}
