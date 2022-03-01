using SodokuSolver.Workers;
using SudokuSolver.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodokuSolver.Strategies
{
    internal class SolverEngine
    {
        private readonly StateManager _stateManager;
        private readonly SudokuMapper _sudokuMapper;

        public SolverEngine(StateManager stateManager, SudokuMapper sudokuMapper)
        {
            _stateManager = stateManager;
            _sudokuMapper = sudokuMapper;
        }

        public bool Solve(int[,] sudokuBoard)
        {
            List<ISudokuStrategy> strategies = new List<ISudokuStrategy>()
            {
                new SimpleMarkupStrategy(_sudokuMapper),
                new NakedPairStrategy(_sudokuMapper)
            };

            var currentState = _stateManager.GenerateState(sudokuBoard);
            var nextState = _stateManager.GenerateState(strategies.First().Solve(sudokuBoard));

            while (!_stateManager.Solved(sudokuBoard) && currentState != nextState)
            {
                currentState = nextState;
                foreach (var strategy in strategies)
                {
                    nextState = _stateManager.GenerateState(strategy.Solve(sudokuBoard));
                }
            }
            
            return _stateManager.Solved(sudokuBoard);
        }
    }
}
