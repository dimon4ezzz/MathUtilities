using static System.Math;
using NUnit.Framework;

namespace MathUtilities.Tests
{
    [TestFixture]
    [TestOf(typeof(InfiniteAnalyzer))]
    public class InfiniteAnalyzerTest
    {
        [Test]
        public void TestAsymptote()
        {
            var ia = new InfiniteAnalyzer();
            var a = ia.GetAsymptote();
            Assert.AreEqual(2, Round(a.Alpha));
        }
    }
}