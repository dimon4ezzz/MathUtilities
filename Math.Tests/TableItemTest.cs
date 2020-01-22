using NUnit.Framework;

namespace MathUtilities
{
    [TestFixture]
    [TestOf(typeof(TableItem))]
    public class TableItemTest
    {
        public TableItem TableItem;

        [Test]
        [TestCase(1E-4, -4)]
        public void TestLgInput(double input, double expected)
        {
            TableItem = new TableItem{Input = input};
            var got = TableItem.LgInput;

            Assert.AreEqual(expected, got);
        }

        [Test]
        [TestCase(1E-4, -4)]
        public void TestLgOutput(double output, double expected)
        {
            TableItem = new TableItem{Output = output};
            var got = TableItem.LgOutput;

            Assert.AreEqual(expected, got);
        }
    }
}
