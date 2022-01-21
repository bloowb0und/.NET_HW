using System.Reflection;
using KUnitFramework;

namespace UnitTestingFramework
{
    public class App
    {
        private readonly IBLL businessLogicLayer;

        public App(IBLL businessLogicLayer)
        {
            this.businessLogicLayer = businessLogicLayer;
        }

        public void StartApp()
        {
            var assembly = Assembly.GetExecutingAssembly();
            this.businessLogicLayer.ShowUnitTestsResults(assembly);
        }
    }
}