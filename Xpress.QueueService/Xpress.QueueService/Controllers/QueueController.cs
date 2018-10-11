using System;
using Microsoft.AspNetCore.Mvc;
using Xpress.QueueService.Handlers;
using Xpress.QueueService.Models;


namespace Xpress.QueueService.Controllers
{
    [Route("api/queue")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private readonly ITicketHandler _ticketHandler;

        public QueueController(ITicketHandler ticketHandler)
        {
            _ticketHandler = ticketHandler;
        }

        [HttpPost, Route("ticket")]
        public ActionResult<TicketResponse> Create([FromBody] CreateTicketRequest request)
        {
            var ticket = _ticketHandler.Create(request.QueueType);
            
            return new TicketResponse
            {
                Id = ticket.Id,
                TicketNumber = ticket.TicketNumber
            };
        }

        [HttpGet, Route("ticket/{id}")]
        public ActionResult<TicketResponse> Get(Guid id)
        {
            var ticket = _ticketHandler.Get(id);

            return new TicketResponse
            {
                Id = ticket.Id,
                TicketNumber = ticket.TicketNumber
            };
        }
    }
}
