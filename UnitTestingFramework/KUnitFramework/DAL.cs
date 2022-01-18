using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace KUnitFramework
{
    internal class DAL
    {
        public MethodInfo[] GetMethodsWithAttribute(Assembly assembly, Type type)
        {
            var methods = assembly.GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes(type, false).Length > 0)
                .ToArray();

            return methods;
        }

        public List<MethodInfo> GetMethodsFromClassWithInterface()
        {
            var methods = new List<MethodInfo>();
            
            var classes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IKTested).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();

            foreach (var cClass in classes)
            {
                methods = methods.Concat(cClass.GetMethods().ToList()).Distinct().ToList();
            }

            return methods;
        }
    }
}