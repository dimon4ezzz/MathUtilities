using static System.Math;

namespace MathUtilities
{
    public static class Math
    {
        /// <summary>
        /// Типичное количество значащих цифр для сравнения.
        /// </summary>
        public const int DEFAULT_VALUE_NUMBERS_AMOUNT = 5;

        /// <summary>
        /// Примерно сравнивает числа.
        /// </summary>
        /// <param name="a">одно число</param>
        /// <param name="b">другое число</param>
        /// <returns>истина, если числа равны хотя бы
        ///  до -5 знака после значащей цифры</returns>
        public static bool Equals(double a, double b)
        {
            var minimalTenPower = Max(MinimalTenPower(a), MinimalTenPower(b));
            if (Abs(a - b) < minimalTenPower)
                return true;

            return false;
        }

        /// <summary>
        /// Передаёт минимальное количество значащих цифр от 0 до 15.
        /// </summary>
        /// <remark>
        /// Не работает для чисел \in (1; 10).
        /// </remark>
        /// <param name="n">число</param>
        /// <param name="restrict15">использовать ограничение на 15 цифр; по-умолчанию - истина</param>
        /// <returns>минимальное количество значащих цифр</returns>
        public static int MinimalPower(double n, bool restrict15 = true)
        {
            if (n == 0) return DEFAULT_VALUE_NUMBERS_AMOUNT;

            // модулируем
            var absolute = Abs(n);
            // находим оригинальную степень
            var power = Log10(absolute);
            // убираем знаки после запятой
            var rounded = Round(power);
            // новая степень десятки - количество значащих цифр;
            // удаляем минус из количества цифр
            var answer = (int) Abs(rounded - DEFAULT_VALUE_NUMBERS_AMOUNT);
            // избавляемся от чисел, меньше 1E-15 или больше 1E+15
            answer = restrict15 && answer > 15 ? 15 : answer;
            
            return answer;
        }

        /// <summary>
        /// Передаёт минимальную степень десятки, соответствующую минимальному количеству значащих цифр.
        /// </summary>
        /// <remark>
        /// Не работает для чисел \in (1; 10).
        /// </remark>
        /// <param name="n">число</param>
        /// <returns>минимальная степень десятки, соответствующая минимальному количеству значащих цифр</returns>
        public static double MinimalTenPower(double n) =>
            Pow(10, -1 * MinimalPower(n));

        /// <summary>
        /// Рассчитывает десятичный логарифм модуля числа.
        /// </summary>
        /// <param name="a">число</param>
        /// <returns>десятичный логарифм модуля числа</returns>
        public static double LogOfAbs(double a) =>
            Log10(Abs(a));

        /// <summary>
        /// Рассчитывает матрицу поворота вектора на указанный угол в пространстве R^2.
        /// </summary>
        /// <remark>
        /// Матрица будет работать только с векторами длины 2.
        /// </remark>
        /// <param name="angle">угол поворота в радианах</param>
        /// <returns>матрица поворота</returns>
        public static Matrix GetRotationMatrix(double angle)
        {
            var matrixAsArray = new double[2][];
            matrixAsArray[0] = new double[]{Cos(angle), Sin(angle)};
            matrixAsArray[1] = new double[]{-1 * Sin(angle), Cos(angle)};
            return new Matrix(matrixAsArray);
        }
    }
}
