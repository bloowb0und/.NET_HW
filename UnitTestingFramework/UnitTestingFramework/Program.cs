using System.Reflection;
using KUnitFramework;

namespace UnitTestingFramework
{
    internal class Program
    {
        public static void Main()
        {
            PL.GetUnitTestsResults(Assembly.GetExecutingAssembly());
        }
    }
}