using System;
using System.Runtime.Serialization;

namespace Xpress.QueueService.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public int TicketNumber { get; set; }
        public DateTime Created { get; set; }
        public QueueType QueueType { get; set; }
    }

    public enum QueueType
    {
        [EnumMember]
        Deli = 0,

        [EnumMember]
        PostalService = 1
    }
}
