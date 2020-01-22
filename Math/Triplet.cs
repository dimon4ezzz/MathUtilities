namespace MathUtilities
{
    /// <summary>
    /// Тройка чисел.
    /// </summary>
    public class Triplet
    {
        /// <summary>
        /// Левая часть тройки.
        /// </summary>
        public double A { get; set; }

        /// <summary>
        /// Правая часть тройки.
        /// </summary>
        public double B { get; set; }

        /// <summary>
        /// Центральная часть тройки.
        /// </summary>
        public double Center { get => A + (B - A) / 2; }

        public override string ToString() =>
            $"{A} - {Center} - {B}";
    }
}
