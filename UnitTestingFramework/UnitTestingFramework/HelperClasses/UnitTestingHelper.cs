using System;
using KUnitFramework;

namespace UnitTestingFramework
{
    public class UnitTestingHelper
    {
        [BeforeAfterTestAttributes.KTestedBeforeTest]
        public void BeforeTestMethod()
        {
            var previousConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("before every test method");
            Console.ForegroundColor = previousConsoleColor;
        }

        [BeforeAfterTestAttributes.KTestedAfterGroup]
        public void AfterGroupTestMethod()
        {
            var previousConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("after test group method");
            Console.ForegroundColor = previousConsoleColor;
        }
    }
}