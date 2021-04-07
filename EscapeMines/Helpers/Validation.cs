using EscapeMines.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace EscapeMines.Helpers
{
    public class Validation
    {
        private (int, int) _gridSize;
        private List<(int, int)> _minePositions;
        private (int, int) _exitPosition;
        private (int, int) _startPosition;


        /// <summary>
        /// Entry point to validate entire setup file
        /// </summary>
        /// <param name="setupLines"></param>
        /// <returns></returns>
        public Result ValidateSetupFile(string[] setupLines)
        {
            var result = ValidateGrid(setupLines[0]);
            if (result == Result.ValidationOk) _gridSize = Util.ParseSetupLineInPosition(setupLines[0]);

            if (result == Result.ValidationOk) result = ValidateMinesList(setupLines[1]);

            if (result == Result.ValidationOk) result = ValidateExit(setupLines[2]);

            if (result == Result.ValidationOk) result = ValidateStart(setupLines[3]);

            if (result == Result.ValidationOk) result = ValidateMoves(setupLines.Skip(4).Take(setupLines.Length - 2).ToArray());

            if (result == Result.ValidationOk) result = ValidateConcurrentObjectPositions();

            return result;
        }

        /// <summary>
        /// Validate characters representing grid size
        /// </summary>
        /// <param name="firstLine"></param>
        private Result ValidateGrid(string firstLine)
        {
            // grid values are always required
            if (string.IsNullOrEmpty(firstLine))
                return Result.MissingGridInput;

            foreach (char c in firstLine)
            {
                if ((c < '0' || c > '9') && c != ' ')
                    return Result.InvalidGridInput;
            }

            if (firstLine.Trim().Split(' ').Length != 2)
                return Result.InvalidGridInput;

            return Result.ValidationOk;
        }

        /// <summary>
        /// Validate characters representing mines positions
        /// </summary>
        /// <param name="secondLine"></param>
        private Result ValidateMinesList(string secondLine)
        {
            // the mines arent required. a grid can have no mines

            foreach (char c in secondLine)
            {
                if ((c < '0' || c > '9') && c != ',' && c != ' ')
                    return Result.InvalidMinesInput;
            }

            _minePositions = Util.GetMines(secondLine);
            var result = Result.ValidationOk;
            foreach (var mine in _minePositions)
            {
                if (result == Result.ValidationOk)
                    result = ValidateIfObjectIsInGrid(mine, Result.OutOfBoundsMines);
            }

            return result;
        }

        /// <summary>
        /// Validate characters representing exit position
        /// </summary>
        /// <param name="thirdLine"></param>
        private Result ValidateExit(string thirdLine)
        {
            // the exit isnt required but the turtle will always be lost

            foreach (char c in thirdLine)
            {
                if ((c < '0' || c > '9') && c != ' ')
                    return Result.InvalidExitInput;
            }

            if (thirdLine.Trim().Split(' ').Length != 2)
                return Result.InvalidExitInput;

            _exitPosition = Util.ParseSetupLineInPosition(thirdLine);
            return ValidateIfObjectIsInGrid(_exitPosition, Result.OutOfBoundsExit);
        }

        /// <summary>
        /// Validate characters representing starting position
        /// </summary>
        /// <param name="fourthLine"></param>
        private Result ValidateStart(string fourthLine)
        {
            // the turtle/start position is always required since moves are executed against this

            if (string.IsNullOrEmpty(fourthLine))
                return Result.MissingStartInput;

            foreach (char c in fourthLine)
            {
                if ((c < '0' || c > '9') && c != 'N' && c != 'S' && c != 'E' && c != 'W' && c != ' ')
                    return Result.InvalidStartInput;
            }

            if (fourthLine.Trim().Split(' ').Length != 3)
                return Result.InvalidStartInput;

            _startPosition = Util.ParseSetupLineInPosition(fourthLine);
            return ValidateIfObjectIsInGrid(_startPosition, Result.OutOfBoundsStart);
        }

        /// <summary>
        /// Validate characters representing list of turtle moves
        /// </summary>
        /// <param name="restOfLines"></param>
        private Result ValidateMoves(string[] restOfLines)
        {
            // the moves arent required, you can declare start position and stay put

            foreach (string line in restOfLines)
            {
                foreach (char c in line)
                {
                    if (c != 'R' && c != 'L' && c != 'M' && c != ' ')
                        return Result.InvalidMovesInput;
                }
            }

            return Result.ValidationOk;
        }

        /// <summary>
        /// Validate if a mine, start or exit are within the grid boundary
        /// </summary>
        /// <param name="objectPosition"></param>
        /// <param name="error"></param>
        private Result ValidateIfObjectIsInGrid((int, int) objectPosition, Result result)
        {
            if (objectPosition.Item1 < 0 ||
               objectPosition.Item2 < 0 ||
               objectPosition.Item1 >= _gridSize.Item1 ||
               objectPosition.Item2 >= _gridSize.Item2)
                return result;

            return Result.ValidationOk;
        }

        /// <summary>
        /// Validate if a mine, start or exit are in the same position
        /// </summary>
        private Result ValidateConcurrentObjectPositions()
        {
            // mines -> start/exit
            foreach (var mine in _minePositions)
            {
                if ((mine.Item1 == _startPosition.Item1 && mine.Item2 == _startPosition.Item2) ||
                    (mine.Item1 == _exitPosition.Item1 && mine.Item2 == _exitPosition.Item2))
                    return Result.MineSamePositionStartExit;
            }

            // start -> exit
            if (_startPosition.Item1 == _exitPosition.Item1 && _startPosition.Item2 == _exitPosition.Item2)
                return Result.StartExitSamePosition;

            return Result.ValidationOk;
        }
    }
}
