using Logger.Core.Models;

namespace Logger.Core.Appenders.Interfaces
{
    public interface IAppender
    {
        //TODO: ILayout

        ReportLevel ReportLevel { get; set; }
        int MessagesAppended { get; }
        void AppendMessage(Message message);

    }
}
