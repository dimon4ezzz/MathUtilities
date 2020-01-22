using static System.Math;
using NUnit.Framework;

namespace MathUtilities.Tests
{
    [TestFixture]
    [TestOf(typeof(MultivariateAnalyzer))]
    public class MultivariateAnalyzerTest
    {
        private const int V = 6;
        private static Vector vec = new Vector(1 / Sqrt(10), 3 / Sqrt(10));

        [Test]
        public void TestAlphaGreaterOnOrth()
        {
            var ma = new MultivariateAnalyzer(
                Function,
                new Vector(1, 1),
                vec
            );
            var alpha1 = ma.GetAsymptote().Alpha;

            ma = new MultivariateAnalyzer(
                Function,
                new Vector(1, 1),
                vec.GetOrth()
            );
            var alpha2 = ma.GetAsymptote().Alpha;

            Assert.Greater(alpha2, alpha1);
        }

        private double Function(Vector vector) =>
            Pow(vector.Get(0), 2) - V * Sqrt(vector.Get(0) * vector.Get(1));
    }
}