using static System.Math;
using NUnit.Framework;

namespace MathUtilities.Test
{
    [TestFixture]
    [TestOf(typeof(Derivative))]
    public class DerivativeTest
    {
        public Derivative Derivative;

        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 3)]
        [TestCase(2, 12)]
        public void TestDerivative(double point, double expected)
        {
            Derivative = new Derivative(TypicalFunction);
            var got = Derivative.GetDerivative(point);

            // delta подбирается эмпирическим путём
            Assert.AreEqual(expected, got, 1E-09);
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 6)]
        [TestCase(2, 12)]
        public void TestSecondDerivative(double point, double expected)
        {
            Derivative = new Derivative(TypicalFunction);
            var got = Derivative.GetSecondDerivative(point);

            // delta подбирается эмпирическим путём
            Assert.AreEqual(expected, got, delta: 1E-06);
        }

        [Test]
        [TestCase(new double[]{0, 0}, 0, 0)]
        [TestCase(new double[]{1, 1}, 0, 2)]
        [TestCase(new double[]{0, 0}, 1, 0)]
        [TestCase(new double[]{1, 1}, 1, 2)]
        public void TestPartial(double[] arr, int index, double expected)
        {
            Derivative = new Derivative(TypicalMultivariateFunction);
            var vector = new Vector(arr);
            var got = Derivative.GetPartialDerivative(vector, index);

            Assert.AreEqual(expected, got);
        }
        
        [Test]
        [TestCase(new double[]{0, 0}, new double[]{0, 0})]
        [TestCase(new double[]{1, 1}, new double[]{2, 2})]
        public void TestGradient(double[] arr, double[] expected)
        {
            Derivative = new Derivative(TypicalMultivariateFunction);
            var vector = new Vector(arr);
            var gradient = Derivative.GetGradient(vector);
            var got = gradient.Items;

            Assert.AreEqual(expected, got);
        }

        [Test]
        [TestCase(new double[]{0, 0}, new double[]{2, 0, 0, 2})]
        [TestCase(new double[]{1, 1}, new double[]{2, 0, 0, 2})]
        public void TestSecondPartial(double[] arr, double[] expected)
        {
            Derivative = new Derivative(TypicalMultivariateFunction);
            var vector = new Vector(arr);
            var joinedResult = new double[4];

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    joinedResult[2 * i + j] = Derivative.GetSecondPartialDerivative(vector, i, j);

            Assert.AreEqual(expected, joinedResult);
        }

        [Test]
        [TestCase(new double[]{0, 0}, new double[] {2, 0}, new double[]{0, 2})]
        [TestCase(new double[]{1, 1}, new double[] {2, 0}, new double[]{0, 2})]
        public void TestHessian(double[] arr, params double[][] expected)
        {
            Derivative = new Derivative(TypicalMultivariateFunction);
            var vector = new Vector(arr);
            var hessian = Derivative.GetHessian(vector);
            var got = hessian.Items;

            for (int i = 0; i < 2; i++)
                Assert.AreEqual(expected[i], got[i]);
        }

        /// <summary>
        /// x^3
        /// </summary>
        /// <remark>
        /// F'(1) = 3
        /// F'(2) = 12
        /// F''(1) = 6
        /// F''(2) = 12
        /// </remark>
        private double TypicalFunction(double x) =>
            Pow(x, 3);

        /// <summary>
        /// x^2 + y^2
        /// </summary>
        /// <remark>
        /// F'(0, 0) = 0;
        /// F'(1, 1) = 2;
        /// </remark>
        private double TypicalMultivariateFunction(Vector vector) =>
            Pow(vector.Get(0), 2) + Pow(vector.Get(1), 2);
    }
}
