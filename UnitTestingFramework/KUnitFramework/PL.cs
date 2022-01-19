using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace KUnitFramework
{
    public static class PL
    {
        public static bool GetUnitTestsResults(Assembly assembly)
        {
            var businessLogicLayer = new BLL();
            var testRes = true;

            var unitTestResults = businessLogicLayer.GetUnitTestResults(assembly);

            if (unitTestResults.ContainsValue("null"))
            {
                var previousConsoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(new string('=', 20));
                Console.WriteLine("Every unit test should contain an Assert statement");
                Console.WriteLine(new string('=', 20));
                Console.ForegroundColor = previousConsoleColor;

                throw new Exception("One or more unit tests doesn't contain Assert statement.");
            }

            Console.Clear();

            Console.WriteLine("\t" + new string('-', 30));
            Console.WriteLine("\t" + "|" + new string(' ', 12) + "kUnit" + new string(' ', 11) + "|");
            Console.WriteLine("\t" + "|" + new string(' ', 9) + "By bloow0und" + new string(' ', 7) + "|");
            Console.WriteLine("\t" + new string('-', 30) + "\n");

            Console.WriteLine($"{"Unit test",15} \t {"Result",15}");
            foreach (var result in unitTestResults)
            {
                var previousConsoleColor = Console.ForegroundColor;

                if (bool.Parse(result.Value))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    testRes = false;
                }

                Console.WriteLine($"{result.Key,15} \t {result.Value,15}");

                Console.ForegroundColor = previousConsoleColor;
            }

            return testRes;
        }
    }
}