using NUnit.Framework;

namespace ccc2010
{
    public abstract class IndexGeneratorTestsBase
    {
        protected static void CheckSequence(IndexGenerator generator, params int[] expectedSequence)
        {
            int i = 0;
            while(i < expectedSequence.Length)
            {
                Assert.That(generator.HasNext);
                var nextValues = generator.NextValues();
                for (int j = 0; j < nextValues.Length; j++)
                {
                    Assert.That(nextValues[j], Is.EqualTo(expectedSequence[i]), "assertion failed at index:" + i);
                    i++;
                }
            }
            Assert.That(generator.HasNext, Is.False);
        }
    }
}
