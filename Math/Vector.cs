using System;

namespace MathUtilities
{
    /// <summary>
    /// Представляет собой вектор любой длины.
    /// </summary>
    public class Vector
    {
        /// <summary>
        /// Массив элементов вектора.
        /// </summary>
        /// <value></value>
        public double[] Items { get; private set; }

        /// <summary>
        /// Длина вектора.
        /// </summary>
        public int Length { get => Items.Length; }

        /// <summary>
        /// Стандартный конструктор задания элементов вектора.
        /// </summary>
        /// <param name="items">элементы вектора</param>
        public Vector(params double[] items) =>
            this.Items = items;

        public override string ToString() =>
            string.Join("\t", Items);

        /// <summary>
        /// Передаёт элемент вектора по индексу.
        /// </summary>
        /// <param name="index">индекс элемента вектора</param>
        /// <returns>элемент вектора в индексе</returns>
        public double Get(int index) =>
            Items[index];

        /// <summary>
        /// Рассчитывает ортогональный этому вектору вектор.
        /// </summary>
        /// <remark>
        /// Реализован только для R^2.
        /// </remark>
        /// <returns>ортогональный вектор</returns>
        public Vector GetOrth() =>
            new Vector(Get(1), -1 * Get(0));

        /// <summary>
        /// Определение оператора сложения векторов.
        /// </summary>
        /// <param name="left">левый вектор</param>
        /// <param name="right">правый вектор</param>
        /// <returns>сумма векторов - новый вектор</returns>
        public static Vector operator +(Vector left, Vector right)
        {
            if (left.Length != right.Length)
                throw new ApplicationException("для сложения векторы должны быть одной длины");

            int length = left.Length;
            var answer = new double[length];
            for (int i = 0; i < length; i++)
                answer[i] = left.Get(i) + right.Get(i);
            return new Vector(answer);
        }

        /// <summary>
        /// Определение оператора разницы векторов.
        /// </summary>
        /// <param name="left">уменьшаемый вектор</param>
        /// <param name="right">вычитаемый вектор</param>
        /// <returns>разница векторов - новый вектор</returns>
        public static Vector operator -(Vector left, Vector right)
        {
            if (left.Length != right.Length)
                throw new ApplicationException("для вычитания векторы должны быть одной длины");

            int length = left.Length;
            var answer = new double[length];
            for (int i = 0; i < length; i++)
                answer[i] = left.Get(i) - right.Get(i);
            return new Vector(answer);
        }

        /// <summary>
        /// Определение операции скалярного произведения векторов.
        /// </summary>
        /// <param name="left">левый вектор</param>
        /// <param name="right">правый вектор</param>
        /// <returns>скалярное произведение векторов - число</returns>
        public static double operator *(Vector left, Vector right)
        {
            if (left.Length != right.Length)
                throw new ApplicationException("для произведения векторы должны быть одной длины");

            int length = left.Length;
            double answer = 0;
            for (int i = 0; i < length; i++)
                answer += left.Get(i) * right.Get(i);
            return answer;
        }

        /// <summary>
        /// Определение операции произведения числа на вектор.
        /// </summary>
        /// <param name="number">число</param>
        /// <param name="vector">вектор</param>
        /// <returns>произведение - новый вектор</returns>
        public static Vector operator *(double number, Vector vector)
        {
            int length = vector.Length;
            var answer = new double[length];
            for (int i = 0; i < length; i++)
                answer[i] = number * vector.Get(i);
            return new Vector(answer);
        }

        /// <summary>
        /// Определение операции произведения вектора на матрицу.
        /// </summary>
        /// <param name="vector">вектор</param>
        /// <param name="matrix">матрица</param>
        /// <returns>произведение - новый вектор</returns>
        public static Vector operator *(Vector vector, Matrix matrix)
        {
            if (vector.Length != matrix.Length)
                throw new ApplicationException("для произведения вектор и матрица должны быть одной длины");

            int length = vector.Length;
            var answer = new double[length];
            for (int i = 0; i < length; i++)
            {
                answer[i] = 0;
                for (int j = 0; j < length; j++)
                    answer[i] += vector.Get(j) * matrix.Get(j, i);
            }

            return new Vector(answer);
        }
    }
}
