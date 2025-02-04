namespace KPBrokers.Submission.Quote.Common.Abstracts
{
    public interface ILoggerService
    {
        /// <summary>
        /// Gets a value indicating whether this instance is debug enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is debug enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsDebugEnabled { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is error enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is error enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsErrorEnabled { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is fatal enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is fatal enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsFatalEnabled { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is information enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is information enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsInfoEnabled { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is trace enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is trace enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsTraceEnabled { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is warn enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is warn enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsWarnEnabled { get; }

        /// <summary>
        ///  Writes the diagnostic message at the Debug level.
        /// </summary>
        /// <param name="message">The message.</param>
        void Debug(string message);
        /// <summary>
        ///  Writes the diagnostic message at the Debug level.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        void Debug(Exception exception, string message = null!);
        /// <summary>
        /// Writes the diagnostic message at the Error level.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error(string message);
        /// <summary>
        ///  Writes the diagnostic message at the Error level.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        void Error(Exception exception, string message = null!);
        /// <summary>
        ///  Writes the diagnostic message at the Fatal level.
        /// </summary>
        /// <param name="message">The message.</param>
        void Fatal(string message);
        /// <summary>
        ///  Writes the diagnostic message at the Fatal level.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        void Fatal(Exception exception, string message = null!);
        /// <summary>
        ///  Writes the diagnostic message at the Information level.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(string message);
        /// <summary>
        ///  Writes the diagnostic message at the Information level.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        void Info(Exception exception, string message = null!);
        /// <summary>
        ///  Writes the diagnostic message at the Tracce level.
        /// </summary>
        /// <param name="message">The message.</param>
        void Trace(string message);
        /// <summary>
        ///  Writes the diagnostic message at the Trace level.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        void Trace(Exception exception, string message = null!);
        /// <summary>
        ///  Writes the diagnostic message at the Warn level.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warn(string message);
        /// <summary>
        ///  Writes the diagnostic message at the Warn level.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        void Warn(Exception exception, string message = null!);
    }
}
