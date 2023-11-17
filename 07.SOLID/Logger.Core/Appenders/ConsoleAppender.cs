using Logger.Core.Appenders.Interfaces;
using Logger.Core.Layout.Interfaces;
using Logger.Core.Models;

namespace Logger.Core.Appenders
{
    public class ConsoleAppender : IAppender
    {
        public ConsoleAppender(ILayout layout, ReportLevel reportLevel = ReportLevel.Info)
        {
            Layout = layout;
            ReportLevel = reportLevel;
        }
        public ILayout Layout { get; private set; }
        public ReportLevel ReportLevel { get; set; }
        public int MessagesAppended { get; private set; }
        public void AppendMessage(Message message)
        {
            Console.WriteLine(string.Format(Layout.Format, message.CreateTime, message.Level, message.Text));

            MessagesAppended++;
        }
    }
}
