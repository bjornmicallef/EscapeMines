using EscapeMines.Helpers;
using EscapeMines.Models;
using EscapeMines.Models.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace EscapeMines
{
    public static class Program
    {
        /* 
         * I added two extra response besides the ones defined in the document (success, mine hit and lost)
         * I added an error response used when validation fails and an outside grid error used when the turtle falls off/steps outside grid
         */

        static void Main(string[] args)
        {
            var cfg = InitOptions<AppConfig>();
            var setupDirectory = Path.Combine(cfg.SetupFilePath, cfg.SetupFileName);
            var setupLines = File.ReadAllLines(setupDirectory);

            var validation = new Validation();
            var result = validation.ValidateSetupFile(setupLines);

            if (result == Result.ValidationOk)
            {
                var minefield = new Minefield(setupLines);
                result = minefield.ExecuteMoves();
            }

            Console.WriteLine(new Response(result).Description);
            Console.ReadKey();
        }

        private static T InitOptions<T>() where T : new()
        {
            var config = InitConfig();
            return config.Get<T>();
        }

        private static IConfigurationRoot InitConfig()
        {
            // load setup file name and path from appsettings.json 
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}