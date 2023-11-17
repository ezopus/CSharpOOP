using Logger.Core.Appenders.Interfaces;
using Logger.Core.Loggers.Interfaces;
using Logger.Core.Models;

namespace Logger.Core.Loggers
{
    public class Logger : ILogger
    {
        private readonly ICollection<IAppender> appenders;

        public Logger(params IAppender[] appenders)
        {
            this.appenders = appenders;
        }

        public void Info(string dateTime, string message)
            => AddAppender(dateTime, message, ReportLevel.Info);
        public void Warning(string dateTime, string message)
            => AddAppender(dateTime, message, ReportLevel.Warning);
        public void Error(string dateTime, string message)
            => AddAppender(dateTime, message, ReportLevel.Error);
        public void Critical(string dateTime, string message)
            => AddAppender(dateTime, message, ReportLevel.Critical);
        public void Fatal(string dateTime, string message)
            => AddAppender(dateTime, message, ReportLevel.Fatal);

        private void AddAppender(string dateTime, string message, ReportLevel level)
        {
            Message msg = new Message(dateTime, message, level);

            foreach (var appender in appenders)
            {
                if (msg.Level >= appender.ReportLevel)
                {
                    appender.AppendMessage(msg);
                }
            }
        }
    }
}
