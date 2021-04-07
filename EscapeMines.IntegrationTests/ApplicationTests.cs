using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace EscapeMines.IntegrationTests
{
    public class ApplicationTests
    {
        private const string testAppSettingsFileDirectory = @"C:\test\EscapeMines\EscapeMines.IntegrationTests\TestAppSettings";
        private const string binAppSettingsFileDirectoryAndName = @"C:\test\EscapeMines\EscapeMines\bin\Debug\netcoreapp3.1\appsettings.json";

        protected Process StartApplication(string testAppSettingsFileName)
        {
            File.Copy($"{testAppSettingsFileDirectory}\\{testAppSettingsFileName}", binAppSettingsFileDirectoryAndName, true);
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = @"C:\test\EscapeMines\EscapeMines\bin\Debug\netcoreapp3.1\EscapeMines.exe";
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.RedirectStandardOutput = true;

            return Process.Start(processStartInfo);
        }

        protected Task<string> WaitForResponse(Process process)
        {
            return Task.Run(() =>
            {
                var output = process.StandardOutput.ReadLine();

                return output;
            });
        }

        [Fact]
        public void RunApplication_TurtleIsLost_ReturnStillInDanger()
        {
            // Arrange
            var process = StartApplication("appsettingsLost.json");

            // Act
            var outputTask = WaitForResponse(process);
            outputTask.Wait();
            var output = outputTask.Result;

            // Assert
            Assert.Equal("Still in Danger - The turtle did not hit a mine but didn't find the exit either.", output);
        }

        [Fact]
        public void RunApplication_TurtleFindsExit_ReturnsSuccess()
        {
            // Arrange
            var process = StartApplication("appsettingsExit.json");

            // Act
            var outputTask = WaitForResponse(process);
            outputTask.Wait();
            var output = outputTask.Result;

            // Assert
            Assert.Equal("Success - The turtle has found the exit.", output);
        }

        [Fact]
        public void RunApplication_TurtleFindsMine_ReturnsMineHit()
        {
            // Arrange
            var process = StartApplication("appsettingsMine.json");

            // Act
            var outputTask = WaitForResponse(process);
            outputTask.Wait();
            var output = outputTask.Result;

            // Assert
            Assert.Equal("Mine Hit - The turtle has stepped on a mine.", output);
        }

        [Fact]
        public void RunApplication_TurtleFindsMine_ReturnsOutsideGrid()
        {
            // Arrange
            var process = StartApplication("appsettingsOutside.json");

            // Act
            var outputTask = WaitForResponse(process);
            outputTask.Wait();
            var output = outputTask.Result;

            // Assert
            Assert.Equal("Outside Grid - The turtle has stepped outside of the grid.", output);
        }

        [Fact]
        public void RunApplication_ValidationErrorOnSetup_ReturnsError()
        {
            // Arrange
            var process = StartApplication("appsettingsError.json");

            // Act
            var outputTask = WaitForResponse(process);
            outputTask.Wait();
            var output = outputTask.Result;

            // Assert
            Assert.Equal("Error - The exit position in the setup file is not valid.", output);
        }
    }
}