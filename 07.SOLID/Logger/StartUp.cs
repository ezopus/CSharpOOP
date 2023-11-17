
using Logger.Core.Appenders;
using Logger.Core.IO;
using Logger.Core.Layout;
using Logger.CustomLayouts;


var simpleLayout = new SimpleLayout();
var xmLayout = new XmlLayout();
var consoleAppender = new ConsoleAppender(xmLayout);

var file = new LogFile();
var fileAppender = new FileAppender(xmLayout, file);

var logger = new Logger.Core.Loggers.Logger(consoleAppender, fileAppender);
logger.Error("3/26/2015 2:08:11 PM", "Error parsing JSON.");
logger.Info("3/26/2015 2:08:11 PM", "User Pesho successfully registered.");
