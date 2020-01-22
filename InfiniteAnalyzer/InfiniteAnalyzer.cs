using System;
using static System.Math;

namespace MathUtilities
{
    /// <summary>
    /// Реализация алгоритма анализа ББВ с помощью БМВ.
    /// </summary>
    public class InfiniteAnalyzer
    {
        /// <summary>
        /// Анализатор бесконечно малой величины.
        /// </summary>
        private readonly InfinitesimalAnalyzer _analyzer;

        /// <summary>
        /// Собственно, сортировка.
        /// </summary>
        /// <remark>
        /// Вместо неё может быть любая ББФ, которая по методу `Sort` возвратит число.
        /// </remark>
        private readonly ISort _sort;

        public InfiniteAnalyzer()
        {
            // ББВ := 1 / БМВ(1 / x)
            _analyzer = new InfinitesimalAnalyzer((x) => (1.0 / IterationsAmount((int) Round(1 / x))));
            _sort = new BubbleSort();
        }

        /// <summary>
        /// Табличный вид функции.
        /// 
        /// Определяется с 10 до 10^6. На больших числах выполняется слишком долго.
        /// </summary>
        /// <returns>табличный вид функции</returns>
        public TableItem[] GetTableRepresentation() =>
            _analyzer.GetTableRepresentation(1, 6);

        /// <summary>
        /// Рассчитывает ассимптоту lg-графика БМВ, которая задана вместо ББВ.
        /// </summary>
        /// <returns>асимптота графика</returns>
        public Asymptote GetAsymptote() =>
            _analyzer.GetAsymptote();

        /// <summary>
        /// Рассчитывает коэффициент `C` БМВ, которая задана вместо ББВ.
        /// </summary>
        /// <returns></returns>
        public double GetCoefficient() =>
            _analyzer.GetCoefficient();

        /// <summary>
        /// Считает количество перестановок для массива.
        /// </summary>
        /// <param name="itemsAmount">количество элементов массива</param>
        /// <returns>количество перестановок в массиве</returns>
        private int IterationsAmount(int itemsAmount)
        {
            // пустой массив
            var arr = new int[itemsAmount];
            // заполняем массив наихудшим случаем
            for (int i = 0; i < itemsAmount; i++)
                arr[i] = i;
            // запускаем сортировку и передаём количество перестановок
            return _sort.Sort(arr);
        }
    }
}
