using SodokuSolver.Strategies;
using SodokuSolver.Workers;
using SudokuSolver.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodokuSolver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SudokuMapper sudokuMapper = new SudokuMapper();
                StateManager stateManager = new StateManager();
                SolverEngine solverEngine = new SolverEngine(stateManager, sudokuMapper);
                SudokuFileReader sudokuFileReader = new SudokuFileReader();
                SudokuBoardDisplayer sudokuBoardDisplayer = new SudokuBoardDisplayer();

                Console.WriteLine("Please enter the file path of the sudoku puzzle you would like to solve:");
                var filename = Console.ReadLine();
                var sudokuBoard = sudokuFileReader.ReadFile(filename);
                sudokuBoardDisplayer.Display("Starting board", sudokuBoard);

                bool isPuzzleSolved = solverEngine.Solve(sudokuBoard);
                sudokuBoardDisplayer.Display("Hmmm...", sudokuBoard);

                Console.WriteLine(isPuzzleSolved
                    ? "The puzzle has been solved!"
                    : "I'm sorry, I could not solve the puzzle.  Please try a different puzzle.");


            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} : Sudoku Solver encountered an error");
            }
        }
    }
}
