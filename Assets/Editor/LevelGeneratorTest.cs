using System;
using LevelGeneration;
using NUnit.Framework;

namespace Editor
{
    // Tests for LevelGenerator
    // Testing LevelGenerator is tricky, because GenerateLevel is the only public method, and the results are random...
    public class LevelGeneratorTest
    {
        // Testing that attempts at generating a level smaller than 3x3 throws exception
        [Test]
        public void SmallerThan3X3ThrowsException()
        {
            var generator = new LevelGenerator();

            Assert.Throws<Exception>(() => generator.GenerateLevel(2));
        }

        // Testing that with big enough edge size, we get a level of said size
        [Test]
        public void GeneratedLevelIsCorrectSize()
        {
            var generator = new LevelGenerator();

            var level = generator.GenerateLevel(3);

            // Testing both arrays since level is a two-dimensional array
            Assert.AreEqual(level.GetLength(0), 3);
            Assert.AreEqual(level.GetLength(1), 3);
        }
    }
}