using System;
using NUnit.Framework;

namespace MathUtilities.Test
{
    [TestFixture]
    [TestOf(typeof(Matrix))]
    public class MatrixTest
    {
        public Matrix matrix;

        [SetUp]
        public void Init()
        {
            var arr = new double[2][];
            arr[0] = new double[]{1, 2};
            arr[1] = new double[]{3, 4};
            matrix = new Matrix(arr);
        }

        [Test]
        public void TestThrown()
        {
            var wrong = new double[1][];
            wrong[0] = new double[3];
            Assert.Throws<ArgumentException>(() => new Matrix(wrong));
        }

        [Test]
        public void TestGet()
        {
            Assert.AreEqual(1, matrix.Get(0,0));
            Assert.AreEqual(2, matrix.Get(0,1));
            Assert.AreEqual(3, matrix.Get(1,0));
            Assert.AreEqual(4, matrix.Get(1,1));
        }

        [Test]
        [TestCase(2)]
        public void TestLength(int expected)
        {
            var got = matrix.Length;
            Assert.AreEqual(expected, got);
        }

        [Test]
        [TestCase(new double[]{4, 2}, new double[]{8, 20})]
        public void TestMultiplicationVectorToMatrix(double[] arr, double[] expected)
        {
            var vector = new Vector(arr);
            var result = matrix * vector;
            var got = result.Items;
            
            Assert.AreEqual(expected, got);
        }

        [Test]
        [TestCase(new double[]{-2, 1}, new double[]{1.5, -0.5})]
        public void TestReversed(params double[][] expected)
        {
            var reversed = matrix.GetReversed();
            var got = reversed.Items;

            Assert.AreEqual(expected, got);
        }

        [Test]
        [TestCase(-2)]
        public void TestSmallDeterminant(double expected)
        {
            var got = matrix.GetSmallDeterminant();

            Assert.AreEqual(expected, got);
        }
    }
}
