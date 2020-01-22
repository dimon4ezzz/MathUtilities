using static System.Math;
using System;

namespace MathUtilities
{
    /// <summary>
    /// Находит оптимум функции одной переменной при помощи метода золотого сечения.
    /// </summary>
    public class GoldenOptimizer
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
        public GoldenOptimizer(
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
                
                // выбираем лучшую между левой и правой
                triplet = FindGood(triplet);
                _logger.Log($"выбрана тройка {triplet}");
            }

            // всё равно передаём центральную точку
            return triplet.A + (triplet.B - triplet.A) / 2;
        }

        /// <summary>
        /// Рассчитывает область поиска или кидает исключение по превышении количества операций.
        /// </summary>
        /// <returns>область поиска оптимума</returns>
        public GoldenTriplet GetArea()
        {
            // записываем шаг поиска
            var step = _step;
            // создаём тройку с нуля
            var triplet = new GoldenTriplet {A = _start, RightCenter = _start + step};
            _logger.Log($"начальная тройка: {triplet}");
            // пока не найдём удачную тройку, ищем
            while (!IsLucky(triplet))
            {
                // если количество операций превысило максимальное, кинуть ошибку
                if (IsCountDown())
                    throw new ApplicationException("не удалось найти область поиска");

                // двигаем провотиположную A сторону на количество шагов
                triplet.LeftCenter = triplet.B;
                _logger.Log($"новая тройка поиска: {triplet}");
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
        private bool IsLucky(GoldenTriplet triplet) =>
            _lookingForMax
                ? _function(triplet.RightCenter) >= _function(triplet.A) &&
                    _function(triplet.RightCenter) >= _function(triplet.B)
                : _function(triplet.RightCenter) <= _function(triplet.A) &&
                    _function(triplet.RightCenter) <= _function(triplet.B);

        /// <summary>
        /// Узнаёт, не мала ли тройка.
        /// </summary>
        /// <param name="triplet">тройка</param>
        /// <returns>истина, если тройка меньше точности поиска</returns>
        private bool IsTooSmall(GoldenTriplet triplet) =>
            Abs(triplet.B - triplet.A) <= _precision;


        /// <summary>
        /// Выбирает из двух частей тройки лучшую.
        /// </summary>
        /// <remark>
        /// Учитывает настройку, какой параметр ищем: max / min.
        /// </remark>
        /// <param name="old">тройка для рассмотрения</param>
        /// <returns>новая тройка, оказавшаяся лучше</returns>
        private GoldenTriplet FindGood(GoldenTriplet old)
        {
            if (_lookingForMax)
                return _function(old.LeftCenter) > _function(old.RightCenter) ? new GoldenTriplet {A = old.LeftCenter, B = old.B} : new GoldenTriplet {A = old.A, B = old.RightCenter};
            else 
                return _function(old.LeftCenter) < _function(old.RightCenter) ? new GoldenTriplet {A = old.A, B = old.RightCenter} : new GoldenTriplet {A = old.LeftCenter, B = old.B};
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
