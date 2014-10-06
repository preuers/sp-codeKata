using System;
using NUnit.Framework;

namespace ccc2010.src.test
{
    [TestFixture]
    public class LevelTests
    {
        [Test]
        public void TestLevel1Description()
        {
            var field = new Field(3, 4);
            Assert.That(field.GetSequence(), Is.EqualTo(new[] {1, 2, 3, 4, 8, 7, 6, 5, 9, 10, 11, 12}));
            var field2 = new Field(2, 5);
            Assert.That(field2.GetSequence(), Is.EqualTo(new[] {1, 2, 3, 4, 5, 10, 9, 8, 7, 6}));

            PrintSequence(new Field(3, 4).GetSequence());
            PrintSequence(new Field(2, 5).GetSequence());
            PrintSequence(new Field(5, 2).GetSequence());
            PrintSequence(new Field(23, 12).GetSequence());
        }

        [Test]
        public void TestLevel1Acceptance()
        {
            PrintSequence(new Field(3, 4).GetSequence());
            PrintSequence(new Field(2, 5).GetSequence());
            PrintSequence(new Field(5, 2).GetSequence());
            PrintSequence(new Field(23, 12).GetSequence());
        }

        [Test]
        public void TestLevel2Description()
        {
            var field = new Field(2, 5, 2, 1);
            Assert.That(field.GetSequence(), Is.EqualTo(new[] {6, 7, 8, 9, 10, 5, 4, 3, 2, 1}));
            field = new Field(5, 2, 5, 2);
            Assert.That(field.GetSequence(), Is.EqualTo(new[] {10, 9, 7, 8, 6, 5, 3, 4, 2, 1}));
        }

        [Test]
        public void TestLevel2Self()
        {
            var field = new Field(2, 5, 2, 1);
            Assert.That(field.GetSequence(), Is.EqualTo(new[] {6, 7, 8, 9, 10, 5, 4, 3, 2, 1}));
            field = new Field(2, 2, 1, 1);
            Assert.That(field.GetSequence(), Is.EqualTo(new[] {1, 2, 4, 3}));
            field = new Field(2, 2, 1, 2);
            Assert.That(field.GetSequence(), Is.EqualTo(new[] { 2, 1, 3, 4 }));
            field = new Field(2, 2, 2, 1);
            Assert.That(field.GetSequence(), Is.EqualTo(new[] { 3, 4, 2, 1 }));
            field = new Field(2, 2, 2, 2);
            Assert.That(field.GetSequence(), Is.EqualTo(new[] { 4, 3, 1, 2 }));
        }

        [Test]
        public void TestLevel2Acceptance()
        {
            PrintSequence(new Field(3, 4, 1, 1).GetSequence());
            PrintSequence(new Field(2, 5, 2, 1).GetSequence());
            PrintSequence(new Field(5, 2, 5, 2).GetSequence());
            PrintSequence(new Field(23, 12, 1, 12).GetSequence());
        }

        [Test]
        public void TestLevel3Description()
        {
            var field = new Field(3, 4, 1, 1, 'S');
            Assert.That(field.GetSequence(), Is.EqualTo(new[] {1, 5, 9, 10, 6, 2, 3, 7, 11, 12, 8, 4}));
            field = new Field(5, 2, 5, 2, 'N');
            Assert.That(field.GetSequence(), Is.EqualTo(new[] {10, 8, 6, 4, 2, 1, 3, 5, 7, 9}));
        }

        [Test]
        public void TestLevel3Self()
        {
            var field = new Field(2, 2, 1, 1, 'O');
            Assert.That(field.GetSequence(), Is.EqualTo(new[] {1, 2, 4, 3}));

            field = new Field(2, 2, 1, 1, 'S');
            Assert.That(field.GetSequence(), Is.EqualTo(new[] { 1, 3, 4, 2 }));

            field = new Field(2, 2, 1, 2, 'S');
            Assert.That(field.GetSequence(), Is.EqualTo(new[] { 2, 4, 3, 1 }));

            field = new Field(2, 2, 1, 2, 'W');
            Assert.That(field.GetSequence(), Is.EqualTo(new[] { 2, 1, 3, 4 }));

            field = new Field(2, 2, 2, 2, 'W');
            Assert.That(field.GetSequence(), Is.EqualTo(new[] { 4, 3, 1, 2 }));

            field = new Field(2, 2, 2, 2, 'N');
            Assert.That(field.GetSequence(), Is.EqualTo(new[] { 4, 2, 1, 3 }));

            field = new Field(2, 2, 2, 1, 'N');
            Assert.That(field.GetSequence(), Is.EqualTo(new[] { 3, 1, 2, 4 }));

            field = new Field(2, 2, 2, 1, 'O');
            Assert.That(field.GetSequence(), Is.EqualTo(new[] { 3, 4, 2, 1 }));
        }

        [Test]
        public void TestLevel3Acceptance()
        {
            PrintSequence(new Field(3, 4, 1, 1, 'S').GetSequence());
            PrintSequence(new Field(2, 5, 1, 5, 'S').GetSequence());
            PrintSequence(new Field(5, 2, 5, 2, 'N').GetSequence());
            PrintSequence(new Field(23, 12, 23, 1, 'N').GetSequence());
        }

        [Test]
        public void TestLevel4Description()
        {
            var field = new Field(3, 4, 1, 4, 'S', 'Z');
            Assert.That(field.GetSequence(), Is.EqualTo(new[] {4, 8, 12, 9, 5, 1, 3, 7, 11, 10, 6, 2}));
            field = new Field(5, 2, 5, 2, 'N', 'Z');
            Assert.That(field.GetSequence(), Is.EqualTo(new[] {10, 8, 6, 4, 2, 1, 3, 5, 7, 9}));
        }

        [Test]
        public void TestLevel4Self()
        {
            var field = new Field(3, 4, 1, 4, 'W', 'Z');
            Assert.That(field.GetSequence(), Is.EqualTo(new[] {4, 3, 2, 1, 9, 10, 11, 12, 8, 7, 6, 5}));

            field = new Field(3, 4, 1, 1, 'O', 'Z');
            Assert.That(field.GetSequence(), Is.EqualTo(new[] { 1, 2, 3, 4, 12, 11, 10, 9, 5, 6, 7, 8 }));

            field = new Field(3, 4, 3, 4, 'W', 'Z');
            Assert.That(field.GetSequence(), Is.EqualTo(new[] {12, 11, 10, 9, 1, 2, 3, 4, 8, 7, 6, 5}));
            
            field = new Field(3, 4, 3, 4, 'N', 'Z');
            Assert.That(field.GetSequence(), Is.EqualTo(new[] {12, 8, 4, 1, 5, 9, 11, 7, 3, 2, 6, 10}));
        }

        [Test]
        public void TestLevel4Acceptance()
        {
            PrintSequence(new Field(3, 4, 1, 4, 'S', 'Z').GetSequence());
            PrintSequence(new Field(2, 5, 2, 1, 'N', 'S').GetSequence());
            PrintSequence(new Field(5, 2, 5, 2, 'N', 'Z').GetSequence());
            PrintSequence(new Field(23, 12, 23, 1, 'N', 'Z').GetSequence());
        }

        [Test]
        [Ignore("Due to Level6 requirements change")]
        public void TestLevel5Description()
        {
            var field = new Field(5, 4, 1, 1, 'O', 'S', 2);
            Assert.That(field.GetSequence(), Is.EqualTo(new[] { 1, 5, 2, 6, 3, 7, 4, 8, 16, 12, 15, 11, 14, 10, 13, 9, 17, 18, 19, 20 }));
            field = new Field(5, 4, 4, 1, 'O', 'Z', 2);
            Assert.That(field.GetSequence(), Is.EqualTo(new[] { 13, 17, 14, 18, 15, 19, 16, 20, 8, 4, 7, 3, 6, 2, 5, 1, 9, 10, 11, 12 }));
        }

        [Test]
        [Ignore("Due to Level6 requirements change")]
        public void TestLevel5Acceptance()
        {
            PrintSequence(new Field(5, 4, 1, 1, 'O', 'S', 2).GetSequence());
            PrintSequence(new Field(5, 4, 4, 1, 'O', 'Z', 2).GetSequence());
            PrintSequence(new Field(10, 10, 10, 10, 'W', 'S', 1).GetSequence());
            PrintSequence(new Field(10, 10, 10, 10, 'W', 'S', 2).GetSequence());
            PrintSequence(new Field(17, 9, 17, 1, 'N', 'Z', 2).GetSequence());
        }

        [Test]
        public void TestLevel6Description()
        {
            var field = new Field(5, 4, 1, 1, 'O', 'S', 2);
            Assert.That(field.GetSequence(), Is.EqualTo(new[] {1, 5, 2, 6, 3, 7, 4, 8, 16, 12, 15, 11, 14, 10, 13, 9, 17, 0, 18, 0, 19, 0, 20, 0}));
            field = new Field(5, 4, 4, 1, 'O', 'Z', 2);
            Assert.That(field.GetSequence(), Is.EqualTo(new[] {13, 17, 14, 18, 15, 19, 16, 20, 8, 4, 7, 3, 6, 2, 5, 1, 9, 0, 10, 0, 11, 0, 12, 0}));
        }

        [Test]
        public void TestLevel6Acceptance()
        {
            PrintSequence(new Field(5, 4, 1, 1, 'O', 'S', 2).GetSequence());
            PrintSequence(new Field(5, 4, 4, 1, 'O', 'Z', 2).GetSequence());
            PrintSequence(new Field(10, 10, 10, 10, 'W', 'S', 1).GetSequence());
            PrintSequence(new Field(10, 10, 10, 10, 'W', 'S', 2).GetSequence());
            PrintSequence(new Field(17, 9, 17, 1, 'N', 'Z', 2).GetSequence());
        }

        private static void PrintSequence(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();
        }
    }
}
