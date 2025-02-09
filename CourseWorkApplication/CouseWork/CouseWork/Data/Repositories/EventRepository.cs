using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CouseWork.Commands;
using CouseWork.Data;
using System.Configuration;
using CouseWork.Views;
using CouseWork.Utilities;
using CouseWork.Models;
using System.Collections.ObjectModel;
using CouseWork.Context;
using Microsoft.EntityFrameworkCore;

namespace CouseWork.Data.Repositories
{
    public class EventRepository : BaseRepository<Events>
    {
        private ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public ObservableCollection<Events> GetEvents()
        {
            var events = _context.Events
                .Include(e => e.Category)
                .Include(e => e.Organizer)
                .ToList();

            return new ObservableCollection<Events>(events);
        }

        public ObservableCollection<Events> GetOrganizerEvents(int organizerID)
        {
            var events = _context.Events
                .Include(e => e.Category)
                .Include(e => e.Organizer)
                .Where(e => e.Organizer.OrganizerID == organizerID)
                .ToList();

            return new ObservableCollection<Events>(events);
        }


        public Events? FindById(int id)
        {
            return _context.Events
                .Include(e => e.Category)
                .Include(e => e.Organizer)
                .FirstOrDefault(e => e.EventID == id);
        }

        public Events? UpdateEvent(int id, string name, TimeSpan duration, int categoryId, int organizerId, string description, decimal cost, DateTime startDate, byte[]? image)
        {
            var eventEntity = FindById(id);

            if (eventEntity.Cost != cost)
            {
                MessageBox.Show((string)Application.Current.Resources["item175"]);
                return null;
            }

            if (eventEntity.StartDate != startDate)
            {
                MessageBox.Show((string)Application.Current.Resources["item178"]);
                return null;
            }

            if (eventEntity != null)
            {
                eventEntity.EventName = name;
                eventEntity.EventDuration = duration;
                eventEntity.CategoryID = categoryId;
                eventEntity.OrganizerID = organizerId;
                eventEntity.Description = description;
                eventEntity.Cost = cost;
                eventEntity.StartDate = startDate;

                if (image != null)
                {
                    eventEntity.Image = image;
                }

                _context.SaveChanges();
                return eventEntity;
            }

            return null;
        }

        public Events? AddEvent(string name, TimeSpan duration, int categoryId, int organizerId, string description, decimal cost, DateTime startDate, byte[]? image)
        {
            var maxEventId = _context.Events.Max(e => (int?)e.EventID) + 1 ?? 1;

            var newEvent = new Events
            {
                EventID = maxEventId,
                EventName = name,
                EventDuration = duration,
                CategoryID = categoryId,
                OrganizerID = organizerId,
                Description = description,
                Cost = cost,
                StartDate = startDate,
                Image = image
            };

            _context.Events.Add(newEvent);
            _context.SaveChanges();
            return newEvent;
        }

        public Events? DeleteEvent(int id)
        {
            if (_context.EventsSchedule.Count(e => e.EventID == id) == 0)
            {
                var eventEntity = FindById(id);

                if (eventEntity != null)
                {
                    _context.Events.Remove(eventEntity);
                    _context.SaveChanges();
                    return eventEntity;
                }
            }
            else
            {
                MessageBox.Show((string)Application.Current.Resources["item42"]);
            }
            return null;
        }
        public bool CheckEventUnique(string name)
        {
            return _context.Events.Any(l => l.EventName == name);
        }
        public bool CheckEventUniqueExceptCurrent(string name, int currentID)
        {
            return _context.Events.Any(l => l.EventName == name && l.EventID != currentID);
        }

        public IQueryable<Events> GetAll()
        {
            return _context.Events;
        }

    }

}
