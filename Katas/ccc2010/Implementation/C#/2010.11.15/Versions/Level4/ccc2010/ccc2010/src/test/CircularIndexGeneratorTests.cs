using NUnit.Framework;

namespace ccc2010
{
    [TestFixture]
    public class CircularIndexGeneratorTests : IndexGeneratorTestsBase
    {
        [Test]
        public void TestOddStartWithLower()
        {
            var generator = new CircularIndexGenerator(3, 0);
            CheckSequence(generator, 0, 2, 1);
        }

        [Test]
        public void TestOddStartWithUpper()
        {
            var generator = new CircularIndexGenerator(3, 2);
            CheckSequence(generator, 2, 0, 1);
        }

        [Test]
        public void TestEvenStartWithLower()
        {
            var generator = new CircularIndexGenerator(4, 0);
            CheckSequence(generator, 0, 3, 1, 2);
        }

        [Test]
        public void TestEvenStartWithUpper()
        {
            var generator = new CircularIndexGenerator(4, 3);
            CheckSequence(generator, 3, 0, 2, 1);
        }
    }
}
