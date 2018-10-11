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

        [HttpDelete, Route("ticket/{id}")]
        public ActionResult Delete(Guid id)
        {
            _ticketHandler.Delete(id);
            return Ok();
        }

        [HttpGet, Route("{queueType}")]
        public ActionResult<QueueResponse> Get(QueueType queueType)
        {
            var ticket = _ticketHandler.GetNextTicketToServe(queueType);
            var queue = _ticketHandler.GetQueue(queueType);
            return new QueueResponse
            {
                NextTicketNumberToServe = ticket.TicketNumber,
                Tickets = queue
            };
        }

        [HttpPost, Route("ticket")]
        public ActionResult<TicketResponse> Create([FromBody] CreateTicketRequest request)
        {
            var ticket = _ticketHandler.Create(request.QueueType);
            var ticketsBefore = _ticketHandler.GetNumberOfTicketsBefore(ticket.Id);
            
            return new TicketResponse
            {
                Id = ticket.Id,
                TicketNumber = ticket.TicketNumber,
                TicketsBefore = ticketsBefore
            };
        }

        [HttpGet, Route("ticket/{id}")]
        public ActionResult<TicketResponse> Get(Guid id)
        {
            var ticket = _ticketHandler.Get(id);
            var ticketsBefore = _ticketHandler.GetNumberOfTicketsBefore(id);

            return new TicketResponse
            {
                Id = ticket.Id,
                TicketNumber = ticket.TicketNumber,
                TicketsBefore = ticketsBefore
            };
        }
    }
}
