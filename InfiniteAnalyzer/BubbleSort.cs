namespace MathUtilities
{
    /// <summary>
    /// Реализация пузырьковой сортировки.
    /// </summary>
    public class BubbleSort : ISort
    {
        /// <summary>
        /// Собственно сама сортировка.
        /// </summary>
        /// <remark>
        /// Атрибуция: https://ru.wikibooks.org/wiki/Примеры_реализации_сортировки_пузырьком
        /// </remark>
        /// <param name="arr">массив, который нужно отсортировать</param>
        /// <returns>количество перестановок</returns>
        public int Sort(int[] arr)
        {
            // изначально перестановок нет
            int counter = 0;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (arr[j+1] > arr[j])
                    {
                        swap(ref arr[j], ref arr[j+1]);
                        counter++;
                    }                   
                }
            }
            // передать количество перестановок
            return counter;
        }

        /// <summary>
        /// Типичная перестановка значений переменных.
        /// </summary>
        /// <param name="a">первая переменная</param>
        /// <param name="b">вторая переменная</param>
        private static void swap(ref int a, ref int b)
        {
            b += a;
            a -= b;
            b -= a;
        }
    }
}