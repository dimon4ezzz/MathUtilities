using static System.Math;
using System;

namespace MathUtilities
{
    /// <summary>
    /// Класс для одномерной оптимизации методом Ньютона.
    /// </summary>
    public class NewtonOptimizer
    {
        /// <summary>
        /// Функция для исследования.
        /// </summary>
        private readonly Func<double, double> _function;

        /// <summary>
        /// Класс для нахождения производной, градиента и Гессиана.
        /// </summary>
        private readonly Derivative _derivative;

        /// <summary>
        /// Начальный вектор поиска.
        /// </summary>
        private readonly double _start;

        /// <summary>
        /// Точность поиска.
        /// </summary>
        private readonly double _precision;

        /// <summary>
        /// Максимальное количество итераций.
        /// </summary>
        private readonly int _operationsAmount;

        /// <summary>
        /// Логгер пишет в консоль.
        /// </summary>
        private readonly Logger _logger;

        /// <summary>
        /// Счётчик операций.
        /// </summary>
        private int counter;

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="function">функция для исследования</param>
        /// <param name="start">начальный вектор</param>
        /// <param name="precision">точность поиска</param>
        /// <param name="operationsAmount">максимальное количество итераций</param>
        /// <param name="verbose">писать ли в консоль о каждом шаге</param>
        public NewtonOptimizer(
            Func<double, double> function,
            double start = 0,
            double precision = 1E-08,
            int operationsAmount = 15,
            bool verbose = false
        ) {
            _function = function;
            _derivative = new Derivative(function);
            _start = start;
            _precision = precision;
            _operationsAmount = operationsAmount;
            _logger = new Logger(verbose);
            ResetCounter();
        }

        /// <summary>
        /// Рассчитывает точку, в которой функция имеет минимум.
        /// </summary>
        public double GetOptimal()
        {
            // считаем производную
            var derivative = _derivative.GetDerivative(_start);
            _logger.Log($"производная: {derivative}");
            // считаем вторую производную
            var secDerivative = _derivative.GetSecondDerivative(_start);
            _logger.Log($"вторая производная: {secDerivative}");
            // считаем текущий вектор по формуле
            var current = _start - derivative / secDerivative;
            _logger.Log($"текущая точка: {current}");
            // пока разница между значениями не станет меньше заданного
            while (!Math.Equals(derivative, 0))
            {
                // если операций не больше максимального
                if (IsCountDown())
                    throw new ApplicationException("не нашлось решения");

                // рассчитать производную в текущей точке
                derivative = _derivative.GetDerivative(current);
                _logger.Log($"производная: {derivative}");
                // считаем вторую производную
                secDerivative = _derivative.GetSecondDerivative(current);
                _logger.Log($"вторая производная: {secDerivative}");
                // считаем текущий вектор по формуле
                current = current - derivative / secDerivative;
                _logger.Log($"текущая точка: {current}");
            }

            // вернуть найденный вектор
            return current;
        }

        /// <summary>
        /// Не превысило ли количество операций максимальное.
        /// </summary>
        private bool IsCountDown() =>
            counter++ > _operationsAmount;

        /// <summary>
        /// Обнуляет количество проделанных операций.
        /// </summary>
        private void ResetCounter() =>
            counter = 0;

        /// <summary>
        /// Проверяет, является ли новое решение слишком близким.
        /// </summary>
        /// <param name="previous">старое решение</param>
        /// <param name="current">текущее решение</param>
        /// <returns>истина, если решения находятся близко друг к другу</returns>
        private bool IsTooSmall(double previous, double current) =>
            Abs(_function(current) - _function(previous)) < _precision;
    }
}