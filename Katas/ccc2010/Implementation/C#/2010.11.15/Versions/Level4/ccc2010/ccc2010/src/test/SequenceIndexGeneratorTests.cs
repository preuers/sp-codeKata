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
        public void TestSwap()
        {
            IndexGenerator generator = new SequenceIndexGenerator(2, 0);
            CheckSequence(generator, 0, 1);
            generator = generator.CreateSwaped();
            CheckSequence(generator, 1, 0);
            generator = generator.CreateSwaped();
            CheckSequence(generator, 0, 1);
        }
    }
}
