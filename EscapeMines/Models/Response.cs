using EscapeMines.Helpers;
using EscapeMines.Models.Enums;
using System;

namespace EscapeMines.Models
{
    public class Response
    {
        public string Description { get; set; }

        public Response(Result result)
        {
            Description = GetResponseFromResult(result);
        }

        private string GetResponseFromResult(Result result)
        {
            switch (result)
            {
                case Result.MissingGridInput:
                case Result.InvalidGridInput:
                case Result.InvalidMinesInput:
                case Result.InvalidExitInput:
                case Result.MissingStartInput:
                case Result.InvalidStartInput:
                case Result.InvalidMovesInput:
                case Result.OutOfBoundsMines:
                case Result.OutOfBoundsExit:
                case Result.OutOfBoundsStart:
                case Result.MineSamePositionStartExit:
                case Result.StartExitSamePosition:
                    return $"Error - {Util.GetEnumDescription(result)}";

                case Result.TurtleOutside:
                    return $"Outside Grid - {Util.GetEnumDescription(result)}";

                case Result.TurtleMine:
                    return $"Mine Hit - {Util.GetEnumDescription(result)}";

                case Result.TurtleExit:
                    return $"Success - {Util.GetEnumDescription(result)}";

                case Result.TurtleLost:
                    return $"Still in Danger - {Util.GetEnumDescription(result)}";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
