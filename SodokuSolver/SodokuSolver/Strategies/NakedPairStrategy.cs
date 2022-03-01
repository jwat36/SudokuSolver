using SudokuSolver.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodokuSolver.Strategies
{
    class NakedPairStrategy : ISudokuStrategy
    {
        private readonly SudokuMapper _SudokuMapper;

        public NakedPairStrategy(SudokuMapper sudokuMapper)
        {
            _SudokuMapper = sudokuMapper;
        }

        public int[,] Solve(int[,] sudokuBoard)
        {
            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetLength(1); col++)
                {
                    eliminatePairFromRow(sudokuBoard, row, col);
                    eliminatePairFromCol(sudokuBoard, row, col);
                    eliminatePairFromBlock(sudokuBoard, row, col);
                }
            }
            return sudokuBoard;
        }

        private void eliminatePairFromRow(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            if (!PairInRow(sudokuBoard, givenRow, givenCol)) return;

            for (int col = 0; col < sudokuBoard.GetLength(1); col++)
            {
                if (sudokuBoard[givenRow, col] != sudokuBoard[givenRow, givenCol] && sudokuBoard[givenRow, col].ToString().Length > 1)
                {
                    eliminatePair(sudokuBoard, sudokuBoard[givenRow, givenCol], givenRow, col);
                }
            }
        }

        private void eliminatePair(int[,] sudokuBoard, int valuesToEliminate, int eliminateFromRow, int eliminateFromCol)
        {
            var valuesToEliminateArray = valuesToEliminate.ToString().ToCharArray();
            foreach (var valueToEliminate in valuesToEliminateArray)
            {
                sudokuBoard[eliminateFromRow, eliminateFromCol] = 
                    Convert.ToInt32(sudokuBoard[eliminateFromRow, eliminateFromCol].ToString().Replace(valueToEliminate.ToString(), string.Empty));
            }
        }

        private bool PairInRow(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            for (int col = 0; col < sudokuBoard.GetLength(1); col++)
            {
                if (givenCol != col && IsPair(sudokuBoard[givenRow, col], sudokuBoard[givenRow, givenCol])) return true;
            }
            return false;
        }

        private void eliminatePairFromCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            if (!PairInCol(sudokuBoard, givenRow, givenCol)) return;

            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                if (sudokuBoard[row, givenCol] != sudokuBoard[givenRow, givenCol] && sudokuBoard[row, givenCol].ToString().Length > 1)
                {
                    eliminatePair(sudokuBoard, sudokuBoard[givenRow, givenCol], row, givenCol);
                }
            }
        }

        private bool PairInCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                if (givenRow != row && IsPair(sudokuBoard[row, givenCol], sudokuBoard[givenRow, givenCol])) return true;
            }
            return false;
        }

        private void eliminatePairFromBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            if (!PairInBlock(sudokuBoard, givenRow, givenCol)) return;

            var sudokuMap = _SudokuMapper.Find(givenRow, givenCol);
            for (int row = sudokuMap.StartRow; row <= sudokuMap.StartRow + 2; row++)
            {
                for (int col = sudokuMap.StartCol; col <= sudokuMap.StartCol + 2; col++)
                {
                    if (sudokuBoard[row, col].ToString().Length > 1 && sudokuBoard[row, col] != sudokuBoard[givenRow, givenCol])
                    {
                        eliminatePair(sudokuBoard, sudokuBoard[givenRow, givenCol], row, col);
                    }
                }
            }
        }
        
        private bool PairInBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            for (int row = 0; row < sudokuBoard.GetLength(1); row++)
            {
                for (int col = 0; col < sudokuBoard.GetLength(1); col++)
                {
                    var sameElement = givenRow == row && givenCol == col;
                    var sameInBlock = _SudokuMapper.Find(givenRow, givenCol).StartRow == _SudokuMapper.Find(row, col).StartRow &&
                        _SudokuMapper.Find(givenRow, givenCol).StartCol == _SudokuMapper.Find(row, col).StartCol;

                    if (!sameElement && sameInBlock && IsPair(sudokuBoard[givenRow, givenCol], sudokuBoard[row, col])) return true;
                }
            }
            return false;
        }

        private bool IsPair(int firstPair, int secondPair)
        {
            return firstPair.ToString().Length == 2 && firstPair == secondPair;
        }
    }
}



    
