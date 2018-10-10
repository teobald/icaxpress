using System;

namespace Xpress.QueueService.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public int TicketNumber { get; set; }
        public DateTime Created { get; set; }
    }
}
