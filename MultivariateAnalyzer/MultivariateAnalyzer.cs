using static System.Math;
using System;

namespace MathUtilities
{
    /// <summary>
    /// Работа с функцией многих переменных.
    /// </summary>
    public class MultivariateAnalyzer
    {
        /// <summary>
        /// Функция многих переменных для рассмотрения.
        /// </summary>
        private readonly Func<Vector, double> _multivariateFunction;

        /// <summary>
        /// Точка рассмотрения. В задаче - просто X_0.
        /// </summary>
        private readonly Vector _pointOfView;

        /// <summary>
        /// Направление движения. В задаче - просто R.
        /// </summary>
        private readonly Vector _vector;

        /// <summary>
        /// Анализатор функции одной переменной.
        /// </summary>
        private readonly InfinitesimalAnalyzer _analyzer;

        public MultivariateAnalyzer(
            Func<Vector, double> multivariateFunction,
            Vector pointOfView,
            Vector vector
        ) {
            _multivariateFunction = multivariateFunction;
            _pointOfView = pointOfView;
            _vector = vector;
            _analyzer = new InfinitesimalAnalyzer(Function);
        }

        /// <summary>
        /// Рассчитывает табличный вид бесконечно малой дополнительной функции.
        /// </summary>
        /// <returns></returns>
        public TableItem[] GetTableRepresentation() =>
            _analyzer.GetTableRepresentation();

        /// <summary>
        /// Рассчитывает асимптоту бесконечно малой дополнительной функции.
        /// </summary>
        /// <returns></returns>
        public Asymptote GetAsymptote() =>
            _analyzer.GetAsymptote();

        /// <summary>
        /// Рассчитывает коэффициент бесконечно малой дополнительной функции.
        /// </summary>
        /// <returns></returns>
        public double GetCoefficient() =>
            _analyzer.GetCoefficient();

        /// <summary>
        /// Передаёт точки кругов коэфициента `C`.
        /// </summary>
        /// <param name="iterationsAmount">количество точек для построения</param>
        /// <returns>точки кругов</returns>
        public Vector[] GetCircle(int iterationsAmount = 360)
        {
            // матрица поворота на 360 / iterations градусов в радианах
            var rotationMatrix = Math.GetRotationMatrix(PI / 180);
            // создаём пустой массив
            var answer = new Vector[iterationsAmount];
            // копируем вектор сюда
            var vector = _vector;
            // такая же функция, что и Function
            Func<double, double> f;
            
            // повторяем много раз
            for (int i = 0; i < iterationsAmount; i++)
            {
                // функция с направлением vector
                f = (t) => _multivariateFunction(_pointOfView + t * vector) - _multivariateFunction(_pointOfView);
                // вставляем её в анализатор
                _analyzer.Function = f;
                // рассчитываем значение вектора
                answer[i] = _pointOfView + _analyzer.GetCoefficient() * vector;
                // поворачиваем вектор
                vector *= rotationMatrix;
            }

            return answer;
        }

        /// <summary>
        /// Дополнительная преобразованная функция многих переменных в БМФ одной переменной.
        /// </summary>
        /// <param name="t">параметр бесконечно малой</param>
        /// <returns>значение БМФ</returns>
        private double Function(double t) =>
            _multivariateFunction(_pointOfView + t * _vector) - _multivariateFunction(_pointOfView);
    }
}
