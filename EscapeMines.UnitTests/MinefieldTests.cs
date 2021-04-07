using EscapeMines.Models.Enums;
using Xunit;

namespace EscapeMines.UnitTests
{
    public class MinefieldTests
    {
        [Fact]
        public void ExecuteMoves_TurtleFindsExit_ReturnsTurtleExit()
        {
            // Arrange
            var setupLines = new string[] { "4 4", "1,1 2,2", "2 3", "0 0 E", "M M M", "R M M M", "R M" };

            // Act
            var minefield = new Minefield(setupLines);
            var result = minefield.ExecuteMoves();

            // Assert
            Assert.Equal(Result.TurtleExit, result);
        }

        [Fact]
        public void ExecuteMoves_TurtleHitsMine_ReturnsTurtleMine()
        {
            // Arrange
            var setupLines = new string[] { "4 4", "1,1 2,2", "2 3", "0 0 E", "M", "R M" };

            // Act
            var minefield = new Minefield(setupLines);
            var result = minefield.ExecuteMoves();

            // Assert
            Assert.Equal(Result.TurtleMine, result);
        }

        [Fact]
        public void ExecuteMoves_TurtleDoesNotFindExit_ReturnsTurtleLost()
        {
            // Arrange
            var setupLines = new string[] { "4 4", "1,1 2,2", "2 3", "0 0 E", "M M" };

            // Act
            var minefield = new Minefield(setupLines);
            var result = minefield.ExecuteMoves();

            // Assert
            Assert.Equal(Result.TurtleLost, result);
        }

        [Fact]
        public void ExecuteMoves_TurtleStepsOutsideGrid_ReturnsTurtleOutside()
        {
            // Arrange
            var setupLines = new string[] { "4 4", "1,1 2,2", "2 3", "0 0 E", "M M M M M M" };

            // Act
            var minefield = new Minefield(setupLines);
            var result = minefield.ExecuteMoves();

            // Assert
            Assert.Equal(Result.TurtleOutside, result);
        }

        [Fact]
        public void ExecuteMoves_TurtleHitsMineOnTheWayToExit_ReturnsTurtleMine()
        {
            // Arrange
            var setupLines = new string[] { "4 4", "1,1 2,2 3,3", "2 3", "0 0 E", "M M M", "R M M M", "R M" };

            // Act
            var minefield = new Minefield(setupLines);
            var result = minefield.ExecuteMoves();

            // Assert
            Assert.Equal(Result.TurtleMine, result);
        }
    }
}