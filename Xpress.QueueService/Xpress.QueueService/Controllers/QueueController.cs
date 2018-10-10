using System;
using Microsoft.AspNetCore.Mvc;
using Xpress.QueueService.Models;


namespace Xpress.QueueService.Controllers
{
    [Route("api/queue")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        [HttpGet, Route("ticket")]
        public ActionResult<Ticket> Get()
        {
            return new Ticket
            {
                Id = Guid.NewGuid(),
                TicketNumber = 1,
                Created = DateTime.UtcNow
            };
        }
    }
}
