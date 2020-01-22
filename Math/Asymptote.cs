using static System.Math;
using static MathUtilities.Math;
using System;

namespace MathUtilities
{
    /// <summary>
    /// Представляет собой асимптоту lg-графика.
    /// </summary>
    public class Asymptote
    {
        /// <summary>
        /// Коэфициент при X.
        /// </summary>
        public double Alpha { get; set; }

        /// <summary>
        /// Свободный член.
        /// </summary>
        public double K { get; set; }

        /// <summary>
        /// Создаёт асимптоту lg-графика.
        /// </summary>
        /// <param name="function">рассматриваемая функция</param>
        public Asymptote(Func<double, double> function)
        {
            // по сути, коэффициент находится там,
            // где хотя бы два отрезка представляют собой прямую линию;
            // но предположим, контрольная точка находится на точке lg(h) = -4
            var left = Pow(10, -4);
            var right = Pow(10, -4.001);

            Alpha = LogOfAbs(function(right) / function(left)) / Log10(right / left);
            K = -1 * Log10(right) * LogOfAbs(function(left) / function(right)) / Log10(left / right) + LogOfAbs(function(right));
        }
    }
}
