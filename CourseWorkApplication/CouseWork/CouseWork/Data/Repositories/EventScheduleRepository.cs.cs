using CouseWork.Context;
using CouseWork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CouseWork.Data.Repositories
{
    public class EventScheduleRepository : BaseRepository<EventsSchedule>
    {
        private readonly ApplicationDbContext _context;

        public EventScheduleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public ObservableCollection<EventsSchedule> GetEventSchedules()
        {
            return new ObservableCollection<EventsSchedule>(_context.EventsSchedule
                .Include(e => e.Event)
                .Include(e => e.Location)
                .ToList());
        }

        public ObservableCollection<EventsSchedule> GetSchedulesByOrganizerId(int organizerId)
        {
                return new ObservableCollection<EventsSchedule>(_context.EventsSchedule
                    .Where(es => es.Event.OrganizerID == organizerId)
                    .Include(es => es.Event) 
                    .Include(es => es.Location) 
                    .ToList());
        }

        public ObservableCollection<EventsSchedule> GetSchedulesByOrganizerIdOnlyNew(int organizerId)
        {
            return new ObservableCollection<EventsSchedule>(_context.EventsSchedule
                .Where(es => es.Event.OrganizerID == organizerId && es.EventDate > DateTime.Now)
                .Include(es => es.Event)
                .Include(es => es.Location)
                .OrderBy(es => es.EventDate)
                .ToList());
        }


        public int FindMaxId()
        {
            return _context.EventsSchedule.Max(e => (int?)e.EventScheduleID) + 1 ?? 1;
        }

        public EventsSchedule? FindById(int id)
        {
            return _context.EventsSchedule
                .Include(e => e.Event)
                .Include(e => e.Location)
                .ThenInclude(l => l.SectorRows)
                .FirstOrDefault(e => e.EventScheduleID == id);
        }
        public ObservableCollection<EventsSchedule>? GetConnectedEventSchedule(int eventScheduleId)
        {
            var targetSchedule = _context.EventsSchedule
                .FirstOrDefault(es => es.EventScheduleID == eventScheduleId);

            if (targetSchedule == null) 
            {
                throw new InvalidOperationException($"{(string)Application.Current.Resources["item35"]}: {eventScheduleId}");
            }

            var targetEventId = targetSchedule.EventID;

            return new ObservableCollection<EventsSchedule>(_context.EventsSchedule
                .Include(es => es.Event)
                .Include(es => es.Location)
                .Where(es => es.EventID == targetEventId && es.EventScheduleID != eventScheduleId && es.EventDate > DateTime.Now)
                .OrderBy(es => es.EventDate)
                .ToList());
        }
        public EventsSchedule AddEventSchedule(DateTime date, int eventId, int locationId)
        {
            var maxEventScheduleId = _context.EventsSchedule.Max(e => (int?)e.EventScheduleID) + 1 ?? 1;
            var schedule = new EventsSchedule
            {
                EventScheduleID = maxEventScheduleId,
                EventDate = date,
                EventID = eventId,
                LocationID = locationId
            };
            _context.EventsSchedule.Add(schedule);
            _context.SaveChanges();
            CreateTicketsForSchedule(schedule);
            return schedule;
        }
        public bool CheckEventScheduleTime(DateTime date, int eventId, int locationId)
        {
            var eventDuration = _context.Events
                .Where(e => e.EventID == eventId)
                .Select(e => e.EventDuration)
                .FirstOrDefault();

            if (eventDuration == default)
            {
                throw new InvalidOperationException((string)Application.Current.Resources["item36"]);
            }

            var eventStart = date;
            var eventEnd = date + eventDuration;

            var overlapExists = _context.EventsSchedule
                .Where(es => es.LocationID == locationId)
                .Include(es => es.Event)
                .AsEnumerable() 
                .Any(es =>
                    (eventStart <= es.EventDate && eventEnd >= es.EventDate) || // Начало пересекает
                    (eventStart <= es.EventDate.Add(es.Event.EventDuration) && eventEnd >= es.EventDate.Add(es.Event.EventDuration)) || // Конец пересекает
                    (eventStart <= es.EventDate && eventEnd >= es.EventDate.Add(es.Event.EventDuration)) // Полное перекрытие
                );

            return overlapExists;
        }
        public bool CheckEventScheduleTimeExceptItself(DateTime date, int eventId, int locationId, int eventScheduleId)
        {
            var eventDuration = _context.Events
                .Where(e => e.EventID == eventId)
                .Select(e => e.EventDuration)
                .FirstOrDefault();

            if (eventDuration == default)
            {
                throw new InvalidOperationException((string)Application.Current.Resources["item36"]);
            }

            var eventStart = date;
            var eventEnd = date + eventDuration;

            var overlapExists = _context.EventsSchedule
                .Where(es => es.LocationID == locationId && es.EventScheduleID != eventScheduleId) 
                .AsEnumerable()
                .Any(es =>
                    (eventStart <= es.EventDate && eventEnd >= es.EventDate) || // Начало пересекает
                    (eventStart <= es.EventDate.Add(es.Event.EventDuration) && eventEnd >= es.EventDate.Add(es.Event.EventDuration)) || // Конец пересекает
                    (eventStart <= es.EventDate && eventEnd >= es.EventDate.Add(es.Event.EventDuration)) // Полное перекрытие
                );

            return overlapExists;
        }
        public void UpdateEventSchedule(int id, DateTime date, int eventId, int locationId)
        {
            var schedule = _context.EventsSchedule.FirstOrDefault(es => es.EventScheduleID == id);
            if (schedule.EventID != eventId)
            {
                MessageBox.Show((string)Application.Current.Resources["item177"]);
                return;
            }

            if (schedule.LocationID != locationId)
            {
                MessageBox.Show((string)Application.Current.Resources["item69"]);
                return;
            }
            if (schedule != null)
            {
                schedule.EventDate = date;
                schedule.EventID = eventId;
                _context.SaveChanges();
            }
        }

        public void DeleteEventSchedule(int id)
        {
            if (_context.Tickets.Count(t => t.EventScheduleID == id && t.Status != "On sale") == 0) {
                var schedule = _context.EventsSchedule.FirstOrDefault(es => es.EventScheduleID == id);
                if (schedule != null)
                {
                    _context.EventsSchedule.Remove(schedule);
                    _context.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show((string)Application.Current.Resources["item43"]);
            }
        }
        private void CreateTicketsForSchedule(EventsSchedule schedule)
        {

            var location = _context.Locations
                .Include(l => l.SectorRows) 
                .FirstOrDefault(l => l.LocationID == schedule.LocationID);

            if (location == null)
            {
                throw new Exception((string)Application.Current.Resources["item36"]);
            }

            var eventCost = _context.Events
                .Where(e => e.EventID == schedule.EventID)
                .Select(e => e.Cost)
                .FirstOrDefault();


            List<Tickets> tickets = new List<Tickets>();

            var maxTicketId = _context.Tickets
                .Max(t => (int?)t.TicketID) ?? 1000;


            foreach (var sectorRow in location.SectorRows)
            {
                var price = eventCost * sectorRow.CostFactor;
                var numberOfSeats = sectorRow.NumberOfSeats;

                for (int i = 1; i <= numberOfSeats; i++)
                {
                    var ticket = new Tickets
                    {
                        TicketID = maxTicketId + 1, 
                        EventScheduleID = schedule.EventScheduleID,
                        Status = "On sale", 
                        Price = price,
                        SectorRowID = sectorRow.SectorRowID,
                        PlaceInRow = i 
                    };

                    tickets.Add(ticket);
                    maxTicketId++;
                }
            }

            _context.Tickets.AddRange(tickets);
            _context.SaveChanges();
        }
        public IQueryable<EventsSchedule> GetAll()
        {
            return _context.EventsSchedule;
        }

    }
}
