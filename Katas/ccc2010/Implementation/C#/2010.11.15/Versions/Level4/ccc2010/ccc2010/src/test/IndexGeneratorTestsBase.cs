using NUnit.Framework;

namespace ccc2010
{
    public abstract class IndexGeneratorTestsBase
    {
        protected static void CheckSequence(IndexGenerator generator, params int[] expectedSequence)
        {
            for (int i = 0; i < expectedSequence.Length; i++)
            {
                Assert.That(generator.HasNext);
                Assert.That(generator.Next(), Is.EqualTo(expectedSequence[i]));
            }
            Assert.That(generator.HasNext, Is.False);
        }
    }
}
