using NUnit.Framework;
using System;

namespace MathUtilities.Tests
{
    [TestFixture]
    [TestOf(typeof(DichotomyOptimizer))]
    public class DichotomyOptimizerTest
    {
        [Test]
        public void TestLeftParabola()
        {
            var o = new DichotomyOptimizer(LeftParabola);
            var got = o.GetOptimal();
            Assert.AreEqual(-1, got, 1E-04);
        }

        [Test]
        public void TestParabola()
        {
            var o = new DichotomyOptimizer(Parabola, start: 1);
            var got = o.GetOptimal();
            Assert.AreEqual(0, got, 1E-04);
        }
        
        [Test]
        public void TestAreaNotFound()
        {
            var o = new DichotomyOptimizer(Line);
            Assert.Catch<ApplicationException>(() => o.GetOptimal());
        }

        private double LeftParabola(double x) =>
            x * x + 2 * x + 1;

        private double Parabola(double x) =>
            x * x;

        private double Line(double x) =>
            2 * x;
    }
}