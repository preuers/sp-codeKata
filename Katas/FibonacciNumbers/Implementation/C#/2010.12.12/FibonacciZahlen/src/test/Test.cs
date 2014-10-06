using System;
using NUnit.Framework;

namespace FibonacciZahlen
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestAnchors()
        {
            AssertResultForInput(0, 0);
            AssertResultForInput(1, 1);
        }

        [Test]
        public void TestRecursive()
        {
            AssertResultForInput(1, 2);
            AssertResultForInput(2, 3);
            AssertResultForInput(3, 4);
            AssertResultForInput(5, 5);
        }

        private static void AssertResultForInput(int result, int input)
        {
            Assert.That(Calculator.CalculateFibunacci(input), Is.EqualTo(result));
        }

        [Test]
        public void TestAcceptanceLevel1()
        {
            PrintResultFor(6);
            PrintResultFor(19);
            PrintResultFor(28);
            PrintResultFor(36);
            PrintResultFor(38);
        }

        private static void PrintResultFor(int n)
        {
            Console.WriteLine(Calculator.CalculateFibunacci(n));
        }
    }
}
