﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xpress.QueueService.Models;

namespace Xpress.QueueService.Handlers
{
    public interface ITicketHandler
    {
        Ticket Create(QueueType queueType);
        Ticket Get(Guid id);
        void Delete(Guid id);
        Ticket GetNextTicketToServe(QueueType queueType);
        List<Ticket> GetQueue(QueueType queueType);
        int GetNumberOfTicketsBefore(Guid id);
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
                QueueType = queueType
            };

            AddTicketToQueue(queueType, ticket);

            return ticket;
        }

        public int GetNumberOfTicketsBefore(Guid id)
        {
            var ticket = Get(id);

            if (_deliTickets.Exists(t => t.Id == id))
            {
                var ticketsBefore = _deliTickets.Where(t => t.TicketNumber < ticket.TicketNumber).ToList();
                return ticketsBefore.Count;
            }

            if (_postalServiceTickets.Exists(t => t.Id == id))
            {
                var ticketsBefore = _postalServiceTickets.Where(t => t.TicketNumber < ticket.TicketNumber).ToList();
                return ticketsBefore.Count;
            }

            throw new Exception("Ticket does not exist.");
        }

        public void Delete(Guid id)
        {
            var ticket = Get(id);

            if (_deliTickets.Exists(t => t.Id == id))
            {
                _deliTickets.Remove(ticket);
            }

            if (_postalServiceTickets.Exists(t => t.Id == id))
            {
                _postalServiceTickets.Remove(ticket);
            }
        }

        public Ticket GetNextTicketToServe(QueueType queueType)
        {
            if (queueType == QueueType.Deli)
            {
                return _deliTickets.FirstOrDefault();
            }

            if (queueType == QueueType.PostalService)
            {
                return _postalServiceTickets.FirstOrDefault();
            }

            throw new ArgumentOutOfRangeException("Queue does not exist");
        }

        public List<Ticket> GetQueue(QueueType queueType)
        {
            if (queueType == QueueType.Deli)
            {
                return _deliTickets;
            }

            if (queueType == QueueType.PostalService)
            {
                return _postalServiceTickets;
            }

            throw new ArgumentOutOfRangeException("Queue does not exist");
        }

        public Ticket Get(Guid id)
        {
            if (_deliTickets.Exists(t => t.Id == id))
            {
                return _deliTickets.SingleOrDefault(t => t.Id == id);
            }

            if (_postalServiceTickets.Exists(t => t.Id == id))
            {
                return _postalServiceTickets.SingleOrDefault(t => t.Id == id);
            }

            return null;
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
                    if (_deliTickets.Count == 0)
                    {
                        return 1;
                    }
                    return _deliTickets.LastOrDefault().TicketNumber + 1;
                case QueueType.PostalService:
                    if (_postalServiceTickets.Count == 0)
                    {
                        return 1;
                    }
                    return _postalServiceTickets.LastOrDefault().TicketNumber + 1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(queueType), queueType, null);
            }
        }
    }
}
