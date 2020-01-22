using static System.Math;
using System;

namespace MathUtilities
{
    /// <summary>
    /// Находит оптимум функции одной переменной при помощи метода дихотомии.
    /// </summary>
    public class DichotomyOptimizer
    {
        /// <summary>
        /// Функция для исследования.
        /// </summary>
        private readonly Func<double, double> _function;

        /// <summary>
        /// Начало поиска.
        /// </summary>
        private readonly double _start;

        /// <summary>
        /// Шаг поиска.
        /// </summary>
        private readonly double _step;

        /// <summary>
        /// Точность поиска. Влияет на размер области поиска.
        /// </summary>
        private readonly double _precision;

        /// <summary>
        /// Максимальное количество операций.
        /// </summary>
        private readonly uint _operationAmount;

        /// <summary>
        /// Смотрим ли максимум или минимум.
        /// </summary>
        private readonly bool _lookingForMax;

        private readonly Logger _logger;

        /// <summary>
        /// Счётчик операций.
        /// </summary>
        private uint counter;

        /// <summary>
        /// Стандартный конструктор анализатора.
        /// </summary>
        /// <param name="function">функция для исследования</param>
        /// <param name="start">точка начала поиска</param>
        /// <param name="step">шаг поиска</param>
        /// <param name="precision">точность поиска</param>
        /// <param name="operationsAmount">максимальное количество операций</param>
        /// <param name="lookingForMax">смотрим ли максимум или минимум</param>
        /// <param name="verbose">писать ли в консоль</param>
        public DichotomyOptimizer(
            Func<double, double> function,
            double start = 0,
            double step = 1E-02,
            double precision = 1E-04,
            uint operationsAmount = 150,
            bool lookingForMax = false,
            bool verbose = false
        ) {
            _function = function;
            _start = start;
            // разворачивает шаг в нужную сторону
            // учитывает max / min
            _step = function(start + step) > function(start - step)
                        ? lookingForMax ? step  : -step
                        : lookingForMax ? -step : step;

            _precision = precision;
            _operationAmount = operationsAmount;
            _lookingForMax = lookingForMax;
            _logger = new Logger(verbose);

            ResetCounter();
        }

        /// <summary>
        /// Рассчитывает оптимальное значение функции.
        /// </summary>
        /// <returns>оптимальное значение функции</returns>
        public double GetOptimal()
        {
            // узнаём область поиска
            var triplet = GetArea();
            _logger.Log($"найдена область поиска: {triplet}");
            // если область поиска маленькая,
            // не продолжать искать
            while (!IsTooSmall(triplet))
            {
                // если количество операций превысило максимальное, выкинуть ошибку
                if (IsCountDown())
                    throw new ApplicationException("не удалось найти оптимального решения");

                // делим область на две части: левую и правую
                var left = new Triplet {A = triplet.A, B = triplet.Center};
                var right = new Triplet {A = triplet.Center, B = triplet.B};
                // выбираем лучшую между левой и правой
                triplet = FindGood(left, right);
                _logger.Log($"выбрана тройка {triplet}");
            }

            return triplet.Center;
        }

        /// <summary>
        /// Рассчитывает область поиска или кидает исключение по превышении количества операций.
        /// </summary>
        /// <returns>область поиска оптимума</returns>
        public Triplet GetArea()
        {
            // записываем шаг поиска
            var step = _step;
            // создаём тройку с нуля
            var triplet = new Triplet {A = _start, B = _start + step * 2 };
            _logger.Log($"начальная тройка: {triplet}");
            // пока не найдём удачную тройку, ищем
            while (!IsLucky(triplet))
            {
                // если количество операций превысило максимальное, кинуть ошибку
                if (IsCountDown())
                    throw new ApplicationException("не удалось найти область поиска");

                // двигаем провотиположную A сторону на количество шагов
                triplet.B += step;
                _logger.Log($"новая тройка поиска: {triplet}");
                // удваиваем шаг
                step *= 2;
            }

            ResetCounter();
            return triplet;
        }

        /// <summary>
        /// Удачна ли тройка для этой функции.
        /// </summary>
        /// <remark>
        /// Учитывает настройку, какой параметр ищем: max / min.
        /// </remark>
        /// <param name="triplet">тройка</param>
        /// <returns>истина, если тройка удачная</returns>
        private bool IsLucky(Triplet triplet) =>
            _lookingForMax
                ? _function(triplet.Center) >= _function(triplet.A) &&
                    _function(triplet.Center) >= _function(triplet.B)
                : _function(triplet.Center) <= _function(triplet.A) &&
                    _function(triplet.Center) <= _function(triplet.B);

        /// <summary>
        /// Узнаёт, не мала ли тройка.
        /// </summary>
        /// <param name="triplet">тройка</param>
        /// <returns>истина, если тройка меньше точности поиска</returns>
        private bool IsTooSmall(Triplet triplet) =>
            Abs(triplet.B - triplet.A) <= _precision;


        /// <summary>
        /// Выбирает из двух троек лучшую.
        /// </summary>
        /// <remark>
        /// Учитывает настройку, какой параметр ищем: max / min.
        /// </remark>
        /// <param name="left">одна тройка</param>
        /// <param name="right">другая тройка</param>
        /// <returns>тройка, которая оказалась лучше</returns>
        private Triplet FindGood(Triplet left, Triplet right)
        {
            // либо оба лучшие, либо оба не лучшие
            if (!(IsLucky(left) ^ IsLucky(right)))
                return
                _lookingForMax
                    ? _function(left.Center) > _function(right.Center) ? left : right
                    : _function(left.Center) < _function(right.Center) ? left : right;
            else if (IsLucky(left))
                return left;
            else if (IsLucky(right))
                return right;
            else
                throw new ApplicationException($"произошла неизвестная ошибка при выборе между {left} и {right}");
        }

        /// <summary>
        /// Увеличивает счётчик на 1 и передаёт, не превысило ли количество операций максимальное.
        /// </summary>
        /// <returns>истина, если количество операций превысило максимальное</returns>
        private bool IsCountDown() =>
            counter++ > _operationAmount;

        /// <summary>
        /// Сбрасывает счётчик.
        /// </summary>
        private void ResetCounter() =>
            counter = 0;
    }
}
