using System;
using NUnit.Framework;

namespace Fractal
{
    [TestFixture]
    public class Level1Tests
    {
        [Test]
        public void OwnTestsLevel1()
        {
            CheckTriangle(9, 0, 27);
            CheckTriangle(9, 1, 36);
            CheckTriangle(9, 2, 48);
        }

        private static void CheckTriangle(double length, int numberOfIterations, int expectedResult)
        {
            var actualResult = Calculator.CalculateFor(FractalType.Triangle, length, numberOfIterations);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestAcceptanceLevel1()
        {
            PrintTriangle(243, 3);
            PrintTriangle(19683, 7);
            PrintTriangle(531441, 7);
            PrintTriangle(531441, 9);
        }

        private static void PrintTriangle(double length, int numberOfIterations)
        {
            var result = Calculator.CalculateFor(FractalType.Triangle, length, numberOfIterations);
            Console.WriteLine(result);
        }

        [Test]
        public void OwnTestsLevel2()
        {
            CheckSquare(9, 0, 36);
            CheckSquare(9, 1, 60);
            CheckSquare(9, 2, 100);
        }

        private static void CheckSquare(double length, int numberOfIterations, int expectedResult)
        {
            var actualResult = Calculator.CalculateFor(FractalType.Square, length, numberOfIterations);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestAcceptanceLevel2()
        {
            PrintSquare(243, 3);
            PrintSquare(19683, 7);
            PrintSquare(531441, 7);
            PrintSquare(531441, 9);
        }

        private static void PrintSquare(double length, int numberOfIterations)
        {
            var result = Calculator.CalculateFor(FractalType.Square, length, numberOfIterations);
            Console.WriteLine(result);
        }
    }
}
