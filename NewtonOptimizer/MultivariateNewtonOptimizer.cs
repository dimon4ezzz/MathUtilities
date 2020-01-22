using static System.Math;
using System;

namespace MathUtilities
{
    /// <summary>
    /// Класс для поиска оптимума методом Ньютона.
    /// </summary>
    public class MultivariateNewtonOptimizer
    {
        /// <summary>
        /// Функция для исследования.
        /// </summary>
        private readonly Func<Vector, double> _function;

        /// <summary>
        /// Класс для нахождения производной, градиента и Гессиана.
        /// </summary>
        private readonly Derivative _derivative;

        /// <summary>
        /// Начальный вектор поиска.
        /// </summary>
        private readonly Vector _start;

        /// <summary>
        /// Точность поиска.
        /// </summary>
        private readonly double _precision;

        /// <summary>
        /// Максимальное количество итераций.
        /// </summary>
        private readonly int _operationsAmount;

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
        public MultivariateNewtonOptimizer(
            Func<Vector, double> function,
            double[] start,
            double precision = 1E-08,
            int operationsAmount = 8
        ) {
            _function = function;
            _derivative = new Derivative(function);
            _start = new Vector(start);
            _precision = precision;
            _operationsAmount = operationsAmount;
            ResetCounter();
        }

        /// <summary>
        /// Рассчитывает точку, в которой функция имеет минимум.
        /// </summary>
        public Vector GetOptimal()
        {
            // градиент в начальной точке
            var gradient = _derivative.GetGradient(_start);
            // гессиан в начальной точке
            var hessian = _derivative.GetHessian(_start);
            
            // запоминаем предыдующий вектор
            var previous = _start;
            // считаем текущий вектор по формуле
            var current = _start - hessian.GetReversed() * gradient;
            // пока поле поиска достаточно большое
            while (!IsTooSmall(previous, current))
            {
                // если операций не больше максимального
                if (IsCountDown())
                    throw new ApplicationException("не нашлось решения");

                // запомнить предыдущий вектор
                previous = current;
                // рассчитать градиент в текущей точке
                gradient = _derivative.GetGradient(previous);
                // рассчитать гессиан в текущей точке
                hessian = _derivative.GetHessian(previous);
                // пересчитать текущий вектор
                current = previous - hessian.GetReversed() * gradient;
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
        private bool IsTooSmall(Vector previous, Vector current) =>
            Abs(_function(current) - _function(previous)) < _precision;
    }
}
