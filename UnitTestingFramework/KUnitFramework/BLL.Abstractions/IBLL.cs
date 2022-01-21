using System.Collections.Generic;
using System.Reflection;

namespace KUnitFramework
{
    public interface IBLL
    {
        Dictionary<string, string> GetUnitTestResults();

        void InvokeBeforeMethod();

        void InvokeAfterGroupMethod();

        void SetAssertTestResValue(object value);

        bool ShowUnitTestsResults(Assembly assembly);
    }
}