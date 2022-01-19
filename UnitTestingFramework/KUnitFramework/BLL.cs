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
            var dataAccessLayer = new DAL();

            var attributeMethods = dataAccessLayer.GetMethodsWithAttribute(assembly, typeof(KTested));
            var interfaceInheritedMethods = dataAccessLayer.GetMethodsFromClassWithInterface();

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

                this.InvokeBeforeMethod(dataAccessLayer, assembly);
                method.Invoke(Activator.CreateInstance(method.DeclaringType), method.GetParameters());

                result.Add($"{method.ReturnType.ToString().Split('.')[1]} {method.Name}()", Assert.TestRes);
            }

            this.InvokeAfterGroupMethod(dataAccessLayer, assembly);

            return result;
        }

        private void InvokeBeforeMethod(DAL dataAccessLayer, Assembly assembly)
        {
            var methodBefore = dataAccessLayer.GetMethodsWithAttribute(assembly, typeof(BeforeAfterTestAttributes.KTestedBeforeTest))[0];

            if (methodBefore == null)
            {
                return;
            }

            methodBefore.Invoke(
                Activator.CreateInstance(methodBefore.DeclaringType),
                methodBefore.GetParameters());
        }

        private void InvokeAfterGroupMethod(DAL dataAccessLayer, Assembly assembly)
        {
            var methodAfterGroup = dataAccessLayer.GetMethodsWithAttribute(assembly, typeof(BeforeAfterTestAttributes.KTestedAfterGroup))[0];

            if (methodAfterGroup == null)
            {
                return;
            }

            methodAfterGroup.Invoke(
                Activator.CreateInstance(methodAfterGroup.DeclaringType),
                methodAfterGroup.GetParameters());
        }

        private void SetAssertTestResValue(object value)
        {
            typeof(Assert).GetProperty("TestRes")?.SetValue(typeof(Assert), value);
        }
    }
}