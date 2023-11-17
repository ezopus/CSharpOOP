using Logger.Core.Exceptions;
using Logger.Core.Utils;

namespace Logger.Core.Models
{
    public class Message
    {
        private string createTime;
        private string text;
        public Message(string createTime, string text, ReportLevel level)
        {
            CreateTime = createTime;
            Text = text;
            Level = level;
        }

        public string CreateTime
        {
            get => createTime;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new EmptyCreateTimeException();
                }

                if (!DateTimeValidator.ValidateDateTime(value))
                {
                    throw new InvalidDateTimeFormatException();
                }

                createTime = value;
            }
        }
        public string Text
        {
            get => text;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new EmptyMessageException();
                }

                text = value;
            }
        }
        public ReportLevel Level { get; set; }
    }
}
