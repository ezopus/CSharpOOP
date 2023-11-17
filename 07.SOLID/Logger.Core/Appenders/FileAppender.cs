using Logger.Core.Appenders.Interfaces;
using Logger.Core.IO.Interfaces;
using Logger.Core.Layout.Interfaces;
using Logger.Core.Models;

namespace Logger.Core.Appenders
{
    public class FileAppender : IAppender
    {
        public FileAppender(ILayout layout, ILogFile logFile, ReportLevel reportLevel = ReportLevel.Info)
        {
            LogFile = logFile;
            Layout = layout;
            ReportLevel = reportLevel;
        }
        public ILogFile LogFile { get; private set; }
        public ILayout Layout { get; private set; }
        public ReportLevel ReportLevel { get; set; }
        public int MessagesAppended { get; private set; }
        public void AppendMessage(Message message)
        {
            string content = string.Format(Layout.Format, message.CreateTime, message.Level, message.Text)
                + Environment.NewLine;

            File.AppendAllText(LogFile.FullPath, content);

            MessagesAppended++;
        }
    }
}
