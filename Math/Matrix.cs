using System;

namespace MathUtilities
{
    /// <summary>
    /// Класс, представляющий матрицу.
    /// 
    /// Прим.: все функции работают только с квадратными матрицами.
    /// Прим.: некоторые функции работают только с матрицей 2 на 2.
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// Разделитель элементов матрицы в строковом представлении.
        /// </summary>
        private const string ITEM_DELIMITER = ", ";

        /// <summary>
        /// Разделитель строк матрицы в строковом представлении.
        /// </summary>
        private const string ROW_DELIMITER = "\n";

        /// <summary>
        /// Элементы матрицы.
        /// </summary>
        public double[][] Items { get; private set; }

        /// <summary>
        /// Стандартный конструктор.
        /// </summary>
        /// <param name="items">двумерный массив элементов матрицы</param>
        public Matrix(double[][] items)
        {
            if (items.Length != items[0].Length)
                throw new ArgumentException("матрица должна быть квадратной");

            this.Items = items;
        }
            
        /// <summary>
        /// Элемент матрицы в индексах.
        /// </summary>
        /// <param name="index1">столбец</param>
        /// <param name="index2">строка</param>
        public double Get(int index1, int index2) =>
            Items[index1][index2];

        /// <summary>
        /// Длина стороны матрицы.
        /// </summary>
        public int Length =>
            Items.Length;

        /// <summary>
        /// Умножение матрицы на вектор.
        /// </summary>
        /// <param name="matrix">матрица</param>
        /// <param name="vector">вектор</param>
        /// <returns>произведение - новый вектор</returns>
        public static Vector operator *(Matrix matrix, Vector vector)
        {
            int length = vector.Length;
            var items = new double[length];
            Vector temp;

            // просто перемножаем строки на вектор,
            // так как строки матрицы - такие же векторы
            for (int i = 0; i < length; i++)
            {
                temp = new Vector(matrix.Items[i]);
                items[i] = temp * vector;
            }

            return new Vector(items);
        }

        /// <summary>
        /// Передаёт обратную матрицу.
        /// 
        /// Здесь представлена только оптимизированная версия для матрицы 2 на 2.
        /// </summary>
        /// <returns>обратная матрице матрица</returns>
        public Matrix GetReversed()
        {
            // получаем определитель матрицы
            var det = GetSmallDeterminant();
            // создаём пустой двумерный массив
            var matrix = new double[2][];
            // заполняем первую строку по формуле
            matrix[0] = new double[2];
            matrix[0][0] = Items[1][1] / det;
            matrix[0][1] = -1 * Items[0][1] / det;
            // заполняем вторую строку по формуле
            matrix[1] = new double[2];
            matrix[1][0] = -1 * Items[1][0] / det;
            matrix[1][1] = Items[0][0] / det;
            return new Matrix(matrix);
        }

        /// <summary>
        /// Считает определитель этой матрицы по оптимизированной формуле для матрицы 2 на 2.
        /// </summary>
        /// <returns>определитель матрицы</returns>
        public double GetSmallDeterminant() =>
            Items[0][0] * Items[1][1] - Items[0][1] * Items[1][0];

        /// <summary>
        /// Переопределение преобразования в строку:
        /// все элементы через запятую, строки через перевод строки.
        /// </summary>
        public override string ToString()
        {
            int length = Length;
            var arr = new string[length];
            for (int i = 0; i < length; i++)
                arr[i] = String.Join(ITEM_DELIMITER, Items[i]);
            return String.Join(ROW_DELIMITER, arr);
        }
    }
}
