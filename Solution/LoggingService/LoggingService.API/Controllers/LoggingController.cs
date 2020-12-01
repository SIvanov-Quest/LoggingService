using System.Collections.Generic;
using System.Threading.Tasks;
using LoggingService.Application.Interfaces;
using LoggingService.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoggingService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LoggingController : ControllerBase
    {
        readonly ILogMessagesService LoggingService;
        public LoggingController(ILogMessagesService loggingService)
        {
            LoggingService = loggingService;
        }

        [HttpPost(nameof(LogMessages))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> LogMessages([FromBody] List<Message> message)
        {
            return Ok(await LoggingService.LogMessages(message));
        }
    }
}
