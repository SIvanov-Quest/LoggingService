using LoggingService.Domain.Enums;
using System;

namespace LoggingService.Application.Models
{
    public class Message
    {
        public DateTime LogDate { get; set; }
        public string ApplicationName { get; set; }
        public string MessageText { get; set; }

        /// <summary>
        /// Processes incoming message in order to separate log level tag from message body
        /// </summary>
        /// <param name="messageText"></param>
        /// <returns>Message body</returns>
        public Message ProcessMessage(string messageText)
        {
            MessageText = messageText.IndexOf('[') == -1 ?
                                messageText :
                                messageText.Substring(messageText.IndexOf(' ') + 1);
            return this;
        }

        /// <summary>
        /// Processes incoming message in order to separate log level tag from message body
        /// </summary>
        /// <param name="messageText"></param>
        /// <returns>LogLevel</returns>
        public LogLevel ProcessLogLevel(string messageText)
        {
            return messageText.IndexOf('[') == -1 ?
                                LogLevel.Info :
                                LogLevelConvert(messageText.Substring(messageText.IndexOf('[') + 1, messageText.IndexOf(']') - 1));
        }

        /// <summary>
        /// Converts Log level tag into LogLevel enum 
        /// </summary>
        /// <param name="logLevelTag"></param>
        /// <returns>LogLevel enum</returns>
        private LogLevel LogLevelConvert(string logLevelTag)
        {
            switch (logLevelTag)
            {
                case "trace":
                    return LogLevel.Trace;
                case "debug":
                    return LogLevel.Debug;
                case "info":
                    return LogLevel.Info;
                case "warn":
                    return LogLevel.Warn;
                case "error":
                    return LogLevel.Error;
                default:
                    return LogLevel.Info;
            }
        }
    }
}
