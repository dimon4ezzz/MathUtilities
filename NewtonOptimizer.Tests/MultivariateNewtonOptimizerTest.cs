using static System.Math;
using NUnit.Framework;

namespace MathUtilities.Tests
{
    [TestFixture]
    [TestOf(typeof(MultivariateNewtonOptimizer))]
    public class MultivariateNewtonOptimizerTest
    {
        [Test]
        [TestCase(new double[]{1, 1}, new double[]{0, 0})]
        public void TestOptimal(double[] start, double[] expected)
        {
            var op = new MultivariateNewtonOptimizer(Function, start);
            var got = op.GetOptimal();

            Assert.AreEqual(expected, got.Items);
        }

        /// <summary>
        /// x^2 + y^2
        /// </summary>
        private static double Function(Vector vector) =>
            Pow(vector.Get(0), 2) + Pow(vector.Get(1), 2);
    }
}