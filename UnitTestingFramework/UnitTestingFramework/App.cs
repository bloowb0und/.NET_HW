using System.Reflection;
using KUnitFramework;

namespace UnitTestingFramework
{
    public class App
    {
        private readonly IBLL _businessLogicLayer;

        public App(IBLL businessLogicLayer, Assembly assembly)
        {
            this._businessLogicLayer = businessLogicLayer;
        }

        public void StartApp()
        {
            var assembly = Assembly.GetExecutingAssembly();
            this._businessLogicLayer.ShowUnitTestsResults(assembly);
        }
    }
}