using System.ComponentModel;

namespace EscapeMines.Models.Enums
{
    public enum Result
    {
        [Description("")]
        ValidationOk,

        [Description("The grid settings are required to setup.")]
        MissingGridInput,

        [Description("The grid settings in the setup file are not valid.")]
        InvalidGridInput,

        [Description("The mines positions in the setup file are not valid.")]
        InvalidMinesInput,

        [Description("The exit position in the setup file is not valid.")]
        InvalidExitInput,

        [Description("The starting position is required to setup.")]
        MissingStartInput,

        [Description("The starting position in the setup file is not valid.")]
        InvalidStartInput,

        [Description("The list of moves in the setup file is not valid.")]
        InvalidMovesInput,

        [Description("One or more mines are outside the grid.")]
        OutOfBoundsMines,

        [Description("The exit is outside the grid.")]
        OutOfBoundsExit,

        [Description("The start is outside the grid.")]
        OutOfBoundsStart,

        [Description("A mine is in the same position of start or exit.")]
        MineSamePositionStartExit,

        [Description("The start and exit are in the same position.")]
        StartExitSamePosition,

        [Description("The turtle has stepped outside of the grid.")]
        TurtleOutside,

        [Description("The turtle has stepped on a mine.")]
        TurtleMine,

        [Description("The turtle has found the exit.")]
        TurtleExit,

        [Description("The turtle did not hit a mine but didn't find the exit either.")]
        TurtleLost,
    }
}
