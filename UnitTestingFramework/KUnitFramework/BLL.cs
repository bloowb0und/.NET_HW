using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace KUnitFramework
{
    internal class BLL
    {
        public Dictionary<string, string> GetUnitTestResults(Assembly assembly)
        {
            var dal = new DAL();

            var attributeMethods = dal.GetMethodsWithAttribute(assembly, typeof(KTested));
            var interfaceInheritedMethods = dal.GetMethodsFromClassWithInterface();

            var allSuitableMethods = attributeMethods
                .Concat(interfaceInheritedMethods)
                .Select(f => f)
                .Where(f => f.DeclaringType != typeof(System.Object))
                .Distinct()
                .ToList();

            var result = new Dictionary<string, string>();

            foreach (var method in allSuitableMethods)
            {
                SetAssertTestResValue("null");

                InvokeBeforeMethod(dal, assembly);
                method.Invoke(Activator.CreateInstance(method.DeclaringType), method.GetParameters());
                
                result.Add(method.ReturnType.ToString().Split('.')[1] + " " + method.Name, Assert.TestRes);
            }

            InvokeAfterGroupMethod(dal, assembly);

            return result;
        }

        private void InvokeBeforeMethod(DAL dal, Assembly assembly)
        {
            var methodBefore = dal.GetMethodsWithAttribute(assembly, typeof(BeforeAfterTestAttributes.KTestedBeforeTest))[0];

            if (methodBefore == null)
            {
                return;
            }

            methodBefore.Invoke(Activator.CreateInstance(methodBefore.DeclaringType),
                methodBefore.GetParameters());
        }
        
        private void InvokeAfterGroupMethod(DAL dal, Assembly assembly)
        {
            var methodAfterGroup = dal.GetMethodsWithAttribute(assembly, typeof(BeforeAfterTestAttributes.KTestedAfterGroup))[0];

            if (methodAfterGroup == null)
            {
                return;
            }

            methodAfterGroup.Invoke(Activator.CreateInstance(methodAfterGroup.DeclaringType),
                methodAfterGroup.GetParameters());
        }

        private void SetAssertTestResValue(object value)
        {
            typeof(Assert).GetProperty("TestRes")?.SetValue(typeof(Assert), value);
        }
    }
}