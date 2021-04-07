using EscapeMines.Helpers;
using EscapeMines.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace EscapeMines
{
    public class Minefield
    {
        private readonly (int, int) _gridSize;
        private readonly List<(int, int)> _minePositions;
        private readonly (int, int) _exitPosition;
        private readonly (int, int) _startPosition;
        private readonly string _startDirection;
        private readonly string[] _movesList;

        private (int, int) _currentPosition;
        private string _currentDirection;

        public Minefield((int, int) gridSize, List<(int, int)> minePositions, (int, int) exitPosition, (int, int) startPosition, string startDirection, string[] movesList)
        {
            _gridSize = gridSize;
            _minePositions = minePositions;
            _exitPosition = exitPosition;
            _startPosition = startPosition;
            _startDirection = startDirection;
            _movesList = movesList;
        }

        public Minefield(string[] setupLines)
        {
            _gridSize = Util.ParseSetupLineInPosition(setupLines[0]);
            _minePositions = Util.GetMines(setupLines[1]);
            _exitPosition = Util.ParseSetupLineInPosition(setupLines[2]);
            _startPosition = Util.ParseSetupLineInPosition(setupLines[3]);
            _startDirection = setupLines[3].Trim().Split(' ')[2];
            _movesList = setupLines.Skip(4).Take(setupLines.Length - 2).ToArray();
        }

        /// <summary>
        /// Entry point to execute moves and move turtle across the grid
        /// </summary>
        /// <returns></returns>
        public Result ExecuteMoves()
        {
            var result = Result.TurtleLost;
            _currentPosition = _startPosition;
            _currentDirection = _startDirection;

            foreach (var line in _movesList)
            {
                foreach (var move in line)
                {
                    switch (move)
                    {
                        case 'M':
                            if (result == Result.TurtleLost)
                                result = Move();
                            else
                                return result;
                            break;
                        case 'R':
                            RotateRight(); break;
                        case 'L':
                            RotateLeft(); break;
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// Updates the turtle's current position based on the current direction
        /// </summary>
        /// <returns></returns>
        private Result Move()
        {
            // update current position values and compare with mines list & exit in here
            switch (_currentDirection)
            {
                case "N":
                    _currentPosition.Item2--; break;
                case "S":
                    _currentPosition.Item2++; break;
                case "E":
                    _currentPosition.Item1++; break;
                case "W":
                    _currentPosition.Item1--; break;
            }

            var result = CheckIfStillInGrid();
            if (result == Result.TurtleLost) result = CheckForMine();
            if (result == Result.TurtleLost) result = CheckForExit();
            return result;
        }

        /// <summary>
        /// Changes the turtle's direction clockwise
        /// </summary>
        private void RotateRight()
        {
            _currentDirection = _currentDirection switch
            {
                "N" => "E",
                "S" => "W",
                "E" => "S",
                "W" => "N",
                _ => throw new System.NotImplementedException(),
            };
        }

        /// <summary>
        /// Changes the turtle's direction anti clockwise
        /// </summary>
        private void RotateLeft()
        {
            _currentDirection = _currentDirection switch
            {
                "N" => "W",
                "S" => "E",
                "E" => "N",
                "W" => "S",
                _ => throw new System.NotImplementedException(),
            };
        }


        /// <summary>
        /// Checks if turtle's new position is still within the boundary of the grid
        /// </summary>
        /// <returns></returns>
        private Result CheckIfStillInGrid()
        {
            if (_currentPosition.Item1 < 0 ||
               _currentPosition.Item2 < 0 ||
               _currentPosition.Item1 >= _gridSize.Item1 ||
               _currentPosition.Item2 >= _gridSize.Item2)
                return Result.TurtleOutside;

            return Result.TurtleLost;
        }


        /// <summary>
        /// Checks if turtle's new position clashes with an exiting placed mine
        /// </summary>
        /// <returns></returns>
        private Result CheckForMine()
        {
            foreach (var mine in _minePositions)
            {
                if (_currentPosition.Item1 == mine.Item1 && _currentPosition.Item2 == mine.Item2)
                    return Result.TurtleMine;
            }

            return Result.TurtleLost;
        }


        /// <summary>
        /// Checks if turtle's new position reached the exit position
        /// </summary>
        /// <returns></returns>
        private Result CheckForExit()
        {
            if (_currentPosition.Item1 == _exitPosition.Item1 && _currentPosition.Item2 == _exitPosition.Item2)
                return Result.TurtleExit;

            return Result.TurtleLost;
        }
    }
}