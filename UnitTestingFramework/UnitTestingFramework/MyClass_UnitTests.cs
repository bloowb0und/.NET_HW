using System;
using KUnitFramework;

namespace UnitTestingFramework
{
    public class MyClass_UnitTests
    {
        public class ASd : IKTested
        {
            public void GetMet()
            {
                Console.WriteLine("GETMETT METHOD INVOKED");

                Assert.IsTrue(true);
            }

            public void HelloMom()
            {
                Console.WriteLine("HelloMom METHOD INVOKED");

                Assert.IsTrue(false);
            }

            [KTested]
            public void CheckMy()
            {
                Console.WriteLine("CheckMy METHOD INVOKED");

                Assert.IsTrue(false);
            }
        }

        public class BBB : IKTested
        {
            public void DadHello()
            {
                Console.WriteLine("DadHello METHOD INVOKED");

                Assert.IsTrue(false);
            }
        }

        public class RETERT
        {
            [KTested]
            public void CheckMethodMy()
            {
                Console.WriteLine("CheckMethodMy METHOD INVOKED");

                Assert.IsTrue(true);
            }
        }
    }
}