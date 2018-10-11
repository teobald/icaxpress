using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xpress.QueueService.Handlers
{
    public class TicketResponse
    {
        public Guid Id { get; set; }
        public int TicketNumber { get; set; }
    }
}
