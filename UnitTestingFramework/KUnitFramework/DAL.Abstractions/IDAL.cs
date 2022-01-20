using System;
using System.Collections.Generic;
using System.Reflection;

namespace KUnitFramework
{
    internal interface IDAL
    {
        MethodInfo[] GetMethodsWithAttribute(Assembly assembly, Type type);

        IEnumerable<MethodInfo> GetMethodsFromClassWithInterface();
    }
}