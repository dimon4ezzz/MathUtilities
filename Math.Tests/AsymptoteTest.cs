using NUnit.Framework;

namespace MathUtilities.Test
{
    [TestFixture]
    [TestOf(typeof(Asymptote))]
    public class AsymptoteTest
    {
        public Asymptote Asymptote;

        [Test]
        [TestCase(1)]
        public void TestAlpha(double expected)
        {
            Asymptote = new Asymptote(Function);
            var got = Asymptote.Alpha;

            Assert.AreEqual(expected, got);
        }

        [Test]
        [TestCase(1)]
        public void TestK(double expected)
        {
            Asymptote = new Asymptote(Function);
            var got = Asymptote.K;

            Assert.AreEqual(expected, got);
        }

        /// <summary>
        /// 10x
        /// </summary>
        private double Function(double x) =>
            10 * x;
    }
}
