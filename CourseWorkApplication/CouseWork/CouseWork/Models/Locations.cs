using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouseWork.Models
{
    public class Locations
    {
        [Key]
        public int LocationID { get; set; }
        public required string LocationName { get; set; }
        public required int NumberOfSectors { get; set; }
        public ICollection<EventsSchedule> EventSchedules { get; set; }
        public ICollection<SectorRows> SectorRows { get; set; }
        public override string ToString()
        {
            return LocationName;
        }
    }
}
