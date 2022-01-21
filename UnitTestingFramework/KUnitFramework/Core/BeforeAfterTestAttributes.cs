using System;

namespace KUnitFramework
{
    public class BeforeAfterTestAttributes
    {
        public class KTestedBeforeTest : Attribute
        {
        }

        public class KTestedAfterGroup : Attribute
        {
        }
    }
}