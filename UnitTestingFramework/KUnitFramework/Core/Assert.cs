using System;

namespace KUnitFramework
{
    public static class Assert
    {
        public static string TestRes { get; set; }

        public static void IsTrue(bool result)
        {
            TestRes = result.ToString();
        }

        public static void IsFalse(bool result)
        {
            TestRes = result.ToString();
        }

        public static void AreEqual<TFirst, TSecond>(TFirst first, TSecond second)
        {
            TestRes = first.Equals(second).ToString();
        }

        public static void AreNotEqual<TFirst, TSecond>(TFirst first, TSecond second)
        {
            TestRes = (!first.Equals(second)).ToString();
        }

        public static void Equals<TFirst, TSecond>(TFirst first, TSecond second)
        {
            TestRes = first.Equals(second).ToString();
        }

        public static void IsNull<TFirst>(TFirst first)
        {
            TestRes = (first == null).ToString();
        }

        public static void IsNotNull<TFirst>(TFirst first)
        {
            TestRes = (first != null).ToString();
        }

        public static void IsInstanceOfType<TFirst>(TFirst first, Type type)
        {
            TestRes = (first.GetType() == type).ToString();
        }

        public static void IsNotInstanceOfType<TFirst>(TFirst first, Type type)
        {
            TestRes = (first.GetType() != type).ToString();
        }
    }
}