using System;

namespace MathUtilities
{
    /// <summary>
    /// Простой логгер. Работает, если указать Verbose = true
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Показывать ли сообщения в консоли.
        /// </summary>
        /// <remark>
        /// Всегда можно обновить и показывать сообщения, если что.
        /// </remark>
        public bool Verbose { get; set; }

        /// <summary>
        /// Стандартный конструктор.
        /// </summary>
        /// <param name="verbose">показывать ли сообщения в консоли</param>
        public Logger(bool verbose) =>
            Verbose = verbose;

        /// <summary>
        /// В зависимости от Verbose показывать сообщения в консоль.
        /// </summary>
        /// <param name="message">сообщение в консоль</param>
        public void Log(string message)
        {
            if (Verbose) Console.WriteLine(message);
        }
    }
}
