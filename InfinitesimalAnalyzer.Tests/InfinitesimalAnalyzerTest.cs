using NUnit.Framework;

namespace MathUtilities.Tests
{
    [TestFixture]
    [TestOf(typeof(InfinitesimalAnalyzer))]
    public class InfinitesimalAnalyzerTest
    {
        public static double[] ExpectedTableInputs = new double[]{
            1, 1E-01, 1E-02, 1E-03, 1E-04, 1E-05, 1E-06, 1E-07, 1E-08, 1E-09, 1E-10, 1E-11, 1E-12, 1E-13, 1E-14, 1E-15
        };

        public static double[] ExpectedTableOutputs = new double[]{
            1E+01, 1, 1E-01, 1E-02, 1E-03, 1E-04, 1E-05, 1E-06, 1E-07, 1E-08, 1E-09, 1E-10, 1E-11, 1E-12, 1E-13, 1E-14
        };

        public static double[] ExpectedTableLgInputs = new double[]{
            0, -1, -2, -3, -4, -5, -6, -7, -8, -9, -10, -11, -12, -13, -14, -15
        };

        public static double[] ExpectedTableLgOutputs = new double[]{
            1, 0, -1, -2, -3, -4, -5, -6, -7, -8, -9, -10, -11, -12, -13, -14
        };

        [Test]
        public void InfinitesimalTest()
        {
            var ia = new InfinitesimalAnalyzer(Function);
            var got = ia.IsInfinitesimal();
            Assert.IsTrue(got);
        }

        [Test]
        public void NotInfinitesimalTest()
        {
            var ia = new InfinitesimalAnalyzer(NotInfinitesimalFunction);
            var got = ia.IsInfinitesimal();
            Assert.IsFalse(got);
        }

        [Test]
        public void TableRepresentationTest()
        {
            var ia = new InfinitesimalAnalyzer(Function);
            var got = ia.GetTableRepresentation();

            for (int i = 0; i < 16; i++)
            {
                Assert.AreEqual(ExpectedTableInputs[i], got[i].Input);
                Assert.IsTrue(Math.Equals(ExpectedTableOutputs[i], got[i].Output));
                Assert.AreEqual(ExpectedTableLgInputs[i], got[i].LgInput);
                Assert.AreEqual(ExpectedTableLgOutputs[i], got[i].LgOutput);
            }
        }

        [Test]
        public void AsymptoteTest()
        {
            var ia = new InfinitesimalAnalyzer(Function);
            var got = ia.GetAsymptote();
            Assert.AreEqual(got.Alpha, 1);
            Assert.AreEqual(got.K, 1);
        }

        [Test]
        public void CoefficientTest()
        {
            var ia = new InfinitesimalAnalyzer(Function);
            var got = ia.GetCoefficient();
            Assert.AreEqual(got, 10);
        }

        public double Function(double x) =>
            10 * x;

        public double NotInfinitesimalFunction(double x) =>
            x + 1;
    }
}