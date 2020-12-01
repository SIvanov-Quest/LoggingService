using LoggingService.Domain.Enums;
using System;

namespace LoggingService.Domain.Entities
{
    public class LoggedMessage
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public LogLevel LogLevel { get; set; }
        public int ApplicationId { get; set; }
        public virtual LoggedApplication LoggedApplication { get; set; }
    }
}
