using System;
using System.Collections.Generic;
using System.Linq;
using Xpress.QueueService.Models;

namespace Xpress.QueueService.Handlers
{
    public interface ITicketHandler
    {
        Ticket Create(QueueType queueType);
        Ticket Get(Guid id);
    }

    public class TicketHandler : ITicketHandler
    {
        private readonly List<Ticket> _deliTickets;
        private readonly List<Ticket> _postalServiceTickets;

        public TicketHandler()
        {
            _deliTickets = new List<Ticket>();
            _postalServiceTickets = new List<Ticket>();
        }

        public Ticket Create(QueueType queueType)
        {
            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                TicketNumber = GetTicketNumber(queueType),
                Created = DateTime.UtcNow,
            };

            AddTicketToQueue(queueType, ticket);

            return ticket;
        }

        public Ticket Get(Guid id)
        {
            return _deliTickets.SingleOrDefault(t => t.Id == id);
        }

        private void AddTicketToQueue(QueueType queueType, Ticket ticket)
        {
            switch (queueType)
            {
                case QueueType.Deli:
                    _deliTickets.Add(ticket);
                    break;
                case QueueType.PostalService:
                    _postalServiceTickets.Add(ticket);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(queueType), queueType, null);
            }
        }

        private int GetTicketNumber(QueueType queueType)
        {
            switch (queueType)
            {
                case QueueType.Deli:
                    return _deliTickets.Count + 1;
                case QueueType.PostalService:
                    return _postalServiceTickets.Count + 1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(queueType), queueType, null);
            }
        }
    }
}
