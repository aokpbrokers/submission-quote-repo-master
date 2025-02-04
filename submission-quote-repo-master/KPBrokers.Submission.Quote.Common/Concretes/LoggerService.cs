using NLog;
using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using KPBrokers.Submission.Quote.Common.Abstracts;

namespace KPBrokers.Submission.Quote.Common.Concretes
{
    public class LoggerService : Logger, ILoggerService
    {
        private const string loggerName = "AgentLogger";        
        private readonly IConfiguration _configuration;
        

        public LoggerService(IConfiguration configuration)
        {            
            _configuration = configuration;            
        }

        /// <summary>
        /// Writes the diagnostic message at the Debug level.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        public new void Debug(Exception exception, string message = null!)
        {
            if (!IsDebugEnabled) return;
            var logEvent = GetLogEvent(loggerName, LogLevel.Debug, exception, message);
            Log(typeof(LoggerService), logEvent);
        }

        /// <summary>
        /// Writes the diagnostic message at the Error level.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        public new void Error(Exception exception, string message = null!)
        {
            if (!IsErrorEnabled) return;
            var logEvent = GetLogEvent(loggerName, LogLevel.Error, exception, message);
            Log(typeof(LoggerService), logEvent);

            // Log to NLog
            var logger = LogManager.GetLogger(loggerName);
            if (!logger.IsErrorEnabled) return;

            logger.Error(exception, message);
        }

        /// <summary>
        /// Writes the diagnostic message at the Fatal level.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        public new void Fatal(Exception exception, string message = null!)
        {
            if (!IsFatalEnabled) return;
            var logEvent = GetLogEvent(loggerName, LogLevel.Fatal, exception, message);
            Log(typeof(LoggerService), logEvent);
        }

        /// <summary>
        /// Writes the diagnostic message at the Information level.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        public new void Info(Exception exception, string message = null!)
        {
            if (!IsInfoEnabled) return;
            var logEvent = GetLogEvent(loggerName, LogLevel.Info, exception, message);
            Log(typeof(LoggerService), logEvent);
        }

        /// <summary>
        /// Writes the diagnostic message at the Trace level.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        public new void Trace(Exception exception, string message = null!)
        {
            if (!IsTraceEnabled) return;
            var logEvent = GetLogEvent(loggerName, LogLevel.Trace, exception, message);
            Log(typeof(LoggerService), logEvent);
        }

        /// <summary>
        /// Writes the diagnostic message at the Warn level.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        public new void Warn(Exception exception, string message = null!)
        {
            if (!IsWarnEnabled) return;
            var logEvent = GetLogEvent(loggerName, LogLevel.Warn, exception, message);
            Log(typeof(LoggerService), logEvent);
        }

        /// <summary>
        /// Gets the log event.
        /// </summary>
        /// <param name="loggerName">Name of the logger.</param>
        /// <param name="level">The level.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        private LogEventInfo GetLogEvent(string loggerName, LogLevel level, Exception exception, string message)
        {
            string assemblyProp = string.Empty;
            string classProp = string.Empty;
            string methodProp = string.Empty;
            string messageProp = string.Empty;
            string innerMessageProp = string.Empty;

            if (message == null)
            {
                message = string.Empty;
            }

            var logEvent = LogEventInfo.Create(level, loggerName, exception, null, message);

            if (exception != null)
            {
                assemblyProp = exception.Source!;
                classProp = exception.TargetSite!.DeclaringType!.FullName!;
                methodProp = exception.TargetSite.Name;
                messageProp = exception.Message;

                if (exception.InnerException != null)
                {
                    innerMessageProp = exception.InnerException.Message;
                }
            }

            logEvent.Properties["error-source"] = assemblyProp;
            logEvent.Properties["error-class"] = classProp;
            logEvent.Properties["error-method"] = methodProp;
            logEvent.Properties["error-message"] = messageProp;
            logEvent.Properties["inner-error-message"] = innerMessageProp;

            return logEvent;
        }
    }
}
