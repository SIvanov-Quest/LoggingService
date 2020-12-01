using LoggingService.Application.Interfaces;
using LoggingService.Application.Models;
using LoggingService.Domain.Entities;
using LoggingService.Infrastructure.Persistance;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingService.Infrastructure.Services
{
    public class LogMessagesService : ILogMessagesService
    {
        readonly LoggingServiceDbContext LoggingServiceDbContext;
        public LogMessagesService(LoggingServiceDbContext loggingServiceContext)
        {
            LoggingServiceDbContext = loggingServiceContext;
        }

        public async Task<string> LogMessages(List<Message> messages)
        {
            var loggedApplication = new LoggedApplication();
            var loggedMessagess = new List<LoggedMessage>();

            var appName = string.Empty;

            foreach (var message in messages)
            {
                var logLevel = message.ProcessLogLevel(message.MessageText);
                loggedMessagess.Add(new LoggedMessage()
                {
                    ApplicationId = loggedApplication.Name == message.ApplicationName ? loggedApplication.Id : 0,
                    Date = message.LogDate,
                    Message = message.ProcessMessage(message.MessageText).MessageText,
                    LogLevel = logLevel
                });

                appName = message.ApplicationName;
            }
            // Because LoggedApplication and LoggedMessage are in one-to-many relation, 
            // we need to check is there already record in application table with the same name,
            // basing on that, we either insert new record or update existing one
            loggedApplication = LoggingServiceDbContext.application.Where(x => x.Name == appName).FirstOrDefault();

            if (loggedApplication == null)
            {
                loggedApplication = new LoggedApplication();
                loggedApplication.Name = appName;
                loggedApplication.LoggedMessages = loggedMessagess;
                await LoggingServiceDbContext.application.AddAsync(loggedApplication);
            }
            else
            {
                loggedApplication.LoggedMessages = loggedMessagess;
                LoggingServiceDbContext.application.Update(loggedApplication);
            }

            await LoggingServiceDbContext.SaveChangesAsync();

            return "Ok";
        }
    }
}
