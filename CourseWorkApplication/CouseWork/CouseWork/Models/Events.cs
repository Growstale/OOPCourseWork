using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouseWork.Models
{
    public class Events
    {
        [Key]
        public int EventID { get; set; }
        public string EventName { get; set; }
        public TimeSpan EventDuration { get; set; }
        public int CategoryID { get; set; }
        public int OrganizerID { get; set; }
        public string? Description { get; set; }
        public decimal Cost { get; set; }
        public byte[]? Image { get; set; }
        public DateTime StartDate { get; set; }

        public Categories Category { get; set; }
        public Organizers Organizer { get; set; }
        public ICollection<EventsSchedule> EventSchedules { get; set; }
        public ICollection<Comments> Comments { get; set; }

    }
}
