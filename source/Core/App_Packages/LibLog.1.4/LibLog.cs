//===============================================================================
// LibLog
//
// https://github.com/damianh/LibLog
//===============================================================================
// Copyright © 2011-2014 Damian Hickey.  All rights reserved.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//===============================================================================

namespace Thinktecture.IdentityServer.Core.Logging
{
    using Thinktecture.IdentityServer.Core.Logging.LogProviders;
    using System;
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// Simple interface that represent a logger.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Log a message the specified log level.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="messageFunc">The message function.</param>
        /// <remarks>
        /// Note to implementors: the message func should not be called if the loglevel is not enabled
        /// so as not to incur perfomance penalties.
        /// </remarks>
        bool Log(LogLevel logLevel, Func<string> messageFunc);

        /// <summary>
        /// Log a message and exception at the specified log level.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="logLevel">The log level.</param>
        /// <param name="messageFunc">The message function.</param>
        /// <param name="exception">The exception.</param>
        /// <remarks>
        /// Note to implementors: the message func should not be called if the loglevel is not enabled
        /// so as not to incur perfomance penalties.
        /// </remarks>
        void Log<TException>(LogLevel logLevel, Func<string> messageFunc, TException exception) where TException : Exception;
    }

    /// <summary>
    /// The log level.
    /// </summary>
    public enum LogLevel
    {
        Trace,
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }

    public static class LogExtensions
    {
        public static bool IsDebugEnabled(this ILog logger)
        {
            GuardAgainstNullLogger(logger);
            return logger.Log(LogLevel.Debug, null);
        }

        public static bool IsErrorEnabled(this ILog logger)
        {
            GuardAgainstNullLogger(logger);
            return logger.Log(LogLevel.Error, null);
        }

        public static bool IsFatalEnabled(this ILog logger)
        {
            GuardAgainstNullLogger(logger);
            return logger.Log(LogLevel.Fatal, null);
        }

        public static bool IsInfoEnabled(this ILog logger)
        {
            GuardAgainstNullLogger(logger);
            return logger.Log(LogLevel.Info, null);
        }

        public static bool IsTraceEnabled(this ILog logger)
        {
            GuardAgainstNullLogger(logger);
            return logger.Log(LogLevel.Trace, null);
        }

        public static bool IsWarnEnabled(this ILog logger)
        {
            GuardAgainstNullLogger(logger);
            return logger.Log(LogLevel.Warn, null);
        }

        public static void Debug(this ILog logger, Func<string> messageFunc)
        {
            GuardAgainstNullLogger(logger);
            logger.Log(LogLevel.Debug, messageFunc);
        }

        public static void Debug(this ILog logger, string message)
        {
            if (logger.IsDebugEnabled())
            {
                logger.Log(LogLevel.Debug, message.AsFunc());
            }
        }

        public static void DebugFormat(this ILog logger, string message, params object[] args)
        {
            if (logger.IsDebugEnabled())
            {
                logger.LogFormat(LogLevel.Debug, message, args);
            }
        }

        public static void DebugException(this ILog logger, string message, Exception exception)
        {
            if (logger.IsDebugEnabled())
            {
                logger.Log(LogLevel.Debug, message.AsFunc(), exception);
            }
        }

        public static void Error(this ILog logger, Func<string> messageFunc)
        {
            logger.Log(LogLevel.Error, messageFunc);
        }

        public static void Error(this ILog logger, string message)
        {
            if (logger.IsErrorEnabled())
            {
                logger.Log(LogLevel.Error, message.AsFunc());
            }
        }

        public static void ErrorFormat(this ILog logger, string message, params object[] args)
        {
            if (logger.IsErrorEnabled())
            {
                logger.LogFormat(LogLevel.Error, message, args);
            }
        }

        public static void ErrorException(this ILog logger, string message, Exception exception)
        {
            if (logger.IsErrorEnabled())
            {
                logger.Log(LogLevel.Error, message.AsFunc(), exception);
            }
        }

        public static void Fatal(this ILog logger, Func<string> messageFunc)
        {
            logger.Log(LogLevel.Fatal, messageFunc);
        }

        public static void Fatal(this ILog logger, string message)
        {
            if (logger.IsFatalEnabled())
            {
                logger.Log(LogLevel.Fatal, message.AsFunc());
            }
        }

        public static void FatalFormat(this ILog logger, string message, params object[] args)
        {
            if (logger.IsFatalEnabled())
            {
                logger.LogFormat(LogLevel.Fatal, message, args);
            }
        }

        public static void FatalException(this ILog logger, string message, Exception exception)
        {
            if (logger.IsFatalEnabled())
            {
                logger.Log(LogLevel.Fatal, message.AsFunc(), exception);
            }
        }

        public static void Info(this ILog logger, Func<string> messageFunc)
        {
            GuardAgainstNullLogger(logger);
            logger.Log(LogLevel.Info, messageFunc);
        }

        public static void Info(this ILog logger, string message)
        {
            if (logger.IsInfoEnabled())
            {
                logger.Log(LogLevel.Info, message.AsFunc());
            }
        }

        public static void InfoFormat(this ILog logger, string message, params object[] args)
        {
            if (logger.IsInfoEnabled())
            {
                logger.LogFormat(LogLevel.Info, message, args);
            }
        }

        public static void InfoException(this ILog logger, string message, Exception exception)
        {
            if (logger.IsInfoEnabled())
            {
                logger.Log(LogLevel.Info, message.AsFunc(), exception);
            }
        }

        public static void Trace(this ILog logger, Func<string> messageFunc)
        {
            GuardAgainstNullLogger(logger);
            logger.Log(LogLevel.Trace, messageFunc);
        }

        public static void Trace(this ILog logger, string message)
        {
            if (logger.IsTraceEnabled())
            {
                logger.Log(LogLevel.Trace, message.AsFunc());
            }
        }

        public static void TraceFormat(this ILog logger, string message, params object[] args)
        {
            if (logger.IsTraceEnabled())
            {
                logger.LogFormat(LogLevel.Trace, message, args);
            }
        }

        public static void TraceException(this ILog logger, string message, Exception exception)
        {
            if (logger.IsTraceEnabled())
            {
                logger.Log(LogLevel.Trace, message.AsFunc(), exception);
            }
        }

        public static void Warn(this ILog logger, Func<string> messageFunc)
        {
            GuardAgainstNullLogger(logger);
            logger.Log(LogLevel.Warn, messageFunc);
        }

        public static void Warn(this ILog logger, string message)
        {
            if (logger.IsWarnEnabled())
            {
                logger.Log(LogLevel.Warn, message.AsFunc());
            }
        }

        public static void WarnFormat(this ILog logger, string message, params object[] args)
        {
            if (logger.IsWarnEnabled())
            {
                logger.LogFormat(LogLevel.Warn, message, args);
            }
        }

        public static void WarnException(this ILog logger, string message, Exception exception)
        {
            if (logger.IsWarnEnabled())
            {
                logger.Log(LogLevel.Warn, message.AsFunc(), exception);
            }
        }

        private static void GuardAgainstNullLogger(ILog logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
        }

        private static void LogFormat(this ILog logger, LogLevel logLevel, string message, params object[] args)
        {
            var result = string.Format(CultureInfo.InvariantCulture, message, args);
            logger.Log(logLevel, result.AsFunc());
        }

        // Avoid the closure allocation, see https://gist.github.com/AArnott/d285feef75c18f6ecd2b
        private static Func<T> AsFunc<T>(this T value) where T : class
        {
            return value.Return;
        }

        private static T Return<T>(this T value)
        {
            return value;
        }
    }

    /// <summary>
    /// Represents a way to get a <see cref="ILog"/>
    /// </summary>
    public interface ILogProvider
    {
        ILog GetLogger(string name);
    }


    /// <summary>
    /// Provides a mechanism to create instances of <see cref="ILog" /> objects.
    /// </summary>
    public static class LogProvider
    {
        private static ILogProvider _currentLogProvider;

        /// <summary>
        /// Gets a logger for the specified type.
        /// </summary>
        /// <typeparam name="T">The type whose name will be used for the logger.</typeparam>
        /// <returns>An instance of <see cref="ILog"/></returns>
        public static ILog For<T>()
        {
            return GetLogger(typeof(T));
        }

        /// <summary>
        /// Gets a logger for the current class.
        /// </summary>
        /// <returns>An instance of <see cref="ILog"/></returns>
        public static ILog GetCurrentClassLogger()
        {
            var stackFrame = new StackFrame(1, false);
            return GetLogger(stackFrame.GetMethod().DeclaringType);
        }

        /// <summary>
        /// Gets a logger for the specified type.
        /// </summary>
        /// <param name="type">The type whose name will be used for the logger.</param>
        /// <returns>An instance of <see cref="ILog"/></returns>
        public static ILog GetLogger(Type type)
        {
            return GetLogger(type.FullName);
        }

        /// <summary>
        /// Gets a logger with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>An instance of <see cref="ILog"/></returns>
        public static ILog GetLogger(string name)
        {
            ILogProvider logProvider = _currentLogProvider ?? ResolveLogProvider();
            return logProvider == null ? new NoOpLogger() : (ILog)new LoggerExecutionWrapper(logProvider.GetLogger(name));
        }

        /// <summary>
        /// Sets the current log provider.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        public static void SetCurrentLogProvider(ILogProvider logProvider)
        {
            _currentLogProvider = logProvider;
        }

        private static ILogProvider ResolveLogProvider()
        {
            try
            {
                if (NLogLogProvider.IsLoggerAvailable())
                {
                    return new NLogLogProvider();
                }
                if (Log4NetLogProvider.IsLoggerAvailable())
                {
                    return new Log4NetLogProvider();
                }
                if (EntLibLogProvider.IsLoggerAvailable())
                {
                    return new EntLibLogProvider();
                }
                if (SerilogLogProvider.IsLoggerAvailable())
                {
                    return new SerilogLogProvider();
                }
                if (LoupeLogProvider.IsLoggerAvailable())
                {
                    return new LoupeLogProvider();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Exception occured resolving a log provider. Logging for this assembly {0} is disabled. {1}",
                    typeof(LogProvider).Assembly.FullName,
                    ex);
            }
            return null;
        }

        public class NoOpLogger : ILog
        {
            public bool Log(LogLevel logLevel, Func<string> messageFunc)
            {
                return false;
            }

            public void Log<TException>(LogLevel logLevel, Func<string> messageFunc, TException exception)
                where TException : Exception
            { }
        }
    }

    public class LoggerExecutionWrapper : ILog
    {
        private readonly ILog _logger;
        public const string FailedToGenerateLogMessage = "Failed to generate log message";

        public ILog WrappedLogger
        {
            get { return _logger; }
        }

        public LoggerExecutionWrapper(ILog logger)
        {
            _logger = logger;
        }

        public bool Log(LogLevel logLevel, Func<string> messageFunc)
        {
            if (messageFunc == null)
            {
                return _logger.Log(logLevel, null);
            }

            Func<string> wrappedMessageFunc = () =>
            {
                try
                {
                    return messageFunc();
                }
                catch (Exception ex)
                {
                    Log(LogLevel.Error, () => FailedToGenerateLogMessage, ex);
                }
                return null;
            };
            return _logger.Log(logLevel, wrappedMessageFunc);
        }

        public void Log<TException>(LogLevel logLevel, Func<string> messageFunc, TException exception) where TException : Exception
        {
            Func<string> wrappedMessageFunc = () =>
            {
                try
                {
                    return messageFunc();
                }
                catch (Exception ex)
                {
                    Log(LogLevel.Error, () => FailedToGenerateLogMessage, ex);
                }
                return null;
            };
            _logger.Log(logLevel, wrappedMessageFunc, exception);
        }
    }
}

namespace Thinktecture.IdentityServer.Core.Logging.LogProviders
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading;

    public class NLogLogProvider : ILogProvider
    {
        private readonly Func<string, object> _getLoggerByNameDelegate;
        private static bool _providerIsAvailableOverride = true;

        public NLogLogProvider()
        {
            if (!IsLoggerAvailable())
            {
                throw new InvalidOperationException("NLog.LogManager not found");
            }
            _getLoggerByNameDelegate = GetGetLoggerMethodCall();
        }

        public static bool ProviderIsAvailableOverride
        {
            get { return _providerIsAvailableOverride; }
            set { _providerIsAvailableOverride = value; }
        }

        public ILog GetLogger(string name)
        {
            return new NLogLogger(_getLoggerByNameDelegate(name));
        }

        public static bool IsLoggerAvailable()
        {
            return ProviderIsAvailableOverride && GetLogManagerType() != null;
        }

        private static Type GetLogManagerType()
        {
            return Type.GetType("NLog.LogManager, nlog");
        }

        private static Func<string, object> GetGetLoggerMethodCall()
        {
            Type logManagerType = GetLogManagerType();
            MethodInfo method = logManagerType.GetMethod("GetLogger", new[] { typeof(string) });
            ParameterExpression nameParam = Expression.Parameter(typeof(string), "name");
            MethodCallExpression methodCall = Expression.Call(null, method, new Expression[] { nameParam });
            return Expression.Lambda<Func<string, object>>(methodCall, new[] { nameParam }).Compile();
        }

        public class NLogLogger : ILog
        {
            private readonly dynamic _logger;

            internal NLogLogger(dynamic logger)
            {
                _logger = logger;
            }

            public bool Log(LogLevel logLevel, Func<string> messageFunc)
            {
                if (messageFunc == null)
                {
                    return IsLogLevelEnable(logLevel);
                }
                switch (logLevel)
                {
                    case LogLevel.Debug:
                        if (_logger.IsDebugEnabled)
                        {
                            _logger.Debug(messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Info:
                        if (_logger.IsInfoEnabled)
                        {
                            _logger.Info(messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Warn:
                        if (_logger.IsWarnEnabled)
                        {
                            _logger.Warn(messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Error:
                        if (_logger.IsErrorEnabled)
                        {
                            _logger.Error(messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Fatal:
                        if (_logger.IsFatalEnabled)
                        {
                            _logger.Fatal(messageFunc());
                            return true;
                        }
                        break;
                    default:
                        if (_logger.IsTraceEnabled)
                        {
                            _logger.Trace(messageFunc());
                            return true;
                        }
                        break;
                }
                return false;
            }

            public void Log<TException>(LogLevel logLevel, Func<string> messageFunc, TException exception)
                where TException : Exception
            {
                switch (logLevel)
                {
                    case LogLevel.Debug:
                        if (_logger.IsDebugEnabled)
                        {
                            _logger.DebugException(messageFunc(), exception);
                        }
                        break;
                    case LogLevel.Info:
                        if (_logger.IsInfoEnabled)
                        {
                            _logger.InfoException(messageFunc(), exception);
                        }
                        break;
                    case LogLevel.Warn:
                        if (_logger.IsWarnEnabled)
                        {
                            _logger.WarnException(messageFunc(), exception);
                        }
                        break;
                    case LogLevel.Error:
                        if (_logger.IsErrorEnabled)
                        {
                            _logger.ErrorException(messageFunc(), exception);
                        }
                        break;
                    case LogLevel.Fatal:
                        if (_logger.IsFatalEnabled)
                        {
                            _logger.FatalException(messageFunc(), exception);
                        }
                        break;
                    default:
                        if (_logger.IsTraceEnabled)
                        {
                            _logger.TraceException(messageFunc(), exception);
                        }
                        break;
                }
            }

            private bool IsLogLevelEnable(LogLevel logLevel)
            {
                switch (logLevel)
                {
                    case LogLevel.Debug:
                        return _logger.IsDebugEnabled;
                    case LogLevel.Info:
                        return _logger.IsInfoEnabled;
                    case LogLevel.Warn:
                        return _logger.IsWarnEnabled;
                    case LogLevel.Error:
                        return _logger.IsErrorEnabled;
                    case LogLevel.Fatal:
                        return _logger.IsFatalEnabled;
                    default:
                        return _logger.IsTraceEnabled;
                }
            }
        }
    }

    public class Log4NetLogProvider : ILogProvider
    {
        private readonly Func<string, object> _getLoggerByNameDelegate;
        private static bool _providerIsAvailableOverride = true;

        public Log4NetLogProvider()
        {
            if (!IsLoggerAvailable())
            {
                throw new InvalidOperationException("log4net.LogManager not found");
            }
            _getLoggerByNameDelegate = GetGetLoggerMethodCall();
        }

        public static bool ProviderIsAvailableOverride
        {
            get { return _providerIsAvailableOverride; }
            set { _providerIsAvailableOverride = value; }
        }

        public ILog GetLogger(string name)
        {
            return new Log4NetLogger(_getLoggerByNameDelegate(name));
        }

        public static bool IsLoggerAvailable()
        {
            return ProviderIsAvailableOverride && GetLogManagerType() != null;
        }

        private static Type GetLogManagerType()
        {
            return Type.GetType("log4net.LogManager, log4net");
        }

        private static Func<string, object> GetGetLoggerMethodCall()
        {
            Type logManagerType = GetLogManagerType();
            MethodInfo method = logManagerType.GetMethod("GetLogger", new[] { typeof(string) });
            ParameterExpression nameParam = Expression.Parameter(typeof(string), "name");
            MethodCallExpression methodCall = Expression.Call(null, method, new Expression[] { nameParam });
            return Expression.Lambda<Func<string, object>>(methodCall, new[] { nameParam }).Compile();
        }

        public class Log4NetLogger : ILog
        {
            private readonly dynamic _logger;

            internal Log4NetLogger(dynamic logger)
            {
                _logger = logger;
            }

            public bool Log(LogLevel logLevel, Func<string> messageFunc)
            {
                if (messageFunc == null)
                {
                    return IsLogLevelEnable(logLevel);
                }
                switch (logLevel)
                {
                    case LogLevel.Info:
                        if (_logger.IsInfoEnabled)
                        {
                            _logger.Info(messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Warn:
                        if (_logger.IsWarnEnabled)
                        {
                            _logger.Warn(messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Error:
                        if (_logger.IsErrorEnabled)
                        {
                            _logger.Error(messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Fatal:
                        if (_logger.IsFatalEnabled)
                        {
                            _logger.Fatal(messageFunc());
                            return true;
                        }
                        break;
                    default:
                        if (_logger.IsDebugEnabled)
                        {
                            _logger.Debug(messageFunc()); // Log4Net doesn't have a 'Trace' level, so all Trace messages are written as 'Debug'
                            return true;
                        }
                        break;
                }
                return false;
            }

            public void Log<TException>(LogLevel logLevel, Func<string> messageFunc, TException exception)
                where TException : Exception
            {
                switch (logLevel)
                {
                    case LogLevel.Info:
                        if (_logger.IsDebugEnabled)
                        {
                            _logger.Info(messageFunc(), exception);
                        }
                        break;
                    case LogLevel.Warn:
                        if (_logger.IsWarnEnabled)
                        {
                            _logger.Warn(messageFunc(), exception);
                        }
                        break;
                    case LogLevel.Error:
                        if (_logger.IsErrorEnabled)
                        {
                            _logger.Error(messageFunc(), exception);
                        }
                        break;
                    case LogLevel.Fatal:
                        if (_logger.IsFatalEnabled)
                        {
                            _logger.Fatal(messageFunc(), exception);
                        }
                        break;
                    default:
                        if (_logger.IsDebugEnabled)
                        {
                            _logger.Debug(messageFunc(), exception);
                        }
                        break;
                }
            }

            private bool IsLogLevelEnable(LogLevel logLevel)
            {
                switch (logLevel)
                {
                    case LogLevel.Debug:
                        return _logger.IsDebugEnabled;
                    case LogLevel.Info:
                        return _logger.IsInfoEnabled;
                    case LogLevel.Warn:
                        return _logger.IsWarnEnabled;
                    case LogLevel.Error:
                        return _logger.IsErrorEnabled;
                    case LogLevel.Fatal:
                        return _logger.IsFatalEnabled;
                    default:
                        return _logger.IsDebugEnabled;
                }
            }
        }
    }

    public class EntLibLogProvider : ILogProvider
    {
        private const string TypeTemplate = "Microsoft.Practices.EnterpriseLibrary.Logging.{0}, Microsoft.Practices.EnterpriseLibrary.Logging";
        private static bool _providerIsAvailableOverride = true;
        private static readonly Type LogEntryType;
        private static readonly Type LoggerType;
        private static readonly Action<string, string, TraceEventType> WriteLogEntry;
        private static Func<string, TraceEventType, bool> ShouldLogEntry;

        static EntLibLogProvider()
        {
            LogEntryType = Type.GetType(string.Format(TypeTemplate, "LogEntry"));
            LoggerType = Type.GetType(string.Format(TypeTemplate, "Logger"));
            if (LogEntryType == null || LoggerType == null)
            {
                return;
            }
            WriteLogEntry = GetWriteLogEntry();
            ShouldLogEntry = GetShouldLogEntry();
        }

        public EntLibLogProvider()
        {
            if (!IsLoggerAvailable())
            {
                throw new InvalidOperationException("Microsoft.Practices.EnterpriseLibrary.Logging.Logger not found");
            }
        }

        public static bool ProviderIsAvailableOverride
        {
            get { return _providerIsAvailableOverride; }
            set { _providerIsAvailableOverride = value; }
        }

        public ILog GetLogger(string name)
        {
            return new EntLibLogger(name, WriteLogEntry, ShouldLogEntry);
        }

        public static bool IsLoggerAvailable()
        {
            return ProviderIsAvailableOverride && LogEntryType != null;
        }

        private static Action<string, string, TraceEventType> GetWriteLogEntry()
        {
            // new LogEntry(...)
            var logNameParameter = Expression.Parameter(typeof(string), "logName");
            var messageParameter = Expression.Parameter(typeof(string), "message");
            var severityParameter = Expression.Parameter(typeof(TraceEventType), "severity");

            MemberInitExpression memberInit = GetWriteLogExpression(messageParameter, severityParameter, logNameParameter);

            //Logger.Write(new LogEntry(....));
            MethodInfo writeLogEntryMethod = LoggerType.GetMethod("Write", new[] { LogEntryType });
            var writeLogEntryExpression = Expression.Call(writeLogEntryMethod, memberInit);

            return Expression.Lambda<Action<string, string, TraceEventType>>(
                writeLogEntryExpression,
                logNameParameter,
                messageParameter,
                severityParameter).Compile();
        }

        private static Func<string, TraceEventType, bool> GetShouldLogEntry()
        {
            // new LogEntry(...)
            var logNameParameter = Expression.Parameter(typeof(string), "logName");
            var severityParameter = Expression.Parameter(typeof(TraceEventType), "severity");

            MemberInitExpression memberInit = GetWriteLogExpression(Expression.Constant("***dummy***"), severityParameter, logNameParameter);

            //Logger.Write(new LogEntry(....));
            MethodInfo writeLogEntryMethod = LoggerType.GetMethod("ShouldLog", new[] { LogEntryType });
            var writeLogEntryExpression = Expression.Call(writeLogEntryMethod, memberInit);

            return Expression.Lambda<Func<string, TraceEventType, bool>>(
                writeLogEntryExpression,
                logNameParameter,
                severityParameter).Compile();
        }

        private static MemberInitExpression GetWriteLogExpression(Expression message,
            ParameterExpression severityParameter, ParameterExpression logNameParameter)
        {
            var entryType = LogEntryType;
            MemberInitExpression memberInit = Expression.MemberInit(Expression.New(entryType), new MemberBinding[]
            {
                Expression.Bind(entryType.GetProperty("Message"), message),
                Expression.Bind(entryType.GetProperty("Severity"), severityParameter),
                Expression.Bind(entryType.GetProperty("TimeStamp"),
                    Expression.Property(null, typeof (DateTime).GetProperty("UtcNow"))),
                Expression.Bind(entryType.GetProperty("Categories"),
                    Expression.ListInit(
                        Expression.New(typeof (List<string>)),
                        typeof (List<string>).GetMethod("Add", new[] {typeof (string)}),
                        logNameParameter))
            });
            return memberInit;
        }

        public class EntLibLogger : ILog
        {
            private readonly string _loggerName;
            private readonly Action<string, string, TraceEventType> _writeLog;
            private readonly Func<string, TraceEventType, bool> _shouldLog;

            internal EntLibLogger(string loggerName, Action<string, string, TraceEventType> writeLog, Func<string, TraceEventType, bool> shouldLog)
            {
                _loggerName = loggerName;
                _writeLog = writeLog;
                _shouldLog = shouldLog;
            }

            public bool Log(LogLevel logLevel, Func<string> messageFunc)
            {
                var severity = MapSeverity(logLevel);
                if (messageFunc == null)
                {
                    return _shouldLog(_loggerName, severity);
                }
                _writeLog(_loggerName, messageFunc(), severity);
                return true;
            }

            public void Log<TException>(LogLevel logLevel, Func<string> messageFunc, TException exception)
                where TException : Exception
            {
                var severity = MapSeverity(logLevel);
                var message = messageFunc() + Environment.NewLine + exception;
                _writeLog(_loggerName, message, severity);
            }

            private static TraceEventType MapSeverity(LogLevel logLevel)
            {
                switch (logLevel)
                {
                    case LogLevel.Fatal:
                        return TraceEventType.Critical;
                    case LogLevel.Error:
                        return TraceEventType.Error;
                    case LogLevel.Warn:
                        return TraceEventType.Warning;
                    case LogLevel.Info:
                        return TraceEventType.Information;
                    default:
                        return TraceEventType.Verbose;
                }
            }
        }
    }

    public class SerilogLogProvider : ILogProvider
    {
        private readonly Func<string, object> _getLoggerByNameDelegate;
        private static bool _providerIsAvailableOverride = true;

        public SerilogLogProvider()
        {
            if (!IsLoggerAvailable())
            {
                throw new InvalidOperationException("Serilog.Log not found");
            }
            _getLoggerByNameDelegate = GetForContextMethodCall();
        }

        public static bool ProviderIsAvailableOverride
        {
            get { return _providerIsAvailableOverride; }
            set { _providerIsAvailableOverride = value; }
        }

        public ILog GetLogger(string name)
        {
            return new SerilogLogger(_getLoggerByNameDelegate(name));
        }

        public static bool IsLoggerAvailable()
        {
            return ProviderIsAvailableOverride && GetLogManagerType() != null;
        }

        private static Type GetLogManagerType()
        {
            return Type.GetType("Serilog.Log, Serilog");
        }

        private static Func<string, object> GetForContextMethodCall()
        {
            Type logManagerType = GetLogManagerType();
            MethodInfo method = logManagerType.GetMethod("ForContext", new[] { typeof(string), typeof(object), typeof(bool) });
            ParameterExpression propertyNameParam = Expression.Parameter(typeof(string), "propertyName");
            ParameterExpression valueParam = Expression.Parameter(typeof(object), "value");
            ParameterExpression destructureObjectsParam = Expression.Parameter(typeof(bool), "destructureObjects");
            MethodCallExpression methodCall = Expression.Call(null, method, new Expression[]
            {
                propertyNameParam, 
                valueParam,
                destructureObjectsParam
            });
            var func = Expression.Lambda<Func<string, object, bool, object>>(methodCall, new[]
            {
                propertyNameParam,
                valueParam,
                destructureObjectsParam
            }).Compile();
            return name => func("Name", name, false);
        }

        public class SerilogLogger : ILog
        {
            private readonly object _logger;
            private static readonly object DebugLevel;
            private static readonly object ErrorLevel;
            private static readonly object FatalLevel;
            private static readonly object InformationLevel;
            private static readonly object VerboseLevel;
            private static readonly object WarningLevel;
            private static readonly Func<object, object, bool> IsEnabled;
            private static readonly Action<object, object, string> Write;
            private static readonly Action<object, object, Exception, string> WriteException;

            static SerilogLogger()
            {
                var logEventTypeType = Type.GetType("Serilog.Events.LogEventLevel, Serilog");
                DebugLevel = Enum.Parse(logEventTypeType, "Debug");
                ErrorLevel = Enum.Parse(logEventTypeType, "Error");
                FatalLevel = Enum.Parse(logEventTypeType, "Fatal");
                InformationLevel = Enum.Parse(logEventTypeType, "Information");
                VerboseLevel = Enum.Parse(logEventTypeType, "Verbose");
                WarningLevel = Enum.Parse(logEventTypeType, "Warning");

                // Func<object, object, bool> isEnabled = (logger, level) => { return ((SeriLog.ILogger)logger).IsEnabled(level); }
                var loggerType = Type.GetType("Serilog.ILogger, Serilog");
                MethodInfo isEnabledMethodInfo = loggerType.GetMethod("IsEnabled");
                ParameterExpression instanceParam = Expression.Parameter(typeof(object));
                UnaryExpression instanceCast = Expression.Convert(instanceParam, loggerType);
                ParameterExpression levelParam = Expression.Parameter(typeof(object));
                UnaryExpression levelCast = Expression.Convert(levelParam, logEventTypeType);
                MethodCallExpression isEnabledMethodCall = Expression.Call(instanceCast, isEnabledMethodInfo, levelCast);
                IsEnabled = Expression.Lambda<Func<object, object, bool>>(isEnabledMethodCall, new[]
                {
                    instanceParam,
                    levelParam
                }).Compile();

                // Action<object, object, string> Write =
                // (logger, level, message) => { ((SeriLog.ILoggerILogger)logger).Write(level, message, new object[]); }
                MethodInfo writeMethodInfo = loggerType.GetMethod("Write", new[] { logEventTypeType, typeof(string), typeof(object[]) });
                ParameterExpression messageParam = Expression.Parameter(typeof(string));
                ConstantExpression propertyValuesParam = Expression.Constant(new object[0]);
                MethodCallExpression writeMethodExp = Expression.Call(instanceCast, writeMethodInfo, levelCast, messageParam, propertyValuesParam);
                Write = Expression.Lambda<Action<object, object, string>>(writeMethodExp, new[]
                {
                    instanceParam,
                    levelParam,
                    messageParam
                }).Compile();

                // Action<object, object, string, Exception> WriteException =
                // (logger, level, exception, message) => { ((ILogger)logger).Write(level, exception, message, new object[]); }
                MethodInfo writeExceptionMethodInfo = loggerType.GetMethod("Write", new[]
                {
                    logEventTypeType,
                    typeof(Exception), 
                    typeof(string),
                    typeof(object[])
                });
                ParameterExpression exceptionParam = Expression.Parameter(typeof(Exception));
                writeMethodExp = Expression.Call(
                    instanceCast,
                    writeExceptionMethodInfo,
                    levelCast,
                    exceptionParam,
                    messageParam,
                    propertyValuesParam);
                WriteException = Expression.Lambda<Action<object, object, Exception, string>>(writeMethodExp, new[]
                {
                    instanceParam,
                    levelParam,
                    exceptionParam,
                    messageParam,
                }).Compile();
            }

            internal SerilogLogger(object logger)
            {
                _logger = logger;
            }

            public bool Log(LogLevel logLevel, Func<string> messageFunc)
            {
                if (messageFunc == null)
                {
                    return IsEnabled(_logger, DebugLevel);
                }

                switch (logLevel)
                {
                    case LogLevel.Debug:
                        if (IsEnabled(_logger, DebugLevel))
                        {
                            Write(_logger, DebugLevel, messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Info:
                        if (IsEnabled(_logger, InformationLevel))
                        {
                            Write(_logger, InformationLevel, messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Warn:
                        if (IsEnabled(_logger, WarningLevel))
                        {
                            Write(_logger, WarningLevel, messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Error:
                        if (IsEnabled(_logger, ErrorLevel))
                        {
                            Write(_logger, ErrorLevel, messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Fatal:
                        if (IsEnabled(_logger, FatalLevel))
                        {
                            Write(_logger, FatalLevel, messageFunc());
                            return true;
                        }
                        break;
                    default:
                        if (IsEnabled(_logger, VerboseLevel))
                        {
                            Write(_logger, VerboseLevel, messageFunc());
                            return true;
                        }
                        break;
                }
                return false;
            }

            public void Log<TException>(LogLevel logLevel, Func<string> messageFunc, TException exception)
                where TException : Exception
            {
                switch (logLevel)
                {
                    case LogLevel.Debug:
                        if (IsEnabled(_logger, DebugLevel))
                        {
                            WriteException(_logger, DebugLevel, exception, messageFunc());
                        }
                        break;
                    case LogLevel.Info:
                        if (IsEnabled(_logger, InformationLevel))
                        {
                            WriteException(_logger, InformationLevel, exception, messageFunc());
                        }
                        break;
                    case LogLevel.Warn:
                        if (IsEnabled(_logger, WarningLevel))
                        {
                            WriteException(_logger, WarningLevel, exception, messageFunc());
                        }
                        break;
                    case LogLevel.Error:
                        if (IsEnabled(_logger, ErrorLevel))
                        {
                            WriteException(_logger, ErrorLevel, exception, messageFunc());
                        }
                        break;
                    case LogLevel.Fatal:
                        if (IsEnabled(_logger, FatalLevel))
                        {
                            WriteException(_logger, FatalLevel, exception, messageFunc());
                        }
                        break;
                    default:
                        if (IsEnabled(_logger, VerboseLevel))
                        {
                            WriteException(_logger, VerboseLevel, exception, messageFunc());
                        }
                        break;
                }
            }
        }
    }

    public class LoupeLogProvider : ILogProvider
    {
        private static bool _providerIsAvailableOverride = true;
        private readonly WriteDelegate _logWriteDelegate;

        public LoupeLogProvider()
        {
            if (!IsLoggerAvailable())
            {
                throw new InvalidOperationException("Gibraltar.Agent.Log (Loupe) not found");
            }

            _logWriteDelegate = GetLogWriteDelegate();
        }

        public static bool ProviderIsAvailableOverride
        {
            get { return _providerIsAvailableOverride; }
            set { _providerIsAvailableOverride = value; }
        }

        public ILog GetLogger(string name)
        {
            return new LoupeLogger(name, _logWriteDelegate);
        }

        public static bool IsLoggerAvailable()
        {
            return ProviderIsAvailableOverride && GetLogManagerType() != null;
        }

        private static Type GetLogManagerType()
        {
            return Type.GetType("Gibraltar.Agent.Log, Gibraltar.Agent");
        }

        private static WriteDelegate GetLogWriteDelegate()
        {
            Type logManagerType = GetLogManagerType();
            Type logMessageSeverityType = Type.GetType("Gibraltar.Agent.LogMessageSeverity, Gibraltar.Agent");
            Type logWriteModeType = Type.GetType("Gibraltar.Agent.LogWriteMode, Gibraltar.Agent");

            MethodInfo method = logManagerType.GetMethod("Write", new[]
                                                                  {
                                                                      logMessageSeverityType, typeof(string), typeof(int), typeof(Exception), typeof(bool), 
                                                                      logWriteModeType, typeof(string), typeof(string), typeof(string), typeof(string), typeof(object[])
                                                                  });

            var callDelegate = (WriteDelegate)Delegate.CreateDelegate(typeof(WriteDelegate), method);
            return callDelegate;
        }

        public class LoupeLogger : ILog
        {
            private const string LogSystem = "LibLog";

            private readonly string _category;
            private readonly WriteDelegate _logWriteDelegate;
            private readonly int _skipLevel;

            internal LoupeLogger(string category, WriteDelegate logWriteDelegate)
            {
                _category = category;
                _logWriteDelegate = logWriteDelegate;
                _skipLevel = 1;
            }

            public bool Log(LogLevel logLevel, Func<string> messageFunc)
            {
                if (messageFunc == null)
                {
                    //nothing to log..
                    return true;
                }

                _logWriteDelegate((int)ToLogMessageSeverity(logLevel), LogSystem, _skipLevel, null, false, 0, null,
                    _category, null, messageFunc.Invoke());

                return true;
            }

            public void Log<TException>(LogLevel logLevel, Func<string> messageFunc, TException exception)
                where TException : Exception
            {
                if (messageFunc == null)
                {
                    //nothing to log..
                    return;
                }

                _logWriteDelegate((int)ToLogMessageSeverity(logLevel), LogSystem, _skipLevel, exception, true, 0, null,
                    _category, null, messageFunc.Invoke());
            }

            public TraceEventType ToLogMessageSeverity(LogLevel logLevel)
            {
                switch (logLevel)
                {
                    case LogLevel.Trace:
                        return TraceEventType.Verbose;
                    case LogLevel.Debug:
                        return TraceEventType.Verbose;
                    case LogLevel.Info:
                        return TraceEventType.Information;
                    case LogLevel.Warn:
                        return TraceEventType.Warning;
                    case LogLevel.Error:
                        return TraceEventType.Error;
                    case LogLevel.Fatal:
                        return TraceEventType.Critical;
                    default:
                        throw new ArgumentOutOfRangeException("logLevel");
                }
            }
        }

        /// <summary>
        /// The form of the Loupe Log.Write method we're using
        /// </summary>
        internal delegate void WriteDelegate(
            int severity,
            string logSystem,
            int skipFrames,
            Exception exception,
            bool attributeToException,
            int writeMode,
            string detailsXml,
            string category,
            string caption,
            string description,
            params object[] args
            );
    }
}
