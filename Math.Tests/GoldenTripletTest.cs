using NUnit.Framework;

namespace MathUtilities
{
    [TestFixture]
    [TestOf(typeof(GoldenTriplet))]
    public class GoldenTripletTest
    {
        public GoldenTriplet GoldenTriplet;

        [Test]
        [TestCase(0, 1, GoldenTriplet.Golden)]
        [TestCase(0, 10, 10 * GoldenTriplet.Golden)]
        [TestCase(0, -10, -10 * GoldenTriplet.Golden)]
        public void TestRightCenter(double left, double right, double expected)
        {
            GoldenTriplet = new GoldenTriplet{A = left, B = right};
            var got = GoldenTriplet.RightCenter;

            Assert.AreEqual(expected, got);
        }

        [Test]
        [TestCase(0, 1, GoldenTriplet.SmallGolden)]
        [TestCase(0, 10, 10 * GoldenTriplet.SmallGolden)]
        [TestCase(0, -10, -10 * GoldenTriplet.SmallGolden)]
        public void TestLeftCenter(double left, double right, double expected)
        {
            GoldenTriplet = new GoldenTriplet{A = left, B = right};
            var got = GoldenTriplet.LeftCenter;

            // delta подбирается эмпирическим путём
            Assert.AreEqual(expected, got, delta: 1E-08);
        }

        [Test]
        [TestCase(0, GoldenTriplet.Golden, 1)]
        [TestCase(0, 10 * GoldenTriplet.Golden, 10)]
        [TestCase(0, -10 * GoldenTriplet.Golden, -10)]
        public void TestBFromRightCenter(double left, double rightCenter, double expected)
        {
            GoldenTriplet = new GoldenTriplet{A = left, RightCenter = rightCenter};
            var got = GoldenTriplet.B;

            // delta подбирается эмпирическим путём
            Assert.AreEqual(expected, got, delta: 1E-08);
        }

        [Test]
        [TestCase(0, GoldenTriplet.SmallGolden, 1)]
        [TestCase(0, 10 * GoldenTriplet.SmallGolden, 10)]
        [TestCase(0, -10 * GoldenTriplet.SmallGolden, -10)]
        public void TestBFromLeftCenter(double left, double leftCenter, double expected)
        {
            GoldenTriplet = new GoldenTriplet{A = left, LeftCenter = leftCenter};
            var got = GoldenTriplet.B;

            // delta подбирается эмпирическим путём
            Assert.AreEqual(expected, got, delta: 1E-08);
        }
    }
}
