using NUnit.Framework;

namespace MathUtilities.Test
{
    [TestFixture]
    [TestOf(typeof(Triplet))]
    public class TripletTest
    {
        public Triplet Triplet;

        [Test]
        [TestCase(1, 3, 2)]
        [TestCase(1, -1, 0)]
        [TestCase(-5, -3, -4)]
        public void TestCenter(double left, double right, double expected)
        {
            Triplet = new Triplet{A = left, B = right};
            var got = Triplet.Center;
            
            Assert.AreEqual(expected, got);
        }
    }
}
