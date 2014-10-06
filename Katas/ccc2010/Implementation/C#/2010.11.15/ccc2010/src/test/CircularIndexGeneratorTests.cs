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

        [Test]
        public void TestOddUpwards2SubUpwards()
        {
            var generator = new CircularIndexGenerator(5, 0, 2, Order.Upwards);
            CheckSequence(generator, 0, 1, 4, 3, null, 2);
        }

        [Test]
        public void TestOddUpwards2SubDownwards()
        {
            var generator = new CircularIndexGenerator(5, 1, 2, Order.Downwards);
            CheckSequence(generator, 1, 0, 3, 4, 2, null);
        }

        [Test]
        public void TestEvenUpwards2SubUpwards()
        {
            var generator = new CircularIndexGenerator(4, 0, 2, Order.Upwards);
            CheckSequence(generator, 0, 1, 3, 2);
        }

        [Test]
        public void TestEvenUpwards2SubDownwards()
        {
            var generator = new CircularIndexGenerator(4, 1, 2, Order.Downwards);
            CheckSequence(generator, 1, 0, 2, 3);
        }

        [Test]
        public void TestOddDownwards2SubUpwards()
        {
            var generator = new CircularIndexGenerator(5, 3, 2, Order.Upwards);
            CheckSequence(generator, 3, 4, 1, 0, 2, null);
        }

        [Test]
        public void TestOddDownwards2SubDownwards()
        {
            var generator = new CircularIndexGenerator(5, 4, 2, Order.Downwards);
            CheckSequence(generator, 4, 3, 0, 1, null, 2);
        }

        [Test]
        public void TestEvenDownwards2SubUpwards()
        {
            var generator = new CircularIndexGenerator(4, 2, 2, Order.Upwards);
            CheckSequence(generator, 2, 3, 1, 0);
        }

        [Test]
        public void TestEvenDownwards2SubDownwards()
        {
            var generator = new CircularIndexGenerator(4, 3, 2, Order.Downwards);
            CheckSequence(generator, 3, 2, 0, 1);
        }

        [Test]
        public void TestLengthEqualToBreadth()
        {
            var generator = new CircularIndexGenerator(2, 0, 2, Order.Upwards);
            CheckSequence(generator, 0, 1);

            generator = new CircularIndexGenerator(2, 1, 2, Order.Downwards);
            CheckSequence(generator, 1, 0);
        }

        [Test]
        public void TestStartSubOrderWithBreadthOf1()
        {
            var generator = new CircularIndexGenerator(2, 1, 1, Order.Upwards);
            CheckSequence(generator, 1, 0);
            generator = new CircularIndexGenerator(2, 1, 1, Order.Downwards);
            CheckSequence(generator, 1, 0);
            generator = new CircularIndexGenerator(2, 0, 1, Order.Upwards);
            CheckSequence(generator, 0, 1);
            generator = new CircularIndexGenerator(2, 0, 1, Order.Downwards);
            CheckSequence(generator, 0, 1);
        }
    }
}
