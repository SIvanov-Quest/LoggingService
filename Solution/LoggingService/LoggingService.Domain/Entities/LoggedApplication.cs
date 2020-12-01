using System.Collections.Generic;

namespace LoggingService.Domain.Entities
{
    public class LoggedApplication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<LoggedMessage> LoggedMessages { get; set; }
    }
}
