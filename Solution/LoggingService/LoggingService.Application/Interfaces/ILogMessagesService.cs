using LoggingService.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoggingService.Application.Interfaces
{
    public interface ILogMessagesService
    {
        Task<string> LogMessages(List<Message> messages);
    }
}
