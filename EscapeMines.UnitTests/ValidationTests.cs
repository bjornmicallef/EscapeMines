using EscapeMines.Helpers;
using EscapeMines.Models.Enums;
using Xunit;

namespace EscapeMines.UnitTests
{
    public class ValidationTests
    {
        private readonly Validation _validation;
        private readonly string[] _setupLines;

        public ValidationTests()
        {
            _validation = new Validation();
            _setupLines = new string[] { "4 4", "1,1 2,2", "2 3", "0 0 E", "M M M", "R M" };
        }

        [Fact]
        public void ValidateSetupFile_NoErrors_ReturnsValidationOk()
        {
            // Arrange

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.ValidationOk, result);
        }

        [Fact]
        public void ValidateSetupFile_EmtptyGridValues_ReturnsInvaliMissingGridInput()
        {
            // Arrange
            _setupLines[0] = ""; // intitial grid setup is always required

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.MissingGridInput, result);
        }

        [Fact]
        public void ValidateSetupFile_UnparseableGridValues_ReturnsInvalidGridInputError()
        {
            // Arrange
            _setupLines[0] = "4 A";

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.InvalidGridInput, result);
        }

        [Fact]
        public void ValidateSetupFile_MissingGridValues_ReturnsInvalidGridInputError()
        {
            // Arrange
            _setupLines[0] = "4";

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.InvalidGridInput, result);
        }

        [Fact]
        public void ValidateSetupFile_ExtraGridValues_ReturnsInvalidGridInputError()
        {
            // Arrange
            _setupLines[0] = "4 4 4";

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.InvalidGridInput, result);
        }

        [Fact]
        public void ValidateSetupFile_UnparseableMinesValues_ReturnsInvalidMinesInput()
        {
            // Arrange
            _setupLines[1] = "1,1 2,%";

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.InvalidMinesInput, result);
        }

        [Fact]
        public void ValidateSetupFile_MinesOutsideGrid_ReturnsOutOfBoundsMines()
        {
            // Arrange
            _setupLines[1] = "1,1 2,2 4,4"; // this would fit in a 5x5 or bigger

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.OutOfBoundsMines, result);
        }

        [Fact]
        public void ValidateSetupFile_UnparseableExitValues_ReturnsInvalidExitInput()
        {
            // Arrange
            _setupLines[2] = "2 A";

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.InvalidExitInput, result);
        }

        [Fact]
        public void ValidateSetupFile_MissingExitValues_ReturnsInvalidExitInput()
        {
            // Arrange
            _setupLines[2] = "2";

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.InvalidExitInput, result);
        }

        [Fact]
        public void ValidateSetupFile_ExtraExitValues_ReturnsInvalidExitInput()
        {
            // Arrange
            _setupLines[2] = "2 3 3";

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.InvalidExitInput, result);
        }

        [Fact]
        public void ValidateSetupFile_ExitOutsideGrid_ReturnsOutOfBoundsExit()
        {
            // Arrange
            _setupLines[2] = "4 4"; // this would fit in a 5x5 or bigger

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.OutOfBoundsExit, result);
        }

        [Fact]
        public void ValidateSetupFile_EmtptyStartValues_ReturnsMissingStartInput()
        {
            // Arrange
            _setupLines[3] = ""; // the turtle/start position is always required

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.MissingStartInput, result);
        }

        [Fact]
        public void ValidateSetupFile_UnparseableStartValues_ReturnsInvalidStartInput()
        {
            // Arrange
            _setupLines[3] = "0 A E";

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.InvalidStartInput, result);
        }

        [Fact]
        public void ValidateSetupFile_MissingStartValues_ReturnsInvalidStartInput()
        {
            // Arrange
            _setupLines[3] = "0 0";

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.InvalidStartInput, result);
        }

        [Fact]
        public void ValidateSetupFile_ExtraStartValues_ReturnsInvalidStartInput()
        {
            // Arrange
            _setupLines[3] = "0 0 E E";

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.InvalidStartInput, result);
        }

        [Fact]
        public void ValidateSetupFile_StartOutsideGrid_ReturnsOutOfBoundsStart()
        {
            // Arrange
            _setupLines[3] = "4 4 E"; // this would fit in a 5x5 or bigger

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.OutOfBoundsStart, result);
        }

        [Fact]
        public void ValidateSetupFile_UnparseableMovesValues_ReturnsInvalidMovesInput()
        {
            // Arrange
            _setupLines[5] = "R M X";

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.InvalidMovesInput, result);
        }

        [Fact]
        public void ValidateSetupFile_StartExitSamePosition_ReturnsStartExitSamePosition()
        {
            // Arrange
            _setupLines[2] = "0 0";
            _setupLines[3] = "0 0 E";

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.StartExitSamePosition, result);
        }

        [Fact]
        public void ValidateSetupFile_StartMineSamePosition_ReturnsMineSamePositionStartExit()
        {
            // Arrange
            _setupLines[1] = "0,0 1,1 2,2";
            _setupLines[3] = "0 0 E";

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.MineSamePositionStartExit, result);
        }

        [Fact]
        public void ValidateSetupFile_ExitMineSamePosition_ReturnsMineSamePositionStartExit()
        {
            // Arrange
            _setupLines[1] = "0,0 1,1 2,2";
            _setupLines[2] = "0 0 ";

            // Act
            var result = _validation.ValidateSetupFile(_setupLines);

            // Assert
            Assert.Equal(Result.MineSamePositionStartExit, result);
        }
    }
}
