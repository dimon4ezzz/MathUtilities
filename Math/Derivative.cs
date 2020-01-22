using static System.Math;
using System;

namespace MathUtilities
{
    /// <summary>
    /// Класс для численного дифференцирования.
    /// </summary>
    public class Derivative
    {
        /// <summary>
        /// Типичное количество значащих цифр.
        /// </summary>
        public const int DEFAULT_VALUE_NUMBERS_AMOUNT = 5;

        /// <summary>
        /// Функция для дифференцирования.
        /// </summary>
        private readonly Func<Vector, double> _function;

        /// <summary>
        /// Стандартный конструктор для дифференцирования.
        /// </summary>
        /// <param name="function">функция для дифференцирования</param>
        public Derivative(Func<Vector, double> function) =>
            _function = function;

        /// <summary>
        /// Конструктор для функции одной переменной.
        /// </summary>
        /// <param name="function">функция для дифференцирования</param>
        public Derivative(Func<double, double> function) =>
            _function = (x) => function(x.Get(0));

        /// <summary>
        /// Рассчитывает производную по одному аргументу при помощи центральной формулы.
        /// </summary>
        /// <param name="point">точка, в которой рассчитывается производная</param>
        /// <returns>значение производной в точке</returns>
        public double GetDerivative(double point)
        {
            var eps = Math.MinimalTenPower(point);
            var left = _function(new Vector(point + eps));
            var right = _function(new Vector(point - eps));
            return (left - right) / (2 * eps);
        }

        /// <summary>
        /// Рассчитывает вторую производную по одному аргументу при помощи центральной формулы.
        /// </summary>
        /// <param name="point">точка, в которой рассчитывается производная</param>
        /// <returns>значение второй производной в точке</returns>
        public double GetSecondDerivative(double point)
        {
            var eps = Math.MinimalTenPower(point);
            var left = _function(new Vector(point + 2 * eps));
            var right = _function(new Vector(point - 2 * eps));
            return (left + right - 2 * _function(new Vector(point))) / (4 * eps * eps);
        }

        /// <summary>
        /// Градиент функции.
        /// </summary>
        /// <param name="vector">точка, в которой ищем градиент</param>
        /// <returns>вектор градиента</returns>
        public Vector GetGradient(Vector vector)
        {
            int length = vector.Length;
            var items = new double[length];
            for (int i = 0; i < length; i++)
                items[i] = GetPartialDerivative(vector, i);
            return new Vector(items);
        }

        /// <summary>
        /// Рассчитывает гессиан - матрицу вторых частных производных.
        /// </summary>
        /// <param name="vector">точка, в которой ищем градиент</param>
        /// <returns>матрица Гессе</returns>
        public Matrix GetHessian(Vector vector)
        {
            int length = vector.Length;
            var answer = new double[length][];
            for (int i = 0; i < vector.Length; i++)
            {
                answer[i] = new double[length];
                for (int j = 0; j < length; j++)
                    answer[i][j] = GetSecondPartialDerivative(vector, i, j);
            }
                
            return new Matrix(answer);
        }

        /// <summary>
        /// Рассчитывает первую частную производную.
        /// </summary>
        /// <param name="vector">точка, в которой ищем производную</param>
        /// <param name="index">индекс переменной ФНП</param>
        /// <param name="useSavedPrecision">использовать ли последующую точность расчётов</param>
        /// <param name="savedPrecision">точность расчётов</param>
        /// <returns>значение первой частной производной в точке</returns>
        public double GetPartialDerivative(
            Vector vector,
            int index,
            bool useSavedPrecision = false,
            double savedPrecision = double.NaN
        ) {
            // клонируем массивы, так как иначе переменная
            // будет являться лишь ссылкой на старый массив
            var leftvec = (double[]) vector.Items.Clone();
            var rightvec = (double[]) vector.Items.Clone();
            // точность расчётов - см. исследование БМВ
            var precision = useSavedPrecision ?
                Max(savedPrecision, Math.MinimalTenPower(vector.Get(index))) :
                Math.MinimalTenPower(vector.Get(index));

            leftvec[index] += precision;
            rightvec[index] -= precision;

            var leftFunction = _function(new Vector(leftvec));
            var rightFunction = _function(new Vector(rightvec));

            var answer = (leftFunction - rightFunction)/(2 * precision);

            return Round(answer, Math.MinimalPower(answer));
        }

        /// <summary>
        /// Вторая частная производная в точке.
        /// </summary>
        /// <param name="vector">точка</param>
        /// <param name="index1">индекс переменной 1</param>
        /// <param name="index2">индекс переменной 2</param>
        /// <returns>значение второй производной</returns>
        public double GetSecondPartialDerivative(Vector vector, int index1, int index2)
        {
            // клонируем массивы, так как иначе переменная
            // будет являться лишь ссылкой на старый массив
            var leftvec = (double[]) vector.Items.Clone();
            var rightvec = (double[]) vector.Items.Clone();
            // точность расчётов - см. исследование БМВ
            var precision = Math.MinimalTenPower(vector.Get(index1));

            leftvec[index1] += precision;
            rightvec[index1] -= precision;

            var leftPartial = GetPartialDerivative(new Vector(leftvec), index2, true, precision);
            var rightPartial = GetPartialDerivative(new Vector(rightvec), index2, true, precision);

            var answer = (leftPartial - rightPartial)/(2 * precision);

            return Round(answer, Math.MinimalPower(answer));
        }
    }
}
