namespace MathUtilities
{
    /// <summary>
    /// Золотая тройка.
    /// </summary>
    public class GoldenTriplet
    {
        /// <summary>
        /// Золотое число. 1 - (\sqrt(5) - 1) / 2
        /// </summary>
        public const double SmallGolden = 0.3819660113;

        /// <summary>
        /// Золотое число. (\sqrt(5) - 1) / 2
        /// </summary>
        public const double Golden = 0.6180339887;

        /// <summary>
        /// Золотое число. (\sqrt(5) + 1) / 2
        /// </summary>
        public const double BigGolden = 1.6180339887;

        /// <summary>
        /// Левая часть тройки.
        /// </summary>
        public double A { get; set; }

        /// <summary>
        /// Правая часть тройки.
        /// </summary>
        public double B { get; set; }

        /// <summary>
        /// Центральная часть тройки ближе к правому краю.
        /// 
        /// Сеттер на самом деле устанавливает правую часть.
        /// </summary>
        /// <remark>
        /// Золотое сечение.
        /// </remark>
        public double RightCenter { get => A + (B - A) * Golden; set => B = value * BigGolden - A * Golden; }

        /// <summary>
        /// Центральная часть тройки ближе к левому краю.
        /// </summary>
        /// <remark>
        /// Золотое сечение.
        /// </remark>
        public double LeftCenter { get => B - (B - A) * Golden; set => B = value + BigGolden * (value - A); }

        public override string ToString() =>
            $"{A} - {LeftCenter} - {RightCenter} - {B}";
    }
}
