using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xpress.QueueService.Models;

namespace Xpress.QueueService.Handlers
{
    public class QueueResponse
    {
        public int NextTicketNumberToServe { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }
    }
}
