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

namespace CouseWork.Data.Repositories
{
    public class LocationRepository : BaseRepository<Locations>
    {
        ApplicationDbContext _context;
        public LocationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public ObservableCollection<Locations> GetLocations()
        {
            var locations = _context.Locations.ToList();

            return new ObservableCollection<Locations>(locations);
        }
        public Locations? FindById(int id)
        {
            return _context.Locations.FirstOrDefault(l => l.LocationID == id);
        }
        public Locations? UpdateLocation(int id, string name, string rows)
        {
            var location = FindById(id);

            if (location == null)
            {
                return null;
            }

            var buytickets =  _context.Tickets.Join(
                _context.SectorRows,
                ticket => ticket.SectorRowID,  
                sectorRow => sectorRow.SectorRowID,
                (ticket, sectorRow) => new { ticket, sectorRow }
            )
            .Where(joined => joined.sectorRow.LocationID == id && joined.ticket.Status != "On sale")
            .Count();

            if (CheckLocationUniqueExceptCurrent(name, id))
            {
                MessageBox.Show((string)Application.Current.Resources["item191"]);
                return null;
            }

            if (location.NumberOfSectors != int.Parse(rows) && buytickets > 0)
            {
                MessageBox.Show((string)Application.Current.Resources["item179"]);
                return null;
            }

            if (location != null)
                {
                    int oldNumberOfSectors = location.NumberOfSectors;
                    location.LocationName = name;
                    location.NumberOfSectors = int.Parse(rows);
                    int newNumberOfSectors = location.NumberOfSectors;
                    if (newNumberOfSectors > oldNumberOfSectors)
                    {
                        var maxSectorRowId = _context.SectorRows
                                  .Max(sr => (int?)sr.SectorRowID) ?? 0;
                        for (int i = oldNumberOfSectors + 1; i <= newNumberOfSectors; i++)
                        {
                            var sectorRow = new SectorRows
                            {
                                SectorRowID = maxSectorRowId + i,
                                SectorRow = i,
                                NumberOfSeats = 30,
                                LocationID = location.LocationID,
                                CostFactor = 1
                            };

                            _context.SectorRows.Add(sectorRow);
                        }

                    }
                    else if (newNumberOfSectors < oldNumberOfSectors)
                    {
                        var sectorsToRemove = _context.SectorRows
                                                      .Where(sr => sr.LocationID == location.LocationID && sr.SectorRow > newNumberOfSectors)
                                                      .ToList();

                        _context.SectorRows.RemoveRange(sectorsToRemove);
                    }

                    _context.SaveChanges();
                    return location;
                }
            
            return null;
        }
        public Locations? AddLocation(string name, string rows)
        {
            if (!CheckLocationUnique(name))
            {
                var maxLocationId = _context.Locations
                             .Max(l => (int?)l.LocationID) + 1 ?? 1;
                var location = new Locations { LocationID = maxLocationId, LocationName = name, NumberOfSectors = int.Parse(rows) };
                _context.Locations.Add(location);
                var numberOfSectors = location.NumberOfSectors;
                var maxSectorRowId = _context.SectorRows
                                              .Max(sr => (int?)sr.SectorRowID) ?? 0;
                for (int i = 1; i <= numberOfSectors; i++)
                {
                    var sectorRow = new SectorRows
                    {
                        SectorRowID = maxSectorRowId + i,
                        SectorRow = i,
                        NumberOfSeats = 30,
                        LocationID = location.LocationID,
                        CostFactor = 1.0m
                    };

                    _context.SectorRows.Add(sectorRow);
                }

                _context.SaveChanges();
                return location;
            }
            return null;
        }

        public Locations? DeleteLocation(int id)
        {
            if (_context.EventsSchedule.Count(e => e.LocationID == id) == 0) {
                var location = _context.Locations.FirstOrDefault(l => l.LocationID == id);

                if (location != null)
                {
                    var relatedRows = _context.SectorRows.Where(sr => sr.LocationID == id);
                    _context.SectorRows.RemoveRange(relatedRows);

                    _context.Locations.Remove(location);
                    _context.SaveChanges();
                    return location;
                }
            }
            else
            {
                System.Windows.MessageBox.Show((string)Application.Current.Resources["item44"]);
            }
            return null;
        }
        public bool CheckLocationUnique(string name)
        {
            return _context.Locations.Any(l => l.LocationName == name);
        }
        public bool CheckLocationUniqueExceptCurrent(string name, int id)
        {
            return _context.Locations.Any(l => l.LocationName == name && l.LocationID != id);
        }

        public IQueryable<Locations> GetAll()
        {
            return _context.Locations;
        }
    }
}
