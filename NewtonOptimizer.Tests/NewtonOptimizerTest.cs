using NUnit.Framework;

namespace MathUtilities
{
    [TestFixture]
    [TestOf(typeof(NewtonOptimizer))]
    public class NewtonOptimizerTest
    {
        [Test]
        public void TestLeftParabola()
        {
            var o = new NewtonOptimizer(LeftParabola, start: -1.5);
            var got = o.GetOptimal();
            Assert.AreEqual(-1, got, 1E-04);
        }

        private double LeftParabola(double x) =>
            x * x + 2 * x + 1;
    }
}