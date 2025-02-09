using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouseWork.Models
{
    public class EventsSchedule
    {
        [Key]
        public int EventScheduleID { get; set; }
        public DateTime EventDate { get; set; }
        public int EventID { get; set; }
        public int LocationID { get; set; }
        public Locations Location { get; set; }
        public Events Event { get; set; }
        public ICollection<Tickets> Tickets { get; set; }
    }
}
