using static MathUtilities.Math;

namespace MathUtilities
{
    /// <summary>
    /// Представляет собой ячейку в табличном виде функции.
    /// </summary>
    public struct TableItem
    {
        /// <summary>
        /// Аргумент функции.
        /// </summary>
        public double Input { get; set; }

        /// <summary>
        /// Значение функции.
        /// </summary>
        public double Output { get; set; }

        /// <summary>
        /// Десятичный логарифм аргумента функции.
        /// </summary>
        /// <remark>
        /// Вычисляет логарифм значения по модулю.
        /// </remark>
        public double LgInput { get => LogOfAbs(Input); }

        /// <summary>
        /// Десятичный логарифм значения функции.
        /// </summary>
        /// <remark>
        /// Вычисляет логарифм значения по модулю.
        /// </remark>
        public double LgOutput { get => LogOfAbs(Output); }
    }
}
