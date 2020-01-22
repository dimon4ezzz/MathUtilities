using static System.Math;
using NUnit.Framework;

namespace MathUtilities.Test
{
    [TestFixture]
    [TestOf(typeof(Math))]
    public class MathTest
    {
        [Test]
        [TestCase(0.781212901, 0.781212999)]
        public void EqualsTest(double a, double b)
        {
            var got = Math.Equals(a, b);

            Assert.IsTrue(got);
        }

        [Test]
        [TestCase(0.2, 0.3)]
        public void NotEqualsTest(double a, double b)
        {
            var got = Math.Equals(a, b);

            Assert.IsFalse(got);
        }

        [Test]
        [TestCase(0.5, 1E-5)]
        public void MinimalTenPowerTest(double n, double expected)
        {
            var got = Math.MinimalTenPower(n);

            Assert.AreEqual(expected, got);
        }

        [Test]
        [TestCase(0.5, 5)]
        public void MinimalPowerTest(double n, int expected)
        {
            var got = Math.MinimalPower(n);

            Assert.AreEqual(expected, got);
        }

        [Test]
        [TestCase(-1E+06, 6)]
        public void TestLogOfAbs(double n, double expected)
        {
            var got = Math.LogOfAbs(n);

            Assert.AreEqual(expected, got);
        }

        [Test]
        [TestCase(new double[]{0, 1}, new double[]{-1, 0})]
        public void TestRotationMatrix(params double[][] expected)
        {
            // PI / 2 = 90\deg
            var rotationMatrix = Math.GetRotationMatrix(PI / 2);
            var got = rotationMatrix.Items;

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    Assert.AreEqual(expected[i][j], got[i][j], 1E-16);
        }
    }
}
