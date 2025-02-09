using CouseWork.Context;
using CouseWork.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CouseWork.Data.Repositories
{
    public class SectorRowRepository : BaseRepository<SectorRows>
    {
        private readonly ApplicationDbContext _context;

        public SectorRowRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public ObservableCollection<SectorRows> GetSectorRows()
        {
            var sectorRows = _context.SectorRows.Include(sr => sr.Location).OrderBy(sr => sr.LocationID).ThenBy(sr => sr.SectorRow).ToList();
            return new ObservableCollection<SectorRows>(sectorRows);
        }

        public SectorRows? FindById(int id)
        {
            return _context.SectorRows.FirstOrDefault(sr => sr.SectorRowID == id);
        }

        public SectorRows? UpdateSectorRow(int id, int numberOfSeats, decimal costFactor)
        {
            var sectorRowEntity = FindById(id);
            if (sectorRowEntity != null &&
                _context.Tickets.Count(t => t.SectorRowID == id && t.Status != "On sale") == 0)
            {
                int oldNumberOfSeats = sectorRowEntity.NumberOfSeats;

                sectorRowEntity.NumberOfSeats = numberOfSeats;
                sectorRowEntity.CostFactor = costFactor;

                var tickets = _context.Tickets.Where(t => t.SectorRowID == sectorRowEntity.SectorRowID).ToList();
                foreach (var ticket in tickets)
                {
                    var eventSchedule = _context.EventsSchedule.Where(t => t.EventScheduleID == ticket.EventScheduleID).FirstOrDefault();
                    if (eventSchedule != null)
                    {
                        var eventEntity = _context.Events.Where(e => e.EventID == eventSchedule.EventID).FirstOrDefault();
                        if (eventEntity != null)
                        {
                            ticket.Price = eventEntity.Cost * costFactor;
                        }
                    }
                }

                if (numberOfSeats > oldNumberOfSeats)
                {
                    var maxTicketId = _context.Tickets
                        .Max(t => (int?)t.TicketID) ?? 1000;

                    for (int i = oldNumberOfSeats + 1; i <= numberOfSeats; i++)
                    {
                        var newTicket = new Tickets
                        {
                            TicketID = maxTicketId + 1,
                            EventScheduleID = tickets.First().EventScheduleID,
                            SectorRowID = sectorRowEntity.SectorRowID,
                            PlaceInRow = i,
                            Status = "On sale",
                            Price = tickets.First().EventsSchedule.Event.Cost * costFactor
                        };
                        _context.Tickets.Add(newTicket);
                        maxTicketId++;
                    }
                }

                if (numberOfSeats < oldNumberOfSeats)
                {
                    var ticketsToRemove = tickets
                        .Where(t => t.PlaceInRow > numberOfSeats) 
                        .ToList();

                    foreach (var ticket in ticketsToRemove)
                    {
                        _context.Tickets.Remove(ticket);
                    }
                }

                _context.SaveChanges();
                return sectorRowEntity;
            }
            else
            {
                MessageBox.Show((string)Application.Current.Resources["item68"]);
            }
            return null;
        }


        private void CreateTicketsForSchedule(EventsSchedule schedule, SectorRows sectorRow)
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
            

            _context.Tickets.AddRange(tickets);
            _context.SaveChanges();
        }

        public SectorRows? AddSectorRow(int numberOfSeats, int locationID, decimal costFactor)
        {
            var maxSectorRowId = _context.SectorRows
                .Max(l => (int?)l.SectorRowID) + 1 ?? 1;

            var nextSectorRow = _context.SectorRows
                .Where(l => l.LocationID == locationID)
                .Max(l => (int?)l.SectorRow) + 1 ?? 1;

            if (nextSectorRow > 150)
            {
                MessageBox.Show((string)Application.Current.Resources["item94"]);
                return null;
            }

            var sectorRowEntity = new SectorRows
            {
                SectorRowID = maxSectorRowId,
                SectorRow = nextSectorRow,
                NumberOfSeats = numberOfSeats,
                LocationID = locationID,
                CostFactor = costFactor
            };

            _context.SectorRows.Add(sectorRowEntity);

            var location = _context.Locations.FirstOrDefault(l => l.LocationID == locationID);
            if (location != null)
            {
                location.NumberOfSectors++;
                _context.Locations.Update(location);
            }

            var eventSchedules = _context.EventsSchedule
                .Where(es => es.LocationID == locationID)
                .ToList();

            var maxTicketId = _context.Tickets.Max(t => (int?)t.TicketID) ?? 1000;

            var ticketsToAdd = new List<Tickets>();

            foreach (var eventSchedule in eventSchedules)
            {
                var eventCost = _context.Events
                    .Where(e => e.EventID == eventSchedule.EventID)
                    .Select(e => e.Cost)
                    .FirstOrDefault();

                var price = eventCost * sectorRowEntity.CostFactor;

                for (int i = 1; i <= sectorRowEntity.NumberOfSeats; i++)
                {
                    maxTicketId++;
                    ticketsToAdd.Add(new Tickets
                    {
                        TicketID = maxTicketId,
                        EventScheduleID = eventSchedule.EventScheduleID,
                        Status = "On sale",
                        Price = price,
                        SectorRowID = sectorRowEntity.SectorRowID,
                        PlaceInRow = i
                    });
                }
            }

            _context.Tickets.AddRange(ticketsToAdd);
            _context.SaveChanges();

            return sectorRowEntity;
        }


        public SectorRows? DeleteSectorRow(int id)
        {
            var sectorRowEntity = FindById(id);
            if (sectorRowEntity != null)
            {
                var locationID = sectorRowEntity.LocationID;
                var deletedSectorRow = sectorRowEntity.SectorRow;

                if (_context.SectorRows.Count(e => e.LocationID == locationID) > 1)
                {
                    if (_context.Tickets.Count(t => t.SectorRowID == id && t.Status != "On sale") == 0)
                    {
                        _context.SectorRows.Remove(sectorRowEntity);

                        var location = _context.Locations.FirstOrDefault(l => l.LocationID == locationID);
                        if (location != null)
                        {
                            location.NumberOfSectors--;
                            _context.Locations.Update(location);
                        }

                        var sectorRowsToShift = _context.SectorRows
                            .Where(sr => sr.LocationID == locationID && sr.SectorRow > deletedSectorRow)
                            .ToList();

                        foreach (var row in sectorRowsToShift)
                        {
                            row.SectorRow--;
                            _context.SectorRows.Update(row);
                        }

                        _context.SaveChanges();
                        return sectorRowEntity;
                    }
                    else
                    {
                        MessageBox.Show((string)Application.Current.Resources["item45"]);
                    }
                }
                else
                {
                    MessageBox.Show((string)Application.Current.Resources["item46"]);
                }
            }
            return null;
        }
    }

}
