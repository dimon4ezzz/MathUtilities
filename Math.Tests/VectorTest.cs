using System;
using NUnit.Framework;

namespace MathUtilities.Test
{
    [TestFixture]
    [TestOf(typeof(Vector))]
    public class VectorTest
    {
        public Vector Vector;

        [Test]
        [TestCase(new double[]{7.9, 9.1}, 1, 9.1)]
        public void TestGet(double[] arr, int index, double expected)
        {
            Vector = new Vector(arr);
            var got = Vector.Get(index);

            Assert.AreEqual(got, expected);
        }

        [Test]
        [TestCase(new double[]{7, 2, 1, 9}, 4)]
        public void TestLength(double[] arr, int expected)
        {
            Vector = new Vector(arr);
            var got = Vector.Length;

            Assert.AreEqual(expected, got);
        }

        [Test]
        [TestCase(new double[]{1, 1}, new double[]{1, -1})]
        [TestCase(new double[]{5, 10}, new double[]{10, -5})]
        public void TestOrth(double[] arr, double[] expected)
        {
            Vector = new Vector(arr);
            var orth = Vector.GetOrth();
            var got = orth.Items;

            Assert.AreEqual(expected, got);
        }

        [Test]
        [TestCase(new double[]{1, 1})]
        [TestCase(new double[]{5, 10})]
        public void AltTestOrth(double[] arr)
        {
            Vector = new Vector(arr);
            var orth = Vector.GetOrth();
            var got = Vector * orth;

            Assert.AreEqual(0, got);
        }

        [Test]
        [TestCase(new double[]{1, 2}, new double[]{5, 7}, new double[]{6, 9})]
        public void TestSum(double[] leftArray, double[] rightArray, double[] expected)
        {
            var left = new Vector(leftArray);
            var right = new Vector(rightArray);
            var result = left + right;
            var got = result.Items;

            Assert.AreEqual(expected, got);
        }

        [Test]
        [TestCase(new double[]{1, 2}, new double[]{1, 2, 3})]
        public void TestSumException(double[] leftArray, double[] rightArray)
        {
            var left = new Vector(leftArray);
            var right = new Vector(rightArray);

            Assert.Throws<ApplicationException>(() => { var a = left + right; });
        }

        [Test]
        [TestCase(new double[]{5, 7}, new double[]{4, 5}, new double[]{1, 2})]
        public void TestMinus(double[] leftArray, double[] rightArray, double[] expected)
        {
            var left = new Vector(leftArray);
            var right = new Vector(rightArray);
            var result = left - right;
            var got = result.Items;

            Assert.AreEqual(expected, got);
        }

        [Test]
        [TestCase(new double[]{1, 2}, new double[]{1, 2, 3})]
        public void TestMinusException(double[] leftArray, double[] rightArray)
        {
            var left = new Vector(leftArray);
            var right = new Vector(rightArray);

            Assert.Throws<ApplicationException>(() => { var a = left - right; });
        }

        [Test]
        [TestCase(new double[]{1, 2}, new double[]{5, 7}, 19)]
        public void TestMultiplication(double[] leftArray, double[] rightArray, double expected)
        {
            var left = new Vector(leftArray);
            var right = new Vector(rightArray);
            var got = left * right;

            Assert.AreEqual(expected, got);
        }

        [Test]
        [TestCase(new double[]{1, 2}, new double[]{1, 2, 3})]
        public void TestMultiplicationException(double[] leftArray, double[] rightArray)
        {
            var left = new Vector(leftArray);
            var right = new Vector(rightArray);

            Assert.Throws<ApplicationException>(() => { var a = left * right; });
        }

        [Test]
        [TestCase(2, new double[]{4, 5}, new double[]{8, 10})]
        public void TestMultiplicationWithNumber(double number, double[] arr, double[] expected)
        {
            Vector = new Vector(arr);
            var result = number * Vector;
            var got = result.Items;

            Assert.AreEqual(expected, got);
        }

        [Test]
        [TestCase(new double[]{4, 2}, new double[]{46, 50})]
        public void TestMultiplicationVectorToMatrix(double[] arr, double[] expected)
        {
            Vector = new Vector(arr);
            var matrixArray = new double[2][];
            matrixArray[0] = new double[]{7, 12};
            matrixArray[1] = new double[]{9, 1};
            var matrix = new Matrix(matrixArray);
            var result = Vector * matrix;
            var got = result.Items;

            Assert.AreEqual(expected, got);
        }
    }
}
