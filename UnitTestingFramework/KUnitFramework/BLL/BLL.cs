using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace KUnitFramework
{
    internal class BLL : IBLL
    {
        private readonly IDAL _dataAccessLayer;
        private Assembly _assembly;
        
        public BLL(IDAL dal)
        {
            this._dataAccessLayer = dal;
        }
        
        public bool ShowUnitTestsResults(Assembly assembly)
        {
            _assembly = assembly;
            
            var testRes = true;

            var unitTestResults = this.GetUnitTestResults();

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
        
        public Dictionary<string, string> GetUnitTestResults()
        {
            var attributeMethods = _dataAccessLayer.GetMethodsWithAttribute(_assembly, typeof(KTested));
            var interfaceInheritedMethods = _dataAccessLayer.GetMethodsFromClassWithInterface();

            var allSuitableMethods = attributeMethods
                .Concat(interfaceInheritedMethods)
                .Select(f => f)
                .Where(f => f.DeclaringType != typeof(object))
                .Distinct()
                .ToList();

            var result = new Dictionary<string, string>();

            foreach (var method in allSuitableMethods)
            {
                this.SetAssertTestResValue("null");

                this.InvokeBeforeMethod();
                method.Invoke(Activator.CreateInstance(method.DeclaringType), method.GetParameters());

                result.Add($"{method.ReturnType.ToString().Split('.')[1]} {method.Name}()", Assert.TestRes);
            }

            this.InvokeAfterGroupMethod();

            return result;
        }

        public void InvokeBeforeMethod()
        {
            var methodBefore = _dataAccessLayer.GetMethodsWithAttribute(_assembly, typeof(BeforeAfterTestAttributes.KTestedBeforeTest))[0];

            if (methodBefore == null)
            {
                return;
            }

            methodBefore.Invoke(
                Activator.CreateInstance(methodBefore.DeclaringType),
                methodBefore.GetParameters());
        }

        public void InvokeAfterGroupMethod()
        {
            var methodAfterGroup = _dataAccessLayer.GetMethodsWithAttribute(_assembly, typeof(BeforeAfterTestAttributes.KTestedAfterGroup))[0];

            if (methodAfterGroup == null)
            {
                return;
            }

            methodAfterGroup.Invoke(
                Activator.CreateInstance(methodAfterGroup.DeclaringType),
                methodAfterGroup.GetParameters());
        }

        public void SetAssertTestResValue(object value)
        {
            typeof(Assert).GetProperty("TestRes")?.SetValue(typeof(Assert), value);
        }
    }
}