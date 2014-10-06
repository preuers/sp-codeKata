using NUnit.Framework;

namespace ccc2010
{
    [TestFixture]
    public class SequenceIndexGeneratorTests : IndexGeneratorTestsBase
    {
        [Test]
        public void TestLowerToUpper()
        {
            var generator = new SequenceIndexGenerator(3, 0);
            CheckSequence(generator, 0, 1, 2);
        }

        [Test]
        public void TestUpperToLower()
        {
            var generator = new SequenceIndexGenerator(3, 2);
            CheckSequence(generator, 2, 1, 0);
        }

        [Test]
        public void TestCreateInverse()
        {
            IndexGenerator generator = new SequenceIndexGenerator(2, 0);
            CheckSequence(generator, 0, 1);
            generator = generator.CreateInverse();
            CheckSequence(generator, 1, 0);
            generator = generator.CreateInverse();
            CheckSequence(generator, 0, 1);
        }

        [Test]
        public void TestCreateInvers01()
        {
            IndexGenerator generator = new SequenceIndexGenerator(4, 0);
            CheckSequence(generator, 0, 1, 2, 3);
            generator = generator.CreateInverse();
            CheckSequence(generator, 3, 2, 1, 0);
            generator = generator.CreateInverse();
            CheckSequence(generator, 0, 1, 2, 3);
        }

        [Test]
        public void TestOddUpwards2SubUpwards()
        {
            var generator = new SequenceIndexGenerator(5, 0, 2, Order.Upwards);
            CheckSequence(generator, 0, 1, 3, 2, 4, null);
        }

        [Test]
        public void TestOddUpwards2SubDownwards()
        {
            var generator = new SequenceIndexGenerator(5, 1, 2, Order.Downwards);
            CheckSequence(generator, 1, 0, 2, 3, null, 4);
        }

        [Test]
        public void TestEvenUpwards2SubUpwards()
        {
            var generator = new SequenceIndexGenerator(4, 0, 2, Order.Upwards);
            CheckSequence(generator, 0, 1, 3, 2);
        }

        [Test]
        public void TestEvenUpwards2SubDownwards()
        {
            var generator = new SequenceIndexGenerator(4, 1, 2, Order.Downwards);
            CheckSequence(generator, 1, 0, 2, 3);
        }

        [Test]
        public void TestOddDownwards2SubUpwards()
        {
            var generator = new SequenceIndexGenerator(5, 3, 2, Order.Upwards);
            CheckSequence(generator, 3, 4, 2, 1, null, 0);
        }

        [Test]
        public void TestOddDownwards2SubDownwards()
        {
            var generator = new SequenceIndexGenerator(5, 4, 2, Order.Downwards);
            CheckSequence(generator, 4, 3, 1, 2, 0, null);
        }

        [Test]
        public void TestEvenDownwards2SubUpwards()
        {
            var generator = new SequenceIndexGenerator(4, 2, 2, Order.Upwards);
            CheckSequence(generator, 2, 3, 1, 0);
        }

        [Test]
        public void TestEvenDownwards2SubDownwards()
        {
            var generator = new SequenceIndexGenerator(4, 3, 2, Order.Downwards);
            CheckSequence(generator, 3, 2, 0, 1);
        }

        [Test]
        public void TestLengthEqualToBreadth()
        {
            var generator = new SequenceIndexGenerator(2, 0, 2, Order.Upwards);
            CheckSequence(generator, 0, 1);

            generator = new SequenceIndexGenerator(2, 1, 2, Order.Downwards);
            CheckSequence(generator, 1, 0);
        }

        [Test]
        public void TestStartSubOrderWithBreadthOf1()
        {
            var generator = new SequenceIndexGenerator(2, 1, 1, Order.Upwards);
            CheckSequence(generator, 1, 0);
            generator = new SequenceIndexGenerator(2, 1, 1, Order.Downwards);
            CheckSequence(generator, 1, 0);
            generator = new SequenceIndexGenerator(2, 0, 1, Order.Upwards);
            CheckSequence(generator, 0, 1);
            generator = new SequenceIndexGenerator(2, 0, 1, Order.Downwards);
            CheckSequence(generator, 0, 1);
        }
    }
}
