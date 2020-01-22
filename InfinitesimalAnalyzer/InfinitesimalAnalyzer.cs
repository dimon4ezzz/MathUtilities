using static System.Math;
using System;

namespace MathUtilities
{
    /// <summary>
    /// Исследует бесконечно малые функции.
    /// </summary>
    public class InfinitesimalAnalyzer
    {
        /// <summary>
        /// Степени десятки для составления табличного вида БМФ.
        /// </summary>
        private static readonly double[] PowersOfTen = new double[]{
            1, 1E-01, 1E-02, 1E-03, 1E-04, 1E-05, 1E-06, 1E-07, 1E-08, 1E-09, 1E-10, 1E-11, 1E-12, 1E-13, 1E-14, 1E-15
        };

        /// <summary>
        /// Функция для исследования.
        /// </summary>
        public Func<double, double> Function { get; set; }

        /// <summary>
        /// Типичный конструктор для создания новой функции.
        /// </summary>
        /// <param name="function">функция для исследования</param>
        public InfinitesimalAnalyzer(Func<double, double> function)
        {
            Function = function;
        }

        /// <summary>
        /// Определяет, является ли функция бесконечно малой.
        /// </summary>
        /// <returns>истина, если функция бесконечно мала</returns>
        public bool IsInfinitesimal() =>
            Function(0) == 0;

        /// <summary>
        /// Определяет табличный вид функции, в том числе логарифмическую таблицу.
        /// </summary>
        /// <param name="start">с какой позиции начинать</param>
        /// <param name="amount">количество ячеек; не более 16; start + amount <= 16</param>
        /// <returns>массив с ячейками</returns>
        public TableItem[] GetTableRepresentation(uint start = 0, uint amount = 16)
        {
            if (amount > 16)
                throw new ArgumentException("количетсво ячеек не более 16");

            if (start + amount > 16)
                throw new ArgumentException("нельзя пытаться вылезти за 16");

            // 16 степеней десятки у нас есть
            var answer = new TableItem[amount - start];

            // все степени и значения функции при этих аргументах добавляем в массив
            for (int i = (int) start; i < amount; i++)
                answer[i - start]  = new TableItem{
                    Input  = PowersOfTen[i],
                    Output = Function(PowersOfTen[i])
                };

            return answer;
        }

        /// <summary>
        /// Определяет асимптоту lg-графика.
        /// </summary>
        public Asymptote GetAsymptote() =>
            new Asymptote(Function);

        /// <summary>
        /// Определяет коэффициент `C` БМФ.
        /// </summary>
        public double GetCoefficient() =>
            Pow(10, GetAsymptote().K);
    }
}
