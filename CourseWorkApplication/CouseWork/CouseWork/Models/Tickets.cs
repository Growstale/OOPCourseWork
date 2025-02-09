using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouseWork.Models
{
    public class Tickets
    {
        [Key]
        public int TicketID { get; set; }
        public int EventScheduleID { get; set; }
        public required string Status { get; set; }
        public int SectorRowID { get; set; }
        public int PlaceInRow { get; set; }
        public decimal Price { get; set; }

        public EventsSchedule EventsSchedule { get; set; }
        public SectorRows SectorRows { get; set; }

        public ICollection<Sales> Sales { get; set; }
        public ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
